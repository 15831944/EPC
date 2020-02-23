
namespace Common.WebAPIKit
{
    public static class Consts
    {
        //用于客户端向服务器端发送“用户名”
        public const string HTTP_HEADER_AUTH_USER = "Custom-Auth-Name";
        //用于客户端向服务器端发送的加密的用于验证的“Key”
        public const string HTTP_HEADER_AUTH_KEY = "Custom-Auth-Key";

        //用于服务器端向客户端返回的Stream资源的“时间戳”
        public const string HTTP_HEADER_UPDATETICKS = "Custom-Update-Ticks";

        //用于验证是否微信方式验证
        public const string HTTP_HEADER_AUTH_WEIXIN_USER = "Custom-Auth-WeiXin-User";
    }
}
