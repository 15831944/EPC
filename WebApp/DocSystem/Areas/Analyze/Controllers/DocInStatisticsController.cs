using Config;
using DocSystem.Logic.Domain;
using Formula.Helper;
using MvcAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
namespace DocSystem.Areas.Analyze.Controllers
{
    public class DocInStatisticsController : DocConstController
    {
        //
        // GET: /Analyze/InStatisticsDocumentType/
        //档案收进统计
        //档案数据
        public JsonResult GetSpaceDcoument()
        {
            List<Dictionary<string, string>> dataList = new List<Dictionary<string, string>>();
            var spaces = Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.Select(a => a);
            foreach (var space in spaces)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.SetValue("value", space.ID);
                dic.SetValue("text", space.Name);
                dataList.Add(dic);
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);//档案空间选项
        }
        #region  档案类型
        public ActionResult DocumentType()
        {
            
            return View();
        }
        
        //档案收进统计按档案类型
        public JsonResult DocumentTypeGetList(string SpaceID)
        {
            var entities=Formula.FormulaHelper.GetEntities<DocConfigEntities>();
            var space = entities.S_DOC_Space.FirstOrDefault(a => a.ID.Equals(SpaceID));
            string sql = @"select * from ({0}) fileTemp full join ({1}) node on fileTemp.FileNodeYear=NodeYear";
            string fileSql = @"SELECT Year(CreateTime) FileNodeYear,
                                count(case when StorageType='Electronic' then StorageType end) ElecCount,
                                count(case when StorageType='Physical' then StorageType end) PhyFileCount,
                                {0}{1}
                                  FROM S_FileInfo 
                                  where Year(CreateTime)>Year(Getdate())-5 group by Year(CreateTime)
                                ";
            string nodeSql = @"SELECT Year(CreateTime) NodeYear,
                                count(case when IsElectronicBox='true' then IsElectronicBox end) ElecNodeCount,
                                count(case when IsPhysicalBox='true' then IsPhysicalBox end) PhyNodeCount,
                                {0}
                                  FROM S_NodeInfo 
                                  where Year(CreateTime)>Year(Getdate())-5 and ConfigID in ('{1}') group by Year(CreateTime) 
                                ";
            StringBuilder fileKeepYearStr = new StringBuilder();
            StringBuilder nodeKeepYearStr = new StringBuilder();
                /* @"count(case when KeepYear='999' then KeepYear end) fileForever,
                                count(case when KeepYear='30' then KeepYear end) fileThirty,
                                count(case when KeepYear='10' then KeepYear end) fileTen";*/
            StringBuilder mediaStr = new StringBuilder();
            var keepYears= EnumBaseHelper.GetEnumDef("DocConst.KeepYear").EnumItem;
            var medias=EnumBaseHelper.GetEnumDef("DocConst.Media").EnumItem;
            //保管期限
            foreach (var keepYear in keepYears)
            {
                fileKeepYearStr.Append("count(case when KeepYear='" + keepYear.ID + "' then KeepYear end) FileYear_" + keepYear.ID + ",");
                nodeKeepYearStr.Append("count(case when KeepYear='" + keepYear.ID + "' then KeepYear end) NodeYear_" + keepYear.ID + ",");
            }
            //载体
            foreach (var media in medias)
                mediaStr.Append("count(case when Media='"+media.ID+"' then Media end) "+media.ID+",");
            //卷册

            var docNodes=entities.S_DOC_Node.Where(a => a.SpaceID.Equals(space.ID) && a.Name.Equals("卷册")).Select(a=>a.ID).ToList();
            fileSql = string.Format(fileSql, fileKeepYearStr.ToString(), mediaStr.ToString().TrimEnd(','));//查询文件表数据
            nodeSql = string.Format(nodeSql, nodeKeepYearStr.ToString().TrimEnd(','), string.Join("','", docNodes));//查询卷册

            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            sql = string.Format(sql, fileSql, nodeSql);
            var db = sqlHelper.ExecuteDataTable(sql);
            return Json(db, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 部门
        public ActionResult Department()
        {
            return View();
        }
        //档案收进统计按部门
        public JsonResult DepartmentGetList(string SpaceID)
        {
            var entities = Formula.FormulaHelper.GetEntities<DocConfigEntities>();
            var space = entities.S_DOC_Space.FirstOrDefault(a => a.ID.Equals(SpaceID));
            string sql = @"select fileTemp.ArchiveDepartmentName+'('+cast(fileTemp.FileNodeYear as nvarchar(200))+')' DeptFileNodeYear, * from ({0}) fileTemp full join ({1}) node on fileTemp.FileNodeYear=NodeYear and fileTemp.ArchiveDepartment= node.ArchiveDepartment";
            string fileSql = @"SELECT Year(CreateTime) FileNodeYear,ArchiveDepartmentName,ArchiveDepartment,
                                count(case when StorageType='Electronic' then StorageType end) ElecCount,
                                count(case when StorageType='Physical' then StorageType end) PhyFileCount,
                                {0}{1}
                                  FROM S_FileInfo 
                                  where Year(CreateTime)>Year(Getdate())-5 group by Year(CreateTime),ArchiveDepartmentName,ArchiveDepartment
                                ";
            string nodeSql = @"SELECT Year(CreateTime) NodeYear,ArchiveDepartmentName,ArchiveDepartment,
                                count(case when IsElectronicBox='true' then IsElectronicBox end) ElecNodeCount,
                                count(case when IsPhysicalBox='true' then IsPhysicalBox end) PhyNodeCount,
                                {0}
                                  FROM S_NodeInfo 
                                  where Year(CreateTime)>Year(Getdate())-5 and ConfigID in ('{1}') group by Year(CreateTime),ArchiveDepartmentName,ArchiveDepartment 
                                ";
            StringBuilder fileKeepYearStr = new StringBuilder();
            StringBuilder nodeKeepYearStr = new StringBuilder();
            /* @"count(case when KeepYear='999' then KeepYear end) fileForever,
                            count(case when KeepYear='30' then KeepYear end) fileThirty,
                            count(case when KeepYear='10' then KeepYear end) fileTen";*/
            StringBuilder mediaStr = new StringBuilder();
            var keepYears = EnumBaseHelper.GetEnumDef("DocConst.KeepYear").EnumItem;
            var medias = EnumBaseHelper.GetEnumDef("DocConst.Media").EnumItem;
            //保管期限
            foreach (var keepYear in keepYears)
            {
                fileKeepYearStr.Append("count(case when KeepYear='" + keepYear.ID + "' then KeepYear end) FileYear_" + keepYear.ID + ",");
                nodeKeepYearStr.Append("count(case when KeepYear='" + keepYear.ID + "' then KeepYear end) NodeYear_" + keepYear.ID + ",");
            }
            //载体
            foreach (var media in medias)
                mediaStr.Append("count(case when Media='" + media.ID + "' then Media end) " + media.ID + ",");
            //卷册

            var docNodes = entities.S_DOC_Node.Where(a => a.SpaceID.Equals(space.ID) && a.Name.Equals("卷册")).Select(a => a.ID).ToList();
            fileSql = string.Format(fileSql, fileKeepYearStr.ToString(), mediaStr.ToString().TrimEnd(','));//查询文件表数据
            nodeSql = string.Format(nodeSql, nodeKeepYearStr.ToString().TrimEnd(','), string.Join("','", docNodes));//查询卷册

            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            sql = string.Format(sql, fileSql, nodeSql);
            var db = sqlHelper.ExecuteDataTable(sql);
            return Json(db, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
