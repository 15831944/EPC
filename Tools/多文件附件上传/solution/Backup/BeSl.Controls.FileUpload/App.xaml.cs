using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using BeSl.Controls.File.UploadControl;

namespace BeSl.Controls.FileUpload
{
    public partial class App : Application
    {
        #region 成员属性

        UserControl uploadPage;
        FileUploadControl uploadControl;

        /// <summary>
        /// 上传页面
        /// </summary>
        public UserControl UploadPage
        {
            get
            {
                return uploadPage;
            }
        }

        /// <summary>
        /// 上传控件
        /// </summary>
        public FileUploadControl UploadControl
        {
            get
            {
                return uploadControl;
            }
        }

        #endregion

        #region 构造函数

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        #endregion

        #region 事件

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            long tempLong = 0;
            int tempInt = 0;
            bool tempBool = false;

            string fileMode = "Multi";  // Single

            if (e.InitParams.Keys.Contains("FileMode") && !string.IsNullOrEmpty(e.InitParams["FileMode"]))
            {
                fileMode = e.InitParams["FileMode"];
            }

            if (fileMode.ToLower() == "single")
            {
                SingleFileUpload sfu = new SingleFileUpload();
                uploadPage = sfu;
                uploadControl = sfu.PageFileUploadCtrl;
            }
            else
            {
                MultiFileUpload mfu = new MultiFileUpload();
                uploadPage = mfu;
                uploadControl = mfu.PageFileUploadCtrl;
            }

            if (e.InitParams.Keys.Contains("UploadPage") && !string.IsNullOrEmpty(e.InitParams["UploadPage"]))
            {
                uploadControl.UploadUrl = new Uri(HtmlPage.Document.DocumentUri, HttpUtility.UrlDecode(e.InitParams["UploadPage"]));
            }

            if (e.InitParams.Keys.Contains("UploadChunkSize") && !string.IsNullOrEmpty(e.InitParams["UploadChunkSize"]))
            {
                if (long.TryParse(e.InitParams["UploadChunkSize"], out tempLong) && tempLong > 0)
                {
                    uploadControl.UploadChunkSize = tempLong;
                }
            }

            if (e.InitParams.Keys.Contains("MaximumTotalUpload") && !string.IsNullOrEmpty(e.InitParams["MaximumTotalUpload"]))
            {
                if (long.TryParse(e.InitParams["MaximumTotalUpload"], out tempLong))
                {
                    uploadControl.MaximumTotalUpload = tempLong;
                }
            }

            if (e.InitParams.Keys.Contains("MaximumUpload") && !string.IsNullOrEmpty(e.InitParams["MaximumUpload"]))
            {
                if (long.TryParse(e.InitParams["MaximumUpload"], out tempLong))
                {
                    uploadControl.MaximumUpload = tempLong;
                }
            }


            if (e.InitParams.Keys.Contains("MaxConcurrentUploads") && !string.IsNullOrEmpty(e.InitParams["MaxConcurrentUploads"]))
            {
                if (int.TryParse(e.InitParams["MaxConcurrentUploads"], out tempInt))
                {
                    uploadControl.MaxConcurrentUploads = tempInt;
                }
            }

            if (e.InitParams.Keys.Contains("MaxNumberToUpload") && !string.IsNullOrEmpty(e.InitParams["MaxNumberToUpload"]))
            {
                if (int.TryParse(e.InitParams["MaxNumberToUpload"], out tempInt))
                {
                    uploadControl.MaxNumberToUpload = tempInt;
                }
            }

            if (e.InitParams.Keys.Contains("ContinueSize") && !string.IsNullOrEmpty(e.InitParams["ContinueSize"]))
            {
                if (int.TryParse(e.InitParams["ContinueSize"], out tempInt))
                {
                    uploadControl.ContinueSize = tempInt;
                }
            }

            // 先取用户定义是否写日志
            if (e.InitParams.Keys.Contains("IsLog") && !string.IsNullOrEmpty(e.InitParams["IsLog"]))
            {
                if (bool.TryParse(e.InitParams["IsLog"], out tempBool))
                {
                    uploadControl.IsLog = tempBool;
                }
            }

            if (e.InitParams.Keys.Contains("ResizeImage") && !string.IsNullOrEmpty(e.InitParams["ResizeImage"]))
            {
                if (bool.TryParse(e.InitParams["ResizeImage"], out tempBool))
                {
                    uploadControl.IsResizeImage = tempBool;
                }
            }

            if (e.InitParams.Keys.Contains("ImageSize") && !string.IsNullOrEmpty(e.InitParams["ImageSize"]))
            {
                if (int.TryParse(e.InitParams["ImageSize"], out tempInt))
                {
                    uploadControl.ImageSize = tempInt;
                }
            }

            if (e.InitParams.Keys.Contains("Filter") && !string.IsNullOrEmpty(e.InitParams["Filter"]))
            {
                uploadControl.Filter = e.InitParams["Filter"];
            }

            if (e.InitParams.Keys.Contains("AllowThumbnail") && !string.IsNullOrEmpty(e.InitParams["AllowThumbnail"]))
            {
                if (bool.TryParse(e.InitParams["AllowThumbnail"], out tempBool))
                {
                    uploadControl.AllowThumbnail = tempBool;
                }
            }

            if (e.InitParams.Keys.Contains("JsCheckFunction") && !string.IsNullOrEmpty(e.InitParams["JsCheckFunction"]))
            {
                uploadControl.JsCheckFunction = e.InitParams["JsCheckFunction"];
            }

            if (e.InitParams.Keys.Contains("JsCompleteFunction") && !string.IsNullOrEmpty(e.InitParams["JsCompleteFunction"]))
            {
                uploadControl.JsCompleteFunction = e.InitParams["JsCompleteFunction"];
            }

            if (e.InitParams.Keys.Contains("JsCancelFunction") && !string.IsNullOrEmpty(e.InitParams["JsCancelFunction"]))
            {
                uploadControl.JsCancelFunction = e.InitParams["JsCancelFunction"];
            }

            if (e.InitParams.Keys.Contains("ExistedFiles") && !string.IsNullOrEmpty(e.InitParams["ExistedFiles"]))
            {
                uploadControl.ExistedFiles = HttpUtility.UrlDecode(e.InitParams["ExistedFiles"]);
            }

            this.RootVisual = uploadPage;
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
