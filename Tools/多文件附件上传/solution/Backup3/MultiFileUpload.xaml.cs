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
using BeSl.Controls.File.UploadControl;

namespace BeSl.Controls.FileUpload
{
    public partial class MultiFileUpload : UserControl
    {
        public MultiFileUpload()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            //this.PageFileUploadCtrl.UploadUriString = "http://localhost:8008/share/upload/SlUpload/FileUploadHandler.ashx";
        }
    }
}
