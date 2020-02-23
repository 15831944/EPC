using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterfaceSyn
{
    [Serializable]
    public class TableModel
    {
        public string TableName { get; set; }
        public string ForeignTableName { get; set; }
        public string ForeignKeyField { get; set; }

        public List<TableModelColumn> Columns { get; set; }

        public TableModel()
        {
            Columns = new List<TableModelColumn>();
        }

        public bool IsMain
        {
            get
            {
                return string.IsNullOrEmpty(ForeignTableName);
            }
        }
    }

    [Serializable]
    public class TableModelColumn
    {
        public string Name { get; set; }
        public bool IsPK { get; set; }
        public string ValueType { get; set; }
        public bool AllowEmpty { get; set; }
        public string DefaultValue { get; set; }
    }
}
