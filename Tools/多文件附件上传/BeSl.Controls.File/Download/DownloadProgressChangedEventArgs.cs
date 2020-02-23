using System;

namespace BeSl.Controls.File.Download
{
    // 文件上传进度改变代理类
    public delegate void DownloadProgressChangedEvent(object sender, DownloadProgressChangedEventArgs args);

    // 文件上传进度改变参数
    public class DownloadProgressChangedEventArgs
    {
        public int ProgressPercentage { get; set; } // 上传百分率   
        public long BytesDownloaded { get; set; } // 已上传字节
        public long TotalBytes { get; set; }    // 总字节
        public string FileName { get; set; }    // 文件名
        public long TotalBytesDownloaded { get; set; }    // 历史所有已上传字节

        public DownloadProgressChangedEventArgs() { }

        public DownloadProgressChangedEventArgs(int progressPercentage, long bytesDownloaded, long totalBytesDownloaded, long totalBytes, string fileName)
        {
            ProgressPercentage = progressPercentage;
            BytesDownloaded = bytesDownloaded;
            TotalBytes = totalBytes;
            FileName = fileName;
            TotalBytesDownloaded = totalBytesDownloaded;
        }
    }

}
