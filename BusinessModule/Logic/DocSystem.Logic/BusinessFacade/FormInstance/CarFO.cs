using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Formula;
using DocSystem.Logic.Domain;
using Config.Logic;

namespace DocSystem.Logic
{
    public class CarFO
    {
        public static int GetCarCount()
        {
            SQLHelper constSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            //购物车数量
            var carCount = constSqlHelper.ExecuteScalar(string.Format("select count(1) from S_CarInfo  with(nolock) where CreateUserId='{0}' and State='New'", FormulaHelper.GetUserInfo().UserID));
            return Convert.ToInt32(carCount);
        }

        public static void CreateCarItem(S_FileInfo fileInfo, string type)
        {
            var user = FormulaHelper.GetUserInfo();
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            if (fileInfo.Space == null) throw new Formula.Exceptions.BusinessException("没有获取到申请文件的配置空间信息，无法进行申请下载");
            if (fileInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("没有获取到申请文件的文件配置类别信息，无法进行申请下载");
            var carItem = entities.Set<S_CarInfo>().FirstOrDefault(d => d.FileID == fileInfo.ID
                && d.State == "New" && d.UserID == user.UserID && d.Type == type);
            if (carItem == null)
            {
                carItem = new S_CarInfo
                {
                    ID = FormulaHelper.CreateGuid(),
                    UserID = user.UserID,
                    UserName = user.UserName,
                    Name = fileInfo.CreateCarName(),
                    Code = fileInfo.DataEntity.GetValue("Code"),
                    DataType = fileInfo.ConfigInfo.Name,
                    Type = type,
                    FileID = fileInfo.ID,
                    NodeID = fileInfo.NodeID,
                    State = ItemState.New.ToString(),
                    CreateDate = DateTime.Now,
                    CreateUser = user.UserName,
                    CreateUserID = user.UserID,
                    ConfigID = fileInfo.ConfigInfo.ID,
                    SpaceID = fileInfo.Space.ID,
                };
                entities.Set<S_CarInfo>().Add(carItem);
            }
        }

        public static void CreateCarItem(S_NodeInfo nodeInfo, string type)
        {
            var user = FormulaHelper.GetUserInfo();
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            if (nodeInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("未能找到文件【" + nodeInfo.ID + "】的配置对象信息");
            var carInfo = entities.Set<S_CarInfo>().FirstOrDefault(d => d.NodeID == nodeInfo.ID
                && d.State == "New" && d.UserID == user.UserID && d.Type == type && d.DataType == nodeInfo.ConfigInfo.Name);
            if (carInfo == null)
            {
                carInfo = new S_CarInfo
                {
                    ID = FormulaHelper.CreateGuid(),
                    UserID = user.UserID,
                    UserName = user.UserName,
                    Name = nodeInfo.CreateCarName(),
                    Code = nodeInfo.DataEntity.GetValue("DocumentCode"),
                    DataType = nodeInfo.ConfigInfo.Name,
                    Type = type,
                    NodeID = nodeInfo.ID,
                    NodeName = nodeInfo.Name,
                    State = ItemState.New.ToString(),
                    CreateDate = DateTime.Now,
                    CreateUser = user.UserName,
                    CreateUserID = user.UserID,
                    ConfigID = nodeInfo.ConfigInfo.ID,
                    SpaceID = nodeInfo.Space.ID
                };
                entities.Set<S_CarInfo>().Add(carInfo);
            }
        }
    }
}
