using FilePrintMiniTool.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilePrintMiniTool
{
    public partial class TestForm : Form
    {
        /// <summary>
        /// 当前的打印机文档对象
        /// </summary>
        private PrintDocument _printer;
        public TestForm()
        {
            InitializeComponent();
            _printer = new PrintDocument();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("路径不能为空");
                return;
            }

            if (!File.Exists(txtPath.Text))
            {
                MessageBox.Show("文件不存在");
                return;
            }

            string printerName = cBoxPrint.SelectedItem.ToString();
            if (radioButton1.Checked)
            {
                string str = PrinterHelper.SendFileToPrinter(printerName, txtPath.Text) ? "成功" : "失败";
                if (!string.IsNullOrEmpty(str))
                {
                    MessageBox.Show(str);
                }
                else
                {
                    MessageBox.Show("发送成功");
                }
            }
            else
            {
                //纸张样式
                string selectedPaper = cBoxPaperSize.SelectedItem.ToString();
                string paperSize = "";
                int paperH = 0, paperW = 0;

                string[] tmps = selectedPaper.TrimStart('[').TrimEnd(']').Split(',');
                 
                paperSize = tmps[0];
                string[] wh = tmps[1].Split('*');

                
                //PrinterUnitConvert.Convert()
                paperW = (int)(Helper.MMToInch(Convert.ToInt32(wh[0])) * 100);
                paperH = (int)(Helper.MMToInch(Convert.ToInt32(wh[1])) * 100);

                PaperSize paper = new PaperSize(paperSize, paperW, paperH);

                //纵横向
                bool isVertical = rBtnVertical.Checked;
                //份数
                int copies = (int)numericUpDownCount.Value;

                string errorStr = PrinterHelper.UsePDFRender4NetToPrintPdf(txtPath.Text, printerName, paper, isVertical, copies);
                if (!string.IsNullOrEmpty(errorStr))
                {
                    MessageBox.Show(errorStr);
                }
                else
                {
                    MessageBox.Show("发送成功");
                }
            }           
        }
              

        private void btnReloadPrinter_Click(object sender, EventArgs e)
        {
            //_printer = new PrintDocument();
            cBoxPrint.Items.Clear();
            string sDefault = _printer.PrinterSettings.PrinterName;//默认打印机名
            //LocalPrintServer printServer = new LocalPrintServer();
            //PrintQueueCollection queery = new PrintQueueCollection();

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                cBoxPrint.Items.Add(sPrint);
                if (sPrint == sDefault)
                    cBoxPrint.SelectedIndex = cBoxPrint.Items.IndexOf(sPrint);
            }
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            //加载纸张大小类型
            cBoxPaperSize.DataSource = Helper.GetPaperSizes().ToList();
            btnReloadPrinter_Click(null, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = radioButton2.Checked;            
               
        }

        private void cBoxPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            string state = PrinterHelper.GetPrinterStatus(cBoxPrint.SelectedItem.ToString());
            MessageBox.Show(state);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }
    }
}
