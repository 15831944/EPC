using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using Plugin.CadJob;
using Common.Logic;

namespace Plugin.CadJob
{
    public partial class MainDlg : Form
    {
        public MainDlg()
        {
            InitializeComponent();
        }

        private bool isLock = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isLock)
                return;

            isLock = true;

            //开始打印
            RunPlotQueue();
            //label2.Text = "最近：["+result.FileName+"]打印成功，打印时间（" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") + "）";

            isLock = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer1.Enabled = true;
            timer1.Start();

            this.button1.Enabled = false;
            this.button2.Enabled = true;
            if (AcadApp.DocumentManager.MdiActiveDocument != null)
                AcadApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\ndwg转图服务启动！\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;

            this.button1.Enabled = true;
            this.button2.Enabled = false;
            if (AcadApp.DocumentManager.MdiActiveDocument != null)
                AcadApp.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\ndwg转图服务停止！\n");
        }

        private void RunPlotQueue()
        {
            FileRepository repo = new FileRepository();
            FileTask task = null;
            while ((task = repo.GetTask("")) != null)
            {
                if (task.ExtName.Equals("dwg",StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        label2.Text = string.Format("正在获取文件...[{0}]", task.Name);
                        //1.从filestore获取文件
                        byte[] bytes = FileStoreHelper.GetFile(task.ID);
                        FileStoreHelper.SaveFileBuffer(bytes, Path.Combine(OfficeHelper.GetFolderPath(task.ID, "Files"), task.Name));
                        //2.设置文件数据库状态为进行中
                        repo.StartTask(task.ID);
                        //3.开始进行转换
                        label2.Text = string.Format("正在进行格式打印...[{0}]", task.Name);
                        var imgDTO = OfficeHelper.InitDTO(task.Name, bytes.Length, task.ID);
                        var result = FileConverter.Exec(imgDTO);

                        bool isSucc = false;
                        if (result != null && result.status)
                        {
                            //4.设置转图层次，并生成json文件
                            imgDTO.Versions[0].ImageZoomLevel = result.ZoomLevel;
                            OfficeHelper.WriteJsonFile(imgDTO);
                            isSucc = true;
                        }
                        Application.DoEvents();
                        //5.回置状态
                        label2.Text = "最近：[" + imgDTO.Name + "]打印" + (isSucc ? "成功":"失败") + "，打印时间（" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分") + "）";
                        repo.EndTask(task.ID, isSucc ? ResultStatus.Success:ResultStatus.Error);
                        //6.删除原文件
                        if (File.Exists(imgDTO.Versions[0].FullPath))
                            File.Delete(imgDTO.Versions[0].FullPath);
                    }
                    catch (Exception ex)
                    {
                        // 记录日志
                        LogWriter.Info(string.Format(ex.Message, DateTime.Now.ToString(), task.ID, task.Name, ex.StackTrace));

                        repo.EndTask(task.ID, ResultStatus.Error);
                    }
                }
            }

        }

        
    }
}

