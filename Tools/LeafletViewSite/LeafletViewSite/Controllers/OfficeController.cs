
using Leaflet.Adaptor;
using System;
using System.Collections.Generic;
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
    public class OfficeController : ApiController
    {

        [HttpGet]
        public string Versions(string id)
        {
            return OfficeHelper.GetFileByID(id);
        }

        [HttpPost]
        public string SetAnnotation([FromBody] string parameter)
        {
            return OfficeHelper.SetAnnotationAndComment(parameter, true);
        }

        [HttpPost]
        public string SetComment([FromBody] string parameter)
        {
            return OfficeHelper.SetAnnotationAndComment(parameter, false);
        }

        [HttpPost]
        public string Del([FromBody] string parameter)
        {
            return OfficeHelper.DelAnnotationAndComment(parameter);
        }
    }
}
