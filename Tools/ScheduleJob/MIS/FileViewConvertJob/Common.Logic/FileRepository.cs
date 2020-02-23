using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Common.Logic
{
    public class FileRepository : IFileTaskRepository
    {

        public FileTask GetTask(string fileID = "")
        {
            var sql = "";
            string viewType = AppConfig.GetAppSettings("ViewType");          
            if (string.IsNullOrWhiteSpace(fileID))
            {
                if (AppConfig.IsCad)
                    sql = @"select top 1 ID,Name,ExtName from FsFile Where (ConvertResult is null) and ExtName='dwg'";
                else
                {
                    if (string.IsNullOrEmpty(viewType))
                    {
                        sql = @"select top 1 ID,Name,ExtName from FsFile Where (ConvertResult is null) and ExtName in ('pdf','doc','docx','xls','xlsx','txt','tiff')";
                    }
                    else
                    {
                        viewType = "'" + viewType.Replace(",", "','") + "'";
                        sql = @"select top 1 ID,Name,ExtName from FsFile Where (ConvertResult is null) and ExtName in (" + viewType + ")";
                    }
                }
            }
            else
            {
                sql = string.Format("select ID,Name,ExtName from FsFile where ID = '{0}'", fileID);
            }

            FileTask task = null;
            var db = SqlHelper.Create(AppConfig.GetConnectionStrings("FileStore"));
            var reader = db.ExecuteReader(sql);
            if (reader.Read())
            {
                task = new FileTask();
                task.ID = reader.Get("ID", "");
                task.Name = reader.Get("Name", "");
                task.ExtName = reader.Get("ExtName", "");
                task.PDFPageCount = 1;
            }
            reader.Close();
            return task;
        }

        public void DeleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public void StartTask(string id)
        {
            var sql = string.Format("Update FsFile set ConvertResult='{2}',ConvertTime='{1}' where ID = '{0}'", id, DateTime.Now, ResultStatus.Process);
            var db = SqlHelper.Create(AppConfig.GetConnectionStrings("FileStore"));
            var reader = db.ExecuteNonQuery(sql);
        }

        public void EndTask(string id, ResultStatus status)
        {
            var sql = string.Format("Update FsFile set ConvertResult='{2}',ConvertTime='{1}' where ID = '{0}'", id, DateTime.Now, status.ToString());
            var db = SqlHelper.Create(AppConfig.GetConnectionStrings("FileStore"));
            var reader = db.ExecuteNonQuery(sql);
        }

        public void AddTask(FileTask task)
        {
            throw new NotImplementedException();
        }
    }
}
