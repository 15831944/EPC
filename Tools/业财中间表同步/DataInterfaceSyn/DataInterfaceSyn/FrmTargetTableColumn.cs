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
    public partial class FrmTargetTableColumn : Form
    {
        private List<TableModelColumn> _columns;
        public FrmTargetTableColumn()
        {
            InitializeComponent();
            dgvContent.AutoGenerateColumns = false;
            _columns = new List<TableModelColumn>();
        }

        public void ShowDialog(List<TableModelColumn> columns)
        {
            _columns = columns;
            dgvContent.DataSource = columns;
            this.ShowDialog();
        }

        private void dgvContent_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
             
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
