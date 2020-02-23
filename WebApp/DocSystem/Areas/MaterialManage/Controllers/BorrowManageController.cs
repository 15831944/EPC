using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Base.Logic.BusinessFacade;
using Formula;
using Formula.Helper;
using Config;
using Config.Logic;
using DocSystem;
using DocSystem.Logic;
using DocSystem.Logic.Domain;

namespace DocSystem.Areas.MaterialManage.Controllers
{
    public class BorrowManageController : DocConstController
    {
        public ActionResult RegisterForm()
        {
            int borrowExpireDate = 7;
            var DocBorrowExpireDays = System.Configuration.ConfigurationManager.AppSettings["DocBorrowExpireDays"];
            if (!string.IsNullOrEmpty(DocBorrowExpireDays))
                borrowExpireDate = Convert.ToInt32(DocBorrowExpireDays);
            ViewBag.BorrowExpireDays = "var BorrowExpireDays = " + borrowExpireDate;
            return View();
        }

        public JsonResult GetChildDetail(string ID)
        {
            var sql = @"select *,ISNULL(LendNumber,0)-ISNULL(ReturnNumber,0)-ISNULL(LostNumber,0) as ToReturnNumber,
datediff(dd, getdate(), BorrowExpireDate) as RemainDay from S_BorrowDetail where ParentID = '" + ID + "' and BorrowState<>'Finish'";
            var children = this.SqlHelper.ExecuteDataTable(sql);
            return Json(children, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLendDetailInfo(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var addList = new List<Dictionary<string, object>>();
            foreach (var root in list)
            {
                var space = DocConfigHelper.CreateConfigSpaceByID(root.GetValue("SpaceID"));
                if (root.GetValue("DetailType") == NodeType.Node.ToString())
                {
                    var rootNode = new S_NodeInfo(root.GetValue("RelateID"), space);
                    root.SetValue("Quantity", rootNode.DataEntity.GetValue("Quantity"));
                    root.SetValue("StorageNumber", rootNode.DataEntity.GetValue("StorageNum"));

                    if (root.GetValue("HasChild").ToLower() == "true")
                    {
                        var pID = root.GetValue("ID");
                        var fState = BorrowDetailState.Finish.ToString();
                        var children = this.entities.Set<S_BorrowDetail>().Where(a => a.ParentID == pID && a.BorrowState != fState).ToList();
                        foreach (var child in children)
                        {
                            var childDic = child.ToDic();
                            addList.Add(childDic);
                            childDic.SetValue("UID", childDic.GetValue("RelateID"));
                            childDic.SetValue("LendNumber", "");
                            childDic.SetValue("ReturnNumber", "");
                            childDic.SetValue("LostNumber", "");
                            childDic.SetValue("ToRtnNumber", (child.LendNumber ?? 0) - (child.ReturnNumber ?? 0) - (child.LostNumber ?? 0));
                            if (childDic.GetValue("DetailType") == NodeType.Node.ToString())
                            {
                                var childNode = new S_NodeInfo(childDic.GetValue("RelateID"), space);
                                childDic.SetValue("Quantity", childNode.DataEntity.GetValue("Quantity"));
                                childDic.SetValue("StorageNumber", childNode.DataEntity.GetValue("StorageNum"));
                                childDic.SetValue("ParentUID", childNode.ParentID);
                                addParentNode(rootNode, childNode, addList);
                            }
                            else
                            {
                                var file = new S_FileInfo(childDic.GetValue("RelateID"), space);
                                childDic.SetValue("Quantity", file.DataEntity.GetValue("Quantity"));
                                childDic.SetValue("StorageNumber", file.DataEntity.GetValue("StorageNum"));
                                childDic.SetValue("ParentUID", file.NodeID);
                                var relateNode = new S_NodeInfo(file.NodeID, space);
                                addParentNode(rootNode, relateNode, addList);
                            }
                        }
                    }
                }
                else
                {
                    var file = new S_FileInfo(root.GetValue("RelateID"), space);
                    root.SetValue("Quantity", file.DataEntity.GetValue("Quantity"));
                    root.SetValue("StorageNumber", file.DataEntity.GetValue("StorageNum"));
                }
                root.SetValue("ToRtnNumber",
                    (Convert.ToInt32(string.IsNullOrEmpty(root.GetValue("LendNumber")) ? "0" : root.GetValue("LendNumber"))
                    - Convert.ToInt32(string.IsNullOrEmpty(root.GetValue("ReturnNumber")) ? "0" : root.GetValue("ReturnNumber"))
                    - Convert.ToInt32(string.IsNullOrEmpty(root.GetValue("LostNumber")) ? "0" : root.GetValue("LostNumber"))));
                root.SetValue("LendNumber", "");
                root.SetValue("ReturnNumber", "");
                root.SetValue("LostNumber", "");
                root.SetValue("UID", root.GetValue("RelateID"));
                root.SetValue("ParentUID", root.GetValue("ParentID"));
            }
            list.AddRange(addList);
            return Json(list);
        }

        private void addParentNode(S_NodeInfo rootNode, S_NodeInfo leafNode, List<Dictionary<string, object>> addList)
        {
            if (leafNode.ID == rootNode.ID)
                return;
            else
            {
                if (addList.FirstOrDefault(a => a.GetValue("UID") == leafNode.ID) == null)
                {
                    var pDic = new Dictionary<string, object>();
                    pDic.Add("ID", "");
                    pDic.Add("UID", leafNode.ID);
                    pDic.Add("ParentUID", leafNode.ParentID);
                    pDic.Add("RelateID", leafNode.ID);
                    pDic.Add("Name", leafNode.Name);
                    pDic.Add("Code", leafNode.DataEntity.GetValue("DocumentCode"));
                    pDic.Add("SpaceID", leafNode.Space.ID);
                    pDic.Add("SpaceName", leafNode.Space.Name);
                    pDic.Add("DataType", leafNode.ConfigInfo.Name);
                    pDic.Add("DetailType", NodeType.Node.ToString());
                    addList.Add(pDic);
                }

                addParentNode(rootNode, leafNode.Parent, addList);
            }
        }

        public JsonResult GetBorrowDetailTree(string ListData)
        {
            var listData = JsonHelper.ToList(ListData);
            var tree = new List<Dictionary<string, object>>();
            foreach (var item in listData)
            {
                if (item.GetValue("_level") == "0")
                {
                    tree.Add(item);
                    var space = DocConfigHelper.CreateConfigSpaceByID(item.GetValue("SpaceID"));
                    if (item.GetValue("DetailType") == NodeType.Node.ToString())
                    {
                        var nodeInfo = new S_NodeInfo(item.GetValue("RelateID"), space);
                        var fileList = getFileList(nodeInfo, listData, item);
                        var childList = getChildNodeList(nodeInfo, listData, item);
                        childList.AddRange(fileList);
                        item.SetValue("children", childList);
                    }
                }
            }
            return Json(tree);
        }

        private List<Dictionary<string, object>> getFileList(S_NodeInfo nodeInfo, List<Dictionary<string, object>> listData, Dictionary<string, object> row)
        {
            var fileList = new List<Dictionary<string, object>>();
            foreach (var fileInfo in nodeInfo.FileInfos)
            {
                if (fileInfo.DataEntity.GetValue("State") != DocState.Published.ToString())
                    continue;
                var newItem = listData.FirstOrDefault(a => a.GetValue("UID") == fileInfo.ID
                    && a.GetValue("ParentID") == row.GetValue("ID"));
                if (newItem == null)
                {
                    newItem = new Dictionary<string, object>();
                    newItem.Add("ID", "");
                    newItem.Add("Name", fileInfo.Name);
                    newItem.Add("Code", fileInfo.DataEntity.GetValue("Code"));
                    newItem.Add("SpaceID", fileInfo.Space.ID);
                    newItem.Add("SpaceName", fileInfo.Space.Name);
                    newItem.Add("DataType", fileInfo.ConfigInfo.Name);
                    newItem.Add("DetailType", NodeType.File.ToString());
                    newItem.Add("RelateID", fileInfo.ID);
                    newItem.Add("ConfigID", fileInfo.ConfigInfo.ID);
                    newItem.Add("LendNumber", "");
                    newItem.Add("ParentID", row.GetValue("ID"));
                }
                newItem.SetValue("UID", fileInfo.ID);
                newItem.SetValue("ParentUID", nodeInfo.ID);
                newItem.SetValue("ApplyNumber", row.GetValue("ApplyNumber"));
                newItem.SetValue("Quantity", fileInfo.DataEntity.GetValue("Quantity"));
                newItem.SetValue("StorageNumber", fileInfo.DataEntity.GetValue("StorageNum"));
                fileList.Add(newItem);
            }
            return fileList;
        }

        private List<Dictionary<string, object>> getChildNodeList(S_NodeInfo nodeInfo, List<Dictionary<string, object>> listData, Dictionary<string, object> row)
        {
            var nodeList = new List<Dictionary<string, object>>();
            foreach (var childNode in nodeInfo.Children)
            {
                var newItem = listData.FirstOrDefault(a => a.GetValue("UID") == childNode.ID
                    && a.GetValue("ParentID") == row.GetValue("ID"));
                if (newItem == null)
                {
                    newItem = new Dictionary<string, object>();
                    newItem.Add("ID", "");
                    newItem.Add("Name", childNode.Name);
                    newItem.Add("Code", childNode.DataEntity.GetValue("DocumentCode"));
                    newItem.Add("SpaceID", childNode.Space.ID);
                    newItem.Add("SpaceName", childNode.Space.Name);
                    newItem.Add("DataType", childNode.ConfigInfo.Name);
                    newItem.Add("DetailType", NodeType.Node.ToString());
                    newItem.Add("RelateID", childNode.ID);
                    newItem.Add("ConfigID", childNode.ConfigInfo.ID);
                    newItem.Add("LendNumber", "");
                    newItem.Add("ParentID", row.GetValue("ID"));
                }
                newItem.SetValue("UID", childNode.ID);
                newItem.SetValue("ParentUID", childNode.ParentID);
                newItem.SetValue("ApplyNumber", row.GetValue("ApplyNumber"));
                newItem.SetValue("Quantity", childNode.DataEntity.GetValue("Quantity"));
                newItem.SetValue("StorageNumber", childNode.DataEntity.GetValue("StorageNum"));

                var thisFileList = getFileList(childNode, listData, row);
                var thisChildNodeList = getChildNodeList(childNode, listData, row);
                thisChildNodeList.AddRange(thisFileList);
                newItem.SetValue("children", thisChildNodeList);

                nodeList.Add(newItem);
            }

            return nodeList;
        }

        public JsonResult SetFinish(string TreeData)
        {
            var treeData = JsonHelper.ToList(TreeData);
            var rootIDs = treeData.Select(a => a.GetValue("ID")).ToList();
            var borrowDetails = this.entities.Set<S_BorrowDetail>().Where(a =>
                rootIDs.Contains(a.ID) || rootIDs.Contains(a.ParentID)).ToList();
            foreach (var rootID in rootIDs)
            {
                var root = borrowDetails.FirstOrDefault(a => a.ID == rootID);
                var children = borrowDetails.Where(a => a.ParentID == rootID);
                if (children.Count(a => a.BorrowState != BorrowDetailState.Finish.ToString()) > 0)
                    throw new Formula.Exceptions.BusinessException("【" + root.Name + "】存在子对象是借出状态，不能完成");
                if ((root.LendNumber ?? 0) - (root.ReturnNumber ?? 0) - (root.LostNumber ?? 0) != 0)
                    throw new Formula.Exceptions.BusinessException("【" + root.Name + "】借出尚未归还，不能完成");

                root.BorrowState = BorrowDetailState.Finish.ToString();
            }
            this.entities.SaveChanges();

            return Json(new { });
        }

        public JsonResult Save(string FormData)
        {
            int borrowExpireDate = 7;
            var DocBorrowExpireDays = System.Configuration.ConfigurationManager.AppSettings["DocBorrowExpireDays"];
            if (!string.IsNullOrEmpty(DocBorrowExpireDays))
                borrowExpireDate = Convert.ToInt32(DocBorrowExpireDays);

            var formData = JsonHelper.ToObject(FormData);
            var registerType = formData.GetValue("RegisterType");
            var applyUser = formData.GetValue("ApplyUser");
            var applyUserName = formData.GetValue("ApplyUserName");
            var applyDept = formData.GetValue("ApplyDept");
            var applyDeptName = formData.GetValue("ApplyDeptName");
            var lostRemarks = formData.GetValue("LostRemarks");
            var listData = JsonHelper.ToList(formData.GetValue("ListData"));

            if (registerType == "Lend")
            {
                foreach (var item in listData)
                {
                    item.SetValue("LendUserID", applyUser);
                    item.SetValue("LendUserName", applyUserName);
                    item.SetValue("LendDeptID", applyDept);
                    item.SetValue("LendDeptName", applyDeptName);
                    if (string.IsNullOrEmpty(item.GetValue("LendDate")))
                    {
                        item.SetValue("LendDate", DateTime.Now);
                    }
                    if (string.IsNullOrEmpty(item.GetValue("BorrowExpireDate")))
                    {
                        item.SetValue("BorrowExpireDate", DateTime.Now.AddDays(borrowExpireDate));
                    }
                    Lend(item);
                }
            }
            else if (registerType == "Return")
            {
                foreach (var item in listData)
                {
                    item.SetValue("ReturnUserID", applyUser);
                    item.SetValue("ReturnUser", applyUserName);
                    item.SetValue("ReturnDeptID", applyDept);
                    item.SetValue("ReturnDeptName", applyDeptName);
                    item.SetValue("ReturnDate", DateTime.Now);
                    Return(item);
                }
            }
            else if (registerType == "Lost")
            {
                foreach (var item in listData)
                {
                    item.SetValue("LostUserID", applyUser);
                    item.SetValue("LostUserName", applyUserName);
                    item.SetValue("LostDeptID", applyDept);
                    item.SetValue("LostDeptName", applyDeptName);
                    item.SetValue("LostDate", DateTime.Now);
                    item.SetValue("LostRemarks", lostRemarks);
                    Lost(item);
                }
            }
            this.entities.SaveChanges();
            return Json(new { });
        }

        private void Lend(Dictionary<string, object> row)
        {
            var lendNumber = 0;
            int.TryParse(row.GetValue("LendNumber"), out lendNumber);
            var id = row.GetValue("ID");
            var borrowDetail = this.entities.Set<S_BorrowDetail>().FirstOrDefault(a => a.ID == id);
            var isNew = false;
            if (lendNumber > 0)
            {
                #region 部分借阅
                if (borrowDetail == null)
                {
                    if (string.IsNullOrEmpty(id))
                        id = FormulaHelper.CreateGuid();
                    borrowDetail = new S_BorrowDetail
                    {
                        ID = id,
                        SpaceID = row.GetValue("SpaceID"),
                        SpaceName = row.GetValue("SpaceName"),
                        DataType = row.GetValue("DataType"),
                        DetailType = row.GetValue("DetailType"),
                        RelateID = row.GetValue("RelateID"),
                        ConfigID = row.GetValue("ConfigID"),
                        Name = row.GetValue("Name"),
                        Code = row.GetValue("Code"),
                        LendUserID = row.GetValue("LendUserID"),
                        LendUserName = row.GetValue("LendUserName"),
                        LendDeptID = row.GetValue("LendDeptID"),
                        LendDeptName = row.GetValue("LendDeptName"),
                        ParentID = row.GetValue("ParentID")
                    };
                    var applyNumber = 0;
                    int.TryParse(row.GetValue("ApplyNumber"), out applyNumber);
                    borrowDetail.ApplyNumber = applyNumber;
                    this.entities.Set<S_BorrowDetail>().Add(borrowDetail);
                    isNew = true;

                    var parent = this.entities.Set<S_BorrowDetail>().FirstOrDefault(a => a.ID == borrowDetail.ParentID);
                    if (parent != null)
                        parent.HasChild = TrueOrFalse.True.ToString();
                }
                #endregion
                borrowDetail.LendUserID = row.GetValue("LendUserID");
                borrowDetail.LendUserName = row.GetValue("LendUserName");
                borrowDetail.LendDeptID = row.GetValue("LendDeptID");
                borrowDetail.LendDeptName = row.GetValue("LendDeptName");
                borrowDetail.LendNumber = (borrowDetail.LendNumber ?? 0) + lendNumber;
                if (borrowDetail.LendDate == null)
                {
                    borrowDetail.LendDate = Convert.ToDateTime(row.GetValue("LendDate"));
                    borrowDetail.BorrowExpireDate = Convert.ToDateTime(row.GetValue("BorrowExpireDate"));
                }
                borrowDetail.BorrowState = BorrowDetailState.ToReturn.ToString();

                if (borrowDetail.DetailType == NodeType.Node.ToString())
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var nodeInfo = new S_NodeInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(nodeInfo, InventoryType.Lend, 0, (0 - lendNumber),
                        borrowDetail.ID, borrowDetail.LendUserID, borrowDetail.LendUserName, EnumBaseHelper.GetEnumDescription(InventoryType.Lend.GetType(),InventoryType.Lend.ToString())+"份数：" + lendNumber+"份");
                    nodeInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Borrow.ToString());
                    nodeInfo.Save(false);

                    if (isNew)
                        borrowDetail.Name = nodeInfo.CreateCarName();
                }
                else
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var fileInfo = new S_FileInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(fileInfo, InventoryType.Lend, 0, (0 - lendNumber),
                        borrowDetail.ID, borrowDetail.LendUserID, borrowDetail.LendUserName, EnumBaseHelper.GetEnumDescription(InventoryType.Lend.GetType(), InventoryType.Lend.ToString()) + "份数：" + lendNumber + "份");
                    fileInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Borrow.ToString());
                    fileInfo.Save(false);

                    if (isNew)
                        borrowDetail.Name = fileInfo.CreateCarName();
                }
            }
            else if (borrowDetail != null)
            {
                if (borrowDetail.LendDate == null)
                    borrowDetail.LendDate = Convert.ToDateTime(row.GetValue("LendDate"));
                if (borrowDetail.BorrowExpireDate == null)
                    borrowDetail.BorrowExpireDate = Convert.ToDateTime(row.GetValue("BorrowExpireDate"));
            }
        }

        private void Return(Dictionary<string, object> row)
        {
            var returnNumber = 0;
            int.TryParse(row.GetValue("ReturnNumber"), out returnNumber);
            var id = row.GetValue("ID");
            var borrowDetail = this.entities.Set<S_BorrowDetail>().FirstOrDefault(a => a.ID == id);
            if (returnNumber > 0 && borrowDetail != null)
            {
                borrowDetail.ReturnUserID = row.GetValue("ReturnUserID");
                borrowDetail.ReturnUser = row.GetValue("ReturnUser");
                borrowDetail.ReturnDeptID = row.GetValue("ReturnDeptID");
                borrowDetail.ReturnDeptName = row.GetValue("ReturnDeptName");
                borrowDetail.ReturnNumber = (borrowDetail.ReturnNumber ?? 0) + returnNumber;
                borrowDetail.ReturnDate = Convert.ToDateTime(row.GetValue("ReturnDate"));

                var isFinish = ((borrowDetail.LendNumber ?? 0) - (borrowDetail.ReturnNumber ?? 0) - (borrowDetail.LostNumber ?? 0)) == 0;
                if (isFinish)
                {
                    borrowDetail.BorrowState = BorrowDetailState.Finish.ToString();
                }

                if (borrowDetail.DetailType == NodeType.Node.ToString())
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var nodeInfo = new S_NodeInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(nodeInfo, InventoryType.Return, 0, returnNumber,
                        borrowDetail.ID, borrowDetail.ReturnUserID, borrowDetail.ReturnUser, EnumBaseHelper.GetEnumDescription(InventoryType.Return.GetType(), InventoryType.Return.ToString()) + "份数：" + returnNumber + "份");

                    if (isFinish)
                        nodeInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Return.ToString());

                    nodeInfo.Save(false);
                }
                else
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var fileInfo = new S_FileInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(fileInfo, InventoryType.Return, 0, returnNumber,
                        borrowDetail.ID, borrowDetail.ReturnUserID, borrowDetail.ReturnUser, EnumBaseHelper.GetEnumDescription(InventoryType.Return.GetType(), InventoryType.Return.ToString()) + "份数：" + returnNumber + "份");

                    if (isFinish)
                        fileInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Return.ToString());

                    fileInfo.Save(false);
                }
            }
        }

        private void Lost(Dictionary<string, object> row)
        {
            var lostNumber = 0;
            int.TryParse(row.GetValue("LostNumber"), out lostNumber);
            var id = row.GetValue("ID");
            var borrowDetail = this.entities.Set<S_BorrowDetail>().FirstOrDefault(a => a.ID == id);
            if (lostNumber > 0 && borrowDetail != null)
            {
                borrowDetail.LostUserID = row.GetValue("LostUserID");
                borrowDetail.LostUserName = row.GetValue("LostUserName");
                borrowDetail.LostDeptID = row.GetValue("LostDeptID");
                borrowDetail.LostDeptName = row.GetValue("LostDeptName");
                borrowDetail.LostNumber = (borrowDetail.LostNumber ?? 0) + lostNumber;
                borrowDetail.LostDate = Convert.ToDateTime(row.GetValue("LostDate"));
                borrowDetail.LostRemarks = row.GetValue("LostRemarks");

                var isFinish = ((borrowDetail.LendNumber ?? 0) - (borrowDetail.ReturnNumber ?? 0) - (borrowDetail.LostNumber ?? 0)) == 0;
                if (isFinish)
                {
                    borrowDetail.BorrowState = BorrowDetailState.Finish.ToString();
                }

                if (borrowDetail.DetailType == NodeType.Node.ToString())
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var nodeInfo = new S_NodeInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(nodeInfo, InventoryType.Lose, 0 - lostNumber, 0,
                        borrowDetail.ID, borrowDetail.LostUserID, borrowDetail.LostUserName, EnumBaseHelper.GetEnumDescription(InventoryType.Lose.GetType(), InventoryType.Lose.ToString()) + "份数：" + lostNumber + "份");
                    if (isFinish)
                        nodeInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Return.ToString());
                    nodeInfo.Save(false);
                }
                else
                {
                    var space = DocConfigHelper.CreateConfigSpaceByID(borrowDetail.SpaceID);
                    var fileInfo = new S_FileInfo(borrowDetail.RelateID, space);
                    InventoryFO.CreateInventoryLedger(fileInfo, InventoryType.Lose, 0 - lostNumber, 0,
                        borrowDetail.ID, borrowDetail.LostUserID, borrowDetail.LostUserName, EnumBaseHelper.GetEnumDescription(InventoryType.Lose.GetType(), InventoryType.Lose.ToString()) + "份数：" + lostNumber + "份");
                    if (isFinish)
                        fileInfo.DataEntity.SetValue("BorrowState", BorrowReturnState.Return.ToString());
                    fileInfo.Save(false);
                }
                CreateLoseReplenish(borrowDetail);
            }
        }

        private void CreateLoseReplenish(S_BorrowDetail detail)
        {
            var S_A_LoseReplenish = new S_A_LoseReplenish
            {
                ID = FormulaHelper.CreateGuid(),
                RelateDocID = detail.RelateID,
                State = LostReplenishState.Lose.ToString(),
                LoseCount = detail.LostNumber,
                ReplenishState = ReplenishState.Unreplenished.ToString(),
                LosePeople = detail.LostUserID,
                LosePeopleName = detail.LostUserName,
                LoseDept = detail.LostDeptID,
                LoseDeptName = detail.LostDeptName,
                Name = detail.Name,
                ConfigID = detail.ConfigID,
                SpaceID = detail.SpaceID,
                Quantity = detail.LostNumber,
                LoseDate = detail.LostDate,
                LoseExplain = detail.LostRemarks,
                RelateDocType = detail.DetailType,
                DocumentCode = detail.Code
            };
            this.entities.Set<S_A_LoseReplenish>().Add(S_A_LoseReplenish);
        }

        public JsonResult ReCall(string ListData)
        {
            IMessageService msgService = FormulaHelper.GetService<IMessageService>();

            var listData = JsonHelper.ToList(ListData);
            var ids = listData.Select(a => a.GetValue("ID"));
            var fState = BorrowDetailState.Finish.ToString();
            var borrowDetails = this.entities.Set<S_BorrowDetail>().Where(a =>
                (ids.Contains(a.ID) || ids.Contains(a.ParentID)) && a.BorrowState != fState).ToList();
            foreach (var item in borrowDetails)
            {
                if (item.BorrowExpireDate == null || string.IsNullOrEmpty(item.LendUserID))
                    continue;
                var title = "借阅归还提醒";
                var content = "您借阅的【" + item.Name + (string.IsNullOrEmpty(item.Code) ? "" : "(" + item.Code + ")")
                    + "】将于【" + ((DateTime)item.BorrowExpireDate).ToShortDateString() + "】到期，请尽快归还。";
                if (((DateTime)item.BorrowExpireDate) < DateTime.Now)
                    content = "您借阅的【" + item.Name + (string.IsNullOrEmpty(item.Code) ? "" : "(" + item.Code + ")")
                        + "】已于【" + ((DateTime)item.BorrowExpireDate).ToShortDateString() + "】到期，请尽快归还。";
                msgService.SendMsg(title, content, "", "", item.LendUserID, item.LendUserName, this.CurrentUserInfo);
            }

            return Json(new { });
        }
    }
}
