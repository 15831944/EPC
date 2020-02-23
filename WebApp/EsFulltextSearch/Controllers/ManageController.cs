using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcAdapter;
using System.Configuration;
using System.IO;
using System.Text;
using Formula.Helper;
using Config.Logic;
using EsHelper;

namespace EsFulltextSearch.Controllers
{
    public class ManageController : EsFulltextSearchController
    {
        public JsonResult GetDictList(QueryBuilder qb)
        {
            var qbItem = qb.Items.FirstOrDefault(a => a.Field == "word");
            var queryValue = string.Empty;
            if (qbItem != null)
                queryValue = qbItem.Value.ToString();
            var txtPath = ConfigurationManager.AppSettings["FulltextSearch_IK_Dict"];
            StreamReader sr = new StreamReader(txtPath, Encoding.UTF8);
            string content = sr.ReadToEnd();
            var ary = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var list = new List<Dictionary<string, string>>();
            foreach (var item in ary)
            {
                if (string.IsNullOrEmpty(queryValue) || item.Contains(queryValue))
                    list.Add(new Dictionary<string, string>() { { "word", item } });
            }
            var data = new GridData(list);
            data.total = ary.Length;
            sr.Close();
            return Json(data);
        }

        public JsonResult SaveDictList()
        {
            //try
            //{
            var txtPath = ConfigurationManager.AppSettings["FulltextSearch_IK_Dict"] ;
            FileStream fs = new FileStream(txtPath, FileMode.Create);
            var listData = Request["ListData"];
            List<Dictionary<string, string>> rows = JsonHelper.ToObject<List<Dictionary<string, string>>>(listData);
            var sb = new StringBuilder();
            foreach (var item in rows)
            {
                var word = item.GetValue("word");
                if (string.IsNullOrEmpty(word))
                    continue;
                if(item!=rows.LastOrDefault())
                    sb.AppendLine(word);
                else
                    sb.Append(word);
            }
            byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
            fs.Write(data, 0, data.Length);

            fs.Flush();
            fs.Close();
            //}
            //catch (Exception e)
            //{
            //    throw new Formula.Exceptions.BusinessException(e.Message);
            //}
            return Json(new { });
        }
        
        public JsonResult GetStopWordList(QueryBuilder qb)
        {
            var qbItem = qb.Items.FirstOrDefault(a => a.Field == "word");
            var queryValue = string.Empty;
            if (qbItem != null)
                queryValue = qbItem.Value.ToString();
            var txtPath = ConfigurationManager.AppSettings["FulltextSearch_IK_StopWord"];
            StreamReader sr = new StreamReader(txtPath, Encoding.UTF8);
            string content = sr.ReadToEnd();
            var ary = content.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var list = new List<Dictionary<string, string>>();
            foreach (var item in ary)
            {
                if (string.IsNullOrEmpty(queryValue) || item.Contains(queryValue))
                    list.Add(new Dictionary<string, string>() { { "word", item } });
            }
            var data = new GridData(list);
            data.total = ary.Length;
            sr.Close();
            return Json(data);
        }

        public JsonResult SaveStopWordList()
        {
            //try
            //{
            var txtPath = ConfigurationManager.AppSettings["FulltextSearch_IK_StopWord"];
            FileStream fs = new FileStream(txtPath, FileMode.Create);
            var listData = Request["ListData"];
            List<Dictionary<string, string>> rows = JsonHelper.ToObject<List<Dictionary<string, string>>>(listData);
            var sb = new StringBuilder();
            foreach (var item in rows)
            {
                var word = item.GetValue("word");
                if (string.IsNullOrEmpty(word))
                    continue;
                if (item != rows.LastOrDefault())
                    sb.AppendLine(word);
                else
                    sb.Append(word);
            }
            byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
            fs.Write(data, 0, data.Length);

            fs.Flush();
            fs.Close();
            //}
            //catch (Exception e)
            //{
            //    throw new Formula.Exceptions.BusinessException(e.Message);
            //}
            return Json(new { });
        }

        public JsonResult EpdateEsData()
        {
            string EsNodeUrl = ConfigurationManager.AppSettings["EsNodeUrl"];
            var esHelper = EsBaseHelper.CreateEsHelper(EsNodeUrl); //查询
            esHelper.DefaultIndex = EsConst.defaultEsFileIndex;
            esHelper.UpdateByQuery();
            return Json(new { });
        }
    }
}
