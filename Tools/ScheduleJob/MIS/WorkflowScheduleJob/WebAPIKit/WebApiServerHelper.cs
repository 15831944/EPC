using System;
using System.Collections.Generic;
using System.Text;
using Common.WebAPIKit.Commlib;

namespace Common.WebAPIKit
{
    public static class WebApiServerHelper
    {
        /// <summary>
        /// 服务器用这个方法来检验Web API请求者的身份是否合法
        /// </summary>
        /// <param name="strAuthUser">来自请求包头的Custom-Auth-Name</param>
        /// <param name="strAuthKey">来自请求包头的Custom-Auth-Key</param>
        /// <param name="strRequestUri">请求的Uri</param>
        /// <param name="strPwdMd5TwiceSvr">从数据库中获取到的密码</param>
        /// <param name="guidRequest">[out]解释出来的请求包GUID，用来防止重发攻击</param>
        public static bool VerifyAuthKey(string strAuthUser, string strAuthKey, string strRequestUri, string strPwdSvr)
        {
            try
            {
                string strUrlAndGuid = Des.Decode(strAuthKey, Md5.MD5TwiceEncode(strPwdSvr));
                string[] arrUrlAndGuid = strUrlAndGuid.Split(new[] { ' ' });
                if (arrUrlAndGuid.Length != 2)
                    return false;

                string strUrl = arrUrlAndGuid[0];
                string strGuid = arrUrlAndGuid[1];

                Guid guidRequest = new Guid(strGuid);

                //判断URL确认身份
                strRequestUri = InternalHelper.GetEffectiveUri(strRequestUri);
                if (string.Compare(Md5.MD5Encode(strRequestUri + guidRequest), strUrl, true) == 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                //Ignore any exception
            }
            return false;
        }

        public static string DecodeConfidentialMessage(string strEncoded, string strPwdMd5TwiceSvr)
        {
            try
            {
                return Des.Decode(strEncoded, strPwdMd5TwiceSvr);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
