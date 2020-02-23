using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Leaflet.Adaptor
{
    public static class UrlDEncrypt
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlEncode(string data)
        {
            byte[] mByte = null;
            mByte = System.Text.Encoding.GetEncoding("GB2312").GetBytes(data);

            return System.Web.HttpUtility.UrlEncode(mByte);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlDecode(string data)
        {
            return HttpUtility.UrlDecode(data, System.Text.Encoding.GetEncoding("GB2312"));
        }
    }
}
