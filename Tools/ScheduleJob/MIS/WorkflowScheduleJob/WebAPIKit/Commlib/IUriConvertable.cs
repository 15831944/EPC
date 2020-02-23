using System;
using System.Collections.Generic;
using System.Text;

namespace Common.WebAPIKit.Commlib
{
    /// <summary>
    /// 生成：?UserName=XXXX&Age=30
    /// </summary>
    public interface IUriConvertable
    {
        string QueryString { get; }
    }
}
