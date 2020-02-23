using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using BeSl.Controls.Utility;
using BeSl.Controls.File.Upload;

namespace BeSl.Controls.File.UploadControl
{
    public class MultiFileUploadControl : FileUploadControl
    {
        #region 变量和属性

        protected int m_count = 0;  // 上传文件数
        protected ScrollHelper m_scrollHelper;    // 支持Silverlight鼠标滚轮
        protected bool Multiselect { get; set; }   // 是否允许多选

        #endregion

        #region 模板及依赖属性

        protected ScrollViewer filesScrollViewer;
        protected Button clearFilesButton;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            filesScrollViewer = GetTemplateChild("FilesScrollViewer") as ScrollViewer;
            clearFilesButton = GetTemplateChild("ClearFilesButton") as Button;

            m_scrollHelper = new ScrollHelper(filesScrollViewer);

            clearFilesButton.Click += new RoutedEventHandler(OnClearFileButtonClick);

            // 第一次加载可能会失败(控件未完全加载，这里在加载一次)
            if (this.AllowThumbnail)
            {
                this.displayThumbailChkBox.Visibility = Visibility.Visible;
            }
            else
            {
                this.displayThumbailChkBox.Visibility = Visibility.Collapsed;
            }            
            if (MaxNumberToUpload > -1)
            {
                AlertInfoTextBlock.Text += "       文件个数上限:" + MaxNumberToUpload.ToString() + "个";
            }
            if (MaximumUpload > -1)
            {
                AlertInfoTextBlock.Text += "  文件大小上限:" + (MaximumUpload/(1024*1024)).ToString() + "M";
            }
        }

        #endregion

        #region 构造函数

        public MultiFileUploadControl()
        {
            this.DefaultStyleKey = typeof(MultiFileUploadControl);
            this.Init();
        }

        // 初始化控件
        private void Init()
        {
            MaxConcurrentUploads = 1;
            MaxNumberToUpload = -1;
            Multiselect = true;
        }

        #endregion

        #region FileUploadControl成员

        // 获取配置上传配置信息类
        public override UploadConfig UploadConfig
        {
            get
            {
                if (m_uploadConfig == null)
                {
                    m_uploadConfig = new UploadConfig(this.MaximumUpload, this.MaxNumberToUpload, this.Filter, this.UploadUrl, this.ContinueSize, this.IsLog);
                }

                return m_uploadConfig;
            }
        }

        // 上传进度改变时触发
        protected override void OnUploadProgressChanged(object sender, BeSl.Controls.File.Upload.UploadProgressChangedEventArgs args)
        {
            base.OnUploadProgressChanged(sender, args);

            progressBar.Value = TotalUploadedSize;
            totalSizeTextBlock.Text = string.Format("{0} / {1}",
                 new ByteConverter().Convert(TotalUploadedSize, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadingSize, this.GetType(), null, null).ToString());
        }

        // 上传状态改变时触发
        protected override void OnUploadStatusChanged(object sender, EventArgs e)
        {
            FileUploader fu = sender as FileUploader;

            if (fu.UploadStatus == UploadStatus.Complete)
            {
                if (!String.IsNullOrEmpty(fu.CompletedFileString) && UploadedFilesString.IndexOf(fu.CompletedFileString) < 0)
                {
                    UploadedFilesString += fu.CompletedFileString + ",";
                }

                if (IsUploading)
                {
                    UploadFile();
                }
            }
            else if (fu.UploadStatus == UploadStatus.Removed)
            {
                m_files.Remove(fu);

                if (IsUploading)
                {
                    UploadFile();
                }
            }
            else if (fu.UploadStatus == UploadStatus.Uploading && m_files.Count(f => f.UploadStatus == UploadStatus.Uploading) < MaxConcurrentUploads)
            {
                UploadFile();
            }
        }

        // 点击上传文件按钮时触发
        protected override void OnAddFilesButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Filter;
            dlg.Multiselect = Multiselect;

            m_count = m_files.Count;

            // 调用外部脚本检查上传文件是否合法


            if ((bool)dlg.ShowDialog())
            {
                foreach (FileInfo file in dlg.Files)
                {
                    try
                    {
                        FileUploader fileUploader = new FileUploader(this.Dispatcher, file, UploadConfig);

                        if (UploadChunkSize > 0)
                            fileUploader.ChunkSize = UploadChunkSize;

                        if (IsResizeImage)
                        {
                            fileUploader.IsResizeImage = IsResizeImage;
                            fileUploader.ImageSize = ImageSize;
                        }

                        if (MaxNumberToUpload > -1)
                        {
                            m_count++;
                            if (m_count > MaxNumberToUpload)
                            {
                                MessageBox.Show("上传文件数已超过最大允许值！");
                                break;
                            }
                        }

                        if (MaximumTotalUpload >= 0 && TotalUploadingSize + fileUploader.FileLength > MaximumTotalUpload)
                        {
                            MessageBox.Show("总上传文件大小已超过最大允许值！");
                            break;
                        }

                        if (MaximumUpload >= 0 && fileUploader.FileLength > MaximumUpload)
                        {
                            MessageBox.Show(string.Format("文件 '{0}' 的大小已超过允许上传文件的最大值。", fileUploader.FileName));
                            continue;
                        }

                        fileUploader.DisplayThumbnail = (bool)displayThumbailChkBox.IsChecked;
                        fileUploader.UploadStatusChanged += new EventHandler(OnUploadStatusChanged);
                        fileUploader.UploadProgressChanged += new UploadProgressChangedEvent(OnUploadProgressChanged);
                        fileUploader.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnUploadPropertyChanged);
                        m_files.Add(fileUploader);
                    }
                    catch (IOException ioex)
                    {
                        MessageBox.Show("文件“" + file.Name + "”不可读，请确认文件是否打开中且拥有读权限！");
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("加载文件“" + file.Name + "”失败，请重试！");
                        break;
                    }
                }
            }
        }

        #endregion

        #region 私有函数

        // 点击清除文件按钮
        protected void OnClearFileButtonClick(object sender, RoutedEventArgs e)
        {
            var q = m_files.Where(f => f.UploadStatus == UploadStatus.Uploading);

            foreach (FileUploader fu in q)
            {
                fu.CancelUpload();
            }
            uploadButton.IsEnabled = true;
            timeLeftTextBlock.Text = "";
            m_files.Clear();
        }

        // 文件数发生变化时触发
        protected override void OnFilesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnFilesCollectionChanged(sender, e);

            m_count = m_files.Count;

            countTextBlock.Text = "文件总数: " + m_files.Count.ToString();
            totalSizeTextBlock.Text = string.Format("{0} of {1}",
                new ByteConverter().Convert(TotalUploadedSize, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadingSize, this.GetType(), null, null).ToString());
            progressBar.Maximum = TotalUploadingSize;
            progressBar.Value = TotalUploadedSize;
        }

        #endregion
    }
}
