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
    public class DesignInputFO : SynDataFO
    {
        public void CreateDesignInputQueue()
        {
            var sourceSql = @"select m.* from S_D_Input m 
left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1";
            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                sourceSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_D_Input>("select * from S_D_Input where 1=1 ");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and m.ModifyDate >='" + startDate + "' and m.ModifyDate <='" + endDate + "'";
            }

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "folder/delete";
            var alldtSql = @"select m.ID from S_D_Input m left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1";
            if (!string.IsNullOrEmpty(synProjectMode))
                alldtSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(alldtSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.Type == DesignInputType.Actual.ToString() && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_D_Input", item.ID, item.Type
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), string.Format(removeUrl, item.Type == "Major" ? "Plan" : "Exchange"));
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            #region Edit

            var saveUrl = this.BaseServerUrl + "folder/edit";
            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);
                var ID = dic.GetValue("ID");
                var projectInfoID = dic.GetValue("ProjectInfoID");
                var catagory = dic.GetValue("Catagory");
                var inputType = dic.GetValue("InputType");
                var name = dic.GetValue("InfoName");
                var sortIndex = dic.GetValue("SortIndex");

                if (!synData.Any(a => a.ProjectInfoID == projectInfoID))
                {
                    var initVirtualSql = initVirtualDesignInput(projectInfoID, sourceDt, synData);
                    saveSb.AppendLine(initVirtualSql);
                }
                //设置参数
                var root = synData.FirstOrDefault(a => a.ProjectInfoID == projectInfoID && a.Code == "Root");

                var typeInput = synData.FirstOrDefault(a => a.ProjectInfoID == projectInfoID && a.Type == DesignInputType.Virtual.ToString()
                    && a.Name == inputType && a.Code == catagory);
                if (typeInput == null)
                {
                    var catagoryInput = synData.FirstOrDefault(a => a.ParentID == root.ID && a.Code == catagory);
                    if (catagoryInput == null)
                    {
                        catagoryInput = new S_D_Input()
                        {
                            Code = catagory,
                            Name = catagory,
                            Type = DesignInputType.Virtual.ToString()
                        };
                        //只可能是新增专业，缺专业对应目录
                        var majorEnum = GlobalData.MajorList.FirstOrDefault(a => a.Code == catagory);
                        if (majorEnum == null)
                            continue;
                        catagoryInput.Name = majorEnum.Name;
                        addChildDesignInput(catagoryInput, root, synData, saveSb);
                    }
                    typeInput = new S_D_Input()
                    {
                        Code = catagory,
                        Name = inputType,
                        Type = DesignInputType.Virtual.ToString()
                    };
                    addChildDesignInput(typeInput, catagoryInput, synData, saveSb);
                }
                var input = new S_D_Input()
                {
                    ID = ID,
                    Code = catagory,
                    Name = name,
                    SortIndex = string.IsNullOrEmpty(sortIndex) ? 0 : Convert.ToInt32(sortIndex),
                    Type = DesignInputType.Actual.ToString()
                };
                addChildDesignInput(input, typeInput, synData, saveSb);
            }

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
            #endregion
        }

        private void addChildDesignInput(S_D_Input child, S_D_Input parent, List<S_D_Input> synData, StringBuilder sb)
        {
            if (string.IsNullOrEmpty(child.ID))
                child.ID = GuidHelper.CreateGuid();
            if (string.IsNullOrEmpty(child.ParentID))
                child.ParentID = parent.ID;
            if (string.IsNullOrEmpty(child.FullID))
                child.FullID = parent.FullID + "." + child.ID;
            if (string.IsNullOrEmpty(child.ProjectInfoID))
                child.ProjectInfoID = parent.ProjectInfoID;
            if (child.SortIndex == 0)
                child.SortIndex = synData.Count(a => a.ParentID == parent.ID) + 1;
            if (!synData.Exists(a => a.ID == child.ID))
                synData.Add(child);
            var param = new DesignInputRequestData();
            param.Id = child.ID;
            param.name = child.Name;
            param.FullID = child.FullID;
            param.parentId = child.ParentID;
            param.ProjectInfoID = child.ProjectInfoID;
            param.sort = child.SortIndex;
            param.type = 8;
            var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_D_Input", child.ID, child.Type
    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DesignInputRequestData>(param).Replace("'", "''")
    , this.BaseServerUrl + "folder/edit");
            sb.AppendLine(queueSql);
        }

        private string initVirtualDesignInput(string projectInfoID, DataTable sourceDt, List<S_D_Input> SynData)
        {
            var param = new DesignInputBatchRequestData();
            param.folders = new List<DesignInputRequestData>();
            param.parentId = projectInfoID;
            var root = new DesignInputRequestData();
            root.Id = GuidHelper.CreateGuid();
            root.children = new List<DesignInputRequestData>();
            root.name = "设计输入资料";
            root.type = 31;
            root.FullID = root.Id;
            root.parentId = string.Empty;
            root.Code = "Root";
            root.sort = 0;
            SynData.Add(new S_D_Input()
            {
                ProjectInfoID = projectInfoID,
                Code = root.Code,
                ID = root.Id,
                FullID = root.FullID,
                ParentID = root.parentId,
                Name = root.name,
                Type = DesignInputType.Virtual.ToString()
            });
            param.folders.Add(root);

            var projectCatagory = new DesignInputRequestData();
            projectCatagory.Id = GuidHelper.CreateGuid();
            projectCatagory.children = new List<DesignInputRequestData>();
            projectCatagory.name = "项目资料";
            projectCatagory.type = 8;
            projectCatagory.sort = 0;
            projectCatagory.FullID = root.Id + "." + projectCatagory.Id;
            projectCatagory.parentId = root.Id;
            projectCatagory.Code = "Project";
            SynData.Add(new S_D_Input()
            {
                ProjectInfoID = projectInfoID,
                ID = projectCatagory.Id,
                ParentID = projectCatagory.parentId,
                FullID = projectCatagory.FullID,
                Name = projectCatagory.name,
                Code = projectCatagory.Code,
                Type = DesignInputType.Virtual.ToString()
            });
            root.children.Add(projectCatagory);

            var types = sourceDt.Select(" ProjectInfoID='" + projectInfoID + "'", "InputTypeIndex asc")
                .Select(d => new { Catagory = d["Catagory"].ToString(), Type = d["InputType"].ToString(), SortIndex = d["InputTypeIndex"] }).Distinct().ToList();

            foreach (var item in types.Where(a => a.Catagory == projectCatagory.Code).ToList())
            {
                var typeCatagory = new DesignInputRequestData();
                typeCatagory.Id = GuidHelper.CreateGuid();
                typeCatagory.name = item.Type;
                typeCatagory.type = 8;
                typeCatagory.sort = 0;
                typeCatagory.FullID = root.Id + "." + projectCatagory.Id + "." + typeCatagory.Id;
                typeCatagory.parentId = projectCatagory.Id;
                typeCatagory.Code = item.Catagory;
                if (item.SortIndex != null && item.SortIndex != DBNull.Value)
                    typeCatagory.sort = Convert.ToInt32(item.SortIndex);
                SynData.Add(new S_D_Input()
                {
                    ProjectInfoID = projectInfoID,
                    ID = typeCatagory.Id,
                    ParentID = typeCatagory.parentId,
                    FullID = typeCatagory.FullID,
                    Name = typeCatagory.name,
                    Code = typeCatagory.Code,
                    Type = DesignInputType.Virtual.ToString()
                });
                projectCatagory.children.Add(typeCatagory);
            }
            var projectMajors = this.SQLHelpeProject.ExecuteScalar("select Major from S_I_ProjectInfo where id='" + projectInfoID + "'");
            if (projectMajors != DBNull.Value && projectMajors != null && !string.IsNullOrEmpty(projectMajors.ToString()))
            {
                var majorList = JsonHelper.ToList(projectMajors.ToString());
                var n = 1;
                foreach (var majorDic in majorList)
                {
                    var majorCatagory = new DesignInputRequestData();
                    majorCatagory.Id = GuidHelper.CreateGuid();
                    majorCatagory.children = new List<DesignInputRequestData>();
                    majorCatagory.name = majorDic.GetValue("Name");
                    majorCatagory.type = 8;
                    majorCatagory.FullID = root.Id + "." + majorCatagory.Id;
                    majorCatagory.parentId = root.Id;
                    majorCatagory.Code = majorDic.GetValue("Value");
                    majorCatagory.sort = n++;
                    SynData.Add(new S_D_Input()
                    {
                        ProjectInfoID = projectInfoID,
                        ID = majorCatagory.Id,
                        ParentID = majorCatagory.parentId,
                        FullID = majorCatagory.FullID,
                        Name = majorCatagory.name,
                        Code = majorCatagory.Code,
                        Type = DesignInputType.Virtual.ToString()
                    });
                    root.children.Add(majorCatagory);

                    foreach (var item in types.Where(a => a.Catagory == majorCatagory.Code).ToList())
                    {
                        var typeCatagory = new DesignInputRequestData();
                        typeCatagory.Id = GuidHelper.CreateGuid();
                        typeCatagory.name = item.Type;
                        typeCatagory.type = 8;
                        typeCatagory.sort = 0;
                        typeCatagory.FullID = root.Id + "." + majorCatagory.Id + "." + typeCatagory.Id;
                        typeCatagory.parentId = majorCatagory.Id;
                        typeCatagory.Code = item.Catagory;
                        if (item.SortIndex != null && item.SortIndex != DBNull.Value)
                            typeCatagory.sort = Convert.ToInt32(item.SortIndex);
                        SynData.Add(new S_D_Input()
                        {
                            ProjectInfoID = projectInfoID,
                            ID = typeCatagory.Id,
                            ParentID = typeCatagory.parentId,
                            FullID = typeCatagory.FullID,
                            Name = typeCatagory.name,
                            Code = typeCatagory.Code,
                            Type = DesignInputType.Virtual.ToString()
                        });
                        majorCatagory.children.Add(typeCatagory);
                    }
                }
            }
            var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.BatchSave.ToString(), "S_D_Input", "", ""
    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DesignInputBatchRequestData>(param).Replace("'", "''")
    , this.BaseServerUrl + "folder/editbatch");

            return queueSql;
        }



        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synData = JsonHelper.ToObject<DesignInputRequestData>(queueData.RequestData);
                var sql = createDesignInputString(synData, synData.ProjectInfoID, queueData.RelateType, queueData.RequestData.Replace("'", "''"));
                interfaceSb.AppendLine(sql);
            }
            else if (queueData.SynType == SynType.BatchSave.ToString())
            {
                var synData = JsonHelper.ToObject<DesignInputBatchRequestData>(queueData.RequestData);
                reccreateDesignInputString(interfaceSb, synData.folders, synData.parentId);
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                var delsql = "update S_D_Input set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
                interfaceSb.AppendLine(delsql);
            }
        }

        private void reccreateDesignInputString(StringBuilder sb, List<DesignInputRequestData> folders, string projectinfoid)
        {
            foreach (var folder in folders)
            {
                sb.AppendLine(createDesignInputString(folder, projectinfoid, DesignInputType.Virtual.ToString(), ""));
                if (folder.children != null && folder.children.Count > 0)
                    reccreateDesignInputString(sb, folder.children, projectinfoid);
            }
        }

        private string createDesignInputString(DesignInputRequestData folder, string projectInfoID, string designInputType, string requestData)
        {
            var saveTmp = @"if exists(select 1 from S_D_Input where ID='{0}')
                                update S_D_Input set ParentID='{1}',FullID='{2}',Code = '{4}', Name='{5}',SynTime='{9}',SynData='{10}',SortIndex='{11}' where ID='{0}'
                            else
	                            insert into S_D_Input(ID,ParentID,FullID,ProjectInfoID,Code,Name,Type,State,SynID,SynTime,SynData,SortIndex) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            return string.Format(saveTmp, folder.Id, folder.parentId, folder.FullID, projectInfoID, folder.Code, folder.name.Replace("'", "''"), designInputType,
                        DataState.Normal.ToString(), "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), requestData.Replace("'", "''"), folder.sort);
        }

    }
}
