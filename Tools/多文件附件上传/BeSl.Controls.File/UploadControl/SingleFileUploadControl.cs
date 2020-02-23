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
    public class SingleFileUploadControl : FileUploadControl
    {
        #region 模板及依赖属性

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (MaximumUpload > -1)
            {
                AlertInfoTextBlock.Text += "       文件大小上限:" + (MaximumUpload / (1024 * 1024)).ToString() + "M";
            }
        }

        #endregion

        #region 构造函数

        public SingleFileUploadControl()
        {
            this.DefaultStyleKey = typeof(SingleFileUploadControl);
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
                    m_uploadConfig = new UploadConfig(this.MaximumUpload, 1, this.Filter, this.UploadUrl, this.ContinueSize, this.IsLog);
                }

                return m_uploadConfig;
            }
        }

        // 上传状态改变时触发
        protected override void OnUploadStatusChanged(object sender, EventArgs e)
        {
            FileUploader fu = sender as FileUploader;

            if (fu.UploadStatus == UploadStatus.Complete)
            {
                UploadedFilesString = fu.CompletedFileString;
            }
            else if (fu.UploadStatus == UploadStatus.Uploading)
            {
                UploadFile();
            }

            if (IsUploading)
            {
                UploadFile();
            }
        }

        // 点击上传文件按钮时触发
        protected override void OnAddFilesButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            try
            {
                dlg.Filter = Filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件过滤格式错误，请检查。");
                return;
            }

            dlg.Multiselect = false;

            if ((bool)dlg.ShowDialog())
            {
                FileInfo file = dlg.File;

                try
                {
                    FileUploader fileUploader = new FileUploader(this.Dispatcher, file, UploadConfig);

                    if (UploadChunkSize > 0)
                    {
                        fileUploader.ChunkSize = UploadChunkSize;
                    }

                    if (IsResizeImage)
                    {
                        fileUploader.IsResizeImage = IsResizeImage;
                        fileUploader.ImageSize = ImageSize;
                    }

                    if (MaximumTotalUpload >= 0 && TotalUploadingSize + fileUploader.FileLength > MaximumTotalUpload)
                    {
                        MessageBox.Show("总上传文件大小已超过最大允许值！");
                        return;
                    }

                    if (MaximumUpload >= 0 && fileUploader.FileLength > MaximumUpload)
                    {
                        MessageBox.Show(string.Format("文件 '{0}' 已超过最大上传文件大小。", fileUploader.FileName));
                        return;
                    }

                    fileUploader.UploadStatusChanged += new EventHandler(OnUploadStatusChanged);
                    fileUploader.UploadProgressChanged += new UploadProgressChangedEvent(OnUploadProgressChanged);
                    fileUploader.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnUploadPropertyChanged);

                    // 若已有上传文件则先删除之
                    if (m_files != null)
                    {
                        m_files.Clear();
                    }

                    m_files.Add(fileUploader);
                }
                catch (IOException ioex)
                {
                    MessageBox.Show("文件“" + file.Name + "”不可读，请确认文件是否打开中且拥有读权限！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载文件“" + file.Name + "”失败，请重试！");
                }
            }
        }

        #endregion
    }
}
