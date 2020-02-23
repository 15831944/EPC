using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Config;
using Config.Logic;
using System.Configuration;
using Formula.Helper;

namespace Interface.Logic
{
    public class FileSynUploadQueueCreator
    {
        #region 公共属性
        
        SQLHelper _SQLHelpeProject;
        public SQLHelper SQLHelpeProject
        {
            get
            {
                if (_SQLHelpeProject == null)
                    _SQLHelpeProject = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _SQLHelpeProject;
            }
        }

        SQLHelper _SQLHelperInterface;
        public SQLHelper SQLHelperInterface
        {
            get
            {
                if (_SQLHelperInterface == null)
                    _SQLHelperInterface = SQLHelper.CreateSqlHelper("SzsowInteface");
                return _SQLHelperInterface;
            }
        }

        string _BaseServerUrl;
        public string BaseServerUrl
        {
            get
            {
                if (_BaseServerUrl == null)
                {
                    _BaseServerUrl = ConfigurationManager.AppSettings["OEMSyncURL"] ?? "";
                    if (!_BaseServerUrl.EndsWith("/"))
                        _BaseServerUrl = _BaseServerUrl + "/";
                }
                return _BaseServerUrl;
            }
        }

        #endregion

        public void CreateQueue()
        {
            CreateDesignInputFileQueue();//设计输入资料
        }

        #region 生成设计输入文件上传队列

        public void CreateDesignInputFileQueue()
        {
            var sourceSql = @"select doc.* from S_D_InputDocument doc
left join S_D_Input m  on m.ID = doc.InputID
left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1 ";

            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                sourceSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_D_InputDocument>("select * from S_D_InputDocument where 1=1 ");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and doc.CreateDate >='" + startDate + "' and doc.CreateDate <='" + endDate + "'";
            }

            #region Add

            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);
            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);
                var ID = dic.GetValue("ID");
                var fsID = dic.GetValue("Files");
                I_FileSynQueue.CreateUploadQueue(saveSb, fsID, JsonHelper.ToJson(dic), "S_D_InputDocument", ID, "");
            }
            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
            #endregion
        }
        #endregion
    }
}
