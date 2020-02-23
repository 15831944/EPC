using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_ProductionDistribute : BaseEPModel
    {
        public S_EP_ProductionDistribute()
        {

        }
        public S_EP_ProductionDistribute(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            var nodeDic = this.GetDataDicByID("S_EP_CBSNode", this.ModelDic.GetValue("CBSNode"));
            if (nodeDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到对应的CBS节点，无法调整产值");
            Expenses.Logic.CBSInfoFO.SynCBSInfo(this.ModelDic, SetCBSOpportunity.ProductionSplit);
            #region 记录CBS变更记录
            var subsidyInfo = new Dictionary<string, object>();
            subsidyInfo.SetValue("UseType", ProductionChangeType.ReSplit.ToString());
            subsidyInfo.SetValue("UseCBSNode", nodeDic.GetValue("ID"));
            subsidyInfo.SetValue("UseCBSInfo", nodeDic.GetValue("CBSInfoID"));
            subsidyInfo.SetValue("UseValue", 0);
            subsidyInfo.SetValue("OperationUser", this.ModelDic.GetValue("CreateUserID"));
            subsidyInfo.SetValue("OperationUserName", this.ModelDic.GetValue("CreateUser"));
            subsidyInfo.SetValue("UseDate", DateTime.Now);
            subsidyInfo.SetValue("UseReason", this.ModelDic.GetValue("Reason"));
            subsidyInfo.SetValue("ApplyFormID", this.ModelDic.GetValue("ID"));
            subsidyInfo.SetValue("ApplyFormCode", "ProductionDistribute");
            subsidyInfo.InsertDB(this.DB, "S_EP_ProductionCBSChangeLog");
            #endregion
        }
    }
}
