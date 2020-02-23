using System;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Browser;
using FluxJpeg.Core;
using FluxJpeg.Core.Decoder;
using FluxJpeg.Core.Encoder;
using FluxJpeg.Core.Filtering;

namespace BeSl.Controls.File.Download
{
    /// <summary>
    /// 上传状态枚举
    /// </summary>
    public enum DownloadStatus
    {
        Pending,
        Downloading,
        Complete,
        Error,
        Canceled,
        Removed
    }

    /// <summary>
    /// 文件批量下载类（针对单个文件）  - edit by ray 2009-11-10
    /// </summary>
    public class FileDownloader : INotifyPropertyChanged
    {
        #region 事件

        public event DownloadProgressChangedEvent DownloadProgressChanged;
        public event EventHandler DownloadStatusChanged;

        #endregion

        #region 变量及属性

        public long ChunkSize = 4194304;    // 默认一次下载获取字节数(4M)

        private Dispatcher Dispatcher;    // 提供UI线程的异步调用

        private bool m_cancel;
        private bool m_remove;

        private FileInfo m_file;  // 要上传的文件

        public FileInfo File
        {
            get { return m_file; }
            set
            {
                m_file = value;
                try
                {
                    // 设置文件长度
                    Stream tempStream = m_file.OpenRead();
                    FileLength = tempStream.Length;
                    tempStream.Close();
                }
                catch (IOException ioe)
                {
                    throw ioe;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        // 文件名
        public string FileName
        {
            get { return this.File.Name; }
        }

        // 文件扩展名
        public string FileExtension
        {
            get { return this.File.Extension; }
        }

        private long m_fileLength;  // 要上传的文件长度

        public long FileLength
        {
            get { return m_fileLength; }
            set
            {
                m_fileLength = value;

                this.InvokePropertyChanged("FileLength");
            }
        }

        private long m_bytesUploaded;   // 已上传文件数

        public long BytesUploaded
        {
            get { return m_bytesUploaded; }
            set
            {
                m_bytesUploaded = value;

                this.InvokePropertyChanged("BytesUploaded");
            }
        }

        private int m_downloadPercent;    // 下载百分率

        public int DownloadPercent
        {
            get { return m_downloadPercent; }
            set
            {
                m_downloadPercent = value;

                this.InvokePropertyChanged("DownloadPercent");
            }
        }

        private DownloadStatus m_downloadStatus;  // 上传状态

        public DownloadStatus DownloadStatus
        {
            get { return m_downloadStatus; }
            set
            {
                m_downloadStatus = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    }

                    if (DownloadStatusChanged != null)
                    {
                        DownloadStatusChanged(this, null);
                    }
                });
            }
        }

        private bool m_displayThumbnail;  // 是否显示缩略图(jpg格式时)

        public bool DisplayThumbnail
        {
            get { return m_displayThumbnail; }
            set
            {
                m_displayThumbnail = value;

                this.InvokePropertyChanged("DisplayThumbnail");
            }
        }

        private DownloadConfig m_downloadConfig;    // 上传配置信息

        public DownloadConfig DownloadConfig
        {
            get { return m_downloadConfig; }
        }

        #endregion

        #region 构造函数

        public FileDownloader(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            m_downloadStatus = DownloadStatus.Pending;
        }

        public FileDownloader(Dispatcher dispatcher, DownloadConfig downloadConfig)
            : this(dispatcher)
        {
            m_downloadConfig = downloadConfig;
        }

        #endregion

        #region 公共方法

        // 执行上传操作
        public void Upload()
        {
            // 无上传文件，或无文件上传处理页面，则取消操作
            if (m_file == null || m_downloadConfig.HandlerPage == null)
            {
                return;
            }

            DownloadStatus = DownloadStatus.Downloading;
            m_cancel = false;

            CheckFileOnServer();
        }

        // 取消上传操作
        public void CancelDownload()
        {
            m_cancel = true;
        }

        // 移除上传操作
        public void RemoveDownload()
        {
            m_cancel = true;
            m_remove = true;

            if (DownloadStatus != DownloadStatus.Downloading)
            {
                DownloadStatus = DownloadStatus.Removed;
            }
        }

        #endregion

        #region 私有方法

        // 上传属性变化时调用
        private void InvokePropertyChanged(string propertyName)
        {
            // 执行代理
            this.Dispatcher.BeginInvoke(delegate()
            {
                if (PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            });
        }

        // 上传文件到服务器，并返回文件信息
        private void CheckFileOnServer()
        {
            UriBuilder ub = new UriBuilder(DownloadConfig.HandlerPage);
            ub.Query = string.Format(
                "{1}filename={0}&GetBytes=true&FileLength={2}&IsLog={3}",
                HttpUtility.UrlEncode(HttpUtility.UrlEncode(File.Name)),
                (string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0, 1) + "&"),
                FileLength,
                DownloadConfig.IsLog);

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnClientDownloadStringCompleted);
            client.DownloadStringAsync(ub.Uri);
        }

        // 获取服务器端文件信息后操作
        void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            // 上传文件到服务器
            DownloadFile();
        }

        // 向服务器上传文件
        private void DownloadFile()
        {
        }

        // 一次文件下载操作（因此大文件下载时，根据接受大小进行分批下载）
        private void FileDownload_WriteCallback(IAsyncResult asynchronousResult)
        {
        }

        // 一次文件下载操作结束后操作
        private void FileDownload_ReadCallback(IAsyncResult asynchronousResult)
        {
        }

        #endregion

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
