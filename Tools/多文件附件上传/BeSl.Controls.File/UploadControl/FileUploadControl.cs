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
using BeSl.Controls.File.Upload;
using BeSl.Controls.Utility;

namespace BeSl.Controls.File.UploadControl
{
    // 上传文件模式（单文件，多文件）
    public enum FileUploadMode
    {
        Multi,
        Single
    }

    public abstract class FileUploadControl : ContentControl
    {
        #region 变量和属性

        protected ObservableCollection<FileUploader> m_files;   // 要上传的文件集合
        protected DateTime m_startTime;   // 上传开始时间
        protected bool IsUploading { get; set; }    // 是否上传中

        protected long TotalUploadingSize { get; set; }   // 所有正在上传文件大小
        protected long TotalUploadedSize { get; set; }    // 所有已上传文件大小

        public int MaxConcurrentUploads { get; set; }   // 最大同时上传文件数
        public int MaxNumberToUpload { get; set; }  // 一次最大上传文件数(针对多文件上传)

        public bool IsResizeImage { get; set; }   // 是否调整jpg文件大小
        public int ImageSize { get; set; }  // 图片大小（jpg文件的情况下）
        public long MaximumTotalUpload { get; set; }    // 最大总上传文件大小
        public Uri UploadUrl { get; set; }  // 上传接受文件地址

        public string ExistedFiles { get; set; }  // 上传控件中原有文件

        //public long UploadChunkSize { get; set; }   // 大文件上传时，一次上传文件大小
        //public string JsCompleteFunction { get; set; }  // 上传完成后回调Html页面Jc方法
        //public string JsCancelFunction { get; set; }  // 上传完成后回调Html页面Jc方法
        //public string Filter { get; set; }  // 文件过滤
        //public bool IsLog { get; set; } // 是否对上传文件作日志
        //public long MaximumUpload { get; set; } // 最大上传文件大小
        // public float ContinueSize { get; set; } // 续传大小（超过此大小文件执行断点续传）

        public long ContinueLength
        {
            get { return (long)ContinueSize * 1024 * 1024; }
        }

        // 已上传文件的字符串集合
        protected string m_uploadedFilesString = String.Empty;

        public string UploadedFilesString
        {
            get { return this.m_uploadedFilesString; }
            set { this.m_uploadedFilesString = value; }
        }

        // 文件上传配置
        protected UploadConfig m_uploadConfig;

        #endregion
       
        #region 模板及依赖属性

        protected CheckBox displayThumbailChkBox;
        protected Button addFilesButton;
        protected Button uploadButton;
        protected Button cancelButton;
        protected TextBlock countTextBlock;
        protected TextBlock totalSizeTextBlock;
        protected TextBlock timeLeftTextBlock;
        public    TextBlock AlertInfoTextBlock;
        protected ProgressBar progressBar;
        protected ItemsControl fileList;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            addFilesButton = GetTemplateChild("AddFilesButton") as Button;
            uploadButton = GetTemplateChild("UploadButton") as Button;
            cancelButton = GetTemplateChild("CancelButton") as Button;
            countTextBlock = GetTemplateChild("CountTextBlock") as TextBlock;
            totalSizeTextBlock = GetTemplateChild("TotalSizeTextBlock") as TextBlock;
            timeLeftTextBlock = GetTemplateChild("TimeLeftTextBlock") as TextBlock;
            AlertInfoTextBlock = GetTemplateChild("AlertInfoTextBlock") as TextBlock;
            progressBar = GetTemplateChild("ProgressBar") as ProgressBar;
            fileList = GetTemplateChild("FileList") as ItemsControl;

            addFilesButton.Click += new RoutedEventHandler(OnAddFilesButtonClick);
            uploadButton.Click += new RoutedEventHandler(OnUploadButtonClick);
            cancelButton.Click += new RoutedEventHandler(OnCancelButtonClick);

            displayThumbailChkBox = GetTemplateChild("DisplayThumbailChkBox") as CheckBox;
            if (displayThumbailChkBox != null)
            {
                displayThumbailChkBox.Checked += new RoutedEventHandler(OnDisplayThumbailChkBoxChecked);
                displayThumbailChkBox.Unchecked += new RoutedEventHandler(OnDisplayThumbailChkBoxChecked);
            }

            fileList.ItemsSource = m_files;
        }

        // 续传大小（超过此大小文件执行断点续传）
        public float ContinueSize
        {
            get { return (float)GetValue(ContinueSizeProperty); }
            set { SetValue(ContinueSizeProperty, value); }
        }

        public static readonly DependencyProperty ContinueSizeProperty =
            DependencyProperty.Register("ContinueSize", typeof(float), typeof(FileUploadControl),
            new PropertyMetadata(100.00F, new PropertyChangedCallback(ContinueSizeChanged)));

        protected static void ContinueSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 是否对上传文件作日志
        public bool IsLog
        {
            get { return (bool)GetValue(IsLogProperty); }
            set { SetValue(IsLogProperty, value); }
        }

        public static readonly DependencyProperty IsLogProperty =
            DependencyProperty.Register("IsLog", typeof(bool), typeof(FileUploadControl),
            new PropertyMetadata(false, new PropertyChangedCallback(IsLogChanged)));

        protected static void IsLogChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //FileUploadControl fuc = sender as FileUploadControl;

            //fuc.UploadConfig.IsLog = (bool)e.NewValue;
        }

        // 是否允许显示缩略图
        public bool AllowThumbnail
        {
            get { return (bool)GetValue(AllowThumbnailProperty); }
            set { SetValue(AllowThumbnailProperty, value); }
        }

        public static readonly DependencyProperty AllowThumbnailProperty =
            DependencyProperty.Register("AllowThumbnail", typeof(bool), typeof(FileUploadControl),
            new PropertyMetadata(false, new PropertyChangedCallback(AllowThumbnailChanged)));

        protected static void AllowThumbnailChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FileUploadControl fuc = sender as FileUploadControl;

            if (fuc.displayThumbailChkBox != null)
            {
                if ((bool)e.NewValue)
                {
                    fuc.displayThumbailChkBox.Visibility = Visibility.Visible;
                }
                else
                {
                    fuc.displayThumbailChkBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        // 上传接受文件地址字符串
        public string UploadUriString
        {
            get { return (string)GetValue(UploadUriStringProperty); }
            set { SetValue(UploadUriStringProperty, value); }
        }

        public static readonly DependencyProperty UploadUriStringProperty =
            DependencyProperty.Register("UploadUriString", typeof(string), typeof(FileUploadControl),
            new PropertyMetadata("", new PropertyChangedCallback(UploadUriStringChanged)));

        protected static void UploadUriStringChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FileUploadControl fuc = sender as FileUploadControl;
            string val = (string)e.NewValue;

            fuc.UploadUrl = new Uri(val);
        }

        // 文件过滤
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(FileUploadControl),
            new PropertyMetadata("所有文件 (*.*)|*.*", new PropertyChangedCallback(FilterChanged)));

        protected static void FilterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 调用客户端脚本检查上传文件合法性
        public string JsCheckFunction
        {
            get { return (string)GetValue(JsCheckFunctionProperty); }
            set { SetValue(JsCheckFunctionProperty, value); }
        }

        public static readonly DependencyProperty JsCheckFunctionProperty =
            DependencyProperty.Register("JsCheckFunction", typeof(string), typeof(FileUploadControl),
            new PropertyMetadata("", new PropertyChangedCallback(JsCheckFunctionChanged)));

        protected static void JsCheckFunctionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 上传完成后回调Html页面Jc方法
        public string JsCompleteFunction
        {
            get { return (string)GetValue(JsCompleteFunctionProperty); }
            set { SetValue(JsCompleteFunctionProperty, value); }
        }

        public static readonly DependencyProperty JsCompleteFunctionProperty =
            DependencyProperty.Register("JsCompleteFunction", typeof(string), typeof(FileUploadControl),
            new PropertyMetadata("", new PropertyChangedCallback(JsCompleteFunctionChanged)));

        protected static void JsCompleteFunctionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 上传取消后回调Html页面Jc方法
        public string JsCancelFunction
        {
            get { return (string)GetValue(JsCancelFunctionProperty); }
            set { SetValue(JsCancelFunctionProperty, value); }
        }

        public static readonly DependencyProperty JsCancelFunctionProperty =
            DependencyProperty.Register("JsCancelFunction", typeof(string), typeof(FileUploadControl),
            new PropertyMetadata("", new PropertyChangedCallback(JsCancelFunctionChanged)));

        protected static void JsCancelFunctionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 最大上传文件大小
        public long MaximumUpload
        {
            get { return (long)GetValue(MaximumUploadProperty); }
            set { SetValue(MaximumUploadProperty, value); }
        }

        public static readonly DependencyProperty MaximumUploadProperty =
            DependencyProperty.Register("MaximumUpload", typeof(long), typeof(FileUploadControl),
            new PropertyMetadata(41943040L, new PropertyChangedCallback(MaximumUploadChanged)));

        protected static void MaximumUploadChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        // 大文件上传时，一次上传文件大小
        public long UploadChunkSize
        {
            get { return (long)GetValue(UploadChunkSizeProperty); }
            set { SetValue(UploadChunkSizeProperty, value); }
        }

        public static readonly DependencyProperty UploadChunkSizeProperty =
            DependencyProperty.Register("UploadChunkSize", typeof(long), typeof(FileUploadControl),
            new PropertyMetadata(4194304L, new PropertyChangedCallback(UploadChunkSizeChanged)));

        protected static void UploadChunkSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        #endregion
        
        #region 构造函数

        public FileUploadControl()
        {
            this.Init();
        }

        public FileUploadControl(Uri uploadUrl)
            : this()
        {
            UploadUrl = uploadUrl;
        }

        // 初始化控件
        private void Init()
        {
            ContinueSize = 800;  // 默认大于80M时提示是否断点续传
            IsLog = false;  // 不写日志
            MaximumTotalUpload = MaximumUpload = -1;
            Filter = "所有文件|*.*";
            IsUploading = false;
            UploadChunkSize = 4194304;  // 默认一次上传请求提交4M

            HtmlPage.RegisterScriptableObject("UploadCtrl", this);

            m_files = new ObservableCollection<FileUploader>();
            m_files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnFilesCollectionChanged);

            Loaded += new RoutedEventHandler(OnFileUploadControlLoaded);
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 获取指定文件名的文本文件的内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [ScriptableMember]
        public string GetTextFileContent(string fileName)
        {
            string strContent = String.Empty;

            IEnumerable<FileUploader> files = m_files.Where(tent => tent.FileName == fileName);

            if (files.Count() > 0)
            {
                FileUploader tfile = files.First();

                return FileHelper.GetTextContent(tfile.File);
            }
            else
            {
                throw new IOException(String.Format("文件名为“{0}”的文件不在上传队列中。", fileName));
            }

            return strContent;
        }

        /// <summary>
        /// 移除文件名为fileName的上传文件
        /// </summary>
        /// <param name="fileName"></param>
        [ScriptableMember]
        public void RemoveUpload(string fileName)
        {
            IEnumerable<FileUploader> files = m_files.Where(tent => tent.FileName == fileName);

            if (files.Count() > 0)
            {
                FileUploader tfile = files.First();
                tfile.RemoveUpload();
                m_files.Remove(tfile);
            }
            else
            {
                throw new IOException(String.Format("文件名为“{0}”的文件不在上传队列中。", fileName));
            }
        }

        #endregion

        #region 私有函数

        // 点击上传文件按钮时触发
        protected virtual void OnAddFilesButtonClick(object sender, RoutedEventArgs e) { }

        // 点击上传文件按钮是触发
        protected virtual void OnUploadButtonClick(object sender, RoutedEventArgs e)
        {
            if ((string)uploadButton.Content == "上传")
            {
                if (!CheckValid())
                {
                    return;
                }

                string repeatedFiles = ReterieveRepeatedFiles();
                if (repeatedFiles != String.Empty)
                {
                    if (MessageBox.Show(string.Format("文件 '{0}' 已上传，是否继续上传？", repeatedFiles), "提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }

                m_startTime = DateTime.Now;

                //uploadButton.Content = "停止";
                uploadButton.IsEnabled = false;

                UploadFile();
            }
            else
            {
                var q = m_files.Where(f => f.UploadStatus == UploadStatus.Uploading);
                foreach (FileUploader fu in q)
                {
                    fu.CancelUpload();
                }

                IsUploading = false;
                uploadButton.Content = "上传";
            }
        }

        // 调用关闭窗口方法
        protected virtual void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(JsCancelFunction))
            {
                try
                {
                    HtmlPage.Window.Invoke(JsCancelFunction);
                }
                catch (Exception ex) { }
            }
        }

        // 点击显示缩略图按钮时触发
        protected virtual void OnDisplayThumbailChkBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkbox = sender as CheckBox;

            foreach (FileUploader fu in m_files)
            {
                if (fu.DisplayThumbnail != chkbox.IsChecked)
                {
                    fu.DisplayThumbnail = (bool)chkbox.IsChecked;
                }
            }
        }

        // 加载控件时触发
        protected virtual void OnFileUploadControlLoaded(object sender, RoutedEventArgs e) { }

        // 加载或上传结束时触发
        protected virtual void OnFilesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TotalUploadingSize = m_files.Sum(f => f.FileLength);
            TotalUploadedSize = m_files.Sum(f => f.BytesUploaded);
        }

        // 上传属性改变时触发
        protected virtual void OnUploadPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileLength" || e.PropertyName == "BytesUploaded")
            {
                OnFilesCollectionChanged(null, null);
            }
        }

        // 上传进度改变时触发
        protected virtual void OnUploadProgressChanged(object sender, BeSl.Controls.File.Upload.UploadProgressChangedEventArgs args)
        {
            TotalUploadedSize += args.BytesUploaded;

            double ByteProcessTime = 0;
            double EstimatedTime = 0;

            try
            {
                TimeSpan timeSpan = DateTime.Now - m_startTime;
                ByteProcessTime = TotalUploadedSize / timeSpan.TotalSeconds;
                EstimatedTime = (TotalUploadingSize - TotalUploadedSize) / ByteProcessTime;
                timeSpan = TimeSpan.FromSeconds(EstimatedTime);
                timeLeftTextBlock.Text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            catch { }
        }

        // 上传状态改变时触发
        protected virtual void OnUploadStatusChanged(object sender, EventArgs e) { }

        // 检查上传文件
        protected virtual bool CheckValid()
        {
            bool rtnflag = false;
            bool hascheck = false;

            if (!String.IsNullOrEmpty(JsCheckFunction))
            {
                hascheck = (bool)HtmlPage.Window.Eval("typeof(" + JsCheckFunction + ") != 'undefined'");    // 检查是否存在检查方法

                if (hascheck)
                {
                    try
                    {
                        rtnflag = (bool)HtmlPage.Window.Invoke(JsCheckFunction
                            , JoinString(m_files.Select(tent => tent.FileName), "|")
                            , JoinString(m_files.Select(tent => tent.FileLength.ToString()), "|"));
                    }
                    catch (Exception ex) { }
                }
            }

            if (rtnflag || !hascheck)
            {
                if (m_files.Count <= 0)
                {
                    MessageBox.Show("请添加上传文件！");
                    rtnflag = false;
                }
                else
                {
                    rtnflag = true;
                }
            }

            return rtnflag;
        }

        // 上传文件操作
        protected virtual void UploadFile()
        {
            IsUploading = true;

            while ((m_files.Count(f => f.UploadStatus == UploadStatus.Uploading || f.UploadStatus == UploadStatus.Resizing) < MaxConcurrentUploads) && IsUploading)
            {
                if (m_files.Count(f => f.UploadStatus != UploadStatus.Complete && f.UploadStatus != UploadStatus.Uploading && f.UploadStatus != UploadStatus.Resizing) > 0)
                {
                    FileUploader fu = m_files.First(f => f.UploadStatus != UploadStatus.Complete && f.UploadStatus != UploadStatus.Uploading && f.UploadStatus != UploadStatus.Resizing);
                    fu.Upload();
                }
                else if (m_files.Count(f => f.UploadStatus == UploadStatus.Uploading || f.UploadStatus == UploadStatus.Resizing) == 0)
                {
                    IsUploading = false;
                    uploadButton.Content = "上传";
                    this.UploadedFilesString = this.UploadedFilesString.TrimEnd(',');

                    if (!string.IsNullOrEmpty(JsCompleteFunction))
                    {
                        try
                        {
                            HtmlPage.Window.Invoke(JsCompleteFunction, this.UploadedFilesString);
                        }
                        catch (Exception e) { }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        // 检查是否存在重复文件
        protected virtual bool IsRepeatedFiles()
        {
            if (this.ExistedFiles != null)
            {
                IEnumerable<string> formatedExisted = this.FormatExistedFileName().Distinct();
                IEnumerable<string> formatedCurrent = this.FormatCurrentFileName();

                if (formatedCurrent.Count() > formatedCurrent.Distinct().Count() || formatedExisted.Union(formatedCurrent).Count() < formatedCurrent.Count() + formatedExisted.Count())
                {
                    return true;
                }
            }

            return false;
        }

        // 获取当前选择中文件在原文件中存在重复的项
        protected virtual string ReterieveRepeatedFiles()
        {
            string rtnFiles = String.Empty;

            if (this.ExistedFiles != null)
            {
                string[] formatedExisted = this.FormatExistedFileName();
                string[] formatedCurrent = this.FormatCurrentFileName();

                IEnumerable<string> repeatedFiles = RetrieveRepeated<string>(formatedExisted, formatedCurrent);

                foreach (string file in repeatedFiles)
                {
                    if (file != null)
                    {
                        rtnFiles += file + ",";
                    }
                }

                rtnFiles = rtnFiles.TrimEnd(',');
            }

            return rtnFiles;
        }
        
        #endregion

        #region 虚拟方法

        public abstract UploadConfig UploadConfig { get; }

        #endregion

        #region 帮助方法

        /// <summary>
        /// 获取文件名集合
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private string JoinString(IEnumerable<string> strs, string divChar)
        {
            string fstr = String.Empty;

            foreach (string tstr in strs)
            {
                fstr += String.Format("{0}{1}", tstr.ToString(), divChar);
            }

            if (!String.IsNullOrEmpty(fstr))
            {
                fstr = fstr.Substring(0, fstr.LastIndexOf(divChar));
            }

            return fstr;
        }

        // vals1中在vals2中存在的类型
        private IEnumerable<T> RetrieveRepeated<T>(T[] vals1, T[] vals2)
        {
            T[] rtns = new T[vals1.Length];
            for (int i = 0; i < vals1.Length; i++)
            {
                for (int j = 0; j < vals2.Length; j++)
                {
                    if (vals1[i].Equals(vals2[j]))
                    {
                        rtns[i] = vals1[i];
                        break;
                    }
                }
            }

            return rtns.Where<T>(rep => (rep != null));
        }

        // 获取重复文件
        private IEnumerable<T> RetrieveRepeated<T>(T[] vals)
        {
            T[] repeatedVals = new T[vals.Length];

            RetrieveRepeated<T>(vals, 0, repeatedVals);

            return repeatedVals.Where<T>(rep => (rep != null));
        }

        /// <summary>
        /// 获取数组中重复的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="vals">待比较的值</param>
        /// <param name="startindex">比较开始下标</param>
        /// <param name="repeatedVals">重复值</param>
        /// <returns></returns>
        private T[] RetrieveRepeated<T>(T[] vals, int startindex, T[] repeatedVals)
        {
            if (startindex < (vals.Length - 1))
            {

                T toBeCompared = vals[startindex];

                for (int i = (startindex + 1); i < vals.Length; i++)
                {
                    if (toBeCompared.Equals(vals[i]))
                    {
                        repeatedVals[startindex] = toBeCompared;
                    }
                }

                RetrieveRepeated<T>(vals, (startindex + 1), repeatedVals);
            }
            
            return repeatedVals;
        }

        // 转换已存在文件名以便于检查是否文件重复(去除文件Id)
        private string[] FormatExistedFileName()
        {
            string[] formatedFiles = this.ExistedFiles.Split(',');

            for (int i = 0; i < formatedFiles.Length; i++)
            {
                formatedFiles[i] = formatedFiles[i].Substring(formatedFiles[i].IndexOf('_') + 1);
            }
            
            return formatedFiles;
        }

        // 转换当前文件名为上传后格式(上传后文件名会有些许变化)
        private string[] FormatCurrentFileName()
        {
            string[] formatedFiles = new string[m_files.Count];

            for (int i = 0; i < formatedFiles.Length; i++)
            {
                formatedFiles[i] = m_files[i].FileName.Replace(' ', '_');
            }

            return formatedFiles;
        }

        #endregion
    }
}
