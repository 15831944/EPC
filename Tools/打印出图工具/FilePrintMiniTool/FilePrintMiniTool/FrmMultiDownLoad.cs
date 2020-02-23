using FilePrintMiniTool.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilePrintMiniTool
{
    public partial class FrmMultiDownLoad : Form
    {
        public Action<string, int> AfterDownLoadSuccess { get; set; }

        public class MultiDownLoadData
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            public string ProjectInfoName { get; set; }
            public string ID { get; set; }
            /// <summary>
            /// 图名
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 文件 
            /// </summary>
            public string PdfFile { get; set; }
            /// <summary>
            /// 提交日期
            /// </summary>
            public string SubmitDate { get; set; }
            public bool IsDownLoad { get; set; }
        }

        public FrmMultiDownLoad()
        {
            InitializeComponent();
            InitMostRecentFiles();
        }

        private List<MultiDownLoadData> _downLoadDatas;
        public DialogResult ShowDialog(List<MultiDownLoadData> downLoadDatas)
        {
            _downLoadDatas = downLoadDatas;
            listView1.Items.Clear();
            if (downLoadDatas.Count > 0)
            {
                foreach (var data in downLoadDatas)
                {
                    //if (!string.IsNullOrEmpty(data.PdfFile))
                    {
                        //var spData = data.PdfFile.Split('_');
                        //if (spData.Length > 1)
                        {
                            string filename = data.ProductName;
                            ListViewItem item = listView1.Items.Add(new ListViewItem(new string[] { (listView1.Items.Count + 1).ToString(), filename, "等待中" }));
                            item.Tag = data.ID;//tag 存PdfFile
                        }
                    }
                }

                return this.ShowDialog();
            }

            return System.Windows.Forms.DialogResult.No;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            string dir = textBox1.Text;

            if (string.IsNullOrEmpty(dir))
            {
                MessageBox.Show("请先指定下载路径");
                return;
            }

            if (listView1.Items.Count > 0)
            {                
                var folderList = _downLoadDatas.Select(a => a.ProjectInfoName).Distinct();
                string fileFolderName = string.Join("_", folderList);
                string folderPath = Path.Combine(dir, fileFolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (ListViewItem viewItem in listView1.Items)
                {
                    var item = _downLoadDatas.FirstOrDefault(a=>a.ID == viewItem.Tag.ToSafeString());
                    if (item.IsDownLoad) continue;

                    string PdfFile = item.PdfFile;
                    //PdfFile = "5448_EPC产品调整清单（中南建筑）20181130.xlsx";

                    if (string.IsNullOrEmpty(PdfFile))
                    {
                        viewItem.SubItems[2].Text = "PdfFile值为空";
                        return;
                    }
                    var splitRes = PdfFile.Split('_');
                    if (splitRes.Length == 0)
                    {
                        viewItem.SubItems[2].Text = "【" + PdfFile + "】中无法找到文件id";
                        return;
                    }
                    string FieldID = splitRes[0];
                    string fileExt = Path.GetExtension(PdfFile);
                    string fileName = FieldID;
                    if (splitRes.Length > 1)
                    {
                        fileName = Path.GetFileNameWithoutExtension(splitRes[1]);
                    }

                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(item.SubmitDate, out dt))
                    {
                        fileName += ("(提交日期" + dt.ToString("yyyy-MM-dd") + ")" + fileExt);
                    }
                    else
                    {
                        fileName += fileExt;
                    }

                    string localFilePath = Path.Combine(folderPath, fileName);
                    string errorInfo = Helper.CopyFile(localFilePath, FieldID);
                    if (!string.IsNullOrEmpty(errorInfo))
                    {
                        viewItem.SubItems[2].Text = errorInfo;
                    }
                    else
                    {
                        viewItem.SubItems[2].Text = "下载成功";
                        item.IsDownLoad = true;
                        if (checkBox1.Checked && AfterDownLoadSuccess != null)
                        {
                            AfterDownLoadSuccess(item.ID, 1);
                        }
                    }
                }
            }
        }
                          
        private void FrmMultiDownLoad_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
                
        private string mrfFileName = "mostRecentFiles.ini";
        private void InitMostRecentFiles()
        {
            string fileName = Application.StartupPath + "\\" + mrfFileName;
            if (!System.IO.File.Exists(fileName))
                return;
            System.IO.StreamReader sr = System.IO.File.OpenText(fileName);
            for (string s = sr.ReadLine(); s != null; s = sr.ReadLine())
                textBox1.Text = s;
            sr.Close();
        }
        private void SaveMostRecentFiles()
        {
            System.IO.StreamWriter sw = System.IO.File.CreateText(Application.StartupPath + "\\" + mrfFileName);
            sw.WriteLine(textBox1.Text);
            sw.Close();
        }

        private void FrmMultiDownLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMostRecentFiles();
        }
    }
}
