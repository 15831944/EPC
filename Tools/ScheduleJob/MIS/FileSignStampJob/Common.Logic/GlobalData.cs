using Common.Logic.Domain;
using Common.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Logic
{
    public class GlobalData
    {

        #region 初始化本地环境

        private static string _pdfImgFolder;
        public static string pdfImgFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_pdfImgFolder))
                {
                    _pdfImgFolder = AppConfig.DllPath + @"\SignImg";
                    if (!Directory.Exists(_pdfImgFolder))
                        Directory.CreateDirectory(_pdfImgFolder);
                }
                return _pdfImgFolder;
            }
        }
        private static string _pdfBaseFolder;
        public static string pdfBaseFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_pdfBaseFolder))
                {
                    _pdfBaseFolder = AppConfig.DllPath + @"\PdfBase";
                    if (!Directory.Exists(_pdfBaseFolder))
                        Directory.CreateDirectory(_pdfBaseFolder);
                }
                return _pdfBaseFolder;
            }
        }
        private static string _pdfSignFolder;
        public static string pdfSignFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_pdfSignFolder))
                {
                    _pdfSignFolder = AppConfig.DllPath + @"\PdfSign";
                    if (!Directory.Exists(_pdfSignFolder))
                        Directory.CreateDirectory(_pdfSignFolder);
                }
                return _pdfSignFolder;
            }
        }
        private static string _pdfCaFolder;
        public static string pdfCaFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_pdfBaseFolder))
                {
                    _pdfCaFolder = AppConfig.DllPath + @"\PdfCA";
                    if (!Directory.Exists(_pdfCaFolder))
                        Directory.CreateDirectory(_pdfCaFolder);
                }
                return _pdfCaFolder;
            }
        }

        #endregion

        #region CA一体机相关
        public static string ServerMoreSignStr = "seal/serverMoreSign";
        public static string QuerySealStrategyStr = "seal/querySealStrategy";
        public static string VerifyStr = "pdf/verify";
        #endregion

        #region 从数据库中获取的全局数据
        private static List<RoleDefine> _allRoles;
        public static List<RoleDefine> AllRoles
        {
            get
            {
                if (_allRoles == null)
                {
                    var sql = "select * from S_D_RoleDefine";
                    var db = SqlHelper.Create(AppConfig.GetConnectionStrings("InfrasBaseConfig"));
                    _allRoles = db.ExecuteList<RoleDefine>(sql);
                }
                return _allRoles;
            }
        }

        private static Dictionary<string, string> _roleNameDic;
        public static Dictionary<string, string> RoleNameDic
        {
            get
            {
                if (_roleNameDic == null)
                {
                    _roleNameDic = new Dictionary<string, string>();
                    foreach (var item in AllRoles)
                        _roleNameDic.Add(item.RoleCode, item.RoleName);
                }
                return _roleNameDic;
            }
        }

        private static List<S_EP_PlotSealInfo> _allSeals;
        public static List<S_EP_PlotSealInfo> AllSeals
        {
            get
            {
                if (_allSeals == null)
                {
                    var sql = "select * from S_EP_PlotSealInfo";
                    var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                    _allSeals = db.ExecuteList<S_EP_PlotSealInfo>(sql);
                }
                return _allSeals;
            }
        }

        private static List<S_EP_PlotSealGroup_GroupInfo> _allGroupSeals;
        public static List<S_EP_PlotSealGroup_GroupInfo> AllGroupSeals
        {
            get
            {
                if (_allGroupSeals == null)
                {
                    var sql = "select * from S_EP_PlotSealGroup_GroupInfo";
                    var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                    _allGroupSeals = db.ExecuteList<S_EP_PlotSealGroup_GroupInfo>(sql);
                }
                return _allGroupSeals;
            }
        }

        private static List<EnumItem> _allMajor;
        public static List<EnumItem> AllMajor
        {
            get
            {
                if (_allMajor == null)
                {
                    var sql = "select Name as text,Code as value,NameEN,SortIndex from S_D_WBSAttrDefine where Type='Major'";
                    var db = SqlHelper.Create(AppConfig.GetConnectionStrings("InfrasBaseConfig"));
                    _allMajor = db.ExecuteList<EnumItem>(sql);
                }
                return _allMajor;
            }
        }
        #endregion
    }
    public class EnumItem
    {
        public string text { get; set; }
        public string value { get; set; }
    }
}
