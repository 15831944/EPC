using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdapter;
using DocSystem.Logic.Domain;
using Formula.DynConditionObject;
using Formula;
using Formula.Helper;
using Config.Logic;
using Config;
using DocSystem.Logic;
using System.Data;

namespace DocSystem.Areas.ApplyInfo.Controllers
{
    public class MyBorrowController : DocConstController<S_DownloadDetail>
    {
        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select *,isnull(datediff(dd,getdate(),[BorrowExpireDate]),7) as RemainDay 
from S_BorrowDetail where BorrowState = '{1}' and LendUserID='{0}'";
            sql = string.Format(sql, FormulaHelper.GetUserInfo().UserID, BorrowDetailState.ToReturn.ToString());
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql, qb);
            return Json(dt);
        }

        public JsonResult getexpirelist(QueryBuilder qb)
        {
            string sql = "select * from S_BorrowDetail where BorrowState='{1}' and LendUserID='{0}'";
            sql = string.Format(sql, FormulaHelper.GetUserInfo().UserID, BorrowDetailState.Finish.ToString());
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql, qb);
            return Json(dt);
        }
        //续借
        public JsonResult DocBorrowContinueDays()
        {
            int borrowExpireDay = Convert.ToInt32(string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DocBorrowContinueDays"]) ? 7 : Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DocBorrowContinueDays"]));

            string ID = string.Empty;
            ID = GetQueryString("IDs").TrimEnd(',').Replace(",", "','");
            ID = "'" + ID + "'";
            string sql1 = @"select  * , case when Remain is null then '7' else Remain end as RemainDay 
 from (select *,DATEDIFF(DD,LendDate,BorrowExpireDate) LendDay,datediff(dd,getdate(),[BorrowExpireDate]) as Remain from S_BorrowDetail) as A  where ID in (" + ID + ")"; ;
            var dts = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql1);
            foreach (DataRow rowRemainDay in dts.Rows)
            {
                if (Convert.ToInt32(rowRemainDay["RemainDay"]) < 0)
                {
                    throw new Formula.Exceptions.BusinessException("过期文件件不能续借");
                }
                var ContinuedTimes = rowRemainDay["ContinuedTimes"];
                if (ContinuedTimes != DBNull.Value)
                    if (Convert.ToInt32(ContinuedTimes) > 0)
                        throw new Formula.Exceptions.BusinessException("已续借一次，禁止续借");
            }
            string sql = " update  S_BorrowDetail set BorrowExpireDate=DATEADD(DAY," + borrowExpireDay + ",BorrowExpireDate),ContinuedTimes = (ISNULL(ContinuedTimes,0)+1) where ID in (" + ID + ")";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteNonQuery(sql);
            return Json("");
        }
        //借阅
        public JsonResult Borrow(string rows)
        {
            //BorrowID DetailType
            // select * from  T_Borrow_FileInfo 拿到fileid 和 NodeId
            var list = JsonHelper.ToList(rows);
            List<Dictionary<string, object>> files = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> nodes = new List<Dictionary<string, object>>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetValue("DetailType") == "File")
                    files.Add(list[i]);
                else
                    nodes.Add(list[i]);
            }
            if (nodes.Count > 0)
                applynodeborrow(nodes);
            if (files.Count > 0)
                applyfileborrow(files);
            return Json("");
        }

        public void applynodeborrow(List<Dictionary<string, object>> nodes)
        {
            var docConstEntities = FormulaHelper.GetEntities<DocConstEntities>();

            var list = nodes;
            foreach (var item in list)
            {
                var space = DocConfigHelper.CreateConfigSpaceByID(item.GetValue("SpaceID"));
                // var FileInfo = docConstEntities.T_Borrow_FileInfo.FirstOrDefault(d => d.T_BorrowID == item.GetValue("BorrowID") && d.FileName == item.GetValue("Name"));
                S_NodeInfo nodeInfo = new S_NodeInfo(item.GetValue("RelateID"), space);
                if (nodeInfo == null) continue;
                if (nodeInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("未能找到文件【" + nodeInfo.ID + "】的配置对象信息");
                var carInfo = docConstEntities.S_CarInfo.FirstOrDefault(d => d.UserID == this.CurrentUserInfo.UserID
                    && d.NodeID == nodeInfo.ID && d.State == "New" && d.Type == "Borrow" && d.DataType == nodeInfo.ConfigInfo.Name);
                if (carInfo == null)
                {
                    carInfo = new S_CarInfo();
                    carInfo.ID = FormulaHelper.CreateGuid(); ;
                    carInfo.Name = nodeInfo.CreateCarName(); //nodeInfo.Name;
                    carInfo.NodeID = nodeInfo.ID;
                    carInfo.NodeName = nodeInfo.Name;
                    carInfo.SpaceID = item.GetValue("SpaceID");
                    carInfo.State = "New";
                    carInfo.Type = ItemType.Borrow.ToString();
                    carInfo.UserID = this.CurrentUserInfo.UserID;
                    carInfo.UserName = this.CurrentUserInfo.UserName;
                    //carInfo.Code = nodeInfo.;
                    carInfo.ConfigID = nodeInfo.ConfigInfo.ID;
                    carInfo.CreateDate = DateTime.Now;
                    carInfo.CreateUser = this.CurrentUserInfo.UserName;
                    carInfo.CreateUserID = this.CurrentUserInfo.UserID;
                    carInfo.DataType = nodeInfo.ConfigInfo.Name;
                    docConstEntities.S_CarInfo.Add(carInfo);
                }
                else if (list.Count == 1)
                    throw new Formula.Exceptions.BusinessException("【" + nodeInfo.Name + "】已在借阅车中，请前往借阅车申请借阅");
            }
            docConstEntities.SaveChanges();
        }

        public void applyfileborrow(List<Dictionary<string, object>> files)
        {
            var list = files;
            var docConstEntities = FormulaHelper.GetEntities<DocConstEntities>();
            var user = FormulaHelper.GetUserInfo();
            foreach (var file in list)
            {
                var space = DocConfigHelper.CreateConfigSpaceByID(file.GetValue("SpaceID"));
                //var FileInfo = docConstEntities.T_Borrow_FileInfo.FirstOrDefault(d => d.T_BorrowID == file.GetValue("BorrowID") && d.FileName == file.GetValue("Name"));
                S_FileInfo fileInfo = new S_FileInfo(file.GetValue("RelateID"), space);
                if (fileInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("未能找到文件【" + fileInfo.ID + "】的配置对象信息");
                if (fileInfo.DataEntity.GetValue("BorrowState") == "Borrow")
                    throw new Formula.Exceptions.BusinessException("已经借出，不能借阅！");
                if (fileInfo.DataEntity.GetValue("Media") == "电子版")
                    throw new Formula.Exceptions.BusinessException("载体是电子版，不能借阅！");
                var carItem = this.entities.Set<S_CarInfo>().FirstOrDefault(d => d.FileID == fileInfo.ID
                    && d.State == "New" && d.UserID == this.CurrentUserInfo.UserID && d.Type == "Borrow");
                if (carItem == null)
                {
                    carItem = new S_CarInfo();
                    carItem.ID = FormulaHelper.CreateGuid();
                    carItem.NodeID = fileInfo.NodeID;
                    carItem.Type = ItemType.Borrow.ToString();
                    carItem.UserID = user.UserID;
                    carItem.UserName = user.UserName;
                    carItem.CreateDate = DateTime.Now;
                    carItem.DataType = fileInfo.ConfigInfo.Name;
                    carItem.FileID = fileInfo.ID;
                    carItem.SpaceID = file.GetValue("SpaceID");
                    carItem.ConfigID = fileInfo.ConfigInfo.ID;
                    carItem.Name = fileInfo.CreateCarName();// fileInfo.Name;
                    carItem.State = ItemState.New.ToString();
                    carItem.CreateUser = user.UserName;
                    carItem.CreateUserID = user.UserID;
                    this.entities.Set<S_CarInfo>().Add(carItem);
                }
                else if (list.Count == 1)
                    throw new Formula.Exceptions.BusinessException("该文件已在借阅车中，请前往借阅车申请借阅");
            }
            this.entities.SaveChanges();
        }

    }
}
