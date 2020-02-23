using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InitOEMData.Helper;
using System.Data;
using Config;
using Config.Logic;
using System.Web.Security;

namespace InitOEMData
{
    public class SzsowData
    {
        MySQLHelper cpHelper = MySQLHelper.CreateSqlHelper("Szsow");
        SQLHelper baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
        SQLHelper hrHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
        SQLHelper projectHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);

        public void InitUser()
        {
            //人员：sys_user 
            //人员的签名文件：sys_userdictionaryfile（DictionaryType='7'）
            var saID = System.Configuration.ConfigurationManager.AppSettings["OEMSaUserID"];
            var _userDt = cpHelper.ExecuteDataTable("select * from sys_user where IsDeleted = 0  and Status = 0");
            var _signDt = cpHelper.ExecuteDataTable("select * from sys_userdictionaryfile where IsDeleted = 0 and DictionaryType='7'");
            //gw
            StringBuilder sb = new StringBuilder();
            StringBuilder hrsb = new StringBuilder();
            var rootID = System.Configuration.ConfigurationManager.AppSettings["OrgRootID"];
            var rootName = System.Configuration.ConfigurationManager.AppSettings["OrgRootName"];
            var userDt = baseHelper.ExecuteDataTable(@"select * from S_A_User");
            var userImgDt = baseHelper.ExecuteDataTable(@"select * from S_A_UserImg");
            var orgUserDt = baseHelper.ExecuteDataTable(@"select * from S_A__OrgUser");
            var hrDt = hrHelper.ExecuteDataTable(@"select * from T_Employee");
            foreach (DataRow row in _userDt.Rows)
            {
                var userID = row["ID"].ToString();
                if (userID == saID) continue;
                var name = row["Name"].ToString();
                var code = row["LoginID"].ToString();
                var pwd = row["Password"].ToString();
                byte[] signBt = null;
                var signRow = _signDt.Select(" UserID='" + userID + "'").FirstOrDefault();
                if (signRow != null)
                {
                    var signContent = signRow["Content"].ToString();
                    signBt = Convert.FromBase64String(signContent);
                }

                var userRow = userDt.Select(" ID ='" + userID + "'").FirstOrDefault();
                if (userRow == null)
                {
                    userRow = userDt.NewRow();
                    userRow["ID"] = userID;
                    userDt.Rows.Add(userRow);
                }
                userRow["Code"] = code;
                userRow["WorkNo"] = code;
                userRow["Name"] = name;
                userRow["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}{1}", code.ToLower(), pwd), "SHA1");
                userRow["Sortindex"] = 0;
                userRow["ErrorCount"] = 0;
                userRow["IsDeleted"] = "0";
                userRow["DeptID"] =rootID;
                userRow["DeptFullID"] = rootID;
                userRow["DeptName"] = rootName;
                userRow["ModifyTime"] = DateTime.Now;
                sb.AppendLine(SQLHelper.CreateUpdateSql("S_A_User", userRow));
                var orgUserRow = orgUserDt.Select("OrgID='" + rootID + "' and UserID='" + userID + "'").FirstOrDefault();
                if (orgUserRow == null)
                {
                    orgUserRow = orgUserDt.NewRow();
                    orgUserRow["OrgID"] = rootID;
                    orgUserRow["UserID"] = userID;
                    orgUserDt.Rows.Add(orgUserRow);
                    sb.AppendLine(SQLHelper.CreateInsertSql("S_A__OrgUser", orgUserRow));
                }
                var hrRow = hrDt.Select("UserID='" + userID + "'").FirstOrDefault();
                if (hrRow == null)
                {
                    hrRow = hrDt.NewRow();
                    hrRow["ID"] = GuidHelper.CreateGuid();
                    hrRow["UserID"] = userID;
                    hrDt.Rows.Add(hrRow);
                }
                hrRow["Code"] = code;
                hrRow["Name"] = name;
                hrRow["EmploymentWay"] = "正式员工";
                hrRow["EmployeeState"] = "Incumbency";
                hrRow["IsDeleted"] = "0";
                hrRow["IsHaveAccount"] = "1";
                hrRow["CreateDate"] = DateTime.Now;
                hrRow["ModifyDate"] = hrRow["CreateDate"];
                if (signBt != null)
                    hrRow["SignDwg"] = signBt;
                hrsb.AppendLine(SQLHelper.CreateUpdateSql("T_Employee", hrRow));

                if (signBt != null)
                {
                    var imgRow = userImgDt.Select(" UserID ='" + userID + "'").FirstOrDefault();
                    if (imgRow == null)
                    {
                        imgRow = userImgDt.NewRow();
                        imgRow["ID"] = GuidHelper.CreateGuid();
                        imgRow["UserID"] = userID;
                        userImgDt.Rows.Add(imgRow);
                    }
                    sb.AppendLine(SQLHelper.CreateUpdateSql("S_A_UserImg", imgRow));
                }
            }
            if (sb.Length > 0)
                baseHelper.ExecuteNonQuery(sb.ToString());
            if (hrsb.Length > 0)
                hrHelper.ExecuteNonQuery(hrsb.ToString());
        }

        public List<string> getDifIDUser()
        {
            var rtn = new List<string>();
            var saID = System.Configuration.ConfigurationManager.AppSettings["OEMSaUserID"];
            var _userDt = cpHelper.ExecuteDataTable("select * from sys_user where IsDeleted = 0  and Status = 0");

            var userDt = baseHelper.ExecuteDataTable(@"select * from S_A_User where IsDeleted='0'");
            foreach (DataRow row in userDt.Rows)
            {
                var userID = row["ID"].ToString();
                var userCode = row["Code"].ToString();
                var _exist = _userDt.Select(" LoginID ='" + userCode + "' and ID<>'" + userID + "'").FirstOrDefault();
                if (_exist != null)
                    rtn.Add(string.Format("ID：{0}，LoginID：{1}", userID, userCode));
            }
            return rtn;
        }

        public void InitProject()
        {
            //项目的ID：pro_project
            //项目管理员：pro_projectroleuser（RoleID='{sys_role.Code=ProjectOwner}'）
            //项目阶段：pro_projectphase
            //子项的ID：pro_subproject
            //项目专业、项目人员角色：pro_projectphaserole}
            /*
             00000000-0000-0000-0004-000000000001	ProjectManager	项目经理
            00000000-0000-0000-0004-000000000002	SpecialtyHead	专业负责
            00000000-0000-0000-0004-000000000003	SpecialtyApproval	专业审定
            00000000-0000-0000-0004-000000000004	SpecialtyAudit	专业审核
            00000000-0000-0000-0004-000000000005	SpecialtyProof	专业校对
            00000000-0000-0000-0004-000000000006	SpecialtyDesign	专业设计
            00000000-0000-0000-0004-000000000007	Drafting	制图
             */
            var _userDt = cpHelper.ExecuteDataTable("select * from sys_user where IsDeleted = 0  and Status = 0");
            var _prjDt = cpHelper.ExecuteDataTable("select * from pro_project where IsDeleted = 0  and Status = 0 and IsMain=0");
            var _prjRoleDt = cpHelper.ExecuteDataTable("select * from pro_projectroleuser where IsDeleted = 0 and RoleID = (select ID from sys_role where Code='ProjectOwner')");
            var _prjPhaseDt = cpHelper.ExecuteDataTable("select * from pro_projectphase where IsDeleted = 0 ");
            var _prjSubDt = cpHelper.ExecuteDataTable("select * from pro_subproject where IsDeleted = 0 ");
            var _prjMajorRoleDt = cpHelper.ExecuteDataTable(@"select pro_projectphaserole.*,sys_specialty.Name SpecialtyName 
from pro_projectphaserole left join sys_specialty on sys_specialty.Id = pro_projectphaserole.SpecialtyId where pro_projectphaserole.IsDeleted = 0 ");
            //gw
            StringBuilder sb = new StringBuilder();
            var projectDt = projectHelper.ExecuteDataTable("select * from S_I_ProjectInfo");
            var wbsDt = projectHelper.ExecuteDataTable("select * from S_W_WBS");
            var roleDt = projectHelper.ExecuteDataTable("select * from S_W_RBS");
            foreach (DataRow row in _prjDt.Rows)
            {
                var projectID = row["ID"].ToString();
                var projectName = row["Name"].ToString();
                var projectCode = row["Code"].ToString();

                var prjRoles = _prjRoleDt.Select("ProjectId='" + projectID + "'");
                foreach (DataRow prjRow in prjRoles)
                {
                    var userID = prjRow["UserID"].ToString();
                }

                var phaseRows = _prjPhaseDt.Select("ProjectID='" + projectID + "'");
                foreach (DataRow phaseRow in phaseRows)
                {
                    var phaseName = phaseRow["Name"].ToString();
                    var phaseID = phaseRow["ID"].ToString();

                    var subPrjRows = _prjSubDt.Select(" ProjectPhaseID = '" + phaseID + "'");
                    if (subPrjRows.Length > 0)
                    {
                        //阶段-子项-专业
                        foreach (var subPrjRow in subPrjRows)
                        {
                            var subPrjID = subPrjRow["ID"].ToString();
                            var subPrjName = subPrjRow["Name"].ToString();

                            var majorRoles = _prjMajorRoleDt.Select(" ProjectPhaseID = '" + phaseID + "' and  SubProjectId = '" + subPrjID + "'");
                            foreach (var majorRole in majorRoles)
                            {
                                var majorName = majorRole["SpecialtyName"].ToString();
                                var userID = majorRole["UserID"].ToString();
                                var roleID = majorRole["RoleID"].ToString();
                            }
                        }
                    }
                    else
                    {
                        //阶段-专业
                    }

                }
            }
        }

    }
}
