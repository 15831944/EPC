using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;
using Formula;
using System.Data;

namespace Market.Areas.MarketUI.Controllers
{
    public class ClueController : MarketFormContorllor<S_P_MarketClue>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            base.BeforeSave(dic, formInfo, isNew);
            //if (isNew)
            //{
            //    var user = Formula.FormulaHelper.GetUserInfo();
            //    //登记投标信息
            //    S_B_Bid bid = new S_B_Bid();
            //    bid.ID = dic.GetValue("ID");
            //    bid.Address = dic.GetValue("Adress");
            //    bid.BidAddress = "";//
            //    bid.BidClearFile = "";//
            //    bid.BidCode = "";//
            //    bid.BidFile = "";//
            //    bid.BidFilePrice = null;//
            //    bid.BidIssuedDate = null;//
            //    bid.BidOtherFile = "";//
            //    bid.BidPhase = "";//
            //    bid.BidResult = "";//
            //    bid.Project = dic.GetValue("ID");
            //    bid.ProjectName = dic.GetValue("Name");

            //    bid.BidSurrogateUnit = "";//
            //    bid.BidType = "";//
            //    bid.BidUntiRemark = "";//
            //    bid.BusinessClass = dic.GetValue("ProjectClass");
            //    bid.BusinessOwner = dic.GetValue("CustomerInfo");
            //    bid.BusinessOwnerName = dic.GetValue("CustomerInfoName");
            //    bid.CompanyID = dic.GetValue("CompanyID");
            //    bid.CompetencyDate = null;//
            //    bid.CompleteBidDate = null;//
            //    bid.CreateDate = DateTime.Now;
            //    bid.CreateUser = user.UserName;
            //    bid.CreateUserID = user.UserID;
            //    bid.DeptInfo = dic.GetValue("DeptInfo");
            //    bid.DeptInfoName = dic.GetValue("DeptInfoName");
            //    bid.EngineeringInfo = "";
            //    bid.EngineeringInfoName = "";
            //    bid.EnsureAmount = null;//
            //    bid.EnsureAmountIsReturn = "";//
            //    bid.EnsureAmountReturnDate = null;//
            //    bid.InviteBidClearFile = "";
            //    bid.InviteBidFile = "";
            //    bid.InviteBidOtherFile = "";
            //    bid.IsWinBid = "";//
            //    bid.ModifyDate = DateTime.Now;
            //    bid.ModifyUser = user.UserName;
            //    bid.ModifyUserID = user.UserID;
            //    bid.OrgID = user.UserOrgID;
            //    bid.PostCode = "";//
            //    bid.ProjectManager = ""; //               
            //    bid.Remark = "";//
            //    bid.ResultUnit = "";//
            //    bid.TenderCode = "";//
            //    bid.TenderOrgan = "";//
            //    bid.TradeAmount = null;//
            //    bid.TradeUnitAmount = null;//

            //    BusinessEntities.Set<S_B_Bid>().Add(bid);
            //}
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var item in Ids)
            {
                var clue = this.GetEntityByID<S_P_MarketClue>(item);
                if (clue != null)
                    clue.Delete();

                BusinessEntities.Set<S_B_Bid>().Delete(a => a.Project == item);
                BusinessEntities.Set<S_B_Bond>().Delete(a => a.Project == item);
            }
        }

        public JsonResult SetEngineeringInfo(string ID, string SingleTaskNotice)
        {
            var clue = this.GetEntityByID(ID);
            if (clue == null) throw new Formula.Exceptions.BusinessException("未能找到指定的前期项目信息，无法启动立项");
            var engineering = this.GetEntityByID<S_I_Engineering>(clue.EngineeringInfo);
            if (engineering == null)
            {
                engineering = new S_I_Engineering();
                engineering.ID = FormulaHelper.CreateGuid();
                engineering.Name = clue.Name;
                engineering.Code = clue.SerialNumber;
                engineering.CustomerInfo = clue.CustomerInfo;
                engineering.CustomerInfoName = clue.CustomerInfoName;
                engineering.BusinessManager = clue.BusinessManager;
                engineering.BusinessManagerName = clue.BusinessManagerName;
                engineering.Country = clue.Country;
                engineering.Province = clue.Province;
                engineering.City = clue.City;
                this.BusinessEntities.Set<S_I_Engineering>().Add(engineering);
                clue.EngineeringInfo = engineering.ID;
                clue.EngineeringInfoName = engineering.Name;
            }
            this.BusinessEntities.SaveChanges();
            if (!String.IsNullOrEmpty(SingleTaskNotice) && SingleTaskNotice == true.ToString())
            {
                var db = Config.SQLHelper.CreateSqlHelper(Config.ConnEnum.Project);
                var obj = db.ExecuteScalar("select count(0) from T_CP_TaskNotice where RelateID='" + ID + "'");
                if (obj != null && obj != DBNull.Value)
                {
                    if (Convert.ToInt32(obj) > 0)
                        throw new Formula.Exceptions.BusinessException("项目【" + clue.Name + "】已经下达过任务单，不能重复下达");
                }
            }
            return Json(engineering);
        }

        public JsonResult GetTrackInfo(String ClueID)
        {
            var data = this.BusinessEntities.Set<S_P_MarketClue_TraceInfo>().Where(d => d.S_P_MarketClueID == ClueID).ToList();
            return Json(data);
        }
    }
}
