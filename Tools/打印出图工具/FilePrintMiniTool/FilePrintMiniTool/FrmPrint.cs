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

    public partial class FrmPrint : Form
    {
        public class MutltiPrintData
        {
            public string ID { get; set; }
            /// <summary>
            /// 图名
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 附件名称
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// 是否已打印完毕
            /// </summary>
            public bool IsPrinted { get; set; }
            /// <summary>
            /// 纸张格式
            /// </summary>
            public string PaperSize { get; set; }
            /// <summary>
            /// 是否竖打
            /// </summary>
            public bool IsVertical { get; set; }
            /// <summary>pdf文件名称</summary>
            public string PdfFile { get; set; } // PdfFile
            /// <summary>plot文件名称</summary>
            public string PlotFile { get; set; } // PlotFile
            /// <summary>
            /// 是否竖打
            /// </summary>
            public string IsVerticalDescrib
            {
                get
                {
                    return IsVertical ? "纵向" : "横向";
                }
            }
            /// <summary>
            /// 打的份数
            /// </summary>
            public int Count { get; set; }
            /// <summary>
            /// 打印返回信息
            /// </summary>
            public string PrintInfo { get; set; }
            /// <summary>
            /// 打开的路径
            /// </summary>
            public string LocalFilePath { get; set; }
        }

        /// <summary>
        /// 打印发送成功后执行
        /// </summary>
        public Action<string, int> AfterSendToPrinter;
        /// <summary>
        /// 当前的打印机文档对象
        /// </summary>
        private PrintDocument _printer;
        private List<MutltiPrintData> _printDataList 
        { 
            get { return (List<MutltiPrintData>)dgvContent.DataSource; }
        }
        public FrmPrint()
        {
            InitializeComponent();
            dgvContent.AutoGenerateColumns = false;
            dgvContent.DataError += dgvContent_DataError;
            _printer = new PrintDocument();
        }

        private void dgvContent_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.GetType() == typeof(FormatException))
            {
                MessageBox.Show("第" + (e.RowIndex + 1) + "行,第" + (e.ColumnIndex + 1) + "列,数据格式有误");
            }
            else
            {
                throw e.Exception;
            }            
        }

        public DialogResult ShowDialog(List<MutltiPrintData> printDatas)
        {
            if (printDatas.Count > 0)
            {
                dgvContent.DataSource = printDatas;
                return this.ShowDialog();
            }

            return System.Windows.Forms.DialogResult.No;
        }

        private void FrmPrintConfig_Load(object sender, EventArgs e)
        {
            //加载打印机
            btnReloadPrinter_Click(null, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!_printDataList.Exists(a => !a.IsPrinted))
            {
                MessageBox.Show("没有需要打印的内容");
                return;
            }

            if (cBoxPrint.SelectedItem == null)
            {
                MessageBox.Show("请选择打印机");
                return;
            }
            else if (PrinterHelper.GetPrinterStatusInt(cBoxPrint.SelectedItem.ToString()) != 0)
            {
                MessageBox.Show(PrinterHelper.GetPrinterStatus(cBoxPrint.SelectedItem.ToString()));
                return;
            }

            //打印机名称
            string printerName = "";
            PrinterSettings settings = new PrinterSettings();
            printerName = cBoxPrint.SelectedItem.ToString();

            btnPrint.Enabled = false;
            foreach (MutltiPrintData printData in _printDataList.Where(a => !a.IsPrinted))
            {
                #region 设置打印参数
                DataGridViewRow row = GetRow(printData.ID);
                row.Cells["colPrintInfo"].Value = "发送打印中...";

                //纸张规格
                Dictionary<string, string> paperList = Helper.GetPaperSizes();
                if (!paperList.ContainsKey(printData.PaperSize))
                {
                    row.Cells["colPrintInfo"].Value = "未找到打印纸为" + printData.PaperSize + "的尺寸参数";
                    continue;
                }
                string[] wh = paperList[printData.PaperSize].Split('*');
                int paperW = (int)(Helper.MMToInch(Convert.ToInt32(wh[0])) * 100);
                int paperH = (int)(Helper.MMToInch(Convert.ToInt32(wh[1])) * 100);
                PaperSize paper = new PaperSize(printData.PaperSize, paperW, paperH);

                //是否竖打
                bool isVertical = Convert.ToBoolean(row.Cells["colIsVertical"].Value);
                //打印份数
                int count = 1;
                if (!Int32.TryParse(row.Cells["colCount"].Value.ToString(), out count))
                {
                    row.Cells["colPrintInfo"].Value = "打印份数格式错误";
                    continue;
                }

                #endregion

                #region 从文件服务器拷贝源文件至本机

                //文件类型判断
                string fileName = printData.PdfFile;
                string fileExt = "";
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = printData.PlotFile;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExt = System.IO.Path.GetExtension(fileName);
                }
                if (string.IsNullOrEmpty(fileExt))
                {
                    row.Cells["colPrintInfo"].Value = "无法判断文件类型";
                    continue;
                }
                else if (fileExt.ToUpper() != ".PDF")
                {
                    row.Cells["colPrintInfo"].Value = "只支持pdf格式";
                    continue;
                }

                //文件拷贝本地及获取本地路径                
                string filePath = "";
                string FieldID = printData.PdfFile.Split('_')[0];
                string errorInfo = Helper.CopyFile(ref filePath, FieldID);
                if (!string.IsNullOrEmpty(errorInfo))
                {
                    row.Cells["colPrintInfo"].Value = errorInfo;
                    continue;
                }

                ////test文件拷贝本地及获取本地路径                
                //string filePath = "";
                //string errorInfo = Helper.TestCopyFile(ref filePath, ConfigurationManager.AppSettings["TestPrintFile"]);
                //if (!string.IsNullOrEmpty(errorInfo))
                //{
                //    row.Cells["colPrintInfo"].Value = errorInfo;
                //    continue;
                //}
                //string fileExt = System.IO.Path.GetExtension(ConfigurationManager.AppSettings["TestPrintFile"]);

                #endregion

                #region 开始发送打印
                string printErrorInfo = PrinterHelper.UsePDFRender4NetToPrintPdf(filePath, printerName, paper, isVertical, count);

                //删除临时文件
                Helper.DelFile(filePath);

                if (!string.IsNullOrEmpty(printErrorInfo))
                {
                    row.Cells["colPrintInfo"].Value = printErrorInfo;
                    continue;
                }

                #endregion

                #region 更新数据库信息
                printData.IsPrinted = true;
                row.Cells["colPrintInfo"].Value = "已发送至打印机";
                if (AfterSendToPrinter != null)
                {
                    AfterSendToPrinter(printData.ID, printData.Count);
                }
                #endregion
            }
            btnPrint.Enabled = true;
        }

        private void btnExtend_Click(object sender, EventArgs e)
        {

        }

        private DataGridViewRow GetRow(string ID)
        {
            foreach (DataGridViewRow row in dgvContent.Rows)
            {
                if (ID == row.Cells["colID"].Value.ToString())
                {
                    return row;
                }
            }
            return null;
        }

        private void btnReloadPrinter_Click(object sender, EventArgs e)
        {
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

        private void cBoxPrint_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cBoxPrint.SelectedItem != null)
            {
                string printerName = cBoxPrint.SelectedItem.ToString();
                btnPrint.Text = "打印(" + PrinterHelper.GetPrinterStatus(printerName) + ")";
            }
        }

        private void dgvContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvContent.Columns["colDownLoad"].Index)
            {
                var row = dgvContent.Rows[e.RowIndex];
                MutltiPrintData printData = _printDataList.FirstOrDefault(a => a.ID == row.Cells["colID"].Value.ToSafeString());
                if (printData != null)
                {
                    if (string.IsNullOrEmpty(printData.LocalFilePath))
                    {
                        string localFilePath = "";
                        if (string.IsNullOrEmpty(printData.PdfFile))
                        {
                            row.Cells["colPrintInfo"].Value = "PdfFile值为空";
                            return;
                        }
                        var splitRes = printData.PdfFile.Split('_');
                        if (splitRes.Length == 0)
                        {
                            row.Cells["colPrintInfo"].Value = "【" + printData.PdfFile + "】中无法找到文件id";
                            return;
                        }
                        string FieldID = splitRes[0];
                        string fileExt = System.IO.Path.GetExtension(printData.PdfFile);
                        string errorInfo = Helper.CopyFile(ref localFilePath, FieldID, fileExt);
                        if (!string.IsNullOrEmpty(errorInfo))
                        {
                            row.Cells["colPrintInfo"].Value = errorInfo;
                            return;
                        }

                        printData.LocalFilePath = localFilePath;
                        row.Cells["colLocalFilePath"].Value = localFilePath;
                        row.Cells["colDownLoad"].Value = "打开";
                    }
                    else
                    {
                        Helper.OpenLocalFile(printData.LocalFilePath);
                    }
                }
            }
        }
    }
}
