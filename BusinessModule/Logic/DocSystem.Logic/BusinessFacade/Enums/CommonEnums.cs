using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DocSystem.Logic
{
    [Description("整编状态")]
    public enum ReorganizeState
    {
        [Description("待整编")]
        Create,
        [Description("整编中")]
        Execute,
        [Description("整编完成")]
        Finish
    }
    [Description("整编状态")]
    public enum PhysicalReorganizeState
    {
        [Description("已签收")]
        Create,
        [Description("整编中")]
        Execute,
        [Description("整编完成")]
        Finish,
        [Description("待整编")]
        Wait
    }
    [Description("文件类别")]
    public enum FileCategory
    {
        [Description("项目成果资料")]
        Product,
        [Description("项目ISO表单")]
        ISO,
        [Description("项目设计输入")]
        DesignInput,
        [Description("项目图集资料")]
        Atlas,
        [Description("其他资料")]
        Other,
    }

    [Description("文件存储类型")]
    public enum StorageType
    {
        [Description("电子")]
        Electronic,
        [Description("实物")]
        Physical,
    }

    [Description("出入库节点类型")]
    public enum InventoryRelateType
    {
        [Description("节点")]
        Node,
        [Description("文件")]
        File
    }

    [Description("出入库类型")]
    public enum InventoryType
    {
        [Description("入库")]
        StorageIn,
        [Description("补充")]
        Replenish,
        [Description("借出")]
        Lend,
        [Description("归还")]
        Return,
        [Description("遗失")]
        Lose,
        [Description("损毁")]
        Destroy,
        [Description("变更保管期限")]
        ChangePeriod,
        [Description("封存")]
        SealUp,
        [Description("修改")]
        Update,
        [Description("浏览")]
        View,
        [Description("借阅")]
        BorrowFile,
        [Description("下载")]
        DownLoad,
        [Description("解封")]
        RelieveSeal
    }

    [Description("借阅状态")]
    public enum BorrowDetailState
    {
        [Description("待借阅")]
        ToLend,
        [Description("待归还")]
        ToReturn,
        [Description("完成")]
        Finish
    }

    [Description("鉴定状态")]
    public enum IdentifyState
    {
        [Description("待鉴定")]
        Create,
        [Description("鉴定中")]
        InFlow,
        [Description("鉴定完成")]
        Identified,
        [Description("送销毁")]
        InDestroy,
        [Description("送变更保存期限")]
        InChangePeriod,
        [Description("完成")]
        Finish
    }

    [Description("处理状态")]
    public enum HandleState
    {
        [Description("未处理")]
        Create,
        [Description("部分处理")]
        Part,
        [Description("处理完")]
        Finish
    }

    [Description("补录状态")]
    public enum ReplenishState
    {
        [Description("未补录")]
        Unreplenished,
        [Description("部分补录")]
        PartReplenish,
        [Description("已补录")]
        Replenish
    }

    [Description("遗失损毁状态")]
    public enum LostReplenishState
    {
        [Description("遗失")]
        Lose,
        [Description("损毁")]
        Destroy
    }
}
