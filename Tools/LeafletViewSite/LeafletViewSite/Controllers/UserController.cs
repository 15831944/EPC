using Leaflet.Adaptor;
using Leaflet.Adaptor.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
    public class UserController : ApiController
    {
        [HttpGet]
        public string Users()
        {
            return OfficeHelper.GetUsers();
        }


        [HttpPost]
        public bool SyncUser()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var data = httpRequest.Form["Data"];
                List<UserDTO> userDTO = JsonHelper.ToObject<List<UserDTO>>(data.ToString());
                foreach (string key in httpRequest.Files)  // 文件键
                {
                    var postedFile = httpRequest.Files[key];    // 获取文件键对应的文件对象
                    Stream stream = postedFile.InputStream;
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    // 设置当前流的位置为流的开始  
                    stream.Seek(0, SeekOrigin.Begin);
                    MemoryStream ms = new MemoryStream(bytes);
                    Image img = Image.FromStream(ms);
                    ImageFormat imageFormat = ImageFormat.Jpeg;

                    string filePath = string.Format("{0}\\UserIco\\{1}", System.Web.HttpContext.Current.Server.MapPath("~/Files"), postedFile.FileName);
                    if (!File.Exists(filePath))
                    {
                        img.Save(filePath, imageFormat);
                    }
                    foreach (var user in userDTO)
                    {
                        if (postedFile.FileName.IndexOf(user.UserID) >= 0)
                        {
                            user.Ico = postedFile.FileName;
                            break;
                        }
                    }
                    ms.Close();
                    stream.Close();
                }
                return OfficeHelper.SetUsers(userDTO);
            }
            catch (Exception err)
            {
                return false;
            }
          
        }
    }
}
