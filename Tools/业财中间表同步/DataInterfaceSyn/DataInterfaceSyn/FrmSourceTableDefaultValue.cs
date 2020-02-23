using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataInterfaceSyn
{
    public partial class FrmSourceTableDefaultValue : Form
    {
        private List<TableModel> _tableModelList;
        private Button btn = new Button();

        public FrmSourceTableDefaultValue()
        {
            InitializeComponent();

            btn.Visible = false;
            btn.Text = "列的默认值设置";
            btn.Click += this.button_Click;
            this.btn.Size = new Size(77, 20);
            this.lvTable.Controls.Add(btn);
            
            _tableModelList = new List<TableModel>();
        }

        public DialogResult ShowDialog(List<TableModel> tableModelList)
        {
            _tableModelList = tableModelList;
            foreach (var item in tableModelList)
            {
                lvTable.Items.Add(new ListViewItem(new string[] { item.TableName, "" }));
            }
            
            return this.ShowDialog();
        }

        private void button_Click(object sender, EventArgs e)
        {
            string tableName = this.lvTable.SelectedItems[0].SubItems[0].Text;
            TableModel tm = _tableModelList.FirstOrDefault(a => a.TableName == tableName);
            FrmTargetTableColumn frm = new FrmTargetTableColumn();
            frm.ShowDialog(tm.Columns);
        }

        private void lvTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvTable.SelectedItems.Count > 0)
            {
                this.btn.Location = new Point(this.lvTable.SelectedItems[0].SubItems[1].Bounds.Left,
                    this.lvTable.SelectedItems[0].SubItems[1].Bounds.Top);
                this.btn.Visible = true;
            }
        }

        private void lvTable_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvTable.Columns[e.ColumnIndex].Width;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
