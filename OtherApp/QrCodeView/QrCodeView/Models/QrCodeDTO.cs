using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrCodeView.Models
{
    public class QrCodeDTO
    {
        public string ID { get; set; }
        public string Type { get; set; } //类型
        public string Title { get; set; } //标题
        public string SubTitle { get; set; } //副标题，跟在标题后显示
        public List<DicItem> FormDic { get; set; } //表单字段与值
        public List<DicItem> SubFormDic { get; set; } //在标题和表单字段中间显示
        public string AuditStep { get; set; } //校审环节

        public bool State { get; set; }
        public string Message { get; set; }
    }

    public class DicItem
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string IconCls { get; set; }
    }

    /// <summary>
    ///图框属性对应表栏位信息
    /// </summary>
    public class AttburiteColInfo
    {
        //属性中文名
        public string Name { get; set; }
        //属性值
        public string Value { get; set; }
        //数据库表列名
        public string DBColName { get; set; }
    }

    public enum ValueFrom
    {
        FrameInfo,
        FieldValue,
        System,
    }

    public enum WBSType
    {
        SubProject,
        Area,
        Device,
        Work,
        Major
    }

    public enum TableName
    {
        S_I_ProjectInfo,
        S_E_ProductVersion,
        S_W_WBS
    }
}