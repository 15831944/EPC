using System;
using System.Collections.Generic;

namespace Leaflet.Adaptor.DTO
{
    /// <summary>
    /// 图片
    /// </summary>
    public class ImageDTO
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 当前版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 所有版本
        /// </summary>
        public List<VersionDTO> Versions { get; set; }

    }

    /// <summary>
    /// 版本
    /// </summary>
    public class VersionDTO
    {
        /// <summary>
        /// 版本ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public string FileID { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 转换后图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 图片层数
        /// </summary>
        public int ImageZoomLevel { get; set; }

        /// <summary>
        /// 切的高度
        /// </summary>
        public int HighHeightUnit { get; set; }

        /// <summary>
        /// 引用
        /// </summary>
        public string XrefPath { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string FontPath { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 批注信息
        /// </summary>
        public List<AnnotationDTO> Annotations { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public List<CommentDTO> Comments { get; set; }
    }

    /// <summary>
    /// 评论/批注信息
    /// </summary>
    public class AnnotationDTO 
    {
        /// <summary>
        /// 评论/批注ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 回复用户
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论时间 
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 批注 
        /// </summary>
        public GraphDataDTO GraphData { get; set; }

        /// <summary>
        /// 层 
        /// </summary>
        public string LayerID { get; set; }

        /// <summary>
        /// 类型 
        /// </summary>
        public string LayerType { get; set; }
    }

    /// <summary>
    /// 批注坐标信息等,前端小写
    /// </summary>
    public class GraphDataDTO
    {

        /// <summary>
        /// ID 
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// layerType 
        /// </summary>
        public string layerType { get; set; }

        /// <summary>
        /// style 
        /// </summary>
        public StyleDTO style { get; set; }

        /// <summary>
        /// geoJSON 
        /// </summary>
        public GeoJSONDTO geoJSON { get; set; }
    }

    /// <summary>
    /// 批注样式,前端小写
    /// </summary>
    public class StyleDTO
    {
        /// <summary>
        /// stroke 
        /// </summary>
        public bool stroke { get; set; }

        /// <summary>
        /// color 
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// weight 
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// opacity 
        /// </summary>
        public double opacity { get; set; }

        /// <summary>
        /// fill 
        /// </summary>
        public bool fill { get; set; }

        /// <summary>
        /// fill 
        /// </summary>
        public bool clickable { get; set; }

        /// <summary>
        /// radius 
        /// </summary>
        public double radius { get; set; }

    }

    /// <summary>
    /// 批注信息,前端小写
    /// </summary>
    public class GeoJSONDTO
    {
        /// <summary>
        /// type 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// properties 
        /// </summary>
        public PropertieDTO properties { get; set; }

        /// <summary>
        /// geometry 
        /// </summary>
        public GeometryDTO geometry { get; set; }
    }

    /// <summary>
    /// 批注信息,前端小写
    /// </summary>
    public class PropertieDTO
    {
    }


    /// <summary>
    /// 批注信息,前端小写
    /// </summary>
    public class GeometryDTO
    {
        /// <summary>
        /// type 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// coordinates 
        /// </summary>
        public List<object> coordinates { get; set; }

    }

    /// <summary>
    /// 意见信息,前端小写
    /// </summary>
    public class CommentDTO
    {
        /// <summary>
        /// 评论/批注ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 回复
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论时间 
        /// </summary>
        public DateTime Time { get; set; }

    }

}
