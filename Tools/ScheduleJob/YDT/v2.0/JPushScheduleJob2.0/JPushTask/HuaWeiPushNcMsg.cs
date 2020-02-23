using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JPushTask
{
    public class HuaWeiPushNcMsg
    {
        private static String appSecret = System.Configuration.ConfigurationManager.AppSettings["HuaWeiAppSecret"];
        private static String appId = System.Configuration.ConfigurationManager.AppSettings["HuaWeiAppId"];//用户在华为开发者联盟申请的appId和appSecret（会员中心->我的产品，点击产品对应的Push服务，点击“移动应用详情”获取）
        private static String tokenUrl = "https://login.cloud.huawei.com/oauth2/v2/token"; //获取认证Token的URL
        private static String apiUrl = "https://api.push.hicloud.com/pushsend.do"; //应用级消息下发API
        private static String accessToken;//下发通知消息的认证Token
        private static DateTime tokenExpiredTime;  //accessToken的过期时间

        private static void refreshToken()
        {
            String msgBody = string.Format(
         "grant_type=client_credentials&client_secret={0}&client_id={1}",
      HttpUtility.UrlEncode(appSecret), appId);


            HttpHelper httpHelper = new HttpHelper();
            String response = httpHelper.httpPost(tokenUrl, msgBody, 5000); //httpPost(tokenUrl, msgBody, 5000, 5000);

            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(response); //JSONObject.parseObject(response);
            accessToken = dic["access_token"]; //obj.getString("access_token");
            tokenExpiredTime = DateTime.Now.AddMilliseconds(long.Parse(dic["expires_in"])); //System.currentTimeMillis() + obj.getLong("expires_in") - 5 * 60 * 1000;
        }

        //type 1:透传；3.通知栏消息
        public static string sendPushMessage(string deviceToken, Hashtable pars, int type = 3)
        {
            if (tokenExpiredTime <= DateTime.Now)
            {
                refreshToken();
            }
            /*PushManager.requestToken为客户端申请token的方法，可以调用多次以防止申请token失败*/
            /*PushToken不支持手动编写，需使用客户端的onToken方法获取*/
            List<string> deviceTokens = new List<string>();//目标设备Token
            deviceTokens.Add(deviceToken);
            //deviceTokens.Add("22345678901234561234567890123456");
            //deviceTokens.Add("32345678901234561234567890123456");

            var body = new Dictionary<string, object>();//仅通知栏消息需要设置标题和内容，透传消息key和value为用户自定义           
            body.Add("title", pars["Title"].ToString());//消息标题
            body.Add("content", pars["Title"].ToString());//消息内容体
            if (type == 1)
            {
                body.Add("fromuser", pars["FromUser"].ToString());
                body.Add("touser", pars["ToUser"].ToString());
                //body.Add("content", pars["Content"].ToString());
                body.Add("id", pars["ID"].ToString());
                body.Add("pid", pars["Pid"].ToString());
                body.Add("type", pars["Type"].ToString());
                body.Add("name", pars["Name"].ToString());
                //body.Add("title", pars["Title"].ToString());
                body.Add("description", pars["Description"].ToString());
                body.Add("badge", pars["Badge"].ToString());
            }
            var msg = new Dictionary<string, object>();
            msg.Add("type", type);//3: 通知栏消息，异步透传消息请根据接口文档设置
            msg.Add("body", body);//通知栏消息body内容

            if (type == 3)
            {
                var param = new Dictionary<string, object>();
                param.Add("appPkgName", "com.ydt_app");//定义需要打开的appPkgName

                var action = new Dictionary<string, object>();
                action.Add("type", 3);//类型3为打开APP，其他行为请参考接口文档设置
                action.Add("param", param);//消息点击动作参数

                msg.Add("action", action);//消息点击动作
            }

            var hps = new Dictionary<string, object>();//华为PUSH消息总结构体
            hps.Add("msg", msg);

            if (type == 3)
            {
                var ext = new Dictionary<string, object>();//扩展信息，含BI消息统计，特定展示风格，消息折叠。
                ext.Add("biTag", "Trump");//设置消息标签，如果带了这个标签，会在回执中推送给CP用于检测某种类型消息的到达率和状态
                ext.Add("icon", "http://pic.qiantucdn.com/58pic/12/38/18/13758PIC4GV.jpg");//自定义推送消息在通知栏的图标,value为一个公网可以访问的URL

                //自定义格式
                var dic = new Dictionary<string, string>();
                dic.Add("fromuser", pars["FromUser"].ToString());
                dic.Add("touser", pars["ToUser"].ToString());
                dic.Add("content", pars["Content"].ToString());
                dic.Add("id", pars["ID"].ToString());
                dic.Add("pid", pars["Pid"].ToString());
                dic.Add("type", pars["Type"].ToString());
                dic.Add("name", pars["Name"].ToString());
                dic.Add("title", pars["Title"].ToString());
                dic.Add("description", pars["Description"].ToString());
                dic.Add("badge", pars["Badge"].ToString());
                var list = new List<Dictionary<string, string>>();
                list.Add(dic);
                ext.Add("customize", list);

                hps.Add("ext", ext);
            }

            //var list = new List<object>();
            //list.Add(new { fromuser = pars["FromUser"].ToString() });
            //list.Add(new { touser = pars["ToUser"].ToString() });
            //list.Add(new { content = pars["Content"].ToString() });
            //list.Add(new { id = pars["ID"].ToString() });
            //list.Add(new { pid = pars["Pid"].ToString() });
            //list.Add(new { type = pars["Type"].ToString() });
            //list.Add(new { name = pars["Name"].ToString() });
            //list.Add(new { title = pars["Title"].ToString() });
            //list.Add(new { description = pars["Description"].ToString() });
            //list.Add(new { badge = pars["Badge"].ToString() });

            //ext.Add("customize", list);


            var payload = new Dictionary<string, object>();
            payload.Add("hps", hps);

            String postBody = string.Format(
             "access_token={0}&nsp_svc={1}&nsp_ts={2}&device_token_list={3}&payload={4}",
               HttpUtility.UrlEncode(accessToken),
                HttpUtility.UrlEncode("openpush.message.api.send"),
                HttpUtility.UrlEncode((DateTime.Now.Ticks / 1000).ToString()),
                HttpUtility.UrlEncode(JsonConvert.SerializeObject(deviceTokens)),
                HttpUtility.UrlEncode(JsonConvert.SerializeObject(payload)));

            String postUrl = apiUrl + "?nsp_ctx=" + HttpUtility.UrlEncode("{\"ver\":\"1\", \"appId\":\"" + appId + "\"}");

            HttpHelper httpHelper = new HttpHelper();
            string result = httpHelper.httpPost(postUrl, postBody, 5000);
            //httpPost(postUrl, postBody, 5000, 5000);
            return result;
        }


    }
}
