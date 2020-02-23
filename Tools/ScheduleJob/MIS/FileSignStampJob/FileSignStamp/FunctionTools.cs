using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSignStamp
{
    public class FunctionTools
    {
        public static string GetFileName(string fileStr)
        {
            if (!string.IsNullOrEmpty(fileStr))
            {
                var fileName = "";
                var fileArray = fileStr.Split('_');
                if (fileArray.Length >= 2)
                    fileName = fileStr.Substring(fileStr.IndexOf('_') + 1);
                else
                    fileName = fileStr;

                if (fileName.LastIndexOf('.') > -1)
                    return fileName.Substring(0, fileName.LastIndexOf('.'));
                else
                    return fileName;
            }
            else
                return "";
        }
    }
    public class FileTrans
    {
        // <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        static public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        static public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>   
        /// 将 Stream 写入文件   
        /// </summary>   
        static public void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]   
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始   
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件   
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
    }

    public class SignAttInfo
    {
        public string RoleName;
        public string UserName;
        public string UserID;
        public string Date;
    };

    public class SignInfo
    {
        public string RoleName;
        public string UserName;
        public string UserID;
        public string LocalPath;
        public string Date;
        public string StepDes;

        public List<string> BlockKeys;
    };

    public class CoSignInfo
    {
        public string DrawingCode;
        public string RoleName;
        public string UserName;
        public string UserID;
        public string LocalPath;
        public string Date;
        public string Major;
    };

    public class QRCodeInfo
    {
        public string Name;
        public string LocalPath;
    };

    public class StampInfo
    {
        public int? ID;
        public string Name;
        public string LocalPath;
        public string AuthInfo;
        public double Width;
        public double Height;
        public double RelativePosX;
        public double RelativePosY;
        public List<StampInfo> ChildStampInfo = new List<StampInfo>();
    };
}
