using Project.Logic.Domain;
using Config.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Logic;

namespace Project.Areas.Gantt.Controllers
{
    public class NewMileStoneGanttController : ProjectController<S_P_MileStone>
    {
        public override JsonResult GetTree()
        {
            string projectInfoID = this.Request["ProjectInfoID"];
            var result = GetData(projectInfoID);
            return Json(result);
        }

        public JsonResult SaveProjectPlanStartDate(string ProjectInfoID, string PlanStartDate)
        {
            var projectInfo = this.GetEntityByID<S_I_ProjectInfo>(ProjectInfoID);
            if (projectInfo == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + ProjectInfoID + "】的项目对象");
            DateTime pSDate;
            if (DateTime.TryParse(PlanStartDate, out pSDate))
            {
                projectInfo.PlanStartDate = pSDate;
                projectInfo.WBSRoot.PlanStartDate = pSDate;
                this.entities.SaveChanges();
            }
            return Json("");
        }

        public List<Dictionary<string, object>> GetData(string projectInfoID)
        {
            var result = new List<Dictionary<string, object>>();

            var projectInfo = this.GetEntityByID<S_I_ProjectInfo>(projectInfoID);
            if (projectInfo == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + projectInfoID + "】的项目对象，无法获取里程碑信息");
            var wbsRoot = projectInfo.WBSRoot;
            var mileStoneList = projectInfo.S_P_MileStone.OrderBy(d => d.SortIndex).ToList();
            var root = wbsRoot.ToDic();
            root.SetValue("UID", wbsRoot.ID);
            root.SetValue("WBSID", wbsRoot.ID);
            root.SetValue("Start", GetStartDate(wbsRoot));
            root.SetValue("PlanStartDate", GetStartDate(wbsRoot));
            if (wbsRoot.PlanEndDate.HasValue)
                root.SetValue("Finish", wbsRoot.PlanEndDate.Value.ToShortDateString() + " 23:59:59");
            result.Add(root);
            FillMileStoneGantList(wbsRoot, result, mileStoneList);

            var subProjectList = projectInfo.S_W_WBS.Where(d => d.WBSType == WBSNodeType.SubProject.ToString()).OrderBy(d => d.SortIndex).ToList();
            foreach (var item in subProjectList)
            {
                var subProject = item.ToDic();
                subProject.SetValue("UID", item.ID);
                subProject.SetValue("ParentTaskUID", item.ParentID);
                subProject.SetValue("WBSID", item.ID);

                subProject.SetValue("Start", GetStartDate(item));

                if (item.PlanEndDate.HasValue)
                    subProject.SetValue("Finish", item.PlanEndDate.Value.ToShortDateString() + " 23:59:59");
                result.Add(subProject);

                FillMileStoneGantList(item, result, mileStoneList);
            }

            return result;
        }

        private string GetStartDate(S_W_WBS node)
        {
            var date = "";
            if (node.PlanStartDate.HasValue)
            {
                date = node.PlanStartDate.Value.ToShortDateString() + " 00:00:00";
            }
            else
            {
                if (node.WBSType == WBSNodeType.Project.ToString())
                {
                    if (node.S_I_ProjectInfo.PlanStartDate.HasValue)
                        date = node.S_I_ProjectInfo.PlanStartDate.Value.ToShortDateString() + " 00:00:00";
                }
                else
                    date = GetStartDate(node.Parent);
            }
            return date;
        }

        private void FillMileStoneGantList(S_W_WBS relateWBS, List<Dictionary<string, object>> result, List<S_P_MileStone> mileStoneList)
        {
            var children = mileStoneList.Where(d => d.WBSID == relateWBS.ID).OrderBy(d => d.PlanFinishDate).ThenBy(d => d.SortIndex).ToList();
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                var dic = child.ToDic();
                dic.SetValue("UID", child.ID);
                dic.SetValue("MileStoneID", child.ID);
                dic.SetValue("ParentTaskUID", relateWBS.ID);
                if (child.PlanStartDate.HasValue)
                {
                    dic.SetValue("Start", child.PlanStartDate.Value.ToShortDateString() + " 00:00:00");
                }
                else
                {
                    if (i == 0)
                    {
                        dic.SetValue("Start", GetStartDate(relateWBS));
                    }
                    else
                    {
                        var preChild = children.Where(a => a.PlanFinishDate < child.PlanFinishDate).OrderByDescending(a => a.PlanFinishDate).FirstOrDefault();
                        if (preChild == null)
                            dic.SetValue("Start", GetStartDate(relateWBS));
                        else if (preChild.PlanFinishDate.HasValue)
                            dic.SetValue("Start", (preChild.PlanFinishDate.Value.AddDays(1)).ToShortDateString() + " 00:00:00");
                    }
                }

                if (child.PlanFinishDate.HasValue)
                    dic.SetValue("Finish", child.PlanFinishDate.Value.ToShortDateString() + " 23:59:59");
                dic.SetValue("Critical", "1");
                result.Add(dic);
            }
        }
    }
}
