using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FilePrintMiniTool.CS
{
    /// <summary>
    /// 纸张型号及对应尺寸(mm)
    /// </summary>
    public enum EnumPaperSize
    {
        [Description("841*1189")]
        A0,
        [Description("594*841")]
        A1,
        [Description("420*594")]
        A2,
        [Description("297*420")]
        A3,
        [Description("210*297")]
        A4
    }
    
}
