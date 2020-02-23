using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDFViewer
{
    public class DefaultPDFTaskRepository : IPDFTaskRepository
    {
        public IList<PDFTask> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public PDFTask GetTask(string Id = "")
        {
            var sql = "";
            if (string.IsNullOrWhiteSpace(Id))
            {
                sql = @"select top 1 * from S_D_PDFTask Where Status = 'New' and StartTime is null";
            }
            else
            {
                sql = string.Format("select * from S_D_PDFTask where ID = '{0}'", Id);
            }

            PDFTask task = null;
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteReader(sql);
            if (reader.Read())
            {
                task = new PDFTask();
                task.ID = reader.Get("ID", "");
                task.EndTime = reader.Get<DateTime>("EndTime");
                task.FileID = reader.Get("FileID", "");
                task.FileType = reader.Get("FileType", "");
                task.IsSplit = reader.Get("IsSplit", false);
                task.PDFFileID = reader.Get("PDFFileIDID", "");
                task.PDFPageCount = reader.Get("PDFPageCount", 0);
                task.SnapFileID = reader.Get("SnapFileID", "");
                task.StartTime = reader.Get<DateTime>("StartTime");
                task.Status = reader.Get("Status", "");
                task.SWFFileID = reader.Get("SWFFileID", "");
            }
            reader.Close();
            return task;
        }

        public void AddTask(PDFTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(string Id)
        {
            throw new NotImplementedException();
        }

        public void StartTask(string ID)
        {
            var sql = string.Format("Update S_D_PDFTask set Status='Process',StartTime='{1}' where ID = '{0}'", ID, DateTime.Now);
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteNonQuery(sql);
        }

        public void EndTask(string ID, string status, string remark)
        {
            var sql = string.Format("Update S_D_PDFTask set Status='{2}',EndTime='{1}',Remark='{3}' where ID = '{0}'", ID, DateTime.Now, status, remark);
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteNonQuery(sql);
        }

        public void UpdatePDFFileID(string ID, string pdfFileID, int pdfPageCount, bool isSplit)
        {
            var sql = string.Format("Update S_D_PDFTask set PDFFileID='{1}', PDFPageCount='{2}', IsSplit='{3}' where ID = '{0}'", ID, pdfFileID, pdfPageCount, isSplit);
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteNonQuery(sql);
        }

        public void UpdateSWFFileID(string ID, string swfFileID)
        {
            var sql = string.Format("Update S_D_PDFTask set SWFFileID='{1}' where ID = '{0}'", ID, swfFileID);
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteNonQuery(sql);
        }

        public void UpdateSnapFileID(string ID, string snapFileID)
        {
            var sql = string.Format("Update S_D_PDFTask set SnapFileID='{1}' where ID = '{0}'", ID, snapFileID);
            var db = SqlHelper.Create("FileStore");
            var reader = db.ExecuteNonQuery(sql);
        }

        private PDFTask Row2PDFTask(DataRow dr)
        {
            var task = new PDFTask();
            task.ID = dr["ID"].ToString();
            task.ID = dr["ID"].ToString();

            return task;
        }
    }
}
