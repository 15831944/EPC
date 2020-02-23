using System;

namespace BeSl.Controls.File.Upload
{
    /// <summary>
    /// 上传信息配置类 - by rey - 2009.5.25
    /// </summary>
    public class UploadConfig
    {
        #region 属性信息

        // 上传者ID
        string m_uploaderID = String.Empty;
        public string UploaderID
        {
            get { return this.m_uploaderID; }
        }

        // 上传者姓名
        string m_uploaderName = String.Empty;
        public string UploaderName
        {
            get { return this.m_uploaderName; }
        }

        // 允许上传文件大小(M)
        float m_maxFileSize = 0;
        public float MaxFileSize
        {
            get { return this.m_maxFileSize; }
        }

        public long MaxFileLength
        {
            get { return (long)this.m_maxFileSize * 1024 * 1024; }
        }

        // 一次上传文件数量控制
        int m_maxFileNumber = 0;
        public int MaxFileNumber
        {
            get { return this.m_maxFileNumber; }
        }

        // 允许上传文件类型, 默认文本文件和jpg,gif图片
        string m_fileFilter = "*.txt|*.jpg;*.gif";
        public string FileFilter
        {
            get { return this.m_fileFilter; }
        }

        // 处理上传文件的页面
        Uri m_handlerPage = null;
        public Uri HandlerPage
        {
            get { return this.m_handlerPage; }
        }

        // 断点续传文件大小(M)
        float m_continueSize = 0;
        public float ContinueSize
        {
            get { return this.m_continueSize; }
        }

        // 断点续传文件长度
        public long ContinueLength
        {
            get { return (long)this.ContinueSize * 1024 * 1024; }
        }

        // 是否为上传文件写日志
        bool m_isLog = false;
        public bool IsLog
        {
            get { return this.m_isLog; }
            set { this.m_isLog = value; }
        }

        #endregion

        #region 构造函数

        public UploadConfig(float maxFileSize, int maxFileNumber, string fileFilter, Uri handlerPage, float continueSize, bool isLog)
        {
            Init(maxFileSize, maxFileNumber, fileFilter, handlerPage, continueSize, isLog);
        }

        // 方便以后扩展，针对每个人设置个性配置时使用
        public UploadConfig(float maxFileSize, int maxFileNumber, string fileFilter, Uri handlerPage, float continueSize, bool isLog, string uploaderID)
        {
            this.m_uploaderID = uploaderID;

            Init(maxFileSize, maxFileNumber, fileFilter, handlerPage, continueSize, isLog);
        }

        // 初始化类
        private void Init(float maxFileSize, int maxFileNumber, string fileFilter, Uri handlerPage, float continueSize, bool isLog)
        {
            if (String.IsNullOrEmpty(this.UploaderID))
            {
                try
                {
                    this.m_maxFileSize = maxFileSize;
                    this.m_maxFileNumber = maxFileNumber;
                    this.m_fileFilter = fileFilter;
                    this.m_handlerPage = handlerPage;
                    this.m_continueSize = continueSize;
                    this.m_isLog = isLog;
                }
                catch { }
            }
        }

        #endregion

        #region 私有方法

        #endregion

        #region 公有方法

        #endregion
    }
}
