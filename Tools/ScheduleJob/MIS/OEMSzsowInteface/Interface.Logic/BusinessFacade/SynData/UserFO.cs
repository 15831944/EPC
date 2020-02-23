using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class UserFO : SynDataFO
    {
        public void CreateUserQueue()
        {
            //URL地址
            var saveUrl = this.BaseServerUrl + "user/edit";
            var removeUrl = this.BaseServerUrl + "user/delete";

            var sourceSql = "select * from S_A_User where IsDeleted='0' ";
            var synUserSysRole = ConfigurationManager.AppSettings["SynUserSysRole"] != null ? ConfigurationManager.AppSettings["SynUserSysRole"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synUserSysRole))
                sourceSql += string.Format(@" and ID in (
select UserID from S_A__RoleUser
where RoleID in (	select ID from S_A_Role where Code in ('{0}') )  
union 
select UserID from S_A__OrgRoleUser
where RoleID in (	select ID from S_A_Role where Code in ('{0}') ) 
union
select UserID from S_A__OrgUser 
inner join S_A__OrgRole on S_A__OrgUser.OrgID = S_A__OrgRole.OrgID
where RoleID in (	select ID from S_A_Role where Code in ('{0}') ) 
)", synUserSysRole.Replace(",", "','"));

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_A_User>("select * from S_A_User");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and ModifyTime >='" + startDate + "' and ModifyTime <='" + endDate + "'";

            }

            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeBase.ExecuteDataTable(sourceSql);
            var signDt = this.SQLHelpeBase.ExecuteDataTable("select DwgFile,UserID from S_A_UserImg");
            var removeSb = new StringBuilder();

            foreach (DataRow row in sourceDt.Rows)
            {
                var userDic = DataHelper.DataRowToDic(row);

                //设置参数
                var param = new UserRequestData();
                param.id = userDic.GetValue("ID");
                var synUser = synData.FirstOrDefault(a => a.ID == param.id);
                if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                    param.id = synUser.SynID;
                param.name = userDic.GetValue("Name");
                param.loginid = userDic.GetValue("Code");
                param.signature = null;
                param.enable = userDic.GetValue("IsDelete") == "1" ? false : true;
                //签名
                var signRow = signDt.Select("UserID='" + param.id + "'");
                if (signRow.Length > 0)
                {
                    var obj = signRow[0]["DwgFile"];
                    if (obj != null && obj != DBNull.Value)
                    {
                        byte[] bytes = (byte[])obj;
                        param.signature = Convert.ToBase64String(bytes);
                    }
                }
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_A_User", param.id, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<UserRequestData>(param).Replace("'", "''"), saveUrl);
                saveSb.AppendLine(queueSql);

            }

            var idSql = "select ID from S_A_User where IsDeleted='0' ";
            if (!string.IsNullOrEmpty(synUserSysRole))
                idSql += string.Format(@" and ID in (
select UserID from S_A__RoleUser
where RoleID in (	select ID from S_A_Role where Code in ('{0}') )  
union 
select UserID from S_A__OrgRoleUser
where RoleID in (	select ID from S_A_Role where Code in ('{0}') ) 
union
select UserID from S_A__OrgUser 
inner join S_A__OrgRole on S_A__OrgUser.OrgID = S_A__OrgRole.OrgID
where RoleID in (	select ID from S_A_Role where Code in ('{0}') ) 
)", synUserSysRole.Replace(",", "','"));
            var alldt = this.SQLHelpeBase.ExecuteDataTable(idSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var delID = item.ID;
                var synUser = synData.FirstOrDefault(a => a.ID == delID);
                if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                    delID = synUser.SynID;
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_A_User", delID, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = delID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty;
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synID = responseData.GetValue("ID");
                var synData = JsonHelper.ToObject<UserRequestData>(queueData.RequestData);
                if (synID == synData.id) synID = string.Empty;
                sql = @"if exists(select 1 from S_A_User where ID='{0}' or SynID='{0}')
                                update S_A_User set Code = '{1}', Name='{2}', SynTime='{5}',SynData='{6}' where ID='{0}' or SynID='{0}'
                            else
	                            insert into S_A_User(ID,Code,Name,State,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                sql = string.Format(sql, queueData.RelateID, synData.loginid, synData.name.Replace("'", "''"), DataState.Normal.ToString(),
                    synID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_A_User set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
