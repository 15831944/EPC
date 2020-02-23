using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Expenses.Logic
{
    public enum CommonState
    {
        [Description("在建")]
        Execute,
        [Description("暂停")]
        Pause,
        [Description("终止")]
        Termination,
        [Description("关闭")]
        Close,
        [Description("移除")]
        Removed
    }

    public enum ModifyState
    {
        [Description("新增")]
        Added,
        [Description("修改")]
        Modified,
        [Description("未更改")]
        Normal,
        [Description("删除")]
        Removed,
    }

    public enum CBSDefineType
    {
        [Description("公司")]
        Company,
        [Description("部门")]
        Org,
        [Description("生产")]
        Produce,
        [Description("管理")]
        Manage,
        [Description("研发")]
        Research,
        [Description("市场")]
        Market,
        [Description("其他")]
        Other
    }

    public enum CBSNodeType
    {
        [Description("根")]
        Root,
        [Description("工程")]
        Engineering,
        [Description("合同")]
        Contract,
        [Description("项目")]
        Project,
        [Description("子项")]
        SubProject,
        [Description("阶段")]
        Phase,
        [Description("专业")]
        Major,
        [Description("组织部门")]
        Org,
        [Description("CBS")]
        CBS,
        [Description("科目")]
        Subject,
        [Description("明细")]
        Detail,
        [Description("工序")]
        Process,
        [Description("公积")]
        Communal,
        [Description("预留")]
        Reserved
    }

    public enum AccountNodeType
    {
        [Description("核算节点")]
        Account,
        [Description("产值节点")]
        Production
    }

    public enum SubjectType
    {
        /// <summary>
        /// 人工费
        /// </summary>
        [Description("人工费")]
        HRCost,

        /// <summary>
        /// 直接费
        /// </summary>
        [Description("直接费")]
        DirectCost,

        /// <summary>
        /// 间接费
        /// </summary>
        [Description("间接费")]
        InDirectCost,

        /// <summary>
        /// 分包
        /// </summary>
        [Description("委外采购费")]
        SubContractCost,

        /// <summary>
        /// 管理费用
        /// </summary>
        [Description("管理费用")]
        Manage,

        /// <summary>
        /// 财务费用
        /// </summary>
        [Description("财务费用")]
        Finance,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other
    }

    public enum IncomeType
    {
        [Description("节点确认法")]
        Progress,
        [Description("成本确认法")]
        Cost,
        [Description("报量确认法")]
        Submit,
        [Description("发票确认法")]
        Invoice
    }

    public enum ProductionType
    {
        [Description("节点确认法")]
        Progress,
        [Description("到款确认法")]
        Income,
        [Description("报量确认法")]
        Submit
    }

    public enum ProductionChangeType
    {
        [Description("补贴")]
        Subsidy,
        [Description("扣减")]
        Deduction,
        [Description("重新分配")]
        ReSplit
    }

    public enum IncomeState
    {
        [Description("待确认")]
        Wait,
        [Description("已结账")]
        Finish,
        [Description("已删除")]
        Removed
    }

    public enum IncomeValidateMode
    {
        [Description("不校验")]
        None,
        [Description("前月份校验")]
        OnlyBefore,
        [Description("后月份校验")]
        OnlyAfter,
        [Description("前后月份校验")]
        BeforeAndAfter
    }

    public enum IncomeAdjustValidateMode
    {
        [Description("不校验")]
        None,
        [Description("后月份校验")]
        OnlyAfter
    }

    public enum CloseOperationEnum
    {
        [Description("锁定成本")]
        LockCost,
        [Description("锁定成本与收入")]
        LockCostAndIncome
    }

    public enum SetCBSOpportunity
    {
        [Description("前期项目立项时")]
        MarketProjectAdd,
        [Description("合同保存时")]
        Contract,
        [Description("工程信息保存时")]
        Engineering,
        [Description("任务单下达时")]
        TaskNoticeComplete,
        [Description("核算立项时")]
        CBSInfoBuilder,
        [Description("策划表完成时")]
        SchemaComplete,
        [Description("产值下达时")]
        ProductionInput,
        [Description("产值分解时")]
        ProductionSplit,
        [Description("产值调整时")]
        ProductionAdjust
    }

    public enum ExpenseType
    {
        [Description("汇总")]
        Summary,
        [Description("可分摊")]
        Apportion
    }

    public struct Condition
    {
        public string GroupName { get; set; }
        public bool Result { get; set; }
        public string FieldName { get; set; }
    }




}
