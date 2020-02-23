using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Formula;
using Config;
using System.Data;
using Config.Logic;
using Formula.Helper;
namespace DocSystem.Logic.Domain
{
    public partial class S_R_PhysicalReorganize
    {
        //Object enumList = EnumBaseHelper.GetEnumDef("DocConst.PhysicalReorganizeState").EnumItem.ToList();
        public void Execute()
        {
            var user = FormulaHelper.GetUserInfo();
            if (!this.State.ToString().Equals(PhysicalReorganizeState.Execute.ToString()))
            {
                this.State = PhysicalReorganizeState.Execute.ToString();
                this.ReorganizeDate = DateTime.Now;
                this.ReorganizeUser = user.UserID;
                this.ReorganizeUserName = user.UserName;
            }
            else if (this.State == PhysicalReorganizeState.Execute.ToString())
            {
                if (this.ReorganizeUser != user.UserID)
                    throw new Formula.Exceptions.BusinessException("【" + this.Name + "】已经由【" + this.ReorganizeUserName + "】整编，请等【" + this.ReorganizeUserName + "】整编完成以后再操作");
                else
                    this.ReorganizeDate = DateTime.Now;
            }
        }
        public void SaveTemporary()
        {
            var user = FormulaHelper.GetUserInfo();
            this.State = DocSystem.Logic.PhysicalReorganizeState.Wait.ToString();
            this.ReorganizeDate = DateTime.Now;
            this.ReorganizeUser = user.UserID;
            this.ReorganizeUserName = user.UserName;
        }
        public void Finish(bool check = false)
        {
            if (check)
                CheckFinish();
            var user = FormulaHelper.GetUserInfo();
            this.State = DocSystem.Logic.PhysicalReorganizeState.Finish.ToString();
            this.ReorganizeDate = DateTime.Now;
            this.ReorganizeUser = user.UserID;
            this.ReorganizeUserName = user.UserName;
        }

        public void CheckFinish()
        {
            var user = FormulaHelper.GetUserInfo();
            if (this.State == PhysicalReorganizeState.Execute.ToString() && this.ReorganizeUser != user.UserID)
                throw new Formula.Exceptions.BusinessException("【" + this.Name + "】已经由【" + this.ReorganizeUserName + "】整编，请等【" + this.ReorganizeUserName + "】整编完成以后再操作");
        }

        //public void Publish()
        //{
        //    var user = FormulaHelper.GetUserInfo();
        //    if (this.State == "Execute")
        //    {
        //        if (this.ReorganizeUser != user.UserID)
        //            throw new Formula.Exceptions.BusinessException("【" + this.Name + "】已经由【" + this.ReorganizeUser + "】整编，请等【" + this.ReorganizeUser + "】整编完成以后再操作");
        //    }
        //    else if (this.State != "Finish")
        //        throw new Formula.Exceptions.BusinessException("【" + this.Name + "】未完成整编，无法归档");

        //    var docList = this.GetDocConfigSqlHelper().ExecuteList<S_R_PhysicalReorganize_FileDetail>
        //        ("select * from S_R_PhysicalReorganize_FileDetail where S_R_PhysicalReorganizeID='" + this.ID + "'");
        //    if (docList.Any(a => string.IsNullOrEmpty(a.ReorganizeFullID)))
        //        throw new Formula.Exceptions.BusinessException("所有文件资料都指定整编目录后才能归档");

        //    //归档
        //    var docSpace = DocConfigHelper.CreateConfigSpaceByID(this.SpaceID);
        //    var nodeIds = docList.Select(a => a.ReorganizeFullID.Split('.').LastOrDefault()).Distinct();
        //    var db = SQLHelper.CreateSqlHelper(docSpace.SpaceKey, docSpace.ConnectString);
        //    string sql = "select * from S_NodeInfo where ID in ('" + string.Join("','", nodeIds) + "') ";
        //    var dt = db.ExecuteDataTable(sql);
        //    var nodeList = new List<S_NodeInfo>();
        //    foreach (DataRow row in dt.Rows)
        //        nodeList.Add(new S_NodeInfo(row));

        //    foreach (var item in docList)
        //    {
        //        var nodeID = item.ReorganizeFullID.Split('.').LastOrDefault();
        //        var node = nodeList.FirstOrDefault(a => a.ID == nodeID);
        //        if (node == null)
        //            throw new Formula.Exceptions.BusinessException("【" + this.Name + "】未找到【" + item.Name + "】的整编目录【" + item.ReorganizePath + "】，无法归档");
        //        var fileConfig = node.ConfigInfo.S_DOC_FileNodeRelation.FirstOrDefault();
        //        if (fileConfig != null && fileConfig.S_DOC_File != null)
        //        {
        //            var file = S_FileInfo.GetFile(docSpace.ID, fileConfig.S_DOC_File.ID, " NodeID='" + node.ID + "' and RelateID='" + item.RelateID + "' ");
        //            if (file == null)
        //            {
        //                file = new DocSystem.Logic.Domain.S_FileInfo(docSpace.ID, fileConfig.S_DOC_File.ID);
        //                if (!String.IsNullOrEmpty(item.Attr))
        //                    this.SetFileAttr(file, item.Attr);
        //                file.DataEntity.SetValue("Name", item.Name);
        //                file.DataEntity.SetValue("Code", item.Code);
        //                file.DataEntity.SetValue("RelateID", item.RelateID);
        //                file.DataEntity.SetValue("State", DocState.Normal);
        //                if (!String.IsNullOrEmpty(item.MainFile))
        //                {
        //                    var attachment = new S_Attachment(docSpace.ID);
        //                    attachment.DataEntity.SetValue("MainFile", item.MainFile);
        //                    attachment.DataEntity.SetValue("PDFFile", item.PDFFile);
        //                    attachment.DataEntity.SetValue("PlotFile", item.PlotFile);
        //                    attachment.DataEntity.SetValue("XrefFile", item.XrefFile);
        //                    attachment.DataEntity.SetValue("DwfFile", item.DwfFile);
        //                    attachment.DataEntity.SetValue("TiffFile", item.TiffFile);
        //                    attachment.DataEntity.SetValue("SignPdfFile", item.SignPdfFile);
        //                    file.AddAttachment(attachment);
        //                }
        //                node.AddFile(file, true);
        //            }
        //            else
        //            {
        //                if (!String.IsNullOrEmpty(item.Attr))
        //                    this.SetFileAttr(file, item.Attr);
        //                file.DataEntity.SetValue("RelateID", item.RelateID);
        //                file.Save();
        //                var attachment = new S_Attachment(docSpace.ID);
        //                attachment.DataEntity.SetValue("MainFile", item.MainFile);
        //                attachment.DataEntity.SetValue("PDFFile", item.PDFFile);
        //                attachment.DataEntity.SetValue("PlotFile", item.PlotFile);
        //                attachment.DataEntity.SetValue("XrefFile", item.XrefFile);
        //                attachment.DataEntity.SetValue("DwfFile", item.DwfFile);
        //                attachment.DataEntity.SetValue("TiffFile", item.TiffFile);
        //                attachment.DataEntity.SetValue("SignPdfFile", item.SignPdfFile);
        //                file.AddAttachment(attachment);
        //            }
        //        }
        //    }
        //}

        //void SetFileAttr(S_FileInfo file, string Attr)
        //{
        //    var dic = JsonHelper.ToObject(Attr);
        //    foreach (string key in dic.Keys)
        //    {
        //        if (key == "ID" || key == "NodeID" || key == "SpaceID" || key == "ConfigID" || key == "FullNodeID" || key == "State" || key == "BorrowState"
        //            || key == "BorrowUserID" || key == "BorrowUserName") continue;
        //        file.DataEntity.SetValue(key, dic.GetValue(key));
        //    }
        //}
    }
}
