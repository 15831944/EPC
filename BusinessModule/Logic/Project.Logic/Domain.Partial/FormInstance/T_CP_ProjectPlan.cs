using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;
using Formula.Helper;
using Config;
using System.Data;
using Config.Logic;
namespace Project.Logic.Domain
{
    public partial class T_CP_ProjectPlan
    {
        public void Relase()
        {
            var entities = FormulaHelper.GetEntities<ProjectEntities>();
            this.IsRelease = "T";
            this.State = ProjectCommoneState.Finish.ToString();
            //设置当前版本
            entities.Set<T_CP_ProjectPlan>().Where(c => c.ProjectInfoID == this.ProjectInfoID).Update(c => c.IsCurrent = "F");
            this.IsCurrent = "T";
            //创建wbs
            this.BuildWBS();
        }

        private void BuildWBS()
        {
            var entities = FormulaHelper.GetEntities<ProjectEntities>();
            var subProjectList = JsonHelper.ToList(this.SubProjectList);
            var majorList = JsonHelper.ToList(this.MajorList);
            var projectinfo = entities.Set<S_I_ProjectInfo>().Find(this.ProjectInfoID);

            var projectwbs = projectinfo.WBSRoot;
            projectwbs.PlanStartDate = this.PlanStartDate;
            projectwbs.PlanEndDate = this.PlanFinishDate;
            projectwbs.WorkLoad = this.ExpectCost;
            
            BuildSubProjectWBS(projectwbs, subProjectList,majorList);
            //无子项
            //BuildMajorWBS(projectwbs, majorList);
        }
        public void BuildSubProjectWBS(S_W_WBS parentWBS, List<Dictionary<string, object>> subProjectList, List<Dictionary<string, object>> majorList)
        {
            foreach (var subProject in subProjectList)
            {
                //子项
                var sCode = subProject.GetValue("Code").ToString();
                S_W_WBS subprojectwbs = parentWBS.AllChildren.FirstOrDefault(c => c.Code == sCode);
                if (subprojectwbs == null)
                {
                    #region 新增
                    subprojectwbs = new S_W_WBS
                    {
                        WBSType = WBSNodeType.SubProject.ToString(),
                        Code = subProject.GetValue("Code").ToString(),
                        Name = subProject.GetValue("Name").ToString(),
                        ChargeUserID = subProject.GetValue("ChargeUserID").ToString(),
                        ChargeUserName = subProject.GetValue("ChargeUserName").ToString()

                    };
                    if (!string.IsNullOrEmpty(subProject.GetValue("PlanStartDate")))
                        subprojectwbs.PlanStartDate = Convert.ToDateTime(subProject.GetValue("PlanStartDate"));
                    if (!string.IsNullOrEmpty(subProject.GetValue("PlanEndDate")))
                        subprojectwbs.PlanEndDate = Convert.ToDateTime(subProject.GetValue("PlanEndDate"));
                    parentWBS.AddChild(subprojectwbs);
                    #endregion
                }
                else
                {
                    subprojectwbs.Name = subProject.GetValue("Name").ToString();
                    if (!String.IsNullOrEmpty(subProject.GetValue("ChargeUserID")))
                    {
                        subprojectwbs.ChargeUserID = subProject.GetValue("ChargeUserID");
                        subprojectwbs.ChargeUserName = subProject.GetValue("ChargeUserName");
                    }
                    if (!string.IsNullOrEmpty(subProject.GetValue("PlanStartDate")))
                        subprojectwbs.PlanStartDate = Convert.ToDateTime(subProject.GetValue("PlanStartDate"));
                    if (!string.IsNullOrEmpty(subProject.GetValue("PlanEndDate")))
                        subprojectwbs.PlanEndDate = Convert.ToDateTime(subProject.GetValue("PlanEndDate"));
                }

                BuildMajorWBS(subprojectwbs, majorList);
            }
        }

        public void BuildMajorWBS(S_W_WBS majorParentWBS, List<Dictionary<string, object>> majorList)
        {
            if (majorList == null || majorList.Count <= 0)
                return;
            var majorEnum = BaseConfigFO.GetWBSEnum(WBSNodeType.Major)
                .AsEnumerable()
                .Select(c => new DicItem
                {
                    Text = c["text"].ToString(),
                    Value = c["value"].ToString()
                }).ToList();
            var majorAttrList = BaseConfigFO.GetWBSAttrList(WBSNodeType.Major.ToString());
            foreach (var major in majorList)
            {
                var mCode = major.GetValue("Major").ToString();
                S_W_WBS majorwbs = majorParentWBS.AllChildren.FirstOrDefault(c => c.WBSValue == mCode);
                if (majorwbs == null)
                {
                    #region 新增
                    majorwbs = new S_W_WBS
                    {
                        WBSType = WBSNodeType.Major.ToString(),
                        Name = majorEnum.FirstOrDefault(c => c.Value == mCode).Text,
                        WBSValue = mCode
                        //Code = mCode,
                    };
                    var attrDefine = majorAttrList.FirstOrDefault(d => d.Code == majorwbs.WBSValue);
                    if (attrDefine != null)
                        majorwbs.SortIndex = attrDefine.SortIndex;
                    majorParentWBS.AddChild(majorwbs);
                    #endregion
                }
                else
                {
                    majorwbs.Name = majorEnum.FirstOrDefault(c => c.Value == mCode).Text;
                    var attrDefine = majorAttrList.FirstOrDefault(d => d.Code == majorwbs.WBSValue);
                    if (attrDefine != null)
                        majorwbs.SortIndex = attrDefine.SortIndex;
                }

                if (!String.IsNullOrEmpty(major.GetValue("MajorPrincipleID")))
                    majorwbs.SetUsers(ProjectRole.MajorPrinciple.ToString(), major.GetValue("MajorPrincipleID").ToString().Split(','));
                if (!String.IsNullOrEmpty(major.GetValue("DesignID")))
                    majorwbs.SetUsers(ProjectRole.Designer.ToString(), major.GetValue("DesignID").Split(','));
                if (!String.IsNullOrEmpty(major.GetValue("CollactorID")))
                    majorwbs.SetUsers(ProjectRole.Collactor.ToString(), major.GetValue("CollactorID").Split(','));
                if (!String.IsNullOrEmpty(major.GetValue("AuditorID")))
                    majorwbs.SetUsers(ProjectRole.Auditor.ToString(), major.GetValue("AuditorID").Split(','));
                if (!String.IsNullOrEmpty(major.GetValue("CheckID")))
                    majorwbs.SetUsers(ProjectRole.Approver.ToString(), major.GetValue("CheckID").Split(','));
            }
        }


        public static T_CP_ProjectPlan GetPublishedPlan(string projectInfoID)
        {
            var entities = FormulaHelper.GetEntities<ProjectEntities>();
            if (String.IsNullOrEmpty(projectInfoID))
                return null;
            var publishedList = entities.Set<T_CP_ProjectPlan>().Where(d => d.ProjectInfoID == projectInfoID && d.IsRelease == "T").OrderByDescending(c=>c.Version).ToList();
            if (publishedList.Count == 0)
                return null;

            return publishedList.FirstOrDefault();
            //int maxVersion = 1;
            //if (publishedList.Max(p => p.Version) != null)
            //    maxVersion = Convert.ToInt32(publishedList.Max(p => p.Version));
            //return publishedList.FirstOrDefault(p => p.Version == maxVersion);
        }


        public static T_CP_ProjectPlan GetCurrentPlan(string projectInfoID)
        {
            var entities = FormulaHelper.GetEntities<ProjectEntities>();
            if (String.IsNullOrEmpty(projectInfoID))
                return null;
            var entity = entities.Set<T_CP_ProjectPlan>().FirstOrDefault(d => d.ProjectInfoID == projectInfoID && d.IsCurrent == "T");
            return entity;
        }
    }
}
