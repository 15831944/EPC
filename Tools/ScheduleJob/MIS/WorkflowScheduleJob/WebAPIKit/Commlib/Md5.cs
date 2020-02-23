using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace Common.WebAPIKit.Commlib
{
    public static class Md5
    {
        public static string MD5Encode(string strToEncode)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(strToEncode, "MD5");
        }

        public static string MD5TwiceEncode(string strToEncode)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(
                FormsAuthentication.HashPasswordForStoringInConfigFile(strToEncode, "MD5"),
                "MD5");
        }

        public static string Md5TwiceEncodeAndHalf(string strToEncode)
        {
            return MD5TwiceEncode(strToEncode).Substring(0, 16);
        }
    }
}
