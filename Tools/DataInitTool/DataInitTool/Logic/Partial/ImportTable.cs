using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInitTool
{
    public partial class ImportTable
    {
        partial void BeforeInsert(System.Data.DataRow row, string SheetName, string TableName)
        {
            if (TableName == "S_I_Engineering")
            {
                //var insertSQL = "insert into T_I_EngineeringBuild () values ()";
            }
        }

        partial void AfterInsert(System.Data.DataRow row, string SheetName, string TableName)
        {

        }
    }
}
