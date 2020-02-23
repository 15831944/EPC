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

namespace BeSl.Controls.File.Upload
{
    /// <summary>
    /// 上传状态枚举
    /// </summary>
    public enum UploadStatus
    {
        Pending,
        Uploading,
        Complete,
        Error,
        Canceled,
        Removed,
        Resizing
    }

    /// <summary>
    /// 文件上传类（针对单个文件）  - edit by ray 2009-09-07
    /// 修改自开源社区(CodePlex)的Silverlight上传控件代码
    /// </summary>
    public class FileUploader : INotifyPropertyChanged
    {
        #region 事件

        public event UploadProgressChangedEvent UploadProgressChanged;
        public event EventHandler UploadStatusChanged;

        #endregion

        #region 变量及属性

        public long ChunkSize = 4194304;    // 默认一次上传提交字节数(4M)

        private Dispatcher Dispatcher;    // 提供UI线程的异步调用

        private bool m_cancel;
        private bool m_remove;

        private MemoryStream m_resizeStream;    // 当调整图片文件文件流
        public bool IsResizeImage { get; set; }   // 是否调图片大小
        public int ImageSize { get; set; }  // 调整后图片的大小

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

        private int m_uploadPercent;    // 上传百分率

        public int UploadPercent
        {
            get { return m_uploadPercent; }
            set
            {
                m_uploadPercent = value;

                this.InvokePropertyChanged("UploadPercent");
            }
        }

        private UploadStatus m_uploadStatus;  // 上传状态

        public UploadStatus UploadStatus
        {
            get { return m_uploadStatus; }
            set
            {
                m_uploadStatus = value;

                this.Dispatcher.BeginInvoke(delegate()
                {
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                    }

                    if (UploadStatusChanged != null)
                    {
                        UploadStatusChanged(this, null);
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

        private UploadConfig m_uploadConfig;    // 上传配置信息

        public UploadConfig UploadConfig
        {
            get { return m_uploadConfig; }
        }

        private string m_completedFileString = String.Empty;  // 上传完成后的文件名

        public string CompletedFileString
        {
            get { return m_completedFileString; }
        }

        #endregion

        #region 构造函数

        public FileUploader(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            UploadStatus = UploadStatus.Pending;
        }

        public FileUploader(Dispatcher dispatcher, UploadConfig uploadConfig)
            : this(dispatcher)
        {
            m_uploadConfig = uploadConfig;
        }

        public FileUploader(Dispatcher dispatcher, FileInfo fileToUpload, UploadConfig uploadConfig)
            : this(dispatcher, uploadConfig)
        {
            File = fileToUpload;
        }

        #endregion

        #region 公共方法

        // 执行上传操作
        public void Upload()
        {
            // 无上传文件，或无文件上传处理页面，则取消操作
            if (m_file == null || m_uploadConfig.HandlerPage == null)
            {
                return;
            }

            UploadStatus = UploadStatus.Uploading;
            m_cancel = false;

            if (IsResizeImage && m_file.Extension == "jpg" && m_resizeStream == null && ImageSize > 0)
            {
                // 调整文件大小
                BackgroundWorker resizeWorker = new BackgroundWorker();
                resizeWorker.DoWork += new DoWorkEventHandler(resizeWorker_DoWork);
                resizeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(resizeWorker_RunWorkerCompleted);
                resizeWorker.RunWorkerAsync();
            }
            else
            {
                CheckFileOnServer();
            }
        }

        // 取消上传操作
        public void CancelUpload()
        {
            m_cancel = true;
        }

        // 移除上传操作
        public void RemoveUpload()
        {
            m_cancel = true;
            m_remove = true;

            if (UploadStatus != UploadStatus.Uploading)
            {
                UploadStatus = UploadStatus.Removed;
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

        // 调整图片大小操作
        private void resizeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ResizeImage();
        }

        // 图片文件大小调整完毕时
        private void resizeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CheckFileOnServer();
        }

        // 调整图片文件大小
        private void ResizeImage()
        {
            UploadStatus = UploadStatus.Resizing;

            using (Stream fileStream = m_file.OpenRead())
            {
                // 利用FluxJpeg解码
                DecodedJpeg jpegIn = new JpegDecoder(fileStream).Decode();

                if (ImageResizer.ResizeNeeded(jpegIn.Image, ImageSize))
                {
                    // 调整
                    DecodedJpeg jpegOut = new DecodedJpeg(
                        new ImageResizer(jpegIn.Image).Resize(ImageSize, ResamplingFilters.NearestNeighbor)
                        , jpegIn.MetaHeaders);  // 获取EXIF详细信息

                    // 编码
                    m_resizeStream = new MemoryStream();
                    new JpegEncoder(jpegOut, 90, m_resizeStream).Encode();

                    // 显示
                    m_resizeStream.Seek(0, SeekOrigin.Begin);
                    FileLength = m_resizeStream.Length;
                }
            }
        }

        // 上传文件到服务器，并返回文件信息
        private void CheckFileOnServer()
        {
            UriBuilder ub = new UriBuilder(UploadConfig.HandlerPage);
            ub.Query = string.Format(
                "{1}filename={0}&GetBytes=true&FileLength={2}&IsLog={3}",
                HttpUtility.UrlEncode(HttpUtility.UrlEncode(File.Name)),
                (string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0, 1) + "&"),
                FileLength,
                UploadConfig.IsLog);

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnClientDownloadStringCompleted);
            client.DownloadStringAsync(ub.Uri);
        }

        // 获取服务器端文件信息后操作
        void OnClientDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            long lengthtemp = 0;    // 服务器端临时文件长度
            if (!string.IsNullOrEmpty(e.Result))
            {
                lengthtemp = long.Parse(e.Result);
            }

            if (lengthtemp > 0)
            {
                MessageBoxResult result;
                if (lengthtemp == FileLength)
                {
                    result = MessageBox.Show("文件已经存在，是否覆盖？", "覆盖？", MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        lengthtemp = 0;
                    }
                    else
                    {
                        UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(100, FileLength - BytesUploaded, BytesUploaded, FileLength, m_file.Name);

                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            UploadProgressChanged(this, args);
                        });

                        BytesUploaded = FileLength;
                        UploadStatus = UploadStatus.Complete;
                        return;
                    }
                }
                else
                {
                    // 文件长度大于继续长度时提示是否续传
                    if (FileLength >= UploadConfig.ContinueLength)
                    {
                        result = MessageBox.Show("文件已经存在，是否继续？", "继续？", MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.Cancel)
                        {
                            lengthtemp = 0;
                        }
                        else if (lengthtemp < FileLength)
                        {
                            BytesUploaded = lengthtemp;
                        }
                    }
                    else
                    {
                        BytesUploaded = 0;
                    }
                }
            }

            // 上传文件到服务器
            UploadFile();
        }

        // 向服务器上传文件
        private void UploadFile()
        {
            UploadStatus = UploadStatus.Uploading;
            long tempLengthLeft = FileLength - BytesUploaded;   // 获取还需要上传的文件数
            bool isComplete = (tempLengthLeft <= ChunkSize);    // 是否完成上传(最后一次上传)

            UriBuilder ub = new UriBuilder(UploadConfig.HandlerPage);
            ub.Query = String.Format("{3}filename={0}&StartByte={1}&Complete={2}&FileLength={4}&IsLog={5}",
                HttpUtility.UrlEncode(File.Name),
                BytesUploaded,
                isComplete,
                string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0, 1) + "&",
                FileLength,
                UploadConfig.IsLog);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(ub.Uri);
            webrequest.Method = "POST";
            webrequest.AllowReadStreamBuffering = false;
            webrequest.ContentType = "multipart/form-data; boundary=" + "----------" + DateTime.Now.Ticks.ToString("x");
            webrequest.BeginGetRequestStream(new AsyncCallback(FileUpload_WriteCallback), webrequest);
        }

        // 一次文件上传操作（由于服务器对每次请求的文件大小一般有限制，因此大文件上传时，根据ChunkSize进行分批上传）
        private void FileUpload_WriteCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webrequest = asynchronousResult.AsyncState as HttpWebRequest;
            // 结束操作
            Stream requestStream = webrequest.EndGetRequestStream(asynchronousResult);

            byte[] buffer = new Byte[4096];
            int bytesRead = 0;
            int tempTotal = 0;

            Stream fileStream = ((m_resizeStream != null) ? (Stream)m_resizeStream : File.OpenRead());

            fileStream.Position = BytesUploaded;

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 && tempTotal + bytesRead < ChunkSize && !m_cancel)
            {
                requestStream.Write(buffer, 0, bytesRead);
                requestStream.Flush();
                BytesUploaded += bytesRead;
                tempTotal += bytesRead;

                if (UploadProgressChanged != null)
                {
                    int percent = (int)(((double)BytesUploaded / (double)FileLength) * 100);
                    UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(percent, bytesRead, BytesUploaded, FileLength, m_file.Name);

                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        UploadProgressChanged(this, args);
                    });
                }
            }

            // 只关闭文件流，不关闭图片调整流，一面第二次上传时再次对文件作调整
            if (m_resizeStream == null)
            {
                fileStream.Close();
            }

            requestStream.Close();

            webrequest.BeginGetResponse(new AsyncCallback(FileUpload_ReadCallback), webrequest);
            //webrequest.GetResponse
        }

        // 一次文件上传操作结束后操作
        private void FileUpload_ReadCallback(IAsyncResult asynchronousResult)
        {
            WebRequest webrequest = asynchronousResult.AsyncState as HttpWebRequest;
            WebResponse response = webrequest.EndGetResponse(asynchronousResult);
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Unicode);

            string respStr = reader.ReadToEnd();
            reader.Close();

            // 检查上传操作是否被取消（取消针对所有上传文件，移除针对当前文件）
            if (m_cancel)
            {
                if (m_resizeStream != null)
                {
                    m_resizeStream.Close();
                }

                if (m_remove)
                {
                    UploadStatus = UploadStatus.Removed;
                }
                else
                {
                    UploadStatus = UploadStatus.Canceled;
                }
            }
            else if (BytesUploaded < FileLength)
            {
                // 没有上传完毕时，继续上传操作
                UploadFile();
            }
            else
            {
                if (m_resizeStream != null)
                {
                    m_resizeStream.Close();
                }

                UploadStatus = UploadStatus.Complete;

                if (!String.IsNullOrEmpty(respStr))
                {
                    m_completedFileString = respStr;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
