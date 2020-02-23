using Common.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInitTool
{
    public interface IValidation
    {
        List<CellErrorInfo> ValidateSheet(GlobalData gData);
    }
}
