using Aspose.Cells;
using Common.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataInitTool
{
    public partial class frmMain : Form
    {
        static GlobalData globalData = null;
        static string excelPath = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnValidateData_Click(object sender, EventArgs e)
        {
            this.btnDownTemplate.Enabled = false;
            this.btnImport.Enabled = false;
            this.btnOpenTemplate.Enabled = false;
            this.btnSelectFile.Enabled = false;
            this.btnValidateData.Enabled = false;
            globalData = null;
            this.lbxInfo.Items.Clear();
            Thread thread = new Thread(new ThreadStart(delegate()
            {
                this.ValidateData();
            }));
            thread.Start();
        }

        void ValidateData()
        {
            try
            {
                var errors = new List<CellErrorInfo>();
                globalData = GlobalData.CreateGlobalData(this.tbExcelFilePath.Text);
                if (globalData != null)
                {
                    foreach (var key in globalData.Data.Keys)
                    {
                        var data = globalData.Data[key];
                        if (data == null)
                            throw new Exception("没有找到编号为【" + key + "】的EXCEL数据源，请确认导入格式和模板是否一致");
                        var sw = new Stopwatch(); sw.Start();
                        var validateResult = data.ValidateSheet(globalData);
                        errors.AddRange(validateResult);
                        sw.Stop();
                        var text = String.Format("校验【{0}】信息完成，耗时【{1}】秒；校验到【{2}】个错误", data.SheetName, sw.ElapsedMilliseconds / 1000, validateResult.Count);
                        sw.Reset();
                        Action act = delegate { this.lbxInfo.Items.Add(text); };
                        this.Invoke(act);
                    }
                    if (errors.Count > 0)
                    {
                        Action beForeAct = delegate
                        {
                            this.lbxInfo.Items.Add("校验未通过，正在生成验证错误的EXCEL文件" + globalData.ErrorExcelPath);

                        };
                        this.Invoke(beForeAct);
                        WriteErrorInfoToExcel(errors, globalData.FileBuffer, globalData.ErrorExcelPath, System.Drawing.Color.Red);
                        Action act = delegate
                        {
                            this.lbxInfo.Items.Add("校验未通过，请查看错误信息" + globalData.ErrorExcelPath);
                            this.btnImport.Enabled = false;
                            this.btnValidateData.Enabled = true;
                            this.btnDownTemplate.Enabled = true;
                            this.btnOpenTemplate.Enabled = true;
                            this.btnSelectFile.Enabled = true;
                            this.btnValidateData.Enabled = true;
                        };
                        this.Invoke(act);
                    }
                    else
                    {
                        Action act = delegate
                        {
                            this.lbxInfo.Items.Add("验证通过");
                            this.btnImport.Enabled = true;
                            this.btnValidateData.Enabled = true;
                            this.btnDownTemplate.Enabled = true;
                            this.btnOpenTemplate.Enabled = true;
                            this.btnSelectFile.Enabled = true;
                        };
                        this.Invoke(act);
                    }
                }
                else
                {
                    Action act = delegate
                    {
                        this.lbxInfo.Items.Add("元数据读取错误");
                        this.btnImport.Enabled = true;
                        this.btnValidateData.Enabled = true;
                        this.btnDownTemplate.Enabled = true;
                        this.btnOpenTemplate.Enabled = true;
                        this.btnSelectFile.Enabled = true;
                    };
                    this.Invoke(act);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Error(exp, exp.Message);
                MessageBox.Show(exp.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Action act = delegate
                {
                    this.btnValidateData.Enabled = true;
                };
                this.Invoke(act);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }

        private void btnConfigEdit_Click(object sender, EventArgs e)
        {
            var frm = new frmConfig();
            frm.ShowDialog();
        }

        void WriteErrorInfoToExcel(IList<CellErrorInfo> errors, byte[] fileBuffer, string errorFilePath, System.Drawing.Color color)
        {
            var workbook = new Workbook(new MemoryStream(fileBuffer));
            var worksheetList = new List<Worksheet>();
            for (int i = 0; i < workbook.Worksheets.Count; i++)
                worksheetList.Add(workbook.Worksheets[i]);
            foreach (var info in errors)
            {
                var worksheet = worksheetList.Find(a => a.Name == info.TableName);

                int commentIndex = worksheet.Comments.Add(info.RowIndex, info.ColIndex);

                Comment comment = worksheet.Comments[commentIndex];

                comment.Note = info.ErrorText;

                comment.Font.Size = 12;
                comment.Font.IsBold = true;
                comment.HeightCM = 5;
                comment.WidthCM = 5;

                //为单元格添加样式    
                var cell = worksheet.Cells[info.RowIndex, info.ColIndex];
                var style = cell.GetStyle();
                //设置背景颜色
                style.ForegroundColor = color;
                style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;
                style.Font.Color = System.Drawing.Color.White;

                cell.SetStyle(style);
            }

            // 保存错误文件
            workbook.Save(errorFilePath);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            ofdExcelFile.Multiselect = false;//该值确定是否可以选择多个文件
            ofdExcelFile.Title = "请选择文件";
            ofdExcelFile.Filter = "Excel文件(*.xlsx,*.xls)|*.xlsx;*.xls";
            if (this.ofdExcelFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.lbxInfo.Items.Add("\r\n已选择文件 " + ofdExcelFile.FileName + System.DateTime.Now.ToLocalTime() + "\r\n");
                this.tbExcelFilePath.Text = ofdExcelFile.FileName;
            }
            if (!string.IsNullOrEmpty(this.tbExcelFilePath.Text))
                this.btnValidateData.Enabled = true;
            else
                this.btnValidateData.Enabled = false;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            this.btnImport.Enabled = false;
            this.btnOpenTemplate.Enabled = false;
            this.btnSelectFile.Enabled = false;
            this.btnValidateData.Enabled = false;

            Thread thread = new Thread(new ThreadStart(delegate()
            {
                this.ImportData();
            }));
            thread.Start();
        }

        void ImportData()
        {
            try
            {
                var errors = new List<CellErrorInfo>();
                if (globalData == null)
                {
                    throw new Exception("没有通过校验，无法导入");
                }
                foreach (var key in globalData.Data.Keys)
                {
                    var d1 = DateTime.Now;
                    var data = globalData.Data[key];
                    var result = data.Import(globalData);
                    errors.AddRange(result);
                    var d2 = DateTime.Now;
                    var span = (d2 - d1).Seconds;
                    var text = String.Format("导入【{0}】信息完成，耗时【{1}】秒,发生【{2}】个错误", data.SheetName, span, result.Count);
                    Action act = delegate { this.lbxInfo.Items.Add(text); };
                    this.Invoke(act);
                }
                if (errors.Count > 0)
                {
                    Action beForeAct = delegate
                    {
                        this.lbxInfo.Items.Add("数据导入未全部成功，正在生成验证错误的EXCEL文件" + globalData.ImportErrorExcelPath);
                    };
                    this.Invoke(beForeAct);
                    WriteErrorInfoToExcel(errors, globalData.FileBuffer, globalData.ImportErrorExcelPath, System.Drawing.Color.Red);
                    Action act = delegate
                    {
                        this.lbxInfo.Items.Add("数据导入过程中有错误，请查看错误信息" + globalData.ImportErrorExcelPath);
                        this.btnImport.Enabled = false;
                        this.btnValidateData.Enabled = true;
                        this.btnDownTemplate.Enabled = true;
                        this.btnOpenTemplate.Enabled = true;
                        this.btnSelectFile.Enabled = true;
                        this.btnValidateData.Enabled = true;
                    };
                    this.Invoke(act);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Error(exp, exp.Message);
                MessageBox.Show(exp.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Action act = delegate
                {
                    this.btnValidateData.Enabled = true;
                };
                this.Invoke(act);
            }
            finally
            {
                Action act = delegate
                {
                    this.btnImport.Enabled = false;
                    this.btnValidateData.Enabled = true;
                    this.btnDownTemplate.Enabled = true;
                    this.btnOpenTemplate.Enabled = true;
                    this.btnSelectFile.Enabled = true;
                };
                this.Invoke(act);
                Thread.CurrentThread.Abort();
            }
        }

        private void btnDownTemplate_Click(object sender, EventArgs e)
        {
            string templatePath = AppDomain.CurrentDomain.BaseDirectory + "TemplateExcel.xlsx";
            if (!File.Exists(templatePath))
            {
                MessageBox.Show("没有找到Excel模板", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var exporter = new AsposeExcelExporter();
                var dt = new DataTable();
                byte[] buffer = null;
                using (var fileStream = new FileStream(templatePath, FileMode.Open))
                {
                    var fileSize = fileStream.Length;
                    buffer = new byte[fileSize];
                    fileStream.Read(buffer, 0, (int)fileSize);
                    fileStream.Close();
                }
                var excelBuffer = exporter.Export(dt, buffer);
                var ocalFilePath = sfd.FileName.ToString();
                using (FileStream fs = new FileStream(ocalFilePath, FileMode.Create))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(excelBuffer, 0, excelBuffer.Length);
                    bw.Close();
                    fs.Close();
                }
            }
        }

        private void btnOpenTemplate_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
