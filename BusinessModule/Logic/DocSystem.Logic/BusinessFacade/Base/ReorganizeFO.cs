using DocSystem.Logic.Domain;
using Formula;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocSystem.Logic
{
    public class ReorganizeFO
    {
        public void CreateReorganize(string spaceKey, S_R_Reorganize reorganizeTask)
        {
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var docConfigEntities = FormulaHelper.GetEntities<DocConfigEntities>();
            var currentUser = FormulaHelper.GetUserInfo();
            var space = docConfigEntities.Set<S_DOC_Space>().FirstOrDefault(a => a.SpaceKey == spaceKey);
            if (space == null)
                throw new Formula.Exceptions.BusinessException("送整编错误：未能找到编号为【" + spaceKey + "】的图档空间配置");
            if (reorganizeTask.S_R_Reorganize_DocumentList == null || reorganizeTask.S_R_Reorganize_DocumentList.Count == 0)
                throw new Formula.Exceptions.BusinessException("送整编错误：明细列表不能为空");
            if (string.IsNullOrEmpty(reorganizeTask.ID))
                reorganizeTask.ID = FormulaHelper.CreateGuid();
            if (string.IsNullOrEmpty(reorganizeTask.SendUser))
            {
                reorganizeTask.SendUser = currentUser.UserID;
                reorganizeTask.SendUserName = currentUser.UserName;
            }
            if (reorganizeTask.SendDate == null)
                reorganizeTask.SendDate = new DateTime?(DateTime.Now);
            reorganizeTask.SpaceID = space.ID;
            reorganizeTask.ReorganizeState = ReorganizeState.Create.ToString();
            reorganizeTask.DocumentList = JsonHelper.ToJson<List<S_R_Reorganize_DocumentList>>(reorganizeTask.S_R_Reorganize_DocumentList.ToList<S_R_Reorganize_DocumentList>());
            foreach (var item in reorganizeTask.S_R_Reorganize_DocumentList)
            {
                if (string.IsNullOrEmpty(item.ID))
                item.ID = FormulaHelper.CreateGuid();
                item.S_R_ReorganizeID = reorganizeTask.ID;
            }
            entities.Set<S_R_Reorganize>().Add(reorganizeTask);
            entities.SaveChanges();
        }
    }
}
