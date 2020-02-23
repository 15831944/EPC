using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Common.Logic.DTO;
using System.IO;

namespace Common.Logic
{
    public class FileRepository : IFileTaskRepository
    {
        private static string SQL = string.Format(@"
select top 1 * from S_E_ProductVersion pd where State!='Invalid' 
--and (PlotSealGroup!='' and PlotSealGroup is not null) 
and (PDFFile !='' and PDFFile is not null)
and SignState in ('{0}','ReSign')", AppConfig.SignState.Replace(",", "','"));

        public FileTask GetTask(string fileID = "")
        {
            FileTask task = new FileTask();
            try
            {
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                var productVersions = db.ExecuteList<ProductVersion>(SQL);
                if (productVersions.Count() > 0)
                {
                    var data = productVersions[0];
                    task.ID = data.ProductID;
                    task.Version = data.Version;
                    task.VersionID = data.ID;
                    task.Name = data.Code;
                    if (string.IsNullOrEmpty(data.PDFSignPositionInfo))
                        throw new Exception("缺少PDF签名位置");
                    task.PDFPosInfo = JsonHelper.ToObject<PDFSignPositionDTO>(data.PDFSignPositionInfo);
                    if (task.PDFPosInfo.BorderConfigs == null)
                        task.PDFPosInfo.BorderConfigs = new List<BorderConfigInfo>();
                    task.PDFSignInfos = JsonHelper.ToObject<List<PdfSignInfo>>(data.AuditSignUser);
                    task.PDFCoSignInfos = JsonHelper.ToObject<List<PdfCoSignInfo>>(data.CoSignUser);
                    task.PdfFile = data.PdfFile;
                    task.PlotSealGroup = JsonHelper.ToObject<PlotSealInfo>(data.PlotSealGroup);

                    #region 查询盖章信息
                    var pdfStampInfoDTOs = new List<PdfStampInfoDTO>();
                    if (task.PlotSealGroup != null)
                    {
                        if (!string.IsNullOrEmpty(task.PlotSealGroup.Groups))
                        {
                            foreach (var groupID in task.PlotSealGroup.Groups.Split(','))
                            {
                                if (!string.IsNullOrEmpty(groupID))
                                {
                                    var groupSeals = GlobalData.AllGroupSeals.Where(a => a.S_EP_PlotSealGroupID == groupID);
                                    if (groupSeals.Count() == 0)
                                        throw new Exception("未找到ID为【" + groupID + "】的出图章组合的图章信息");
                                    var pdfStampInfo = new PdfStampInfoDTO { FollowSeals = new List<StampInfoDTO>() };
                                    foreach (var seal in groupSeals)
                                    {
                                        var sealDTO = new StampInfoDTO
                                        {
                                            ID = seal.PlotSeal,
                                            BlockKey = seal.BlockKey,
                                            AuthInfo = seal.AuthInfo,
                                            Width = seal.Width,
                                            Height = seal.Height,
                                            CorrectPosX = seal.CorrectPosX,
                                            CorrectPosY = seal.CorrectPosY
                                        };
                                        var s = GlobalData.AllSeals.FirstOrDefault(a => a.ID == sealDTO.ID);
                                        if (s != null)
                                            sealDTO.Relational = s.Relational;
                                        if (seal.IsMain == "true")
                                            pdfStampInfo.MainSeal = sealDTO;
                                        else
                                            pdfStampInfo.FollowSeals.Add(sealDTO);
                                    }
                                    if (pdfStampInfo.MainSeal == null)
                                        throw new Exception("ID为【" + groupID + "】的出图章组合未设置主章");
                                    pdfStampInfoDTOs.Add(pdfStampInfo);
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(task.PlotSealGroup.Seals))
                        {
                            foreach (var sealID in task.PlotSealGroup.Seals.Split(','))
                            {
                                if (!string.IsNullOrEmpty(sealID))
                                {
                                    var seal = GlobalData.AllSeals.FirstOrDefault(a => a.ID == sealID);

                                    if (seal == null)
                                        throw new Exception("未找到ID为【" + sealID + "】的图章信息");
                                    var pdfStampInfo = new PdfStampInfoDTO
                                    {
                                        MainSeal = new StampInfoDTO
                                        {
                                            ID = seal.ID,
                                            BlockKey = seal.BlockKey,
                                            AuthInfo = seal.AuthInfo,
                                            Width = seal.Width,
                                            Height = seal.Height,
                                            Relational = seal.Relational
                                        },
                                        FollowSeals = new List<StampInfoDTO>()
                                    };
                                    pdfStampInfoDTOs.Add(pdfStampInfo);
                                }
                            }
                        }
                    }
                    task.PDFStampInfos = pdfStampInfoDTOs;
                    #endregion
                }
                else
                    return null;
                return task;
            }
            catch (Exception ex)
            {
                LogWriter.Info("GetTask异常：" + ex.Message);
                TaskException(task, ex.Message);
                return null;
            }
        }

        public bool TaskException(FileTask task, string errorMessage)
        {
            try
            {
                var sql = string.Format(@"
update S_E_Product set SignState = 'Error' where ID = '{0}' and Version = '{1}'
update S_E_ProductVersion set SignState = 'Error' where ProductID = '{0}' and Version = '{1}'
insert into S_EP_PDFSignLog values('{2}','{0}','{1}','{3}','{4}','{5}')",
task.ID, task.Version, FormulaHelper.CreateGuid(), task.VersionID, errorMessage, System.DateTime.Now);
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                var reader = db.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateTask(string pdfPath, FileTask task)
        {
            try
            {
                string fileID = string.Empty;
                #region 上传文件
                try
                {
                    byte[] pReadByte = new byte[0];
                    FileStream pFileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
                    BinaryReader r = new BinaryReader(pFileStream);
                    r.BaseStream.Seek(0, SeekOrigin.Begin);
                    pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                    pFileStream.Close();
                    fileID = FileStoreHelper.SaveFile(pReadByte, Path.GetFileName(pdfPath));
                }
                catch (Exception)
                {
                    throw new Exception("上传文件失败");
                }
                #endregion
                if (string.IsNullOrEmpty(fileID))
                    throw new Exception("上传文件失败");
                var sql = string.Format(@"
Update S_E_Product set SignState = '{2}' , SignPdfFile = '{1}' where ID = '{0}'
Update S_E_ProductVersion set SignState = '{2}', SignPdfFile = '{1}' 
where ProductID = '{0}' and Version=(select Version from S_E_Product where ID='{0}')",
task.ID, fileID, "True");
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                var reader = db.ExecuteNonQuery(sql);

                return true;
            }
            catch (Exception ex)
            {
                TaskException(task, ex.Message);
                return false;
            }
        }

        public byte[] TaskGetSign(string userID, string userName)
        {
            try
            {
                byte[] reVal = null;
                var sql = string.Format(@"select SignImg from S_A_UserImg where UserID = '{0}'", userID);
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Base"));
                var reader = db.ExecuteReader(sql);
                while (reader.Read())
                {
                    reVal = reader.Get("SignImg", new byte[0]);
                }
                if (reVal == null || reVal.Length == 0) throw new Exception("[" + userName + "]未上传签名图片！");
                return reVal;
            }
            catch (Exception ex)
            {
                throw new Exception("TaskGetSign异常：" + ex.Message);
            }
        }

        public byte[] TaskGetStamp(string stampID)
        {
            try
            {
                byte[] reVal = null;
                var sql = string.Format(@"select SealInfo from S_EP_PlotSealInfo Where ID = '{0}'", stampID);
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("Project"));
                var reader = db.ExecuteReader(sql);
                while (reader.Read())
                {
                    reVal = reader.Get("SealInfo", new byte[0]);
                }
                if (reVal == null || reVal.Length == 0) throw new Exception("下载ID为【" + stampID + "】的图章失败");
                return reVal;
            }
            catch (Exception ex)
            {
                throw new Exception("TaskGetStamp异常：" + ex.Message);
            }
        }

        public void DeleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public void StartTask(string id)
        {
            throw new NotImplementedException();
        }

        public void EndTask(string id, ResultStatus status)
        {
            throw new NotImplementedException();
        }

        public void AddTask(FileTask task)
        {
            throw new NotImplementedException();
        }
    }
}
