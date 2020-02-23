using DocSystem.Logic.Domain;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config.Logic;
using Config;

namespace DocSystem.Logic
{
    public class InventoryFO
    {
        public static void CreateNewInventoryLedger(S_FileInfo file, string detail = "", InventoryType type = InventoryType.StorageIn)
        {
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var currentUser = FormulaHelper.GetUserInfo();
            var InventoryLedger = new S_A_InventoryLedger
            {
                SpaceID = file.Space.ID,
                RelateID = file.ID,
                RelateName = file.Name,
                RelateType = InventoryRelateType.File.ToString(),
                RelateDetailInfoID = "",
                CarItemName = "",
                Type = type.ToString(),
                Detail=detail,
                CreateDept=currentUser.UserDeptNames,
                CreateDate = System.DateTime.Now,
                CreateUserID = currentUser.UserID,
                CreateUserName = currentUser.UserName,
                TargetUserID = "",
                TargetUserName = ""
            };
            if (!string.IsNullOrEmpty(file.DataEntity.GetValue("Quantity")))
            {
                var num = 0;
                if (int.TryParse(file.DataEntity.GetValue("Quantity"), out num))
                {
                    InventoryLedger.TotalAmount = num;
                    InventoryLedger.InventoryAmount = num;
                }
                if (num == 0) return;
            }
            entities.Set<S_A_InventoryLedger>().Add(InventoryLedger);
            entities.SaveChanges();
        }

        public static void CreateNewInventoryLedger(S_NodeInfo node, string detail = "" ,InventoryType type = InventoryType.StorageIn)
        {
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var currentUser = FormulaHelper.GetUserInfo();
            var InventoryLedger = new S_A_InventoryLedger
            {
                SpaceID = node.Space.ID,
                RelateID = node.ID,
                RelateName = node.Name,
                RelateType = InventoryRelateType.Node.ToString(),
                RelateDetailInfoID = "",
                CarItemName = "",
                Type = type.ToString(),
                Detail=detail,
                CreateDept=currentUser.UserDeptNames,
                CreateDate = System.DateTime.Now,
                CreateUserID = currentUser.UserID,
                CreateUserName = currentUser.UserName,
                TargetUserID = "",
                TargetUserName = ""
            };
            if (!string.IsNullOrEmpty(node.DataEntity.GetValue("Quantity")))
            {
                var num = 0;
                if (int.TryParse(node.DataEntity.GetValue("Quantity"), out num))
                {
                    InventoryLedger.TotalAmount = num;
                    InventoryLedger.InventoryAmount = num;
                }
                if (num == 0) return;
            }
            entities.Set<S_A_InventoryLedger>().Add(InventoryLedger);
            entities.SaveChanges();
        }

        public static void CreateInventoryLedger(S_FileInfo file, InventoryType type, int totalAmount, int inventoryAmount,
            string relateDetailInfoID, string targetUserID = "", string targetUserName = "", string detail = "")
        {
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var currentUser = FormulaHelper.GetUserInfo();
            var InventoryLedger = new S_A_InventoryLedger
            {
                CarItemName = "",
                CreateDate = System.DateTime.Now,
                CreateUserID = currentUser.UserID,
                CreateUserName = currentUser.UserName,
                SpaceID = file.Space.ID,
                Type = type.ToString(),
                Detail = detail,
                CreateDept = currentUser.UserDeptNames,
                InventoryAmount = inventoryAmount,
                TotalAmount = totalAmount,
                RelateID = file.ID,
                RelateName = file.Name,
                RelateType = ListConfigType.File.ToString(),
                RelateDetailInfoID = relateDetailInfoID,
                TargetUserID = targetUserID,
                TargetUserName = targetUserName
            };

            var qNum = int.Parse(string.IsNullOrEmpty(file.DataEntity.GetValue("Quantity")) ? "0" : file.DataEntity.GetValue("Quantity")) + totalAmount;
            file.DataEntity.SetValue("Quantity", qNum);
            var sNum = int.Parse(string.IsNullOrEmpty(file.DataEntity.GetValue("StorageNum")) ? "0" : file.DataEntity.GetValue("StorageNum")) + inventoryAmount;
            file.DataEntity.SetValue("StorageNum", sNum);
            if (qNum < 0 || sNum < 0)
                throw new Formula.Exceptions.BusinessException("遗失份数大于库存，请重新填写");

            file.Save(false);
            entities.Set<S_A_InventoryLedger>().Add(InventoryLedger);
            entities.SaveChanges();
        }

        public static void CreateInventoryLedger(S_NodeInfo node, InventoryType type, int totalAmount, int inventoryAmount,
            string relateDetailInfoID, string targetUserID = "", string targetUserName = "", string detail = "")
        {
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var currentUser = FormulaHelper.GetUserInfo();
            var InventoryLedger = new S_A_InventoryLedger
            {
                CarItemName = "",
                CreateDate = System.DateTime.Now,
                CreateUserID = currentUser.UserID,
                CreateUserName = currentUser.UserName,
                SpaceID = node.Space.ID,
                Type = type.ToString(),
                Detail = detail,
                CreateDept = currentUser.UserDeptNames,
                InventoryAmount = inventoryAmount,
                TotalAmount = totalAmount,
                RelateID = node.ID,
                RelateName = node.Name,
                RelateType = ListConfigType.File.ToString(),
                RelateDetailInfoID = relateDetailInfoID,
                TargetUserID = targetUserID,
                TargetUserName = targetUserName
            };

            var qNum = int.Parse(string.IsNullOrEmpty(node.DataEntity.GetValue("Quantity")) ? "0" : node.DataEntity.GetValue("Quantity")) + totalAmount;
            node.DataEntity.SetValue("Quantity", qNum);
            var sNum = int.Parse(string.IsNullOrEmpty(node.DataEntity.GetValue("StorageNum")) ? "0" : node.DataEntity.GetValue("StorageNum")) + inventoryAmount;
            node.DataEntity.SetValue("StorageNum", sNum);
            if (qNum < 0 || sNum < 0)
                throw new Formula.Exceptions.BusinessException("遗失份数大于库存，请重新填写");

            node.Save(false);
            entities.Set<S_A_InventoryLedger>().Add(InventoryLedger);
            entities.SaveChanges();
        }
        public static void Delete(string RelateID)
        {
            var sql = "delete from S_A_InventoryLedger where RelateID = '" + RelateID + "'";
            var DB = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DB.ExecuteNonQuery(sql);
        }
    }
}
