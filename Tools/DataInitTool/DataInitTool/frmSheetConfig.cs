using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logic;

namespace DataInitTool
{
    public partial class frmSheetConfig : Form
    {
        Dictionary<string, object> _dic = new Dictionary<string, object>();
        public Dictionary<string, object> Data
        {
            get
            {
                return _dic;
            }
        }

        public frmSheetConfig()
        {
            InitializeComponent();
        }

        public frmSheetConfig(Dictionary<string, object> data = null)
        {
            InitializeComponent();
            if (data != null)
            {
                _dic = data;
            }
        }

        private void frmSheetConfig_Load(object sender, EventArgs e)
        {
            this.tbDB.Text = this.Data.GetValue("DB");
            this.tbSheetName.Text = this.Data.GetValue("SheetName");
            this.tbTableName.Text = this.Data.GetValue("TableName");
            this.tbRequired.Text = this.Data.GetValue("Required");
            this.tbUnique.Text = this.Data.GetValue("Unique");

            var selecotor = this.Data.GetValue("SelectorFields");
            if (!String.IsNullOrEmpty(selecotor))
            {
                var list = JsonHelper.ToList(selecotor);
                foreach (var item in list)
                {
                    var rowIndex = this.dgvSelector.Rows.Add();
                    var row = this.dgvSelector.Rows[rowIndex];
                    row.Cells["FieldName"].Value = item.GetValue("FieldName");
                    row.Cells["SheetName"].Value = item.GetValue("SheetName");
                    row.Cells["RelateFieldName"].Value = item.GetValue("RelateFieldName");
                    row.Cells["ReturnValue"].Value = item.GetValue("ReturnValue");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Data.SetValue("DB", tbDB.Text);
            this.Data.SetValue("TableName", tbTableName.Text);
            this.Data.SetValue("SheetName", tbSheetName.Text);
            this.Data.SetValue("Required", this.tbRequired.Text);
            this.Data.SetValue("Unique", this.tbUnique.Text);

            var selectors = new List<Dictionary<string, object>>();
            for (int i = 0; i < this.dgvSelector.Rows.Count; i++)
            {
                var row = this.dgvSelector.Rows[i];
                var item = new Dictionary<string, object>();
                item.SetValue("FieldName", row.Cells["FieldName"].Value);
                item.SetValue("SheetName", row.Cells["SheetName"].Value);
                item.SetValue("RelateFieldName", row.Cells["RelateFieldName"].Value);
                item.SetValue("ReturnValue", row.Cells["ReturnValue"].Value);
                selectors.Add(item);
            }
            var selectorJSON = JsonHelper.ToJson(selectors);
            this.Data.SetValue("SelectorFields", selectorJSON);
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dgvSelector.Rows.Add();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dgvSelector.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            for (int i = 0; i < this.dgvSelector.SelectedRows.Count; i++)
            {
                var row = this.dgvSelector.SelectedRows[i];
                this.dgvSelector.Rows.Remove(row);
            }
        }
    }
}
