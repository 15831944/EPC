using Monitor.Logic.Enum;

namespace Monitor.Logic.Helper
{
    public class WebApi
    {
        /// <summary>
        /// 成功后的输出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultDTO Success(object data)
        {
            return new ResultDTO
            {
                status = true,
                info = data
            };
        }

        /// <summary>
        /// 失败后的输出
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static ResultDTO Error(EnumException exception)
        {
            return new ResultDTO
            {
                status = false,
                info = exception.ToString()
            };
        }

        /// <summary>
        /// 失败后的输出
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static ResultDTO Error(string mess)
        {
            return new ResultDTO
            {
                status = false,
                info = mess
            };
        }
    }


    /// <summary>
    /// 输出结果
    /// </summary>
    public class ResultDTO
    {
        public bool status; //状态
        public object info; //详情
    }
}
