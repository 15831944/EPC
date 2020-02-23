using Newtonsoft.Json;
using QrCodeView.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QrCodeView.Controllers
{
    public class QrCodeController : Controller
    {
        public ActionResult Show()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult FileList()
        {
            return View();
        }

        public ActionResult UploadPic()
        {
            return View();
        }

        public JsonResult Get(string id)
        {
            try
            {
                var result = new QrCodeDTO();
                var key = System.Configuration.ConfigurationManager.AppSettings["QrCodeKey"];
                var values = Des.DesDecrypt(id, Des.GetLegalKey(key)).Split('|');
                string type = values[0];
                if (values.Length > 1)
                {
                    string ID = values[1], version = "";
                    if (values.Length > 2) version = values[2];

                    var qrcode = ProductService.GetProductDetail(ID, decimal.Parse(version));
                    qrcode.Type = type;
                    result = qrcode;

                    result.State = true;
                }
                else
                {
                    throw new Exception("系统错误，请联系管理员");
                }
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(new QrCodeDTO { State = false, Message = e.Message });
            }
        }

        //测试用
        public JsonResult G(string id, decimal versionnumber)
        {
            return Json(ProductService.GetProductDetail(id, versionnumber), JsonRequestBehavior.AllowGet);
        }
    }
}
