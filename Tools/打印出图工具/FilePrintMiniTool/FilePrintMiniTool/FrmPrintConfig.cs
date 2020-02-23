using FilePrintMiniTool.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Printing;

namespace FilePrintMiniTool
{

    public partial class FrmPrintConfig : Form
    {
        public class PrintConfigData
        {
            public string ID { get; set; }
            /// <summary>
            /// 纸张格式
            /// </summary>
            public string PaperSize { get; set; }
            /// <summary>
            /// 是否竖打
            /// </summary>
            public bool IsVertical { get; set; }
            /// <summary>
            ///  
            /// </summary>
            public string IsVerticalDescrib
            {
                get
                {
                    return IsVertical ? "纵向" : "横向";
                }
            }
        }

        public List<PrintConfigData> ConfigDatas { get; set; }
        public FrmPrintConfig()
        {
            InitializeComponent();
            ConfigDatas = new  List<PrintConfigData>();
            //加载纸张大小类型
            cBoxPaperSize.DataSource = Helper.GetPaperSizes().ToList();
        }

        public DialogResult ShowDialog(List<PrintConfigData> configDatas)
        {
            ConfigDatas = configDatas;
            if (configDatas.Count > 0)
            {
                //如果只选中一条信息则读取其配置
                if (configDatas.Count == 1)
                {
                    //读取纸张配置数据
                    foreach (object obj in cBoxPaperSize.Items)
                    {
                        if (obj.ToString().Contains(configDatas[0].PaperSize))
                        {
                            cBoxPaperSize.SelectedIndex = cBoxPaperSize.Items.IndexOf(obj);
                        }
                    }

                    rBtnVertical.Checked = configDatas[0].IsVertical;
                    rBtnHorizontal.Checked = !configDatas[0].IsVertical;
                }
                return this.ShowDialog();
            }

            return System.Windows.Forms.DialogResult.No;
        }

        private void FrmPrintConfig_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string selectedPaper = cBoxPaperSize.SelectedItem.ToString();
            string[] tmps = selectedPaper.TrimStart('[').TrimEnd(']').Split(',');
            Debug.Assert(tmps.Count() == 2);

            foreach (PrintConfigData ConfigData in ConfigDatas)
            {
                ConfigData.PaperSize = tmps[0];
                ConfigData.IsVertical = rBtnVertical.Checked;
            }
            
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
