using System;
using System.Collections.Generic;
using System.Text;

namespace Common.WebAPIKit
{
    public class InternalHelper
    {
        /// <summary>
        /// 获取API的有效相对地址
        /// </summary>
        /// <param name="strUri">全路径</param>
        /// <returns></returns>
        public static string GetEffectiveUri(string strUri)
        {
            int iTheLastPos = strUri.Length;
            int iTheLastQuestionMarkPos = strUri.LastIndexOf('?');
            if (iTheLastQuestionMarkPos != -1)
            {
                iTheLastPos = iTheLastQuestionMarkPos;
            }
            int iTheApiPosition = strUri.IndexOf("/api/", 0, StringComparison.InvariantCultureIgnoreCase);
            if (iTheApiPosition != -1 && iTheApiPosition != 0)
            {
                return strUri.Substring(iTheApiPosition, iTheLastPos - iTheApiPosition);
            }
            return strUri.Substring(0, iTheLastPos);
        }
    }
}
