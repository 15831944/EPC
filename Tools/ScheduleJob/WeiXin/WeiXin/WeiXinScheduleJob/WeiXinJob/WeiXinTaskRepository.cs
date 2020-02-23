using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WeiXinScheduleJob;
using WeiXinScheduleJob.DTO;
using System.IO;
using System.Text.RegularExpressions;

namespace WeiXinScheduleJob
{
    public class WeiXinTaskRepository
    {
        /// <summary>
        /// 同步组织
        /// </summary>
        public void SyncOrg(DataTable table)
        {
            DataTable dtNew = table.Copy();
            var orgList = WeiXinService.GetWeiXinOrg(); //获取企业微信所有组织列表
            var withoutdept = AppSettingService.WithOutOrgField;
          
            if (dtNew.Rows.Count > 0)
            {
                List<OrgDTO> misOrgList = new List<OrgDTO>();
                int curID = orgList.Count > 0 ? orgList.OrderByDescending(c => c.id).First().id : 1; //微信最大部门ID
                foreach (DataRow dr in dtNew.Rows) //处理部门上下级为整数
                {
                    string id = dr["ID"].ToString();
                    if (string.IsNullOrEmpty(dr["ParentID"].ToString()))
                    {
                        dr["ID"] = 1;
                    }
                    else
                    {
                        dr["ID"] = curID;
                    }
                    foreach (DataRow cdr in dtNew.Select(string.Format("ParentID='{0}'", id)))
                    {
                        cdr["ParentID"] = dr["ID"];
                    }
                    curID++;
                    misOrgList.Add(new OrgDTO
                    {
                        id = Convert.ToInt32(dr["ID"]),
                        misid= id,
                        name = dr[AppSettingService.OrgNameField].ToString(),
                        parentid = string.IsNullOrEmpty(dr["ParentID"].ToString()) ? 0 : Convert.ToInt32(dr["ParentID"]),
                        order = string.IsNullOrEmpty(dr["SortIndex"].ToString()) ? 0 : Convert.ToInt32(dr["SortIndex"]),
                        enable = Convert.ToInt32(dr["IsDeleted"]) > 0 ? 1 : 0
                    });
                }

                bool isMisSyscOrg = Convert.ToBoolean(AppSettingService.IsMisSyscOrg); //强制按系统同步组织架构
                foreach (var mis in misOrgList)
                {                    
                    if (mis.parentid > 0)
                    {
                        var misParentDept = misOrgList.FirstOrDefault(c => c.id == mis.parentid); //获取mis的上级部门
                        OrgDTO wxOrg = null; //企业微信中对应的部门
                        if (orgList.Count > 0)
                        {
                            var wx = orgList.FirstOrDefault(c => c.name.ToLower() == mis.name);
                            wxOrg = wx != null ? wx : null;
                        }
                        if (wxOrg != null) //存在
                        {
                            //项目上需要排除某个部门，排除规则为部门和部门下人员都排除，已存在的删除
                            if (!string.IsNullOrEmpty(withoutdept) && withoutdept.IndexOf(mis.misid) >= 0)
                            {
                                //删除时若部门里之前时有人员的，要先清空人员才能删除部门
                                WeiXinService.DeleteDepartment(wxOrg);
                                continue;
                            }
                            if (isMisSyscOrg)
                            {                            
                                //如果MIS系统与企业微信上下级不一样则是不同部门,创建
                                var pwx = orgList.FirstOrDefault(c => c.id == wxOrg.parentid);
                                var pmis = misOrgList.FirstOrDefault(c => c.id == mis.parentid);
                                if (pwx != null && pmis != null && pwx.name != pmis.name && pmis.id != 0)
                                {
                                    var parentOrg = orgList.FirstOrDefault(c => c.name.ToUpper() == misParentDept.name.ToUpper());
                                    var pid = 1;
                                    if (parentOrg!= null)
                                    {
                                        pid = parentOrg.id;
                                    }
                                    if (parentOrg != null || pid==1)
                                    {
                                        WeiXinService.CreateDepartment(new OrgDTO
                                        {
                                            id = mis.id,
                                            name = mis.name,
                                            order = mis.order,
                                            parentid = pid //对应获取企业微信的上级
                                        });
                                    }
                                }
                                if (mis.enable > 0 && wxOrg != null)
                                    WeiXinService.DeleteDepartment(wxOrg);
                            }
                        }
                        else
                        {
                            //项目上需要排除某个部门，排除规则为部门和部门下人员都排除，已存在的删除
                            if (!string.IsNullOrEmpty(withoutdept) && withoutdept.IndexOf(mis.misid) >= 0)
                            {
                                continue;
                            }
                            if (mis.enable < 1)
                            {
                                //如果上级部门在企业微信中已存在，则取企业微信的上级
                                var wxParentDept = orgList.FirstOrDefault(c => c.name == misParentDept.name);
                                if (wxParentDept != null)
                                    mis.parentid = wxParentDept.id;
                                WeiXinService.CreateDepartment(mis);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 获取微信成员
        /// </summary>
        public List<UserDTO> GetWXUser()
        {
            var wxUserJson = WeiXinService.GetUserList();
            return JsonHelper.ToObject<List<UserDTO>>(wxUserJson.GetValue(WeiXinConstant.USERLIST).ToString());
        }

        /// <summary>
        /// 同步人员
        /// </summary>
        public string SyscUser(DataTable departments, DataTable table, List<UserDTO> wxUsers)
        {
            StringBuilder sb = new StringBuilder();
            var updateSql = "update S_A_User set {0} where ID = '{1}'";
            var orgList = WeiXinService.GetWeiXinOrg(); //获取企业微信所有组织列表
            if (table.Rows.Count > 0)
            {
                List<UserDTO> misUserList = new List<UserDTO>();
                string mailboxSuffix = AppSettingService.MailboxSuffix;
                var withoutdept = AppSettingService.WithOutOrgField;
                foreach (DataRow dr in table.Rows)
                {
                    string deptID = Convert.ToString(dr["DeptID"]);
                    string code = Convert.ToString(dr["Code"]);
                    
                    int[] deptIDs = GetLevelDept(orgList, departments, deptID); //当前用户所在部门
                   
                    //添加到列表中
                    misUserList.Add(new UserDTO
                    {
                        userid = code,
                        deptid = deptID,
                        id = Convert.ToString(dr["ID"]),
                        workNo = Convert.ToString(dr["WorkNo"]),
                        Weixin = Convert.ToString(dr["Weixin"]),
                        position = Convert.ToString(dr["Position"]),
                        telephone = Convert.ToString(dr["Phone"]),
                        name = Convert.ToString(dr["Name"]),
                        english_name = Convert.ToString(dr["NameEN"]),
                        mobile = Convert.ToString(dr["MobilePhone"]),
                        department = deptIDs,
                        gender = Convert.ToString(dr["Sex"]) == "FeMale" ? 2 : 1,
                        email = string.IsNullOrEmpty(dr["Email"].ToString()) ? string.IsNullOrEmpty(mailboxSuffix) ? "" : code + mailboxSuffix : dr["Email"].ToString(),
                        isDelete = Convert.ToInt32(dr["IsDeleted"])
                    });
                }

                //删除的人员
                var bdus = new Dictionary<string, object>();
                List<string> users = new List<string>();

                var isUpdateExistUser = Convert.ToBoolean(AppSettingService.IsUpdateExistUser);
                //如果userid在企业微信中不存在则创建，否则更新
                var wxCount = wxUsers.Count;
                foreach (var misUser in misUserList)
                {
                    
                    if (!string.IsNullOrEmpty(misUser.userid) && !string.IsNullOrEmpty(misUser.name))
                    {
                        var wxUser = wxUsers.FirstOrDefault(c => c.userid == misUser.userid || c.userid == misUser.workNo
                            || c.userid == misUser.Weixin || (c.mobile == misUser.mobile && !string.IsNullOrEmpty(c.mobile)));
                        if (wxUser != null)
                        {
                            //项目上需要排除某个部门，排除规则为部门和部门下人员都排除，已存在的删除
                            if (!string.IsNullOrEmpty(withoutdept) && withoutdept.IndexOf(misUser.deptid) >= 0 && !string.IsNullOrEmpty(misUser.deptid))
                            {
                                users.Add(wxUser.userid);
                                continue;
                            }
                            misUser.userid = wxUser.userid;

                            if (misUser.isDelete > 0)
                                users.Add(wxUser.userid);
                            else
                            {
                                if (isUpdateExistUser)
                                {
                                    misUser.enable = 1;
                                    WeiXinService.UpdateUser(misUser);
                                }                               
                                //根据企业微信同步user表的mobile、weixin字段
                                var fieldStr = string.Empty;
                                if (!string.IsNullOrEmpty(wxUser.mobile) && wxUser.mobile != misUser.mobile)
                                    fieldStr += "MobilePhone = '" + wxUser.mobile + "',";
                                if (wxUser.userid != misUser.Weixin)
                                    fieldStr += "Weixin = '" + wxUser.userid + "',";
                                if (!string.IsNullOrEmpty(fieldStr))
                                    sb.AppendFormat(updateSql, fieldStr.TrimEnd(','), misUser.id);
                            }
                        }
                        else
                        {
                            //项目上需要排除某个部门，排除规则为部门和部门下人员都排除，已存在的删除
                            if (!string.IsNullOrEmpty(withoutdept)&&withoutdept.IndexOf(misUser.deptid) >= 0&&!string.IsNullOrEmpty(misUser.deptid))
                            {
                                continue;
                            }
                            if (misUser.isDelete < 1 && wxCount < Convert.ToInt32(AppSettingService.WeiXinUserCount)) //企业微信人员总数
                            {
                                misUser.enable = 1;
                                var result = WeiXinService.CreateUser(misUser);
                                if (result != null)
                                    wxCount++;
                                sb.AppendFormat(updateSql, "Weixin = '" + misUser.userid + "'", misUser.id);
                            }
                        }
                    }
                }
                bdus.SetValue("useridlist", users);
                //删除的人员
                if (users.Count > 0)
                {
                    WeiXinService.BatchDeleteUser(bdus);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        public DataTable GetDepartment()
        {
            var sql = "select ID,ParentID,Name,ShortName,SortIndex,IsDeleted from S_A_Org";
            var db = SqlHelper.Create("Base");
            return db.ExecuteDataTable(sql);
        }


        /// <summary>
        /// 获取用户
        /// </summary>
        public DataTable GetUser()
        {
            var sql = @"select ID,Code,Name,NameEN,WorkNo,Phone,MobilePhone,Email,Sex,DeptID,DeptName,IsDeleted,Position,Weixin from S_A_User";
            var db = SqlHelper.Create("Base");
            return db.ExecuteDataTable(sql);
        }
        /// <summary>
        /// MIS与企业微信的上级部门是否相同，相同则同步
        /// </summary>
        private int[] GetLevelDept(List<OrgDTO> orgList, DataTable table, string deptID)
        {
            int[] deptIDs = { 1 };
            try
            {
                var misCurDept = table.Select(string.Format("ID='{0}'", deptID)).First(); //当前MIS的部门
                if (misCurDept != null)
                {
                    string shortName = misCurDept["ShortName"].ToString().ToLower();
                    //强制小写去掉，带字母的部门企业人员同步不过去所属部门
                    string deptName = string.IsNullOrEmpty(shortName) ? misCurDept["Name"].ToString() : shortName; //当前MIS的部门名称
                    var wxCurDepts = orgList.Where(c => c.name == deptName).ToList(); //企业微信的当前部门
                    foreach (var wxCurDept in wxCurDepts)
                    {
                        var misLevelDept = table.Select(string.Format("ID='{0}'", misCurDept["ParentID"].ToString())).First(); //当前MIS的上级部门
                        if (misLevelDept != null)
                        {
                            shortName = misLevelDept["ShortName"].ToString().ToLower();
                            deptName = string.IsNullOrEmpty(shortName) ? misLevelDept["Name"].ToString().ToLower() : shortName; //当前MIS的上级部门名称
                            var wxLevelDept = orgList.FirstOrDefault(c => c.id == wxCurDept.parentid); //企业微信的上级部门
                            if (wxLevelDept != null
                                 && (deptName == wxLevelDept.name
                                 || (string.IsNullOrEmpty(misLevelDept["ParentID"].ToString()) && wxLevelDept.parentid == 0)
                                 )) //如果上级部门相同或是最大级部门时
                            {
                                deptIDs[0] = wxCurDept.id;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return deptIDs;
        }


        /// <summary>
        /// 获取发送消息对应的AgentId
        /// </summary>
        private UseDTO GetUse(string temp, string code = "", bool isFlow = false)
        {
            UseDTO useMsgDTO = new UseDTO()
            {
                AgentId = AppSettingService.agentId,
                UseCorpsecret = AppSettingService.usecorpsecret,
                Title = "",
                MenuID=""

            };
            if (!string.IsNullOrEmpty(temp))
            {
                var dto = JsonHelper.ToObject<UseMsgDTO>(temp);
                //if (isFlow && !string.IsNullOrEmpty(code))
                if (isFlow)//code 取自menucode
                {
                    var flows = dto.Flow;
                    if (flows.Count > 0)
                    {
                        foreach (var item in flows)
                        {
                            useMsgDTO.AgentId = item.AgentId;
                            useMsgDTO.UseCorpsecret = item.UseCorpsecret;
                            useMsgDTO.Title = item.Title;
                            useMsgDTO.MenuID = item.MenuID;                            
                            break;
                        }
                    }
                }
                else
                {
                    if (dto.Msg != null)
                    {
                        useMsgDTO.AgentId = dto.Msg.AgentId;
                        useMsgDTO.UseCorpsecret = dto.Msg.UseCorpsecret;
                        useMsgDTO.Title = dto.Msg.Title;
                    }
                }
            }

            return useMsgDTO;
        }
        private string MenuItems()
        {
            string menuCode = "";
            var dbTerminal = SqlHelper.Create("Terminal");
            DataTable table =  dbTerminal.ExecuteDataTable("select Code from S_S_EntryMenu where Type in ('2','16')");
            foreach (DataRow dr in table.Rows)
            {
                if (string.IsNullOrEmpty(dr["code"].ToString()))
                {
                    menuCode = "all";
                    break;
                }else
                {
                    menuCode += dr["code"].ToString().TrimEnd(',') + ",";
                }
            }
            menuCode = menuCode.TrimEnd(',');
            return menuCode;
        }
        /// <summary>
        /// 审批任务推送(定时)
        /// </summary>
        private void RegularPushSendUndoTasks(DataTable userTable, List<UserDTO> wxUsers)
        {
            string menuItems = MenuItems();
            string[] menus = menuItems.Split(',');
            string msgsBeginTime = AppSettingService.MsgsBeginTime; //获取推送消息的开始时间数据
            string sql = @"select 
		        count(S_WF_InsTaskExec.ExecUserID) TaskCount, 
		        S_WF_InsTaskExec.ExecUserID,
		        S_WF_InsTaskExec.ExecUserName,
                max(S_WF_InsDefFlow.Code) TmplCode,
                cast(DATEPART(Month,GETDATE()) as varchar) + '月' 
                +cast(DATEPART(Day,GETDATE())as varchar)+'日' +
                cast(DATEPART(Hour,GETDATE()) as varchar) +'点'+
                cast(DATEPART(Minute,GETDATE()) as varchar)+'分' as EndTime 
                from S_WF_InsTaskExec
                join S_WF_InsTask on ExecTime is null  and S_WF_InsTask.Type in('Normal','Inital') and S_WF_InsTask.CreateTime >'{0}' and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='') and S_WF_InsTask.ID=InsTaskID
                join S_WF_InsFlow on S_WF_InsFlow.Status='Processing' and S_WF_InsFlow.ID=S_WF_InsTask.InsFlowID  
                join S_WF_InsDefFlow on InsDefFlowID=S_WF_InsDefFlow.ID {1}
                join S_WF_InsDefStep on InsDefStepID = S_WF_InsDefStep.ID 
                join S_WF_DefStep on DefStepID=S_WF_DefStep.ID and S_WF_DefStep.Type='Normal' and S_WF_DefStep.AllowToMobile ='1'  
                group by S_WF_InsTaskExec.ExecUserID,S_WF_InsTaskExec.ExecUserName";
             var db = SqlHelper.Create("Workflow");
            if (menuItems != "all")
            {
                menuItems = " and S_WF_InsDefFlow.Code in(";
                foreach (var item in menus)
                {
                    menuItems = menuItems + "'" + item + "',";
                }
                menuItems = menuItems.TrimEnd(',') + ")";
            }else
            {
                menuItems = "";
            }
            DataTable table = db.ExecuteDataTable(string.Format(sql, msgsBeginTime, menuItems));
            string mt = GetMT();
            string temp = GetUseTemp();
            mt = mt.Substring(mt.IndexOf("||") + 2, mt.Length - mt.IndexOf("||") - 2);
            string msgsSendURL = AppSettingService.MsgsSendURL; //消息推送地址
            foreach (DataRow dr in table.Rows)
            {
                var user = userTable.Select(string.Format("ID='{0}'", dr["ExecUserID"].ToString()));
                if (user.Count() > 0)
                {
                    string code = user[0]["Code"].ToString();
                    string workNo = user[0]["WorkNo"].ToString();
                    string weixin = user[0]["Weixin"].ToString();

                    if (wxUsers.Where(c => c.userid == code || c.userid == workNo || c.userid == weixin).Count() > 0)
                    {
                        //处理消息
                        string msg = mt;
                        Regex reg = new Regex(@"\{(.+?)}");
                        foreach (Match m in reg.Matches(mt))
                        {
                            var value = m.Value.Trim('{', '}');
                            msg = msg.Replace(m.Value, dr[value].ToString());
                        }
                        var useDTO = GetUse(temp, "", true);
                        var uid = code;
                        if (wxUsers.Count(a => a.userid == workNo) > 0)
                            uid = workNo;
                        else if (wxUsers.Count(a => a.userid == weixin) > 0)
                            uid = weixin;
                        string menuID = useDTO.MenuID;
                        if (!string.IsNullOrEmpty(menuID))
                        {
                            menuID = "&menuID=" + menuID;
                        }
                        string url = msgsSendURL + string.Format("/Main/Login?SSO=task&IsRegularPush=true&UserID={0}&Account={1}"+ menuID,
                                    dr["ExecUserID"].ToString(), code
                                    );
                        string title = useDTO.Title;
                        if (title != "")
                        {
                            title = title.Substring(title.IndexOf("||") + 2, title.Length - title.IndexOf("||") - 2);
                        }
                      
                        List<ArticlesDTO> lits = new List<ArticlesDTO>();
                        ArticlesDTO art = new ArticlesDTO();
                        art.title = title;
                        art.description = msg;
                        art.url = url;
                        art.picurl = "";
                        lits.Add(art);
                        WeiXinService.sendMessage(new MessageNewsDTO
                        {
                            touser = uid,
                            toparty = "",
                            totag = "",
                            msgtype = "news",
                            agentid = Convert.ToInt32(useDTO.AgentId),
                            news = new MessageNewsItemDTO
                            {
                                articles = lits,
                            },
                            safe = 0
                        }, useDTO.UseCorpsecret);
                    }
                }
            }
        }

        /// <summary>
        /// 审批任务推送
        /// </summary>
        public void SendUndoTasks(DataTable userTable, List<UserDTO> wxUsers)
        {
            bool isRegularPush = Convert.ToBoolean(AppSettingService.IsRegularPush);
            if (isRegularPush)
            {
                RegularPushSendUndoTasks(userTable, wxUsers);
            }
            else
            {
                string msgsBeginTime = AppSettingService.MsgsBeginTime; //获取推送消息的开始时间数据
                var lastDTO = GetLastDTO();
                if (lastDTO.FlowLastTime != null && lastDTO.FlowLastTime.ToString("yyyy") != "0001")
                {
                    msgsBeginTime = lastDTO.FlowLastTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                WriteJobLast(lastDTO, true);
                #region UndoTask 获取待处理的任务
                string sql = @"select S_WF_InsTaskExec.ID as TaskID
                ,S_WF_InsDefFlow.Name as DefFlowName
                ,S_WF_InsDefStep.Name as DefStepName
                ,TaskName
                ,SendTaskUserNames
                ,S_WF_InsTaskExec.ExecUserID
                ,S_WF_InsTaskExec.ExecUserName
                ,S_WF_InsTask.CreateTime as CreateTime
                ,FormInstanceID
                ,FlowName
                ,S_WF_InsDefFlow.Code as TmplCode
                ,0 State
                ,S_WF_InsDefFlow.FormUrl
                ,S_WF_InsFlow.CreateUserName as CreateUserName
                ,S_WF_InsFlow.CreateUserID as CreateUserID
                from S_WF_InsTaskExec
                join S_WF_InsTask on ExecTime is null  and S_WF_InsTask.Type in('Normal','Inital') and S_WF_InsTask.CreateTime >'{0}' and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='') and S_WF_InsTask.ID=InsTaskID
                join S_WF_InsFlow on S_WF_InsFlow.Status='Processing' and S_WF_InsFlow.ID=S_WF_InsTask.InsFlowID  
                join S_WF_InsDefFlow on InsDefFlowID=S_WF_InsDefFlow.ID 
                join S_WF_InsDefStep on InsDefStepID = S_WF_InsDefStep.ID 
                join S_WF_DefStep on DefStepID=S_WF_DefStep.ID and S_WF_DefStep.Type='Normal' and S_WF_DefStep.AllowToMobile ='1' 
                order by S_WF_InsTask.CreateTime desc";
                #endregion
                var db = SqlHelper.Create("Workflow");
                DataTable table = db.ExecuteDataTable(string.Format(sql, msgsBeginTime));
                var menuItems = MenuItems();
                string mt = GetMT();
                string temp = GetUseTemp();
                mt = mt.Substring(0, mt.IndexOf("||"));
                string msgsSendURL = AppSettingService.MsgsSendURL; //消息推送地址

                foreach (DataRow dr in table.Rows)
                {
                    var user = userTable.Select(string.Format("ID='{0}'", dr["ExecUserID"].ToString()));
                    bool menufalg;
                    string[] menus = menuItems.Split(',');
                    menufalg = menus.FirstOrDefault(x => x == dr["TmplCode"].ToString()) != null;
                    if (menuItems == "all")
                    {
                        menufalg = true;
                    }
               
                  
                    if (user.Count() > 0 && menufalg)
                    {
                        string code = user[0]["Code"].ToString();
                        string workNo = user[0]["WorkNo"].ToString();
                        string weixin = user[0]["Weixin"].ToString();
                        if (wxUsers.Where(c => c.userid == code || c.userid == workNo || c.userid == weixin).Count() > 0)
                        {
                            //处理消息
                            string msg = mt;
                            Regex reg = new Regex(@"\{(.+?)}");
                            foreach (Match m in reg.Matches(mt))
                            {
                                var value = m.Value.Trim('{', '}');
                                if (value.ToLower() == "createuserdept")
                                {
                                    var CreateUser = userTable.Select(string.Format("ID='{0}'", dr["CreateUserID"].ToString()));
                                    if (CreateUser.Count() > 0)
                                    {
                                        msg = msg.Replace(m.Value, CreateUser[0]["DeptName"].ToString());
                                    }

                                }
                                else
                                {
                                    msg = msg.Replace(m.Value, dr[value].ToString());
                                }


                            }
                            var useDTO = GetUse(temp, dr["TmplCode"].ToString(), true);
                            var uid = code;
                            if (wxUsers.Count(a => a.userid == workNo) > 0)
                                uid = workNo;
                            else if (wxUsers.Count(a => a.userid == weixin) > 0)
                                uid = weixin;
                            string url = msgsSendURL + string.Format("/Main/Login?SSO=Flow&TaskID={0}&FormInstanceID={1}&Type=undo&TmplCode={2}&UserID={3}&Account={4}&IsRegularPush=false&FormUrl={5}&isMsg=true",
                                        dr["TaskID"].ToString(),
                                        dr["FormInstanceID"].ToString(),
                                        dr["TmplCode"].ToString(),
                                        dr["ExecUserID"].ToString(),
                                        code,
                                        Convert.ToString(dr["FormUrl"])
                                        );
                            string title = useDTO.Title;
                            if (title != "")
                            {
                                title = title.Substring(0, title.IndexOf("||"));
                                foreach (Match m in reg.Matches(title))
                                {
                                    var value = m.Value.Trim('{', '}');
                                    title = title.Replace(m.Value, dr[value].ToString());
                                    //  msg = msg.Replace(m.Value, dr[value].ToString());
                                }
                            }
                            else
                            {
                                continue;
                            }


                            List<ArticlesDTO> lits = new List<ArticlesDTO>();
                            ArticlesDTO art = new ArticlesDTO();
                            art.title = title;
                            art.description = msg;
                            art.url = url;
                            art.picurl = "";
                            lits.Add(art);
                            WeiXinService.sendMessage(new MessageNewsDTO
                            {
                                touser = uid,
                                toparty = "",
                                totag = "",
                                msgtype = "news",
                                agentid = Convert.ToInt32(useDTO.AgentId),
                                news = new MessageNewsItemDTO
                                {
                                    articles = lits,
                                },
                                safe = 0
                            }, useDTO.UseCorpsecret);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 消息推送(定时)
        /// </summary>
        private void RegularPushSendMsgs(DataTable userTable, List<UserDTO> wxUsers)
        {
            string msgsBeginTime = AppSettingService.MsgsBeginTime; //获取推送消息的开始时间数据
            #region Msgs 获取未读消息
            string sql = @"select a.UserID,count(a.ID) MsgCount,
                cast(DATEPART(Month,GETDATE()) as varchar) + '月' 
                +cast(DATEPART(Day,GETDATE())as varchar)+'日' +
                cast(DATEPART(Hour,GETDATE()) as varchar) +'点'+
                cast(DATEPART(Minute,GETDATE()) as varchar)+'分' as EndTime     
                from S_S_MsgReceiver a, S_S_MsgBody b           
                where a.MsgBodyID=b.ID and a.FirstViewTime is null and  b.SendTime > '{0}' GROUP BY a.UserID";
            #endregion
            var db = SqlHelper.Create("Base");
            DataTable table = db.ExecuteDataTable(string.Format(sql, msgsBeginTime));
            string msgsSendURL = AppSettingService.MsgsSendURL; //消息推送地址
            string temp = GetUseTemp();
            foreach (DataRow dr in table.Rows)
            {
                var user = userTable.Select(string.Format("ID='{0}'", dr["UserID"].ToString()));
                if (user.Count() > 0)
                {
                    string code = user[0]["Code"].ToString();
                    string workNo = user[0]["WorkNo"].ToString();
                    string weixin = user[0]["Weixin"].ToString();
                    if (wxUsers.Where(c => c.userid == code || c.userid == workNo || c.userid == weixin).Count() > 0)
                    {
                        //处理消息
                        string msg = GetMT(false);
                        msg = msg.Substring(msg.IndexOf("||") + 2, msg.Length - msg.IndexOf("||") - 2);
                        Regex reg = new Regex(@"\{(.+?)}");
                        foreach (Match m in reg.Matches(msg))
                        {
                            var value = m.Value.Trim('{', '}');
                            if (value.ToUpper() == "RECEIVERNAME")
                            {
                                msg = msg.Replace(m.Value, user[0]["Name"].ToString());
                            }
                            else
                                msg = msg.Replace(m.Value, dr[value].ToString());
                        }
                        var useDTO = GetUse(temp);
                        var uid = code;
                        if (wxUsers.Count(a => a.userid == workNo) > 0)
                            uid = workNo;
                        else if (wxUsers.Count(a => a.userid == weixin) > 0)
                            uid = weixin;

                        string url = msgsSendURL + string.Format("/Main/Login?SSO=Msgs&IsRegularPush=true&UserID={0}&Account={1}", dr["UserID"].ToString(), code);
                        string title = useDTO.Title;
                        if (title != "")
                        {
                            title = title.Substring(title.IndexOf("||") + 2, title.Length - title.IndexOf("||") - 2);
                        }
                        List<ArticlesDTO> lits = new List<ArticlesDTO>();
                        ArticlesDTO art = new ArticlesDTO();
                        art.title = title;
                        art.description = msg;
                        art.url = url;
                        art.picurl = "";
                        lits.Add(art);
                        WeiXinService.sendMessage(new MessageNewsDTO
                        {
                            touser = uid,
                            toparty = "",
                            totag = "",
                            msgtype = "news",
                            agentid = Convert.ToInt32(useDTO.AgentId),
                            news = new MessageNewsItemDTO
                            {
                                articles = lits,
                            },
                            safe = 0
                        }, useDTO.UseCorpsecret);
                
                    }
                }
            }
        }


        /// <summary>
        /// 消息推送
        /// </summary>
        public void SendMsgs(DataTable userTable, List<UserDTO> wxUsers)
        {
            bool isRegularPush = Convert.ToBoolean(AppSettingService.IsRegularPush);
            if (isRegularPush)
            {
                RegularPushSendMsgs(userTable, wxUsers);
            }
            else
            {
                string msgsBeginTime = AppSettingService.MsgsBeginTime; //获取推送消息的开始时间数据
                var lastDTO = GetLastDTO();
                if (lastDTO.MsgLastTime != null && lastDTO.MsgLastTime.ToString("yyyy") != "0001")
                {
                    msgsBeginTime = lastDTO.MsgLastTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                WriteJobLast(lastDTO, false);
                #region Msgs 获取未读消息
                string sql = @"SELECT msg.ID, msg.SenderID, msg.SenderName, msg.SendTime,receiver.ID as ReceiverID, 
                            msg.ReceiverIDs, msg.Title, 0 State,msg.ContentText as ContentText,msg.Content
                            FROM [dbo].[S_S_MsgBody] AS msg
                            join (SELECT MIN(ID) as ID, MsgBodyID,MIN(FirstViewTime) as FirstViewTime FROM S_S_MsgReceiver WHERE IsDeleted='0' GROUP BY MsgBodyID) as receiver
                            on msg.ID=receiver.MsgBodyID and  receiver.FirstViewTime is null and  msg.SendTime > '{0}' 
                            ORDER BY msg.SendTime DESC";
                #endregion
                var db = SqlHelper.Create("Base");
                DataTable table = db.ExecuteDataTable(string.Format(sql, msgsBeginTime));
                string msgsSendURL = AppSettingService.MsgsSendURL; //消息推送地址
                string temp = GetUseTemp();
                foreach (DataRow dr in table.Rows)
                {
                 
                     var userIDs = dr["ReceiverIDs"].ToString().Split(',');
                    foreach (var userID in userIDs)
                    {
                        var user = userTable.Select(string.Format("ID='{0}'", userID));
                        if (user.Count() > 0)
                        {
                            string code = user[0]["Code"].ToString();
                            string workNo = user[0]["WorkNo"].ToString();
                            string weixin = user[0]["Weixin"].ToString();
                            if (wxUsers.Where(c => c.userid == code || c.userid == workNo || c.userid == weixin).Count() > 0)
                            {
                                //处理消息
                                string msg = GetMT(false);
                                msg = msg.Substring(0, msg.IndexOf("||"));
                                Regex reg = new Regex(@"\{(.+?)}");
                                foreach (Match m in reg.Matches(msg))
                                {
                                    var value = m.Value.Trim('{', '}');
                                   if (value.ToUpper() == "RECEIVERNAME")
                                    {
                                        msg = msg.Replace(m.Value, user[0]["Name"].ToString());
                                    }
                                    else
                                        msg = msg.Replace(m.Value, dr[value].ToString());
                                }
                                var useDTO = GetUse(temp);
                                var uid = code;
                                if (wxUsers.Count(a => a.userid == workNo) > 0)
                                    uid = workNo;
                                else if (wxUsers.Count(a => a.userid == weixin) > 0)
                                    uid = weixin;

                                string url = msgsSendURL + string.Format("/Main/Login?SSO=Msgs&IsRegularPush=false&ID={0}&ReceiverID={1}&UserID={2}&Account={3}", dr["ID"].ToString(), dr["ReceiverID"].ToString(), userID, code);
                                string title = useDTO.Title;
                                if (title != "")
                                {
                                    title = title.Substring(0, title.IndexOf("||"));
                                    foreach (Match m in reg.Matches(title))
                                    {
                                        var value = m.Value.Trim('{', '}');
                                        if (value.ToUpper() == "RECEIVERNAME")
                                        {
                                            title = title.Replace(m.Value, user[0]["Name"].ToString());
                                        }
                                        else
                                            title = title.Replace(m.Value, dr[value].ToString());
                                        //  msg = msg.Replace(m.Value, dr[value].ToString());
                                    }
                                }
                               
                                List<ArticlesDTO> lits = new List<ArticlesDTO>();
                                ArticlesDTO art = new ArticlesDTO();
                                art.title = title;
                                art.description = msg;
                                art.url = url;
                                art.picurl = "";
                                lits.Add(art);
                                WeiXinService.sendMessage(new MessageNewsDTO
                                {
                                    touser = uid,
                                    toparty = "",
                                    totag = "",
                                    msgtype = "news",
                                    agentid = Convert.ToInt32(useDTO.AgentId),
                                    news = new MessageNewsItemDTO
                                    {
                                        articles = lits,
                                    },
                                    safe = 0
                                }, useDTO.UseCorpsecret);

                              
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 创建项目群组 
        /// </summary>
        public void CreateProjectGroup(DataTable userTable, List<UserDTO> wxUsers)
        {
            var db = SqlHelper.Create("Project");
            List<ProjectGroupDTO> list = new List<ProjectGroupDTO>();
            string sysTableSql = "select * from dbo.sysobjects where id = object_id('S_W_OBSUser') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            DataTable table = db.ExecuteDataTable(sysTableSql);
            string projectSql = "select ID,Name from {0} ";
            string odsUserSql = "select ID,{1},MajorName,RoleCode,RoleName,UserID from {0} ";
            DataTable projectTable = null;
            DataTable odsUserTable = null;
            if (table.Rows.Count > 0) //不是EPC
            {
                projectSql = string.Format(projectSql, "S_I_ProjectInfo");
                odsUserSql = string.Format(odsUserSql, "S_W_OBSUser", "ProjectInfoID");
            }
            else
            {
                projectSql = string.Format(projectSql, "S_I_Engineering");
                odsUserSql = string.Format(odsUserSql, "S_I_OBS_User", "EngineeringInfoID");
            }
            projectTable = db.ExecuteDataTable(projectSql);
            odsUserTable = db.ExecuteDataTable(odsUserSql);
            foreach (DataRow pdr in projectTable.Rows)
            {
                string projectID = pdr["ID"].ToString();
                var uRows = odsUserTable.Select(string.Format(" {1} = '{0}'", projectID, table.Rows.Count > 0 ? "ProjectInfoID" : "EngineeringInfoID"));
                List<ProjectUserDTO> users = new List<ProjectUserDTO>();
                foreach (var udr in uRows)
                {
                    var user = userTable.Select(string.Format(" ID = '{0}'", udr["UserID"].ToString()));
                    if (user.Count() > 0)
                    {
                        string code = user[0]["Code"].ToString();
                        string workNo = user[0]["WorkNo"].ToString();
                        string weixin = user[0]["Weixin"].ToString();
                        var uid = code;
                        if (wxUsers.Count(a => a.userid == workNo && a.status == 1) > 0)
                            uid = workNo;
                        else if (wxUsers.Count(a => a.userid == weixin && a.status == 1) > 0)
                            uid = weixin;

                        if (wxUsers.Count(c => c.userid == uid && c.status == 1) > 0)
                        {
                            if (users.Where(c => c.UserCode == uid).Count() == 0)
                                users.Add(new ProjectUserDTO { UserCode = uid, IsProjectManager = udr["RoleCode"].ToString() == "ProjectManager" });
                        }
                    }
                }
                if (users.Count >= 2)
                {
                    var ownerUser = users.Where(c => c.IsProjectManager == true).Count() > 0 ? users.FirstOrDefault(c => c.IsProjectManager == true) : users.First();
                    List<string> userlist = new List<string>();
                    foreach (var u in users)
                    {
                        userlist.Add(u.UserCode);
                    }
                    list.Add(new ProjectGroupDTO
                    {
                        name = pdr["Name"].ToString(),
                        owner = ownerUser.UserCode,
                        userlist = userlist,
                        ProjectID = pdr["ID"].ToString(),
                        chatid = pdr["ID"].ToString().Substring(8).Replace("-", "")
                    });
                }
            }

            foreach (var item in list)
            {
                var group = JsonHelper.ToObject<ProjectGroupDTO>(WeiXinService.GetProjectGroup(item.chatid).GetValue("chat_info"));
                if (group != null)
                {
                    if (group.owner != item.owner || group.userlist != item.userlist)
                    {
                        List<string> addUsers = new List<string>();
                        List<string> delUsers = new List<string>();
                        foreach (var user in item.userlist)
                        {
                            if (group.userlist.Where(c => c == user).Count() <= 0)
                            {
                                addUsers.Add(user);
                            }
                        }
                        foreach (var user in group.userlist)
                        {
                            if (item.userlist.Where(c => c == user).Count() <= 0)
                            {
                                delUsers.Add(user);
                            }
                        }
                        if (group.owner != item.owner || addUsers.Count > 0 || delUsers.Count > 0)
                        {
                            item.add_user_list = addUsers;
                            item.del_user_list = delUsers;
                            WeiXinService.UpdateProjectGroup(item);
                        }
                    }
                }
                else
                {
                    var info = WeiXinService.CreateProjectGroup(item);
                    WeiXinService.CreateProjectSend(new ProjectMessageDTO
                    {
                        chatid = item.chatid,
                        msgtype = "text",
                        text = new MessageTextDTO
                        {
                            content = item.name + "群组创建成功!"
                        },
                        safe = 0
                    });
                }
            }
        }

        /// <summary>
        /// 获取消息模板
        /// </summary>
        private string GetMT(bool isFlow = true)
        {
            string fileStr = "";
            DirectoryInfo rootDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            string root = rootDir.Parent.Parent.FullName;
            string fullName = root + @"\MT\" + (isFlow ? "WorkFlow.Json" : "Msgs.Json");
            if (!string.IsNullOrEmpty(fullName))
            {
                if (File.Exists(fullName))
                {
                    StreamReader sr = File.OpenText(fullName);
                    string str = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(str))
                    {
                        fileStr = str;
                    }
                    sr.Close();
                }
            }
            return fileStr;
        }

        /// <summary>
        /// 获取已发送的时间
        /// </summary>
        private JobLastDTO GetLastDTO()
        {
            JobLastDTO dto = new JobLastDTO();
            DirectoryInfo rootDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            string root = rootDir.Parent.Parent.FullName;
            string fullName = root + @"\MT\" + "JobLastTime.js";
            if (!string.IsNullOrEmpty(fullName))
            {
                StreamReader sr = File.OpenText(fullName);
                string str = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(str))
                {
                    dto = JsonHelper.ToObject<JobLastDTO>(str);
                }
                sr.Close();
            }
            return dto;
        }

        /// <summary>
        /// 发送后把时间保存到文件中
        /// </summary>
        private void WriteJobLast(JobLastDTO dto, bool isFlow)
        {
            string msgsBeginTime = AppSettingService.MsgsBeginTime; //获取推送消息的开始时间数据
            JobLastDTO lastDTO = new JobLastDTO();
            if (isFlow)
            {
                dto.FlowLastTime = DateTime.Now;
                dto.MsgLastTime = dto.MsgLastTime != null ? dto.MsgLastTime : Convert.ToDateTime(msgsBeginTime);
            }
            else
            {
                dto.FlowLastTime = dto.FlowLastTime != null ? dto.FlowLastTime : Convert.ToDateTime(msgsBeginTime);
                dto.MsgLastTime = DateTime.Now;
            }
            DirectoryInfo rootDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            string root = rootDir.Parent.Parent.FullName;
            string fullName = root + @"\MT\" + "/JobLastTime.js";
            if (!string.IsNullOrEmpty(fullName))
            {
                FileStream myFs = new FileStream(fullName, FileMode.Create);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write(JsonHelper.ToJson(dto));
                mySw.Close();
                myFs.Close();
            }
        }

        /// <summary>
        /// 获取应用模板
        /// </summary>
        private string GetUseTemp()
        {
            string temp = "";
            DirectoryInfo rootDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            string root = rootDir.Parent.Parent.FullName;
            string fullName = root + @"\MT\" + "UseMsg.js";
            if (!string.IsNullOrEmpty(fullName))
            {
                StreamReader sr = File.OpenText(fullName);
                string str = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(str))
                {
                    temp = str;
                }
                sr.Close();
            }
            return temp;
        }

    }
}