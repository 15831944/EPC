using System;

namespace BeSl.Controls.File.Upload
{
    // 文件上传进度改变代理类
    public delegate void UploadProgressChangedEvent(object sender, UploadProgressChangedEventArgs args);

    // 文件上传进度改变参数
    public class UploadProgressChangedEventArgs
    {
        public int ProgressPercentage { get; set; } // 上传百分率   
        public long BytesUploaded { get; set; } // 已上传字节
        public long TotalBytes { get; set; }    // 总字节
        public string FileName { get; set; }    // 文件名
        public long TotalBytesUploaded { get; set; }    // 历史所有已上传字节

        public UploadProgressChangedEventArgs() { }

        public UploadProgressChangedEventArgs(int progressPercentage, long bytesUploaded, long totalBytesUploaded, long totalBytes, string fileName)
        {
            ProgressPercentage = progressPercentage;
            BytesUploaded = bytesUploaded;
            TotalBytes = totalBytes;
            FileName = fileName;
            TotalBytesUploaded = totalBytesUploaded;
        }
    }

}
