using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Logic
{
    public enum picture_form
    {
        Person = 1,
        Company = 2,
        Quality = 3
    }

    public enum quality_type
    {
        [Description("一级注册结构工程师")]
        a1 = 1,
        [Description("二级注册结构工程师")]
        a2 = 2,
        [Description("注册土木工程师（岩土）")]
        a3 = 3,
        [Description("注册电气工程师（供配电）")]
        a4 = 4,
        [Description("注册电气工程师（发输变电）")]
        a5 = 5,
        [Description("一级注册建筑师")]
        a6 = 6,
        [Description("二级注册建筑师")]
        a7 = 7,
        [Description("注册城市规划师")]
        a8 = 8,
        [Description("注册造价工程师")]
        a9 = 9,
        [Description("注册公用设备工程师（给水排水）")]
        a10 = 10,
        [Description("注册公用设备工程师（暖通空调）")]
        a11 = 11,
        [Description("注册公用设备工程师（动力）")]
        a12 = 12,
        [Description("注册咨询工程师（投资）")]
        a13 = 13,
        [Description("上海市咨询专家")]
        a14 = 14,
        [Description("上海市咨询师")]
        a15 = 15,
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }
    }
}