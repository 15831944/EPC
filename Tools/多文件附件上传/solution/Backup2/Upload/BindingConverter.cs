using System;
using System.Windows.Data;

namespace BeSl.Controls.File.Upload
{
    // 提供UI绑定的转换类(字节数转换)
    public class ByteConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string size = "0 KB";

            if (value != null)
            {
                long byteCount = (long)value;

                if (byteCount >= 1073741824)
                {
                    size = String.Format("{0:##.##}", byteCount / 1073741824) + " GB";
                }
                else if (byteCount >= 1048576)
                {
                    size = String.Format("{0:##.##}", byteCount / 1048576) + " MB";
                }
                else if (byteCount >= 1024)
                {
                    size = String.Format("{0:##.##}", byteCount / 1024) + " KB";
                }
                else if (byteCount > 0 && byteCount < 1024)
                {
                    size = "1 KB";
                }
            }

            return size;
        }

        // 单向绑定，不需要实现此方法
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    // 提供UI绑定的转换类(字节数转换)
    public class UploadStatusConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string rtnstr = "待定";

            if (value != null)
            {
                switch ((UploadStatus)value)
                {
                    case UploadStatus.Canceled:
                        rtnstr = "取消";
                        break;
                    case UploadStatus.Complete:
                        rtnstr = "完成";
                        break;
                    case UploadStatus.Error:
                        rtnstr = "错误";
                        break;
                    case UploadStatus.Removed:
                        rtnstr = "移除";
                        break;
                    case UploadStatus.Resizing:
                        rtnstr = "调整";
                        break;
                    case UploadStatus.Uploading:
                        rtnstr = "上传";
                        break;
                    case UploadStatus.Pending:
                    default:
                        rtnstr = "待定";
                        break;
                }
            }

            return rtnstr;
        }

        // 单向绑定，不需要实现此方法
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
