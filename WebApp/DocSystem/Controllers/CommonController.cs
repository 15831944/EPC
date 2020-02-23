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
using Config;
using DocSystem.Logic;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Config.Logic;
using DocSystem.Logic.Domain;

namespace DocSystem.Controllers
{
    public class CommonController : BaseController
    {
        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetEnum(string EnumKey)
        {
            var enumService = FormulaHelper.GetService<IEnumService>();
            var dt = enumService.GetEnumTable(EnumKey);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }

        protected override DbContext entities
        {
            get { throw new NotImplementedException(); }
        }

        public JsonResult GetItemEnums(string spaceID, string configID, string type)
        {
            var list = new List<object>();
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null)
                throw new Formula.Exceptions.BusinessException("未能找ID为【" + spaceID + "】的档案空间对象");
            if (type == "File")
            {
                var fileConfig = space.S_DOC_File.FirstOrDefault();
                if (!string.IsNullOrEmpty(configID))
                    fileConfig = space.S_DOC_File.FirstOrDefault(a => a.ID == configID);
                if (fileConfig == null)
                    throw new Formula.Exceptions.BusinessException("未能找到文件的定义内容");
                var fileAttrs = fileConfig.S_DOC_FileAttr.Where(a => a.Visible == true.ToString()).ToList();
                var fileChildren = new List<object>();
                foreach (var fileAttr in fileAttrs)
                {
                    if (fileAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(fileAttr.EnumKey))
                        fileChildren.Add(new { id = fileAttr.FileAttrField, text = fileAttr.FileAttrName, isEnum = true, enumKey = fileAttr.EnumKey, pid = "File.Items" });
                    else
                        fileChildren.Add(new { id = fileAttr.FileAttrField, text = fileAttr.FileAttrName, pid = "File.Items" });
                }
                list.Add(new { id = "File.Items", text = "文件属性", children = fileChildren });
                var nodeIDs = fileConfig.S_DOC_FileNodeRelation.Select(a => a.NodeID).Distinct().ToList();
                var nodes = space.S_DOC_Node.Where(a => nodeIDs.Contains(a.ID)).ToList();
                foreach (var node in nodes)
                {
                    var nodeStrcut = space.S_DOC_NodeStrcut.FirstOrDefault(a => a.NodeID == node.ID);
                    var nodeName = nodeStrcut == null ? "" : nodeStrcut.Name;
                    var nodeAttrs = node.S_DOC_NodeAttr.Where(a => a.Visible == true.ToString()).ToList();
                    var nodeChildren = new List<object>();
                    foreach (var nodeAttr in nodeAttrs)
                    {
                        if (nodeAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(nodeAttr.EnumKey))
                            nodeChildren.Add(new { id = nodeAttr.AttrField + "(" + node.ID + ")", text = nodeAttr.AttrName, isEnum = true, enumKey = nodeAttr.EnumKey, pid = "Node.Items(" + node.ID + ")" });
                        else
                            nodeChildren.Add(new { id = nodeAttr.AttrField + "(" + node.ID + ")", text = nodeAttr.AttrName, pid = "Node.Items(" + node.ID + ")" });
                    }
                    list.Add(new { id = "Node.Items(" + node.ID + ")", text = nodeName + "属性", children = nodeChildren });
                }
            }
            else
            {
                var nodeConfig = space.S_DOC_Node.FirstOrDefault(a => a.ID == configID);
                if (nodeConfig == null)
                    throw new Formula.Exceptions.BusinessException("未能找到节点的定义内容");
                var nodeAttrs = nodeConfig.S_DOC_NodeAttr.Where(a => a.Visible == true.ToString()).ToList();
                var nodeChildren = new List<object>();
                foreach (var nodeAttr in nodeAttrs)
                {
                    if (nodeAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(nodeAttr.EnumKey))
                        nodeChildren.Add(new { id = nodeAttr.AttrField, text = nodeAttr.AttrName, isEnum = true, enumKey = nodeAttr.EnumKey, pid = "This.Items" });
                    else
                        nodeChildren.Add(new { id = nodeAttr.AttrField, text = nodeAttr.AttrName, pid = "This.Items" });
                }
                list.Add(new { id = "This.Items", text = nodeConfig.Name + "属性", children = nodeChildren });
                var parentIDs = space.S_DOC_NodeStrcut.Where(a => a.NodeID == configID).Select(a => a.ParentID).Distinct().ToList();
                var nodeIDs = space.S_DOC_NodeStrcut.Where(a => parentIDs.Contains(a.ID)).Select(a => a.NodeID).ToList();
                var nodes = space.S_DOC_Node.Where(a => nodeIDs.Contains(a.ID)).ToList();
                foreach (var node in nodes)
                {
                    var nodeStrcut = space.S_DOC_NodeStrcut.FirstOrDefault(a => a.NodeID == node.ID);
                    var nodeName = nodeStrcut == null ? "" : nodeStrcut.Name;
                    var parentNodeAttrs = node.S_DOC_NodeAttr.Where(a => a.Visible == true.ToString()).ToList();
                    var parentNodeChildren = new List<object>();
                    foreach (var parentNodeAttr in parentNodeAttrs)
                    {
                        if (parentNodeAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(parentNodeAttr.EnumKey))
                            parentNodeChildren.Add(new { id = parentNodeAttr.AttrField + "(" + node.ID + ")", text = parentNodeAttr.AttrName, isEnum = true, enumKey = parentNodeAttr.EnumKey, pid = "Node.Items(" + node.ID + ")" });
                        else
                            parentNodeChildren.Add(new { id = parentNodeAttr.AttrField + "(" + node.ID + ")", text = parentNodeAttr.AttrName, pid = "Node.Items(" + node.ID + ")" });
                    }
                    list.Add(new { id = "Node.Items(" + node.ID + ")", text = nodeName + "属性", children = parentNodeChildren });
                }
            }
            return Json(list);
        }

        public JsonResult AddHistory()
        {
            var FileID = Request["FileID"];
            var Name = Request["Name"];
            var NodeID = Request["NodeID"];
            var SpaceID = Request["SpaceID"];
            var OperateType = Request["OperateType"];
            Guid ID = Guid.NewGuid();
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            //首先判断，该用户的记录中是否已存在该数据
            string sql = "select ID from S_DOC_UserSearhHistory where fileId='{0}' and NodeID='{1}' and CreateUserid='{2}'";
            object result = sqlHelp.ExecuteScalar(string.Format(sql, FileID, NodeID, FormulaHelper.GetUserInfo().UserID));
            //存在 update S_DOC_UserSearhHistory set createdate=Date.Now();
            if (!(result == null || result is DBNull))
            {
                sql = string.Format("update S_DOC_UserSearhHistory set createdate='{0}',OperateType='{1}' where id='{2}'", DateTime.Now, OperateType, result.ToString());
                sqlHelp.ExecuteNonQuery(sql);
            }
            //否 判断该用户数据是否够十条 select count(1) from S_DOC_UserSearhHistory where CreateUserid=this.CurrentUserInfo.UserID
            else
            {
                sql = string.Format("select count(1) from S_DOC_UserSearhHistory where CreateUserid='{0}'", FormulaHelper.GetUserInfo().UserID);
                int count = Convert.ToInt32(sqlHelp.ExecuteScalar(sql));
                //否 insert into S_DOC_UserSearhHistory(ID,FileID,Name,CreateDate) values('11','22','33','2019-08-15')
                if (count < 20)
                    sql = " insert into S_DOC_UserSearhHistory(ID,FileID,Name,CreateDate,CreateUserID,NodeID,SpaceID,OperateType) values(@ID,@FileID,@Name,@CreateDate,@CreateUserID,@NodeID,@SpaceID,@OperateType)";
                else
                    //是 delete from S_DOC_UserSearhHistory where id=(select top 1 id from S_DOC_UserSearhHistory order by createdate asc)  insert into S_DOC_UserSearhHistory(ID,FileID,Name,CreateDate) values('11','22','33','2019-08-15')
                    sql = "delete from S_DOC_UserSearhHistory where id=(select top 1 id from S_DOC_UserSearhHistory order by createdate asc)  insert into S_DOC_UserSearhHistory(ID,FileID,Name,CreateDate,CreateUserID,NodeID,SpaceID,OperateType) values(@ID,@FileID,@Name,@CreateDate,@CreateUserID,@NodeID,@SpaceID,@OperateType)";
                SqlParameter[] parameter = { new SqlParameter("@ID", ID), new SqlParameter("@FileID", FileID), new SqlParameter("@Name", Name), new SqlParameter("@CreateDate", DateTime.Now), new SqlParameter("@CreateUserID", FormulaHelper.GetUserInfo().UserID), new SqlParameter("@NodeID", NodeID), new SqlParameter("@SpaceID", SpaceID), new SqlParameter("@OperateType", OperateType) };
                sqlHelp.ExecuteNonQuery(sql, parameter, CommandType.Text);
            }
            return Json(new { });
        }

        public JsonResult UploadPicture()
        {
            if (Request.Files["FileData"] != null)
            {
                var t = Request.Files["FileData"].InputStream;
                string fileName = Request.Files["FileData"].FileName;
                string extName = Path.GetExtension(fileName);
                Image img = Image.FromStream(t);
                ImageFormat imgFormat = ImageHelper.GetImageFormat(extName);
                byte[] bt = ImageHelper.ImageToBytes(img, imgFormat);
                int height = img.Height;
                int width = img.Width;
                int limitedHeight = !string.IsNullOrEmpty(Request["ThumbHeight"]) ? Convert.ToInt32(Request["ThumbHeight"]) : 60;
                int thumbHeight, thumbWidth;
                byte[] btThumb = null;
                if (height > limitedHeight)
                {
                    thumbHeight = limitedHeight;
                    thumbWidth = thumbHeight * width / height;
                    Image imgThumb = img.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
                    btThumb = ImageHelper.ImageToBytes(imgThumb, imgFormat);
                }
                else
                {
                    btThumb = bt;
                }
                var fileID = FileStoreHelper.UploadFile(fileName, bt, "", "", "", "DocAtlas");
                var thumbFileID = FileStoreHelper.UploadFile(fileName, btThumb, "", "", "", "DocAtlas");

                return Json(new { PictureName = fileID, ThumbName = thumbFileID });
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult ValidFlowIsUsed(string Code)
        {
            var entities = FormulaHelper.GetEntities<DocConfigEntities>();
            var isNodeUsed = entities.Set<S_DOC_Node>().Count(a => a.BorrowFlowKey == Code);
            var isFileUsed = entities.Set<S_DOC_File>().Count(a => a.BorrowFlowKey == Code || a.DownloadFlowKey == Code);
            if (isNodeUsed + isFileUsed > 0)
                return Json(new { isUsed = true });
            return Json(new { isUsed = false });
        }

        public JsonResult GetBorrowDetailInfo(string BorrowDetailID)
        {
            var docConstSQLDB = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            var docConstEntities = FormulaHelper.GetEntities<DocConstEntities>();
            var borrowDetailIDs = docConstEntities.Set<S_BorrowDetail>().Where(a => a.ID == BorrowDetailID || a.ParentID == BorrowDetailID).Select(a => a.ID).ToArray();
            var sql = @"select *,
TargetUserName as OperateUser,CreateDate as OperateDate,[Type] as Operation,ABS(TotalAmount+InventoryAmount) as Number from S_A_InventoryLedger 
where RelateDetailInfoID in ('" + string.Join("','", borrowDetailIDs) + "') and [Type]<>'StorageIn'";
            var dt = docConstSQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        #region 图集显示

        public ActionResult Gallery()
        {
            return View();
        }

        public JsonResult GetGalleryData(string FileID, string SpaceID)
        {
            var result = new Dictionary<string, object>();
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var file = new S_FileInfo(FileID, space);
            var titleInfos = file.ConfigInfo.S_DOC_AtlasConfigDetail.Where(a => a.Type == "List" && a.Dispaly == "True")
                .OrderBy(a => a.DetailSort).Take(3).ToArray();
            var blockInfos = file.ConfigInfo.S_DOC_AtlasConfigDetail.Where(a => a.Type == "Block" && a.Dispaly == "True");
            var dt = file.InstanceDB.ExecuteDataTable(@"
select S_FileInfo.*,S_Attachment.AtlasFile from S_FileInfo
left join S_Attachment on S_FileInfo.ID = S_Attachment.FileID and CurrentVersion = 'True'
where S_FileInfo.ID = '" + file.ID + "'");
            DataRow fileData = null;
            if (dt.Rows.Count > 0)
                fileData = dt.Rows[0];
            if (fileData == null)
                new Formula.Exceptions.BusinessException("未找到指定的文件信息");
            var titles = new List<object>();
            foreach (var item in titleInfos)
            {
                var value = "";
                if (dt.Columns.Contains(item.AttrField) && fileData[item.AttrField] != DBNull.Value)
                {
                    if (dt.Columns[item.AttrField].DataType.Name.Contains("DateTime"))
                        value = ((DateTime)fileData[item.AttrField]).ToString("yyyy年MM月dd日");
                    else
                        value = (fileData[item.AttrField]).ToString();
                }
                titles.Add(new { Left = item.AttrName, Right = value, Field = item.AttrField, DataType = dt.Columns[item.AttrField].DataType.Name });
            }
            result.Add("TitleInfos", titles);

            var blocks = new List<object>();
            foreach (var item in blockInfos)
            {
                var value = "";
                if (dt.Columns.Contains(item.AttrField) && fileData[item.AttrField] != DBNull.Value)
                {
                    if (dt.Columns[item.AttrField].DataType.Name.Contains("DateTime"))
                        value = ((DateTime)fileData[item.AttrField]).ToString("yyyy年MM月dd日");
                    else
                        value = (fileData[item.AttrField]).ToString();
                }
                blocks.Add(new { Left = item.AttrName, Right = value, Field = item.AttrField, DataType = dt.Columns[item.AttrField].DataType.Name });
            }
            result.Add("BlockInfos", blocks);

            var attList = new List<object>();
            if (fileData["AtlasFile"] != DBNull.Value && !string.IsNullOrEmpty(fileData["AtlasFile"].ToString()))
            {
                var AtlasFile = JsonHelper.ToList(fileData["AtlasFile"].ToString());
                foreach (var item in AtlasFile)
                {
                    attList.Add(new
                    {
                        PictureFileID = item.GetValue("PictureFileID"),
                        ThumbFileID = item.GetValue("ThumbFileID"),
                        Description = item.GetValue("Description")
                    });
                }
            }
            result.Add("AtlasList", attList);

            return Json(result);
        }

        public ActionResult GetPic(string fileID, int? width, int? height)
        {
            byte[] file = FileStoreHelper.GetFile(fileID);
            if (file != null)
            {
                return new ImageActionResult(file, width, height);
            }
            return Content(string.Empty);
        }

        #endregion
    }
}