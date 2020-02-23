using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
namespace Market.Areas.Basic.Controllers
{
    public class BidController : MarketFormContorllor<S_B_Bid>
    {
        protected override void BeforeDelete(string[] Ids)
        {
            base.BeforeDelete(Ids);
        }
    }
}
