using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Config;
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Formula.Helper;

namespace DocSystem.Areas.MaterialManage.Controllers
{
    public class LostDamageController : DocSystemFormContorllor<T_Lose_LostDetail>
    {

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {

            List<Dictionary<string, object>> lostReplenishList = null;
            List<Dictionary<string, object>> replenishList = new List<Dictionary<string, object>>();
            S_NodeInfo nodeInfo = null;
            S_FileInfo fileInfo = null;
            //遗失登记
            if (dic.ContainsKey("LostDetail"))
            {
                lostReplenishList = JsonHelper.ToList(dic["LostDetail"]);
                //遗失损毁数量校验
                LostDamageVerifica(dic, lostReplenishList);
            }
            else
                lostReplenishList = JsonHelper.ToList(dic["Detail"]);//补录
            if (lostReplenishList.Count <= 0)
                throw new Formula.Exceptions.BusinessException("请添加内容之后，再保存！");
            var entities = Formula.FormulaHelper.GetEntities<DocConstEntities>();
            InventoryType stateType = new InventoryType();
            foreach (var lostReplenishDetail in lostReplenishList)
            {
                string relateID = lostReplenishDetail.ContainsKey("RelateDocID") ? lostReplenishDetail["RelateDocID"].ToString() : "";//文件或节点ID
                string spaceID = lostReplenishDetail.ContainsKey("SpaceID") ? lostReplenishDetail["SpaceID"].ToString() : "";
                S_DOC_Space space = Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(spaceID));
                string relateDocType = lostReplenishDetail.ContainsKey("RelateDocType") ? lostReplenishDetail["RelateDocType"].ToString() : "";//文件或结点
                string state = "";
                if (dic.ContainsKey("LostDetail"))
                    state = lostReplenishDetail.ContainsKey("LoseDamageState") ? lostReplenishDetail["LoseDamageState"].ToString() : "";//遗失损毁状态
                //遗失或损毁补录份数
                #region 库存数量的增减
                int LostReplenishCount = 0;
                int datilCount = 0;//补录份数可能为负数
                if (state.Equals(InventoryType.Destroy.ToString()) || state.Equals(InventoryType.Lose.ToString()))
                {
                    stateType = state.Equals(InventoryType.Destroy.ToString()) ? InventoryType.Destroy : InventoryType.Lose;
                    LostReplenishCount = Convert.ToInt32(lostReplenishDetail["LoseCount"]);
                    LostReplenishCount = -LostReplenishCount;
                }
                else
                {
                    stateType = InventoryType.Replenish;
                    LostReplenishCount = Convert.ToInt32(lostReplenishDetail["ReplenishCount"]);
                }
                datilCount =stateType == InventoryType.Replenish? LostReplenishCount:-LostReplenishCount;
                if (relateDocType == "Node")
                {
                    nodeInfo = new S_NodeInfo(relateID, space);
                    InventoryFO.CreateInventoryLedger(nodeInfo, stateType, LostReplenishCount, LostReplenishCount, "", "", "", EnumBaseHelper.GetEnumDescription(stateType.GetType(), stateType.ToString()) + "份数：" + datilCount + "份");//入库
                }
                else
                {
                    fileInfo = new S_FileInfo(relateID, space);
                    InventoryFO.CreateInventoryLedger(fileInfo, stateType, LostReplenishCount, LostReplenishCount, "", "", "", EnumBaseHelper.GetEnumDescription(stateType.GetType(), stateType.ToString()) + "份数：" + datilCount + "份");
                }
                #endregion
                if (dic.ContainsKey("LostDetail"))
                    AddLostDamage(dic, lostReplenishDetail, entities);//遗失
                else
                    AddReplenish(dic, lostReplenishDetail, entities, ref replenishList);//补录
            }
            if (dic.ContainsKey("Detail"))
                dic["Detail"] = JsonHelper.ToJson(replenishList).ToString();
            entities.SaveChanges();

        }
        //遗失损毁校验
        public void LostDamageVerifica(Dictionary<string, string> dic, List<Dictionary<string, object>> lostReplenishList)
        {
            foreach (var lostReplenishDetail in lostReplenishList)
            {
                string relateID = lostReplenishDetail.ContainsKey("RelateDocID") ? lostReplenishDetail["RelateDocID"].ToString() : "";//文件或节点ID
                string spaceID = lostReplenishDetail.ContainsKey("SpaceID") ? lostReplenishDetail["SpaceID"].ToString() : "";
                S_DOC_Space space = Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(spaceID));
                string relateDocType = lostReplenishDetail.ContainsKey("RelateDocType") ? lostReplenishDetail["RelateDocType"].ToString() : "";//文件或结点
                string state = "";
                if (dic.ContainsKey("LostDetail"))
                    state = lostReplenishDetail.ContainsKey("LoseDamageState") ? lostReplenishDetail["LoseDamageState"].ToString() : "";//遗失损毁状态
                int lostReplenishCount = lostReplenishCount = Convert.ToInt32(lostReplenishDetail["LoseCount"]);
                if (relateDocType.ToLower() == "node")
                {
                    S_NodeInfo node = new S_NodeInfo(relateID, space);
                    object storageNum = null;
                    node.DataEntity.TryGetValue("StorageNum", out storageNum);
                    if (lostReplenishCount > Convert.ToInt32(storageNum))
                        throw new Formula.Exceptions.BusinessException("遗失份数大于库存，请重新填写");
                    continue;
                }
                else
                {
                    S_FileInfo file = new S_FileInfo(relateID, space);
                    object storageNum = null;
                    file.DataEntity.TryGetValue("StorageNum", out storageNum);
                    if (lostReplenishCount > Convert.ToInt32(storageNum))
                        throw new Formula.Exceptions.BusinessException("遗失份数大于库存，请重新填写");
                    continue;
                }
            }
        }
        //S_A_LoseReplenish遗失登记数据存储
        public void AddLostDamage(Dictionary<string, string> lostForm, Dictionary<string, object> lostDamage, DocConstEntities entities)
        {
            S_A_LoseReplenish loseReplenish = new S_A_LoseReplenish();
            loseReplenish.ID = Formula.FormulaHelper.CreateGuid();
            loseReplenish.LoseCount = Convert.ToInt32(lostDamage["LoseCount"]);
            loseReplenish.LoseDate = !lostDamage.ContainsKey("LoseDate") || lostDamage["LoseDate"] == null || lostDamage["LoseDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(lostDamage["LoseDate"]);
            loseReplenish.LoseDept = lostForm["LoseDept"].ToString();
            loseReplenish.LoseDeptName = lostForm["LoseDeptName"].ToString();
            loseReplenish.LosePeople = lostForm["LosePeople"].ToString();
            loseReplenish.LosePeopleName = lostForm["LosePeopleName"].ToString();
            loseReplenish.DocumentCode = lostDamage.ContainsKey("DocumentCode") && lostDamage["DocumentCode"] != null && !string.IsNullOrEmpty(lostDamage["DocumentCode"].ToString()) ? lostDamage["DocumentCode"].ToString() : "";
            loseReplenish.LoseExplain = lostDamage.ContainsKey("LoseExplain") && lostDamage["LoseExplain"] != null ? lostDamage["LoseExplain"].ToString() : "";
            //loseReplenish.Name = lostDamage["Name"].ToString();
            loseReplenish.Quantity = Convert.ToInt32(lostDamage["Quantity"]) - loseReplenish.LoseCount;
            loseReplenish.RelateDocID = lostDamage["RelateDocID"].ToString();
            loseReplenish.State = lostDamage["LoseDamageState"].ToString();
            loseReplenish.ConfigID = lostDamage["ConfigID"].ToString();
            loseReplenish.ReplenishState = ReplenishState.Unreplenished.ToString();
            loseReplenish.SpaceID = lostDamage["SpaceID"].ToString();
            loseReplenish.RelateDocType = lostDamage["RelateDocType"].ToString();
            loseReplenish.Name = GetFinalFullPathName(loseReplenish.RelateDocID, loseReplenish.ConfigID, loseReplenish.RelateDocType, lostDamage["Name"].ToString());
            if (string.IsNullOrEmpty(loseReplenish.Name))
                loseReplenish.Name = lostDamage["Name"].ToString();
            entities.S_A_LoseReplenish.Add(loseReplenish);
        }
        //S_A_LoseReplenish补录数据存储
        public void AddReplenish(Dictionary<string, string> ReplenishForm, Dictionary<string, object> Replenish, DocConstEntities entities, ref List<Dictionary<string, object>> ReplenishList)
        {
            string loseReplenishID = Replenish.ContainsKey("S_A_LoseReplenishID") ? Replenish["S_A_LoseReplenishID"].ToString() : "";
            string replenishState = null;
            //遗失损毁补录
            if (!string.IsNullOrEmpty(loseReplenishID))
            {
                S_A_LoseReplenish loseReplenish = entities.S_A_LoseReplenish.FirstOrDefault(b => b.ID.Equals(loseReplenishID));
                int lostCount = int.Parse(loseReplenish.LoseCount.ToString());
                if (loseReplenish.ReplenishCount == null || string.IsNullOrEmpty(loseReplenish.ReplenishCount.ToString()))
                    loseReplenish.ReplenishCount = Convert.ToInt32(Replenish["ReplenishCount"].ToString());
                else
                    loseReplenish.ReplenishCount += Convert.ToInt32(Replenish["ReplenishCount"].ToString());
                replenishState = lostCount > loseReplenish.ReplenishCount ? ReplenishState.PartReplenish.ToString() : ReplenishState.Replenish.ToString();
                loseReplenish.ReplenishState = replenishState;
                loseReplenish.ReplenishPeopleName = ReplenishForm["ReplenishPeopleName"];
                loseReplenish.ReplenishPeople = ReplenishForm["ReplenishPeople"];
                loseReplenish.ReplenishExplain = !Replenish.ContainsKey("ReplenishExplain") || Replenish["ReplenishExplain"] == null ? "" : Replenish["ReplenishExplain"].ToString();
                loseReplenish.ReplenishDeptName = ReplenishForm["ReplenishDeptName"];
                loseReplenish.ReplenishDept = ReplenishForm["ReplenishDept"];
                loseReplenish.RelateDocID = Replenish["RelateDocID"].ToString();
                loseReplenish.DocumentCode = Replenish["DocumentCode"].ToString();
                loseReplenish.RelateDocType = Replenish["RelateDocType"].ToString();
                loseReplenish.ReplenishDate = Replenish["ReplenishDate"] == null || Replenish["ReplenishDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(Replenish["ReplenishDate"]);

                loseReplenish.Quantity = Convert.ToInt32(Replenish["Quantity"]) + Convert.ToInt32(Replenish["ReplenishCount"].ToString());
            }
            else
            {
                replenishState = ReplenishState.Replenish.ToString();
                S_A_LoseReplenish loseReplenish = new S_A_LoseReplenish();
                loseReplenish.ID = Formula.FormulaHelper.CreateGuid();
                loseReplenish.State = "";
                loseReplenish.ReplenishState = replenishState;
                loseReplenish.ReplenishPeopleName = ReplenishForm["ReplenishPeopleName"];
                loseReplenish.ReplenishPeople = ReplenishForm["ReplenishPeople"];
                loseReplenish.ReplenishExplain = !Replenish.ContainsKey("ReplenishExplain") || Replenish["ReplenishExplain"] == null ? "" : Replenish["ReplenishExplain"].ToString();
                loseReplenish.ReplenishDeptName = ReplenishForm["ReplenishDeptName"];
                loseReplenish.ReplenishDept = ReplenishForm["ReplenishDept"];
                loseReplenish.RelateDocID = Replenish["RelateDocID"].ToString();
                loseReplenish.DocumentCode = Replenish.ContainsKey("DocumentCode") && Replenish["DocumentCode"] != null && !string.IsNullOrEmpty(Replenish["DocumentCode"].ToString()) ? Replenish["DocumentCode"].ToString() : "";
                // loseReplenish.DocumentCode = Replenish["DocumentCode"].ToString();
                loseReplenish.RelateDocType = Replenish["RelateDocType"].ToString();
                loseReplenish.ReplenishCount = Convert.ToInt32(Replenish["ReplenishCount"].ToString());
                loseReplenish.Quantity = Convert.ToInt32(Replenish["Quantity"]) + loseReplenish.ReplenishCount;
                loseReplenish.ConfigID = Replenish["ConfigID"].ToString();
                loseReplenish.SpaceID = Replenish["SpaceID"].ToString();
                loseReplenish.ReplenishDate = Replenish["ReplenishDate"] == null || Replenish["ReplenishDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(Replenish["ReplenishDate"]);
                loseReplenish.Name = GetFinalFullPathName(loseReplenish.RelateDocID, loseReplenish.ConfigID, loseReplenish.RelateDocType, Replenish["Name"].ToString());
                if (string.IsNullOrEmpty(loseReplenish.Name))
                    loseReplenish.Name = Replenish["Name"].ToString();
                entities.S_A_LoseReplenish.Add(loseReplenish);
            }
            Replenish["ReplenishState"] = replenishState;
            ReplenishList.Add(Replenish);//用于修改子表中的数据
        }
        //禁用删除按钮
        public void DeleteLoseReplenish()
        {
            throw new Exception("此页面信息不可删除！");
        }

        //显示全路径名称
        public string GetFinalFullPathName(string fileNodeID, string configID, string relateDocType, string fileNodeName)
        {
            var docConfigEntities = Formula.FormulaHelper.GetEntities<DocConfigEntities>();
            S_DOC_Node docNode = null;
            S_DOC_File docFile = null;
            List<string> fullPathNameTemp = new List<string>();
            if (relateDocType.ToLower().Equals("node"))
            {
                docNode = docConfigEntities.S_DOC_Node.FirstOrDefault(a => a.ID.Equals(configID));

                Dictionary<string, object> extentionJson = JsonHelper.ToObject(docNode.ExtentionJson);
                string UseFullName = extentionJson.ContainsKey("Ext_Car_UseFullName") ? extentionJson["Ext_Car_UseFullName"].ToString() : "";
                if (string.IsNullOrEmpty(UseFullName) || UseFullName.ToLower() != "true")//判断是否显示全路径
                    return "";
                fullPathNameTemp = GetFullPathName(docNode.SpaceID, fileNodeID);
                return ReplaceFullPathName(extentionJson, fileNodeName, fullPathNameTemp);
            }
            else
            {
                docFile = docConfigEntities.S_DOC_File.FirstOrDefault(b => b.ID.Equals(configID));
                Dictionary<string, object> extentionJson = new Dictionary<string, object>();
                extentionJson = JsonHelper.ToObject(docFile.ExtentionJson);
                string UseFullName = extentionJson.ContainsKey("Ext_Car_UseFullName") ? extentionJson["Ext_Car_UseFullName"].ToString() : "";
                if (string.IsNullOrEmpty(UseFullName) || UseFullName.ToLower() != "true")
                    return "";
                fullPathNameTemp = GetFullPathName(docFile.SpaceID, fileNodeID);
                return ReplaceFullPathName(extentionJson, fileNodeName, fullPathNameTemp);
            }
        }
        //从S_NodeInfo或S_FileInfo中取得全路径Name
        public List<string> GetFullPathName(string SpaceID, string fileNodeID)
        {
            var space = Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(SpaceID));
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            string sql = @"select FullPathID from (SELECT ID,Name,(select FullNodeID from S_FileInfo where S_FileInfo.ID=fileInfo.ID)+'.'+ID FullPathID
                              FROM S_FileInfo   fileInfo
                              union all
                              select ID,Name,FullPathID from S_NodeInfo) fileNode where fileNode.ID='{0}'";
            string FullPathID = sqlHelper.ExecuteScalar(string.Format(sql, fileNodeID)).ToString();
            sql = @"select Name from S_NodeInfo where ID in ('{0}')";
            DataRowCollection rows = sqlHelper.ExecuteDataTable(string.Format(sql, FullPathID.Replace(".", "','"))).Rows;
            List<string> fullPathNameTemp = new List<string>();
            foreach (DataRow row in rows)
                fullPathNameTemp.Add(row["Name"].ToString());
            return fullPathNameTemp;
        }
        //替换成要显示的全路径名样式
        public string ReplaceFullPathName(Dictionary<string, object> extentionJson, string fileNodeName, List<string> fullPathNameTemp)
        {
            string FullNameSplit = extentionJson.ContainsKey("Ext_Car_FullNameSplit") ? extentionJson["Ext_Car_FullNameSplit"].ToString() : "";
            string NameFormat = extentionJson.ContainsKey("Ext_Car_NameFormat") ? extentionJson["Ext_Car_NameFormat"].ToString() : "";
            string fullPathName = string.Join(FullNameSplit, fullPathNameTemp);
            if (!string.IsNullOrEmpty(NameFormat))
                fullPathName = NameFormat.Replace("{Name}", fileNodeName).Replace("{FullName}", fullPathName);
            else
                fullPathName = "";
            return fullPathName;
        }
    }
}
