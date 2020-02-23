using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPush.Api.Push.Mode;
using JPush.Api.Push.Notification;

namespace JPush.Api
{
    public class PushMessage
    {
        /// <summary>
        /// 推送给所有平台所有用户
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_All(string content)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(content);
            return pushPayload;
        }

        /// <summary>
        /// 推送给指定用户（个人或群组）
        /// 用别名来标识一个用户。一个设备只能绑定一个别名，但多个设备可以绑定同一个别名。一次推送最多 1000 个
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="alias">用户群组</param>
        /// <returns></returns>
        public static PushPayload PushObject_All_Alias(string content, string[] alias)
        {

            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.s_alias(alias);
            pushPayload.notification = new Notification().setAlert(content);
            return pushPayload;
        }

        /// <summary>
        /// 推送给安卓用户
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <returns></returns>
        public static PushPayload PushObject_Android_All(string content, string title, Dictionary<string, string> dict = null)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android();
            pushPayload.audience = Audience.all();
            pushPayload.notification=Notification.android(content, title);

            //判断是否有额外信息
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    pushPayload.notification.AndroidNotification.AddExtra(item.Key, item.Value);
                }
            }

            return pushPayload;
        }

        /// <summary>
        /// 推送给IOS用户
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="sound">提示音</param>
        /// <param name="dict">额外信息</param>
        /// <returns></returns>
        public static PushPayload PushObject_Ios_All(string content, Dictionary<string, string> dict = null, string sound = "default")
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = Audience.all();
            var notification = new Notification();
            notification.IosNotification = new IosNotification();
            notification.IosNotification.setAlert(content).incrBadge(1).setSound(sound);

            //判断是否有额外信息
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    notification.IosNotification.AddExtra(item.Key, item.Value);
                }
            }

            pushPayload.notification = notification;
            pushPayload.message = Message.content(content);
            return pushPayload;
        }


        /// <summary>
        /// 推送到ios和andriod平台
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <param name="alias">用户群组</param>
        /// <param name="isProduction">是生产环境</param>
        /// <returns></returns>
        public static PushPayload PushObject_AndroidAndIos_Alias(string content, string title, string[] alias, bool isProduction = true, Dictionary<string, string> dict = null)
        {
            return PushObject_AndroidAndIos_Audience(content, title, Audience.s_alias(alias),isProduction, dict);
        }

        /// <summary>
        /// 推送到ios和andriod平台
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <param name="isProduction">是生产环境</param>
        /// <returns></returns>
        public static PushPayload PushObject_AndroidAndIos_All(string content, string title, bool isProduction = true, Dictionary<string, string> dict = null)
        {
            return PushObject_AndroidAndIos_Audience(content, title, Audience.all(),isProduction, dict);
        }

        /// <summary>
        /// 推送到ios和andriod平台
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <param name="tag">用户群组</param>
        /// <param name="isProduction">是生产环境</param>
        /// <returns></returns>
        public static PushPayload PushObject_AndroidAndIos_Tag(string content, string title, string[] tag, bool isProduction = true, Dictionary<string, string> dict = null)
        {
            return PushObject_AndroidAndIos_Audience(content, title, Audience.s_tag(tag), isProduction,dict);
        }

        /// <summary>
        /// 推送到ios和andriod平台
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <param name="tag">用户群组</param>
        /// <param name="isProduction">是生产环境</param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static PushPayload PushObject_AndroidAndIos_Tag_Alias(string content, string title, string[] tag, string[] alias, bool isProduction = true, Dictionary<string, string> dict = null)
        {
            return PushObject_AndroidAndIos_Audience(content, title, Audience.s_tag(tag).alias(alias),isProduction,dict);
        }

        /// <summary>
        /// 推送到ios和andriod平台
        /// </summary>
        /// <param name="content">通知内容</param>
        /// <param name="title">通知标题</param>
        /// <param name="dict">额外信息</param>
        /// <param name="isProduction">是生产环境</param>
        /// <param name="audience">推送的用户</param>
        /// <returns></returns>
        public static PushPayload PushObject_AndroidAndIos_Audience(string content, string title, Audience audience, bool isProduction=true, Dictionary<string, string> dict = null)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android_ios();
            pushPayload.audience = audience;

            var notification = new Notification().setAlert(content);

            notification.AndroidNotification = new AndroidNotification();
            notification.AndroidNotification.setTitle(title);

            notification.IosNotification = new IosNotification();
            notification.IosNotification.incrBadge(1);
            notification.IosNotification.setSound("default");

            //判断是否有额外信息
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    notification.AndroidNotification.AddExtra(item.Key, item.Value);
                    notification.IosNotification.AddExtra(item.Key, item.Value);
                }
            }

            pushPayload.notification = notification.Check();
            pushPayload.ResetOptionsApnsProduction(isProduction);
            return pushPayload;
        }
    }
}
