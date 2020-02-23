
using Leaflet.Adaptor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace LeafletViewSite.Controllers
{
    public class FileController : ApiController
    {
        /// <summary>
        /// 下载附件.
        /// </summary>
        /// <param name="id">附件名称格式：xxx_Name.pdf</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DownloadAttachment(string id)
        {
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Files") + "\\" + UrlDEncrypt.UrlDecode(id);
            FileStream fs = new FileStream(filePath, FileMode.Open);
            //获取文件大小 
            long size = fs.Length;
            byte[] file = new byte[size];
            //将文件读到byte数组中 
            fs.Read(file, 0, file.Length);
            fs.Close();

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(file);

            string mimeType = "application/octet-stream";
            string fileName = id;
            //if (id.Contains('_'))
            //{
            //    fileName = id.Substring(id.IndexOf('_') + 1);
            //    //可能存在文件名后缀包括_ by:p_ling
            //    if (fileName.Contains('_'))
            //    {
            //        fileName = fileName.Substring(0, fileName.LastIndexOf('_'));
            //    }
            //    if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg"))
            //        mimeType = "image/jpeg";
            //    else if (fileName.EndsWith(".png"))
            //        mimeType = "image/png";
            //    else if (fileName.EndsWith(".tif"))
            //        mimeType = "image/tif";
            //    else if (fileName.EndsWith(".gif"))
            //        mimeType = "image/gif";
            //    else if (fileName.EndsWith(".bmp"))
            //        mimeType = "application/x-MS-bmp";
            //}

            result.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

            if (mimeType == "application/octet-stream")
            {
                fileName = System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8); //HttpContext.Current.Server.UrlEncode(realFileName);
                fileName = fileName.Replace("+", "%20");
                result.Content.Headers.ContentEncoding.Add("UTF-8");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = fileName;
            }

            return result;
        }


        //[HttpPost]
        //public async Task<Dictionary<string, string>> Post(string fileID = "", string relevanceID = "")
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    string root = HttpContext.Current.Server.MapPath("~/Files");//指定要将文件存入的服务器物理位置
        //    var provider = new MultipartFormDataStreamProvider(root);
        //    try
        //    {
        //        await Request.Content.ReadAsMultipartAsync(provider);

        //        foreach (MultipartFileData file in provider.FileData)
        //        {//接收文件
        //            string fileName = file.Headers.ContentDisposition.FileName;
        //            FileInfo fileInfo = new FileInfo(Path.Combine(root, file.LocalFileName));
        //            fileInfo.MoveTo(Path.Combine(root, fileName));

        //            double length = Convert.ToDouble(fileInfo.Length);

        //            //if (!string.IsNullOrEmpty(relevanceID))
        //            //    OfficeHelper.FileUpgrade(fileName, root, relevanceID, length);
        //            //else
        //            //    OfficeHelper.CreateFile(fileName, root, length, fileID);
        //        }//TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器
        //        foreach (var key in provider.FormData.AllKeys)
        //        {//接收FormData
        //            dic.Add(key, provider.FormData[key]);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return dic;
        //}


        [HttpPost]
        public bool Upload()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string root = HttpContext.Current.Server.MapPath("~/Files");
                var relevanceID = httpRequest.Form["RelevanceID"];
                var fileID = httpRequest.Form["FileID"]; 
                foreach (string key in httpRequest.Files)  // 文件键
                {
                    var postedFile = httpRequest.Files[key];    // 获取文件键对应的文件对象
                    var file = root + "/" + postedFile.FileName;
                    postedFile.SaveAs(file);
                    //if (!string.IsNullOrEmpty(relevanceID))
                    //    OfficeHelper.FileUpgrade(postedFile.FileName, root, relevanceID, postedFile.ContentLength);
                    //else
                    //    OfficeHelper.CreateFile(postedFile.FileName, root, postedFile.ContentLength, fileID); 
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }


    }

}
