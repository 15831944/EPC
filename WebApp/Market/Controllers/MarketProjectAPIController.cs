using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Market.Logic.Domain;
using Formula;
namespace Market.Controllers
{
    public class MarketProjectAPIController : ApiController
    {

        public string PostProgramme(string ProgrammeID)
        {
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var ety = entities.Set<S_I_Engineering>().Find(ProgrammeID);
            if (ety != null)
            {
                string json = Formula.Helper.JsonHelper.ToJson(ety);
                return json;
            }
            else
            {
                throw new Formula.Exceptions.BusinessException("未找到工程信息。");
            }
        }

        public string PostMarketProject(string MPID)
        {
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var ety = entities.Set<S_P_MarketClue>().Find(MPID);
            if (ety != null)
            {
                string json = Formula.Helper.JsonHelper.ToJson(ety);
                return json;
            }
            else
            {
                throw new Formula.Exceptions.BusinessException("未找到经营项目信息。");
            }
        }

        public bool PostUpdateMPState(string MPID, string Phase)
        {
            bool isSuccess = true;
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var ety = entities.Set<S_P_MarketClue>().Find(MPID);
            if (ety != null)
            {
                ety.State = Market.Logic.MarketProjectState.Builded.ToString();//已立项
                //ety.ExistsPhase += "," + Microsoft.JScript.GlobalObject.unescape(Phase);
                //ety.ExistsPhase = ety.ExistsPhase.TrimStart(',');
            }
            else
            {
                isSuccess = false;
            }
            entities.SaveChanges();

            return isSuccess;
        }
    }
}