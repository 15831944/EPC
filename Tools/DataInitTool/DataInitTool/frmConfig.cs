using Common.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataInitTool
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            string configFile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Settings.json";
            if (!System.IO.File.Exists(configFile))
            {
                return;
            }
            var configJSON = string.Empty;
            configJSON = File.ReadAllText(configFile);
            if (String.IsNullOrEmpty(configJSON))
            {
                return;
            }
            var config = JsonHelper.ToObject(configJSON);
            this.tbDefautUserID.Text = config.GetValue("DefaultUser");
            this.tbDefaultUserName.Text = config.GetValue("DefaultUserName");
            if (config["TemplateConfig"] != null)
            {
                var templateConfigs = JsonHelper.ToList(config["TemplateConfig"].ToString());
                foreach (var item in templateConfigs)
                {
                    var rowIndex = this.dgvConfig.Rows.Add();
                    var row = this.dgvConfig.Rows[rowIndex];
                    row.Cells["ID"].Value = rowIndex;
                    row.Cells["SheetName"].Value = item.GetValue("SheetName");
                    row.Cells["TableName"].Value = item.GetValue("TableName");
                    row.Cells["DB"].Value = item.GetValue("DB");
                    row.Cells["Required"].Value = item.GetValue("Required");
                    row.Cells["Unique"].Value = item.GetValue("Unique");
                    row.Cells["SelectorFields"].Value = item.GetValue("SelectorFields");
                }
            }
        }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var confiWin = new frmSheetConfig();
            var result = confiWin.ShowDialog();
            var data = confiWin.Data;
            if (data.Count != 0)
            {
                var rowIndex = this.dgvConfig.Rows.Add();
                var row = this.dgvConfig.Rows[rowIndex];
                row.Cells["ID"].Value = rowIndex;
                row.Cells["SheetName"].Value = data.GetValue("SheetName");
                row.Cells["TableName"].Value = data.GetValue("TableName");
                row.Cells["DB"].Value = data.GetValue("DB");
                row.Cells["Required"].Value = data.GetValue("Required");
                row.Cells["Unique"].Value = data.GetValue("Unique");
                row.Cells["SelectorFields"].Value = data.GetValue("SelectorFields");
            }
        }

        private void tsBtnEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvConfig.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            var row = this.dgvConfig.SelectedRows[0];
            var data = new Dictionary<string, object>();
            data.SetValue("ID", row.Cells["ID"].Value);
            data.SetValue("SheetName", row.Cells["SheetName"].Value);
            data.SetValue("TableName", row.Cells["TableName"].Value);
            data.SetValue("DB", row.Cells["DB"].Value);
            data.SetValue("Required", row.Cells["Required"].Value);
            data.SetValue("Unique", row.Cells["Unique"].Value);
            data.SetValue("SelectorFields", row.Cells["SelectorFields"].Value);
            var configWin = new frmSheetConfig(data);
            configWin.ShowDialog();
            var modifyData = configWin.Data;
            row.Cells["SheetName"].Value = modifyData.GetValue("SheetName");
            row.Cells["TableName"].Value = modifyData.GetValue("TableName");
            row.Cells["DB"].Value = modifyData.GetValue("DB");
            row.Cells["Required"].Value = modifyData.GetValue("Required");
            row.Cells["Unique"].Value = modifyData.GetValue("Unique");
            row.Cells["SelectorFields"].Value = modifyData.GetValue("SelectorFields");
        }

        private void tsBtnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvConfig.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一条记录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            }
            this.dgvConfig.Rows.Remove(this.dgvConfig.SelectedRows[0]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var configInfo = new Dictionary<string, object>();
            configInfo.SetValue("DefaultUser", this.tbDefautUserID.Text);
            configInfo.SetValue("DefaultUserName", this.tbDefaultUserName.Text);
            var templateConfigs = new List<Dictionary<string, object>>();
            for (int i = 0; i < this.dgvConfig.Rows.Count; i++)
            {
                var row = this.dgvConfig.Rows[i];
                var item = new Dictionary<string, object>();
                item.SetValue("ID", row.Cells["ID"].Value);
                item.SetValue("SheetName", row.Cells["SheetName"].Value);
                item.SetValue("TableName", row.Cells["TableName"].Value);
                item.SetValue("DB", row.Cells["DB"].Value);
                item.SetValue("Required", row.Cells["Required"].Value);
                item.SetValue("Unique", row.Cells["Unique"].Value);
                item.SetValue("SelectorFields", row.Cells["SelectorFields"].Value);
                templateConfigs.Add(item);
            }
            configInfo.SetValue("TemplateConfig", templateConfigs);
            var json = JsonHelper.ToJson(configInfo);
            string configFile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\Settings.json";
            System.IO.File.WriteAllText(configFile, json);
            this.Close();
            this.Dispose();
        }
    }
}
