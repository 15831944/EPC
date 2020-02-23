using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;

using RTXSAPILib;
using System.Xml;
using System.Data;
using Config;


namespace RTX_SynchDataTool
{
    class SynchDeptAndUserService
    {
        #region 公有变量

        RTXSAPIRootObj RootObj = new RTXSAPIRootObj();
        RTXSAPIDeptManager deptManager = null;
        RTXSAPIUserManager userManager = null;
        List<string> absoluteFullDpetNameList = new List<string>();
        public static SQLHelper baseSqlHelper;
        DataTable orgDt = baseSqlHelper.ExecuteDataTable(" select ID,Name ,Code,FullID,ParentID from S_A_Org ");

        #endregion

        #region 同步组织架构信息

        //同步组织架构的主方法
        public void StartSynchDeptInfo()
        {
            string logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";

            //获取组织架构根节点
            var rootOrg_BE_Temp = orgDt.Select("ParentID is null or ParentID = ''");
            if (rootOrg_BE_Temp == null || rootOrg_BE_Temp.Count() == 0)
            {

                logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "获取组织架构根节点信息失败。"
                                  , "失败时间：" + DateTime.Now, "获取组织架构根节点信息失败：", "", DateTime.Now, "", "");
                baseSqlHelper.ExecuteNonQuery(logSql);
            }
            deptManager = RootObj.DeptManager;
            if (deptManager == null)
            {
                //记录异常信息
                logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "获取RTX组织管理对象失败。"
                                  , "失败时间：" + DateTime.Now, "获取RTX组织管理对象失败：", "", DateTime.Now, "", "");
                baseSqlHelper.ExecuteNonQuery(logSql);
            }

            DataRow rootOrg = rootOrg_BE_Temp[0];
            DataRow parentNode_Root = null;
            var rootOrgID = rootOrg["ID"].ToString();
            DataRow[] children = orgDt.Select(" ParentID='" + rootOrgID + "'");
            var rootOrgName = rootOrg["Name"] == null ? "" : rootOrg["Name"].ToString();
            AddDeptToRTX(rootOrg, parentNode_Root, children);

            //删除RTX中存在，金慧BE平台不存在的部门
            string childOrgInfo = deptManager.GetChildDepts(rootOrgName);
            DeleteExistedRTXAndNotExistedBE(rootOrgName, rootOrgName, childOrgInfo);

        }

        public string GetRTXDeptName(string rtx_deptInfo)
        {
            string deptName = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(BeforeLoadXML(rtx_deptInfo));
            XmlElement xmlEle = xmlDoc.DocumentElement;
            XmlNodeList nodelist = xmlEle.SelectNodes("//Department");
            if (nodelist != null && nodelist.Count > 0)
            {
                XmlNode node = nodelist[0];
                deptName = ((XmlElement)node).GetAttribute("Name");
            }
            return deptName;
        }

        //BE同步至RTX
        public void AddDeptToRTX(DataRow currentOrg, DataRow parentOrg, DataRow[] children)
        {
            string logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";

            if (currentOrg == null) return;

            absoluteFullDpetNameList.Clear();
            GetAbsoluteFullDpetName(currentOrg);
            var absoluteFullDpetName = string.Join("\\", absoluteFullDpetNameList);
            var currentOrgName = currentOrg["Name"] == null ? "" : currentOrg["Name"].ToString();
            if (!deptManager.IsDeptExist(absoluteFullDpetName + "\\" + currentOrgName))
            {
                if (parentOrg == null)
                {
                    try
                    {
                        deptManager.AddDept(currentOrgName, "");

                    }
                    catch (Exception exp)
                    {
                        logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                        logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "同步" + currentOrgName + "部门信息时失败。"
                                  , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                        SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);
                        return;
                    }

                }
                else
                {
                    try
                    {
                        deptManager.AddDept(currentOrgName, absoluteFullDpetName);

                    }
                    catch (Exception exp)
                    {
                        logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                        logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "同步" + currentOrgName + "部门信息时失败。"
                                 , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                        SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);
                        return;
                    }

                }
            }
            if (children != null && children.Count() > 0)
                foreach (DataRow child in children)
                {
                    var Id = child["ID"].ToString();
                    var grandsen = orgDt.Select(" ParentID='" + Id + "'");
                    AddDeptToRTX(child, currentOrg, grandsen);
                }

        }

        //删除BE中没有而RTX中存在的组织信息
        public void DeleteExistedRTXAndNotExistedBE(string deptName, string parentDeptNameFullPath, string childDeptInfo)
        {
            string logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            var orgInfo_BE_Temp = orgDt.Select(" Name='" + deptName + "' ");
            DataRow orgInfo_BE = null;
            if (orgInfo_BE_Temp != null && orgInfo_BE_Temp.Count() > 0)
                orgInfo_BE = orgInfo_BE_Temp[0];
            if (orgInfo_BE == null)
            {
                try
                {
                    deptManager.DelDept(deptName, true);
                }
                catch (Exception exp)
                {
                    logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                    logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "删除" + deptName + "部门失败"
                                 , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                    SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);

                    return;
                }

            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(BeforeLoadXML(childDeptInfo));
            XmlElement xmlEle = xmlDoc.DocumentElement;
            XmlNodeList nodelist = xmlEle.SelectNodes("//Department");
            if (nodelist == null && nodelist.Count == 0) return;
            if (nodelist != null && nodelist.Count > 0)
            {
                for (int i = 0; i < nodelist.Count; i++)
                {
                    var node = nodelist[i];
                    string childDeptName = ((XmlElement)node).GetAttribute("Name");
                    var tempdeptNameDeptNameFullPath = parentDeptNameFullPath + "\\" + childDeptName;
                    var grandsenDeptInfo = deptManager.GetChildDepts(tempdeptNameDeptNameFullPath);
                    DeleteExistedRTXAndNotExistedBE(childDeptName, tempdeptNameDeptNameFullPath, grandsenDeptInfo);
                }
            }
        }

        #endregion

        #region 同步用户信息

        public void StartSynchUserInfo()
        {
            string logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";

            userManager = RootObj.UserManager;
            if (userManager == null) return;
            deptManager = RootObj.DeptManager;
            if (deptManager == null) return;
            var userDt = baseSqlHelper.ExecuteDataTable(" select * from S_A_User ");
            //  var userList = baseEntites.S_A_User.ToList();
            //获取人员和部门的关系信息
            string sql = @" select UserID,OrgID,Name as DeptName,ParentID from S_A__OrgUser
left join S_A_Org on S_A__OrgUser.OrgID = S_A_Org.ID ";
            DataTable userOrgInfoDt = baseSqlHelper.ExecuteDataTable(sql);
            var userOrgInfoRows = userOrgInfoDt.Rows;
            var userRows = userDt.Rows;
            foreach (DataRow userItem in userRows)
            {
                var systemName = userItem["Code"] == null ? "" : userItem["Code"].ToString();
                systemName = systemName.TrimStart(' ').TrimEnd(' ');
                var isDeleted = userItem["IsDeleted"] == null ? "" : userItem["IsDeleted"].ToString();
                var deptName = userItem["DeptName"] == null ? "" : userItem["DeptName"].ToString();
                var orgID = userItem["DeptID"] == null ? "" : userItem["DeptID"].ToString();
                DataRow orgInfo = null;
                var orgInfo_Temp = orgDt.Select(" ID='" + orgID + "'");
                if (orgInfo_Temp != null && orgInfo_Temp.Count() > 0)
                    orgInfo = orgInfo_Temp[0];
                absoluteFullDpetNameList.Clear();
                if (orgInfo != null)
                {
                    GetAbsoluteFullDpetName(orgInfo);
                    var absoluteFullDpetName = string.Join("\\", absoluteFullDpetNameList);
                    deptName = absoluteFullDpetName + "\\" + deptName;
                }
                if (isDeleted == "1")
                {
                    //删除用户信息
                    if (userManager.IsUserExist(systemName))
                    {
                        try
                        {
                            userManager.DeleteUser(systemName);
                        }
                        catch (Exception exp)
                        {
                            logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                            logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "删除用户" + systemName + "失败"
                                 , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                            SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);

                            continue;
                        }

                    }
                }
                else
                {
                    if (!userManager.IsUserExist(systemName))
                    {
                        try
                        {
                            userManager.AddUser(systemName, 0);
                        }
                        catch (Exception exp)
                        {
                            logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                            logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "添加用户" + systemName + "失败"
                               , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                            SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);

                            continue;
                        }
                        if (!string.IsNullOrEmpty(deptName) && deptManager.IsDeptExist(deptName))
                        {
                            try
                            {
                                deptManager.AddUserToDept(systemName, "", deptName, false);
                            }
                            catch (Exception exp)
                            {
                                logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";

                                logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "添加用户部门之间的关联关系失败，用户(" + systemName + "),部门(" + deptName + ")"
                                   , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                                SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);

                                continue;
                            }
                        }
                    }
                    else
                    {
                        //设置用户部门信息
                        string UserDeptInfo = deptManager.GetUserDepts(systemName);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(BeforeLoadXML(UserDeptInfo));
                        XmlElement xmlEle = xmlDoc.DocumentElement;
                        XmlNodeList nodelist = xmlEle.SelectNodes("//Department");
                        if (nodelist.Count > 0)
                        {
                            //删除RTX中人员的部门信息
                            foreach (XmlNode node in nodelist)
                            {
                                string name = ((XmlElement)node).GetAttribute("Name");
                                try
                                {
                                    deptManager.DelUserFromDept(systemName, name);
                                }
                                catch (Exception exp)
                                {
                                    logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                                    logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "删除用户部门之间的关联关系失败，用户(" + systemName + "),部门(" + name + ")"
                                     , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                                    SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);

                                    continue;
                                }
                            }
                        }
                        var userId = userItem["ID"];
                        var userOrgInfoRowsOfSingleUser = userOrgInfoDt.Select(" UserID='" + userId + "'");
                        foreach (DataRow row in userOrgInfoRowsOfSingleUser)
                        {
                            deptName = row["DeptName"] == null ? "" : row["DeptName"].ToString();
                            absoluteFullDpetNameList.Clear();
                            if (orgInfo != null)
                            {
                                GetAbsoluteFullDpetName(row);
                                var absoluteFullDpetName = string.Join("\\", absoluteFullDpetNameList);
                                if (!string.IsNullOrEmpty(absoluteFullDpetName))
                                    deptName = absoluteFullDpetName + "\\" + deptName;

                            }
                            if (!string.IsNullOrEmpty(deptName) && deptManager.IsDeptExist(deptName))
                            {
                                try
                                {
                                    deptManager.AddUserToDept(systemName, "", deptName, false);
                                }
                                catch (Exception exp)
                                {
                                    logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                                    logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "添加用户部门之间的关联关系失败，用户(" + systemName + "),部门(" + deptName + ")"
                                     , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                                    SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);
                                    continue;
                                }
                            }
                        }
                    }
                    //设置用户的基本信息(用户姓名，用户性别，用户手机，用户电子邮件，用户电话，用户认证类型)
                    string Name = userItem["Name"] == null ? "" : userItem["Name"].ToString();
                    string Sex = userItem["Sex"] == null ? "" : userItem["Sex"].ToString();
                    string MobilePhone = userItem["MobilePhone"] == null ? "" : userItem["MobilePhone"].ToString();
                    string Email = userItem["Email"] == null ? "" : userItem["Email"].ToString();
                    string Phone = userItem["Phone"] == null ? "" : userItem["Phone"].ToString();
                    try
                    {
                        userManager.SetUserBasicInfo(systemName, Name, GetSex(Sex), MobilePhone, Email, Phone, 0);
                    }
                    catch (Exception exp)
                    {
                        logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                        logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "设置用户基本信息失败，用户(" + systemName + ")"
                                    , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                        SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);
                        continue;
                    }
                    //设置用户的详细信息(用户生日,用户血型,用户星座,用户毕业院校,用户主页,用户传真,用户地址,用户邮编,用户所在国家, 用户所在省份, 用户所在城市,用户所在街道,用户职务,用户权值 (用于排序) )
                    string strSortIndex = userItem["SortIndex"] == null || userItem["SortIndex"].ToString() == "" ? "0" : userItem["SortIndex"].ToString();
                    int sortIndex = Convert.ToInt32(strSortIndex);
                    try
                    {
                        userManager.SetUserDetailInfo(systemName, -1, -1, -1, "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", "RTX_NULL", sortIndex);
                    }
                    catch (Exception exp)
                    {
                        logSql = @" insert into S_D_ModifyLog (ID,ConnName,TableName,ModifyMode,EntityKey,CurrentValue,OriginalValue
                               ,ModifyUserID,ModifyUserName,ModifyTime,ClientIP,UserHostAddress) 
                               Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
                        logSql = string.Format(logSql, FormulaHelper.CreateGuid(), "Base", "", "Added", "设置用户详细信息失败，用户(" + systemName + ")"
                                    , "失败时间：" + DateTime.Now, "失败详细信息：" + exp.Message, "", "", DateTime.Now, "", "");
                        SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteNonQuery(logSql);
                        continue;
                    }

                }

            }


        }

        #endregion

        #region 内部方法

        internal void GetAbsoluteFullDpetName(DataRow currentOrg)
        {
            var parentID = currentOrg["ParentID"].ToString();
            var parentNodes = orgDt.Select(" ID='" + parentID + "'");
            if (parentNodes == null || parentNodes.Count() == 0)
            {
                absoluteFullDpetNameList.Reverse();
                return;
            }
            currentOrg = parentNodes[0];
            var orgName = currentOrg["Name"] == null ? "" : currentOrg["Name"].ToString();
            if (currentOrg != null && !string.IsNullOrEmpty(orgName))
            {
                absoluteFullDpetNameList.Add(orgName);
                GetAbsoluteFullDpetName(currentOrg);
            }
        }

        internal int GetSex(string sexValue)
        {
            if (sexValue == "Male" || sexValue == "M")
                return 0;
            else
                return 1;

        }

        public string BeforeLoadXML(string content)
        {
            var rtn = content.Replace("&", "&amp;");
            return rtn;
        }

        #endregion


    }
}
