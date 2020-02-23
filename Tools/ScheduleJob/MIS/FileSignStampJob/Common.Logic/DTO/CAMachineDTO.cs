using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Logic.DTO
{
    public class CAMachineDTO
    {
        /// <summary>
        /// 平台分配的API Key
        /// </summary>
        public string api_key { get; set; }
        /// <summary>
        /// 平台分配的API Secret
        /// </summary>
        public string api_secret { get; set; }
        /// <summary>
        /// 应用签名，由印章服务器策略决定是否需要必填
        /// </summary>
        public string api_sign { get; set; }

        public CAMachineDTO()
        {
            this.api_key = AppConfig.API_KEY;
            this.api_secret = AppConfig.API_SECRET;
        }
        public CAMachineDTO(string key, string secret)
        {
            this.api_key = key;
            this.api_secret = secret;
        }
    }

    /// <summary>
    /// 印章策略查询请求格式
    /// </summary>
    public class QuerySealStrategy : CAMachineDTO
    {
        /// <summary>
        /// 开始时间（拼接组装签名原文）时间格式为yyyyMMddHHmmss
        /// </summary>
        public string begin_time { get; set; }
        /// <summary>
        /// 结束时间（拼接组装签名原文）时间格式为yyyyMMddHHmmss
        /// </summary>
        public string end_time { get; set; }

        public QuerySealStrategy() : base() { }
        public QuerySealStrategy(string key, string secret) : base(key, secret) { }
    }

    /// <summary>
    /// 服务端批量签章请求格式
    /// </summary>
    public class ServerMoreSign : CAMachineDTO
    {
        /// <summary>
        /// Base64编码后的PDF文件数据
        /// </summary>
        public string pdf { get; set; }
        /// <summary>
        /// 文档唯一编号
        /// </summary>
        public string document_no { get; set; }
        /// <summary>
        /// 签章信息json数组（包涵多种签章策略信息）
        /// </summary>
        public List<CASealDTO> seal { get; set; }
        public ServerMoreSign() : base() { }
        public ServerMoreSign(string key, string secret) : base(key, secret) { }
    }

    /// <summary>
    /// 服务端验章请求格式
    /// </summary>
    public class Verify : CAMachineDTO
    {
        /// <summary>
        /// 数据节点
        /// </summary>
        public Verify_Data data { get; set; }
        public Verify() : base() { }
        public Verify(string key, string secret) : base(key, secret) { }
    }

    public class Verify_Data
    {
        /// <summary>
        /// Base64编码后的PDF文件数据
        /// </summary>
        public string pdf { get; set; }
    }

    public class CASealDTO
    {
        /// <summary>
        /// 签章策略ID
        /// </summary>
        public int seal_strategy_id { get; set; }
        /// <summary>
        /// 签章位置说明，支持后台配置规则定位、位置定位和关键字两种方式，只能选择一种，如果选择则对应的配置需要必填
        ///auto:基于后台配置的位置规则
        ///position：基于位置盖章
        ///keyword：基于关键字盖章
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 签章位置信息
        /// </summary>
        public Position position { get; set; }
        /// <summary>
        /// 签章位置信息
        /// </summary>
        public Keyword keyword { get; set; }
        /// <summary>
        /// PDF中印章显示的大小
        /// </summary>
        public Size size { get; set; }
        /// <summary>
        /// 签章时间，格式为yyyyMMddHHmmss。如果不传入，则使用服务器当前时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// pdf旋转角度，格式为0，90，180，270，360
        /// </summary>
        public string degree { get; set; }
        /// <summary>
        /// 是否骑缝盖章   true：是  false：否
        /// </summary>
        public string perforation { get; set; }
        /// <summary>
        /// 图章透明设置，1-10，透明度依次递减
        /// </summary>
        public string alpha { get; set; }

        public CASealDTO()
        {
            this.type = "position";
            this.degree = "0";
            this.perforation = "false";
            this.alpha = AppConfig.Alpha;
            this.time = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    public class Position
    {
        /// <summary>
        /// 页码
        /// </summary>
        public string page { get; set; }
        /// <summary>
        /// 对应文档x坐标，（左下角为原点，最大50000）
        /// </summary>
        public string x { get; set; }
        /// <summary>
        /// 对应文档y坐标，（左下角为原点，最大50000）
        /// </summary>
        public string y { get; set; }
    }

    public class Keyword
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 关键字位置文字，使用UrlEncode(UTF-8)编码传输
        /// </summary>
        public int content { get; set; }
    }

    public class Size
    {
        /// <summary>
        /// 印章图片显示宽度，单位毫米（mm）
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// 印章图片显示高度，单位毫米（mm）
        /// </summary>
        public double height { get; set; }
    }

    /// <summary>
    /// 印章策略查询返回格式
    /// </summary>
    public class QuerySealStrategy_Return
    {
        /// <summary>
        /// 结果代码，0为成功，其他为失败
        /// </summary>
        public int ret_code { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 策略信息节点
        /// </summary>
        public List<CASealDTO_Return> data { get; set; }
    }

    /// <summary>
    /// 服务端批量签章返回格式
    /// </summary>
    public class ServerMoreSign_Return
    {
        /// <summary>
        /// 结果代码，0为成功，其他为失败
        /// </summary>
        public int ret_code { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 文档唯一标号
        /// </summary>
        public string document_no { get; set; }
        /// <summary>
        /// Base64编码后的PDF文件数据
        /// </summary>
        public string pdf { get; set; }
    }

    /// <summary>
    /// 服务端验章返回格式
    /// </summary>
    public class Verify_Return
    {
        /// <summary>
        /// 结果代码，0为成功，其他为失败
        /// </summary>
        public int ret_code { get; set; }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 验证结果详情节点（数组）
        /// </summary>
        public List<VerifyNode_Return> data { get; set; }
    }

    public class CASealDTO_Return
    {
        /// <summary>
        /// 策略插入时间(时间格式为yyyyMMddHHmmss)
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 策略名称
        /// </summary>
        public string strategy_name { get; set; }
        /// <summary>
        /// 证书是否过期。 true策略证书有效，false策略证书过期
        /// </summary>
        public bool expires { get; set; }
        /// <summary>
        /// 印章缩略图片
        /// </summary>
        public string seal_thumbnail { get; set; }
        /// <summary>
        /// 策略证书过期时间(时间格式为yyyyMMddHHmmss)
        /// </summary>
        public string not_after { get; set; }
        /// <summary>
        /// 策略id
        /// </summary>
        public int strategy_id { get; set; }
        /// <summary>
        /// 印模类型
        /// 1.个人
        /// 2.公司
        /// 3.资质
        /// </summary>
        public int picture_form { get; set; }
        /// <summary>
        /// 印模类型：
        ///1.一级注册结构工程师
        ///2.二级注册结构工程师
        ///3.注册土木工程师（岩土）
        ///4.注册电气工程师（供配电）
        ///5.注册电气工程师（发输变电）
        ///6.一级注册建筑师
        ///7.二级注册建筑师
        ///8.注册城市规划师
        ///9.注册造价工程师
        ///10.注册公用设备工程师（给水排水）
        ///11.注册公用设备工程师（暖通空调）
        ///12.注册公用设备工程师（动力）
        ///13.注册咨询工程师（投资）
        ///14.上海市咨询专家
        ///15.上海市咨询师
        /// </summary>
        public int quality_type { get; set; }
    }

    public class VerifyNode_Return
    {
        /// <summary>
        /// 验证是否成功，成功true,失败false（表明改节点的签章验证成功为true，验证失败则为false）
        /// </summary>
        public bool verify { get; set; }
        /// <summary>
        /// 验证结果说明
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 签章名称
        /// </summary>
        public string sign_name { get; set; }
        /// <summary>
        /// 签署时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 签章算法
        /// </summary>
        public string sign_alg { get; set; }
        /// <summary>
        /// 签名证书序列号
        /// </summary>
        public string cert_sn { get; set; }
        /// <summary>
        /// 签名证书主题
        /// </summary>
        public string cert_subject { get; set; }
    }
}
