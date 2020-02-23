using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MarketScheduleJob
{
    public class Enum
    {
        //提醒类型
        public enum AlarmTypeEnum
        {
            [Description("重点客户联系预警")]
            ImportantCustomerLinkCustomerWarning,
            [Description("未归档(两周内)合同预警")]
            ManageContractArchiveWarning,
            [Description("未审批结束(两周内)合同预警")]
            ManageContractExamineWarning,
            [Description("前期项目跟踪信息填写预警")]
            MarketProjectTraceAddWarning,
            [Description("计划收款预警")]
            ReceiptPlanWarning,
        }
    }
}
