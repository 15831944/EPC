using System;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BeSl.Controls.File
{
    public class FileHelper
    {
        public const int MaxReadableFileLength = 1048576;   // 最大可读文件长度1M

        /// <summary>
        /// 检查是否文本文件
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsTextFile(FileInfo file)
        {
            bool isTextFile = true;

            using (FileStream fs = file.OpenRead())
            {
                try
                {
                    int i = 0;
                    int length = (int)fs.Length;
                    byte data;

                    while (i < length && isTextFile)
                    {
                        data = (byte)fs.ReadByte();

                        isTextFile = (data != 0);
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }

            return isTextFile;
        }

        /// <summary>
        /// 获取文件文本内容
        /// </summary>
        /// <returns></returns>
        public static string GetTextContent(FileInfo file)
        {
            if (file.Length > MaxReadableFileLength)
            {
                throw new IOException(String.Format("文件“{0}”大小为“{1}k”超过最大可读文件大小1M。", (int)(file.Length / 1024), file.Name));
            }
            else if (!FileHelper.CheckIsTextFile(file))
            {
                throw new IOException(String.Format("文件“{0}”不是合法的文本文件。", file.Name));
            }
            else
            {
                string txtContent = String.Empty;

                using (StreamReader tr = new StreamReader(file.OpenRead(), true))
                {
                    txtContent = tr.ReadToEnd();
                }

                return txtContent;
            }
        }

        /// <summary>
        /// GB2312转换为UTF8
        /// </summary>
        /// <returns></returns>
        public static string GB2UTF8(string str)
        {
            string strResult = str;

            if(!String.IsNullOrEmpty(str)){
                string strGB = str;

                int itemp = 0;
                long iunicode = 0;

                foreach (char tchar in str)
                {
                    itemp = (int)tchar;
                    if (itemp > 127 || itemp < 0)
                    {
                        iunicode = (long)tchar;

                        if (iunicode < 0)
                        {
                            iunicode += 65536;
                        }
                    }
                    else
                    {
                        iunicode = itemp;
                    }
                }

                strResult += U2UTF8(iunicode);
            }

            return strResult;
        }

        public static string U2UTF8(long num){
            string strResult = String.Empty;
            int iTemp = 0, iHexNum = 0;

            if(iHexNum < 128){
                //strResult = strResult & iHexNum;
            }else if(iHexNum < 2048) {
                //sResult = ChrB(&H80 + (iHexNum And &H3F))
                //iHexNum = iHexNum \ &H40
                //sResult = ChrB(&HC0 + (iHexNum And &H1F)) & sResult
            }else if(iHexNum < 65536){
                //sResult = ChrB(&H80 + (iHexNum And &H3F))
                //iHexNum = iHexNum \ &H40
                //sResult = ChrB(&H80 + (iHexNum And &H3F)) & sResult
                //iHexNum = iHexNum \ &H40
                //sResult = ChrB(&HE0 + (iHexNum And &HF)) & sResult
            }

            return strResult;
        }
    }
}
