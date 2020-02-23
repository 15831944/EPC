using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsFulltextSearch
{
    public class EsFulltextSearchController: MvcAdapter.BaseController
    {
        protected override System.Data.Entity.DbContext entities
        {
            get { throw new NotImplementedException(); }
        }
    }
}