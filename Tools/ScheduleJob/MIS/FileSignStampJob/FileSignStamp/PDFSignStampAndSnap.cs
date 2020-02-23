using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.IO;

using Quartz;
using Common.Logic;
using Common.Logic.DTO;
using System.Text.RegularExpressions;
using Common.Logic.Domain;
using System.Globalization;
using iTextSharp.text.pdf;

namespace FileSignStamp
{
    [Description("PDF进行CA签章")]
    public class PDFSignStampAndSnap : IJob
    {
        public PDFSignStampAndSnap()
        {
            AppConfig.InitContext();
            Log4NetConfig.ConfigureFile();
        }

        public string ErrInfo = "<br>  记录时间：{0}<br>  相关序号：{1}<br>  文件ID：{2}<br>  错误描述：{3}<br>  详细信息：{4}";

        public void CAExecute(IJobExecutionContext context)
        {
            #region 同步图章
            if (AppConfig.IsSynchSeal)
            {
                if (AppConfig.LastSyncTime.AddHours(AppConfig.TimeInterval) <= DateTime.Now)
                {
                    float dpiX = 0, dpiY = 0;
                    using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
                    {
                        dpiX = graphics.DpiX;
                        dpiY = graphics.DpiY;
                    }
                    var rtn = BaseHttpService.GetCASealList();
                    if (rtn.ret_code == 0)
                    {
                        using (var db = new ProjectContext())
                        {
                            foreach (var item in rtn.data)
                            {
                                var id = item.strategy_id;
                                var seal_Mis = GlobalData.AllSeals.FirstOrDefault(a => a.Relational == id);
                                if (seal_Mis == null)
                                {
                                    seal_Mis = CreateSealInfo(item);
                                    System.Drawing.Image image = ImageHelper.BytesToImage(seal_Mis.SealInfo);
                                    seal_Mis.Width = (decimal)Math.Round((float)image.Width / dpiX * 25.4);
                                    seal_Mis.Height = (decimal)Math.Round((float)image.Height / dpiY * 25.4);
                                    db.S_EP_PlotSealInfo.Add(seal_Mis);
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        LogWriter.Error(string.Format(ErrInfo, DateTime.Now.ToString(), 0, "同步图章", "获取图章列表失败", rtn.ret_code + "-" + rtn.ret_msg));
                    }
                    AppConfig.LastSyncTime = DateTime.Now;
                }
            }
            #endregion

            var taskId = string.Empty;
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
            {
                taskId = jobDataTaskId.ToString();
            }
            FileRepository repo = new FileRepository();
            FileTask task = null;
            while ((task = repo.GetTask(taskId)) != null)
            {
                try
                {
                    string fileName = FunctionTools.GetFileName(task.PdfFile);

                    string pdfFilePath = GlobalData.pdfBaseFolder + @"\" + fileName + ".pdf";
                    string pdfSignFilePath = GlobalData.pdfSignFolder + @"\" + fileName + ".pdf";
                    string pdfStampFilePath = GlobalData.pdfCaFolder + @"\" + fileName + ".pdf";

                    var pdfPageSize = new Dictionary<string, double>();
                    try
                    {
                        FileStoreHelper.SaveFileBuffer(FileStoreHelper.GetFile(task.PdfFile), pdfFilePath);
                        var pr = new PdfReader(pdfFilePath);
                        for (int i = 1; i <= pr.NumberOfPages; i++)
                        {
                            var page = pr.GetPageSizeWithRotation(i);
                            var width = page.Width;
                            var height = page.Height;
                            pdfPageSize.Add("Width_" + i, (double)(width / 72.0 * 25.4));
                            pdfPageSize.Add("Height_" + i, (double)(height / 72.0 * 25.4));
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("下载PDF失败");
                    }

                    List<PdfSignInfo> pdfSignInfoDTOs = task.PDFSignInfos ?? new List<PdfSignInfo>();
                    List<PdfCoSignInfo> pdfCoSignInfoDTOs = task.PDFCoSignInfos ?? new List<PdfCoSignInfo>();
                    PDFSignPositionDTO signPosInfo = task.PDFPosInfo;
                    List<PdfStampInfoDTO> pdfStampInfoDTOs = task.PDFStampInfos ?? new List<PdfStampInfoDTO>();

                    #region 下载图片
                    List<string> signImgIds = new List<string>();
                    List<string> signImgName = new List<string>();
                    Dictionary<string, string> IdImgPaths = new Dictionary<string, string>();

                    foreach (PdfSignInfo item in pdfSignInfoDTOs)
                    {
                        if (GlobalData.RoleNameDic.ContainsKey(item.ActivityKey))
                            item.AuditStepDes = GlobalData.RoleNameDic[item.ActivityKey];
                        if (!signImgIds.Contains(item.UserID))
                        {
                            signImgIds.Add(item.UserID);
                            signImgName.Add(item.UserName);
                        }
                    }
                    foreach (PdfCoSignInfo item in pdfCoSignInfoDTOs)
                    {
                        if (!signImgIds.Contains(item.UserID))
                        {
                            signImgIds.Add(item.UserID);
                            signImgName.Add(item.UserName);
                        }
                    }
                    for (int i = 0; i < signImgIds.Count; i++)
                    {
                        string imgPath = GlobalData.pdfImgFolder + @"\" + signImgIds[i] + ".png";
                        if (!File.Exists(imgPath))
                        {
                            byte[] imgStream = repo.TaskGetSign(signImgIds[i], signImgName[i]);
                            FileStoreHelper.SaveFileBuffer(imgStream, imgPath);
                        }
                        IdImgPaths.Add(signImgIds[i], imgPath);
                    }
                    #endregion

                    #region PDF签字盖章

                    List<SignInfo> signInfos = new List<SignInfo>();
                    List<CoSignInfo> coSignInfos = new List<CoSignInfo>();
                    List<StampInfo> stampInfos = new List<StampInfo>();

                    var attrIndexDic = new Dictionary<string, int>();
                    foreach (var role in GlobalData.AllRoles)
                        attrIndexDic.Add(role.RoleCode, 1);
                    if (!attrIndexDic.ContainsKey("CoSign"))
                        attrIndexDic.Add("CoSign", 1);

                    if (AppConfig.IsSign)
                    {
                        #region 拼凑签名信息
                        foreach (PdfSignInfo sign in pdfSignInfoDTOs)
                        {
                            if (string.IsNullOrEmpty(sign.UserID) || string.IsNullOrEmpty(sign.UserName))
                                continue;

                            var roleDefine = GlobalData.AllRoles.FirstOrDefault(a => a.RoleCode == sign.ActivityKey);
                            if (roleDefine == null)
                                throw new Exception("未找到[" + sign.ActivityKey + "]的角色定义");
                            string imgPath = IdImgPaths[sign.UserID];
                            string strSignDate = sign.SignDate;
                            var index = attrIndexDic[sign.ActivityKey];
                            signInfos.Add(GetRoleSignInfo(roleDefine, sign.UserName, sign.UserID, strSignDate, index, imgPath, sign.AuditStepDes));
                            attrIndexDic[sign.ActivityKey] = index + 1;
                        }
                        #endregion

                        #region 拼凑会签信息
                        foreach (PdfCoSignInfo coSign in pdfCoSignInfoDTOs)
                        {
                            if (string.IsNullOrEmpty(coSign.UserID) || string.IsNullOrEmpty(coSign.UserName))
                                continue;
                            string imgPath = IdImgPaths[coSign.UserID];
                            string strSignDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                            var index = attrIndexDic["CoSign"];
                            coSignInfos.Add(GetRoleCoSignInfo(coSign.UserName, coSign.UserID, coSign.MajorName, strSignDate, "会签", index, imgPath));
                            attrIndexDic["CoSign"] = index + 1;
                        }
                        #endregion
                    }

                    if (AppConfig.IsStamp)
                    {
                        #region 拼凑盖章信息
                        foreach (PdfStampInfoDTO itemDTO in pdfStampInfoDTOs)
                        {
                            StampInfo stampInfo = new StampInfo();
                            stampInfo.ID = itemDTO.MainSeal.Relational;
                            stampInfo.Name = itemDTO.MainSeal.BlockKey;
                            stampInfo.Width = Convert.ToDouble(itemDTO.MainSeal.Width);
                            stampInfo.Height = Convert.ToDouble(itemDTO.MainSeal.Height);
                            foreach (var fSeal in itemDTO.FollowSeals)
                            {
                                StampInfo childStampInfo = new StampInfo();
                                childStampInfo.ID = fSeal.Relational;
                                childStampInfo.Width = Convert.ToDouble(fSeal.Width);
                                childStampInfo.Height = Convert.ToDouble(fSeal.Height);
                                childStampInfo.RelativePosX = Convert.ToDouble(fSeal.CorrectPosX);
                                childStampInfo.RelativePosY = Convert.ToDouble(fSeal.CorrectPosY);
                                stampInfo.ChildStampInfo.Add(childStampInfo);
                            }
                            stampInfos.Add(stampInfo);
                        }
                        #endregion
                    }

                    List<PdfImage> pdfImgs = new List<PdfImage>();
                    List<PdfText> pdfTxts = new List<PdfText>();
                    List<CASealDTO> caSeals = new List<CASealDTO>();

                    BorderConfigInfo signConfig = null, cosignConfig = null, barcodeConfig = null, qrcodeConfig = null;
                    foreach (var item in signPosInfo.BorderConfigs)
                    {
                        if (item.Category == "Sign")
                            signConfig = item;
                        else if (item.Category == "CoSign")
                            cosignConfig = item;
                        else if (item.Category == "Barcode")
                            barcodeConfig = item;
                        else
                            qrcodeConfig = item;
                    }
                    for (int i = 0; i < signPosInfo.AuditRoles.Count; i++)
                    {
                        var key = signPosInfo.AuditRoles[i];
                        var isMajorCosignBlock = isMajorCosign(key);
                        double width = 0, height = 0, charHeight = 0, correctX = 0, correctY = 0;
                        int pageNum = 1, angle = 0;
                        bool visiable = true;
                        bool hasConfigFlag = false;
                        if (signPosInfo.AuditRolesConfig != null && signPosInfo.AuditRolesConfig.Count > i)
                        {
                            var bConfig = signPosInfo.AuditRolesConfig[i];
                            if (bConfig != null)
                            {
                                width = bConfig.Width;
                                height = bConfig.Height;
                                charHeight = bConfig.CharHeight;
                                angle = bConfig.Angle;
                                visiable = bConfig.Visiable;
                                hasConfigFlag = true;
                            }
                        }
                        if (visiable)
                        {
                            if (signPosInfo.AuditRolesPageNum != null && signPosInfo.AuditRolesPageNum.Count > i)
                                pageNum = signPosInfo.AuditRolesPageNum[i];
                            var auditRolesType = "";
                            if (signPosInfo.AuditRolesType != null && signPosInfo.AuditRolesType.Count > i)
                                if (!string.IsNullOrEmpty(signPosInfo.AuditRolesType[i]))
                                    auditRolesType = signPosInfo.AuditRolesType[i];

                            if (key == "条码")
                            {

                            }
                            else if (key == "二维码")
                            {

                            }
                            else if (key.Contains("章") || key.Contains("策略"))
                            {
                                #region 图章
                                var stampInfo = stampInfos.FirstOrDefault(a => a.Name == key);
                                if (stampInfo != null)
                                {
                                    if (stampInfo.ID == null) continue;
                                    if (!hasConfigFlag)
                                    {
                                        width = stampInfo.Width;
                                        height = stampInfo.Height;
                                    }

                                    double attPtX = signPosInfo.PositionXs[i];
                                    double attPtY = signPosInfo.PositionYs[i];
                                    var dto = new CASealDTO();
                                    dto.seal_strategy_id = (int)stampInfo.ID;
                                    dto.position = new Position
                                    {
                                        page = pageNum.ToString(),
                                    };
                                    if (pdfPageSize.ContainsKey("Width_" + pageNum))
                                    {
                                        int x = (int)Math.Round(attPtX * 50000.0 / pdfPageSize["Width_" + pageNum]);
                                        int y = (int)Math.Round(attPtY * 50000.0 / pdfPageSize["Height_" + pageNum]);
                                        dto.position.x = x.ToString();
                                        dto.position.y = y.ToString();
                                    }
                                    dto.size = new Size
                                    {
                                        width = width,
                                        height = height
                                    };
                                    if (angle != 0)
                                        dto.degree = angle.ToString();
                                    caSeals.Add(dto);

                                    foreach (StampInfo childInfo in stampInfo.ChildStampInfo)
                                    {
                                        if (childInfo.ID == null) continue;
                                        var childImgPosX = attPtX + childInfo.RelativePosX;
                                        var childImgPosY = attPtY + childInfo.RelativePosY;
                                        var cDto = new CASealDTO();
                                        cDto.seal_strategy_id = (int)childInfo.ID;
                                        cDto.position = new Position
                                        {
                                            page = pageNum.ToString(),
                                        };
                                        if (pdfPageSize.ContainsKey("Width_" + pageNum))
                                        {
                                            int x = (int)Math.Round(childImgPosX * 50000.0 / pdfPageSize["Width_" + pageNum]);
                                            int y = (int)Math.Round(childImgPosY * 50000.0 / pdfPageSize["Height_" + pageNum]);
                                            cDto.position.x = x.ToString();
                                            cDto.position.y = y.ToString();
                                        }
                                        cDto.size = new Size
                                        {
                                            width = childInfo.Width,
                                            height = childInfo.Height
                                        };
                                        if (angle != 0)
                                            cDto.degree = angle.ToString();
                                        caSeals.Add(cDto);
                                    }
                                }
                                #endregion
                            }
                            else if (isMajorCosignBlock || auditRolesType.Contains("会签") || key.Contains("会签"))
                            {
                                #region 会签
                                if (!hasConfigFlag)
                                {
                                    width = cosignConfig.Width;
                                    height = cosignConfig.Height;
                                    charHeight = cosignConfig.CharHeight;
                                    angle = (int)cosignConfig.Angle;
                                    correctX = cosignConfig.CorrectPosX;
                                    correctY = cosignConfig.CorrectPosY;
                                }
                                double attPtX = signPosInfo.PositionXs[i] + correctX;
                                double attPtY = signPosInfo.PositionYs[i] + correctY;
                                double left = attPtX;
                                double bottom = attPtY;
                                var index = 1;
                                var indexStr = Regex.Replace(key, @"[^\d]*", "");
                                if (!string.IsNullOrEmpty(indexStr)) index = int.Parse(indexStr);
                                CoSignInfo cosignInfo = null;
                                if (isMajorCosignBlock)
                                {
                                    cosignInfo = coSignInfos.FirstOrDefault(a => key.Contains(a.Major));
                                }
                                else
                                {
                                    if (coSignInfos.Count >= index)
                                        cosignInfo = coSignInfos[index - 1];
                                }
                                if (cosignInfo != null)
                                {
                                    if (key.Contains("日期"))
                                    {
                                        pdfTxts.Add(new PdfText(cosignInfo.Date, "STSONG.TTF",
                                            (float)charHeight, 1,
                                            (float)attPtX, (float)attPtY,
                                            (float)angle, pageNum));
                                    }
                                    else if (key.Contains("专业"))
                                    {
                                        pdfTxts.Add(new PdfText(cosignInfo.Major, "STSONG.TTF",
                                            (float)charHeight, 1,
                                            (float)attPtX, (float)attPtY,
                                            (float)angle, pageNum));
                                    }
                                    else
                                    {
                                        if (angle == 0 || angle == 180 || angle == -180)
                                        {
                                            left -= width / 2;
                                            bottom -= height / 2;
                                        }
                                        else
                                        {
                                            left -= height / 2;
                                            bottom -= width / 2;
                                        }
                                        PdfImage pdfImg = new PdfImage(cosignInfo.LocalPath,
                                            (float)width, (float)height,
                                            (float)left, (float)bottom,
                                            (float)angle, pageNum);
                                        pdfImgs.Add(pdfImg);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 签名
                                if (!hasConfigFlag)
                                {
                                    width = signConfig.Width;
                                    height = signConfig.Height;
                                    charHeight = signConfig.CharHeight;
                                    angle = (int)signConfig.Angle;
                                    correctX = cosignConfig.CorrectPosX;
                                    correctY = cosignConfig.CorrectPosY;
                                }
                                double attPtX = signPosInfo.PositionXs[i] + correctX;
                                double attPtY = signPosInfo.PositionYs[i] + correctY;
                                double left = attPtX;
                                double bottom = attPtY;
                                var index = 1;
                                var indexStr = Regex.Replace(key, @"[^\d]*", "");
                                if (!string.IsNullOrEmpty(indexStr)) index = int.Parse(indexStr);
                                var roleName = Regex.Replace(key.Replace("签名", ""), @"\d", "");
                                var signInfo = signInfos.FirstOrDefault(a => a.BlockKeys.Contains(roleName + index.ToString()));
                                if (signInfo != null)
                                {
                                    if (key.Contains("日期"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.Date, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else if (key.Contains("签名角色"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.StepDes, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else if (!key.Contains("签名"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.UserName, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else
                                    {
                                        if (angle == 0 || angle == 180 || angle == -180)
                                        {
                                            left -= width / 2;
                                            bottom -= height / 2;
                                        }
                                        else
                                        {
                                            left -= height / 2;
                                            bottom -= width / 2;
                                        }
                                        PdfImage pdfImg = new PdfImage(signInfo.LocalPath,
                                            (float)width, (float)height,
                                            (float)left, (float)bottom,
                                            (float)angle, pageNum);
                                        pdfImgs.Add(pdfImg);
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    if (File.Exists(pdfSignFilePath))
                    {
                        File.Delete(pdfSignFilePath);
                    }

                    float pageOneWidth = 0, pageOneHeight = 0;
                    if (signPosInfo.FrameProperty != null && signPosInfo.FrameProperty.Count > 0)
                    {
                        pageOneWidth = (float)signPosInfo.FrameProperty[0].PaperWidth;
                        pageOneHeight = (float)signPosInfo.FrameProperty[0].PaperHeight;
                    }
                    else
                    {
                        pageOneWidth = (float)signPosInfo.PaperWidth;
                        pageOneHeight = (float)signPosInfo.PaperHeight;
                    }
                    PdfOperate pdfOpe = new PdfOperate(pageOneWidth, pageOneHeight);
                    pdfOpe.PdfSign(pdfFilePath, pdfSignFilePath, pdfImgs, pdfTxts);

                    #endregion

                    #region CA一体机盖章

                    var pdfBase64 = string.Empty;
                    if (File.Exists(pdfSignFilePath))
                    {
                        byte[] b = File.ReadAllBytes(pdfSignFilePath);
                        pdfBase64 = Convert.ToBase64String(b);
                    }

                    var serverMoreSign = new ServerMoreSign();
                    serverMoreSign.document_no = "111";
                    serverMoreSign.pdf = pdfBase64;
                    serverMoreSign.seal = caSeals;

                    var rtn = BaseHttpService.PostSign(serverMoreSign);
                    if (rtn.ret_code == 0)
                    {
                        if (File.Exists(pdfStampFilePath))
                            File.Delete(pdfStampFilePath);
                        var contents = Convert.FromBase64String(rtn.pdf);
                        using (var fs = new FileStream(pdfStampFilePath, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(contents, 0, contents.Length);
                            fs.Flush();
                        }
                        if (File.Exists(pdfStampFilePath))
                        {
                            byte[] b = File.ReadAllBytes(pdfStampFilePath);
                            pdfBase64 = Convert.ToBase64String(b);
                        }

                        var verify = new Verify();
                        verify.data = new Verify_Data { pdf = pdfBase64 };
                        var verify_return = BaseHttpService.Post(GlobalData.VerifyStr, verify);
                        var v_rtn = JsonHelper.ToObject<Verify_Return>(verify_return);
                        if (v_rtn.ret_code == 0)
                        {
                            repo.UpdateTask(pdfStampFilePath, task);
                        }
                        else
                        {
                            throw new Exception(v_rtn.ret_msg); LogWriter.Error(string.Format(ErrInfo, DateTime.Now.ToString(), 0, "CA签章验证", "验证失败", v_rtn.ret_code + "-" + v_rtn.ret_msg));
                        }
                    }
                    else
                    {
                        throw new Exception(rtn.ret_msg);
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    repo.TaskException(task, ex.Message);
                    //记录日志
                    LogWriter.Error(string.Format(ErrInfo, DateTime.Now.ToString(), task.ID, task.Name, ex.Message, ex.StackTrace));
                }
            }
        }

        public void TestExecute(IJobExecutionContext context)
        {
            var pdfPath = "D:/Test/t.pdf";
            var querySealStrategy = new QuerySealStrategy();
            var querySealStrategy_return = BaseHttpService.Post(GlobalData.QuerySealStrategyStr, querySealStrategy);
            var list = JsonHelper.ToObject<QuerySealStrategy_Return>(querySealStrategy_return);

            if (list.data.Count > 0)
            {
                var pdfBase64 = "";
                if (File.Exists(pdfPath))
                {
                    byte[] b = File.ReadAllBytes(pdfPath);
                    pdfBase64 = Convert.ToBase64String(b);
                }
                var sealList = new List<CASealDTO>();
                sealList.Add(new CASealDTO
                {
                    alpha = "5",
                    seal_strategy_id = list.data[0].strategy_id,
                    type = "position",
                    position = new Position
                    {
                        page = "1",
                        x = "20000",
                        y = "20000"
                    },
                    size = new Size
                    {
                        width = 20,
                        height = 20
                    },
                    degree = "90",
                    perforation = "false",
                    time = DateTime.Now.ToString("yyyyMMddHHmmss")
                });
                sealList.Add(new CASealDTO
                {
                    alpha = "5",
                    seal_strategy_id = list.data[2].strategy_id,
                    type = "position",
                    position = new Position
                    {
                        page = "1",
                        x = "40000",
                        y = "40000"
                    },
                    size = new Size
                    {
                        width = 20,
                        height = 20
                    },
                    perforation = "false",
                    time = DateTime.Now.ToString("yyyyMMddHHmmss")
                });
                var serverMoreSign = new ServerMoreSign();
                serverMoreSign.document_no = "111";
                serverMoreSign.pdf = pdfBase64;
                serverMoreSign.seal = sealList;
                var serverMoreSign_return = BaseHttpService.Post(GlobalData.ServerMoreSignStr, serverMoreSign);
                var rtn = JsonHelper.ToObject<ServerMoreSign_Return>(serverMoreSign_return);

                pdfPath = "D:/Test/t_sign.pdf";
                if (File.Exists(pdfPath))
                    File.Delete(pdfPath);
                var contents = Convert.FromBase64String(rtn.pdf);
                using (var fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(contents, 0, contents.Length);
                    fs.Flush();
                }
                if (File.Exists(pdfPath))
                {
                    byte[] b = File.ReadAllBytes(pdfPath);
                    pdfBase64 = Convert.ToBase64String(b);
                }

                var verify = new Verify();
                verify.data = new Verify_Data { pdf = pdfBase64 };
                var verify_return = BaseHttpService.Post(GlobalData.VerifyStr, verify);
                var d = JsonHelper.ToObject<Verify_Return>(verify_return);
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            var taskId = string.Empty;
            var jobDataTaskId = context.Trigger.JobDataMap["taskId"];
            if (jobDataTaskId != null)
            {
                taskId = jobDataTaskId.ToString();
            }
            FileRepository repo = new FileRepository();

            FileTask task = null;
            while ((task = repo.GetTask(taskId)) != null)
            {
                var tempList = new List<string>();
                try
                {
                    string fileName = FunctionTools.GetFileName(task.PdfFile);

                    string pdfFilePath = GlobalData.pdfBaseFolder + @"\" + fileName + ".pdf";
                    string pdfSignFilePath = GlobalData.pdfSignFolder + @"\" + fileName + ".pdf";
                    string pdfStampFilePath = GlobalData.pdfCaFolder + @"\" + fileName + ".pdf";

                    try
                    {
                        FileStoreHelper.SaveFileBuffer(FileStoreHelper.GetFile(task.PdfFile), pdfFilePath);
                    }
                    catch (Exception)
                    {
                        throw new Exception("下载PDF失败");
                    }

                    List<PdfSignInfo> pdfSignInfoDTOs = task.PDFSignInfos ?? new List<PdfSignInfo>();
                    List<PdfCoSignInfo> pdfCoSignInfoDTOs = task.PDFCoSignInfos ?? new List<PdfCoSignInfo>();
                    PDFSignPositionDTO signPosInfo = task.PDFPosInfo;
                    List<PdfStampInfoDTO> pdfStampInfoDTOs = task.PDFStampInfos ?? new List<PdfStampInfoDTO>();

                    #region 下载图片
                    List<string> signImgIds = new List<string>();
                    List<string> signImgName = new List<string>();
                    List<string> stampImgIds = new List<string>();
                    Dictionary<string, string> IdImgPaths = new Dictionary<string, string>();

                    #region 下载签名
                    foreach (PdfSignInfo item in pdfSignInfoDTOs)
                    {
                        if (GlobalData.RoleNameDic.ContainsKey(item.ActivityKey))
                            item.AuditStepDes = GlobalData.RoleNameDic[item.ActivityKey];
                        if (!signImgIds.Contains(item.UserID))
                        {
                            signImgIds.Add(item.UserID);
                            signImgName.Add(item.UserName);
                        }
                    }
                    foreach (PdfCoSignInfo item in pdfCoSignInfoDTOs)
                    {
                        if (!signImgIds.Contains(item.UserID))
                        {
                            signImgIds.Add(item.UserID);
                            signImgName.Add(item.UserName);
                        }
                    }
                    for (int i = 0; i < signImgIds.Count; i++)
                    {
                        string imgPath = GlobalData.pdfImgFolder + @"\" + signImgIds[i] + ".png";
                        if (!File.Exists(imgPath))
                        {
                            byte[] imgStream = repo.TaskGetSign(signImgIds[i], signImgName[i]);
                            FileStoreHelper.SaveFileBuffer(imgStream, imgPath);
                        }
                        IdImgPaths.Add(signImgIds[i], imgPath);
                    }
                    #endregion

                    #region 下载图章
                    foreach (PdfStampInfoDTO item in pdfStampInfoDTOs)
                    {
                        if (!stampImgIds.Contains(item.MainSeal.ID))
                        {
                            stampImgIds.Add(item.MainSeal.ID);
                        }
                        foreach (var fSeal in item.FollowSeals)
                        {
                            if (!stampImgIds.Contains(fSeal.ID))
                            {
                                stampImgIds.Add(fSeal.ID);
                            }
                        }
                    }
                    for (int i = 0; i < stampImgIds.Count; i++)
                    {
                        string imgPath = GlobalData.pdfImgFolder + @"\" + stampImgIds[i] + ".png";
                        if (!File.Exists(imgPath))
                        {
                            byte[] imgStream = repo.TaskGetStamp(stampImgIds[i]);
                            FileStoreHelper.SaveFileBuffer(imgStream, imgPath);
                        }
                        IdImgPaths.Add(stampImgIds[i], imgPath);
                    }
                    #endregion

                    #endregion

                    #region PDF签字盖章

                    List<SignInfo> signInfos = new List<SignInfo>();
                    List<CoSignInfo> coSignInfos = new List<CoSignInfo>();
                    List<StampInfo> stampInfos = new List<StampInfo>();

                    var attrIndexDic = new Dictionary<string, int>();
                    foreach (var role in GlobalData.AllRoles)
                        attrIndexDic.Add(role.RoleCode, 1);
                    if (!attrIndexDic.ContainsKey("CoSign"))
                        attrIndexDic.Add("CoSign", 1);

                    if (AppConfig.IsSign)
                    {
                        #region 拼凑签名信息
                        foreach (PdfSignInfo sign in pdfSignInfoDTOs)
                        {
                            if (string.IsNullOrEmpty(sign.UserID) || string.IsNullOrEmpty(sign.UserName))
                                continue;

                            var roleDefine = GlobalData.AllRoles.FirstOrDefault(a => a.RoleCode == sign.ActivityKey);
                            if (roleDefine == null)
                                throw new Exception("未找到[" + sign.ActivityKey + "]的角色定义");
                            string imgPath = IdImgPaths[sign.UserID];
                            string strSignDate = sign.SignDate;
                            var index = attrIndexDic[sign.ActivityKey];
                            signInfos.Add(GetRoleSignInfo(roleDefine, sign.UserName, sign.UserID, strSignDate, index, imgPath, sign.AuditStepDes));
                            attrIndexDic[sign.ActivityKey] = index + 1;
                        }
                        #endregion

                        #region 拼凑会签信息
                        foreach (PdfCoSignInfo coSign in pdfCoSignInfoDTOs)
                        {
                            if (string.IsNullOrEmpty(coSign.UserID) || string.IsNullOrEmpty(coSign.UserName))
                                continue;
                            string imgPath = IdImgPaths[coSign.UserID];
                            string strSignDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                            var index = attrIndexDic["CoSign"];
                            coSignInfos.Add(GetRoleCoSignInfo(coSign.UserName, coSign.UserID, coSign.MajorName, strSignDate, "会签", index, imgPath));
                            attrIndexDic["CoSign"] = index + 1;
                        }
                        #endregion
                    }

                    if (AppConfig.IsStamp)
                    {
                        #region 拼凑盖章信息
                        foreach (PdfStampInfoDTO itemDTO in pdfStampInfoDTOs)
                        {
                            string imgPath = IdImgPaths[itemDTO.MainSeal.ID];
                            StampInfo stampInfo = new StampInfo();
                            stampInfo.Name = itemDTO.MainSeal.BlockKey;
                            stampInfo.LocalPath = imgPath;
                            stampInfo.AuthInfo = itemDTO.MainSeal.AuthInfo;
                            stampInfo.Width = Convert.ToDouble(itemDTO.MainSeal.Width);
                            stampInfo.Height = Convert.ToDouble(itemDTO.MainSeal.Height);
                            foreach (var fSeal in itemDTO.FollowSeals)
                            {
                                StampInfo childStampInfo = new StampInfo();
                                childStampInfo.Name = fSeal.BlockKey;
                                string childImgPath = IdImgPaths[fSeal.ID];
                                childStampInfo.LocalPath = childImgPath;
                                childStampInfo.AuthInfo = fSeal.AuthInfo;
                                childStampInfo.Width = Convert.ToDouble(fSeal.Width);
                                childStampInfo.Height = Convert.ToDouble(fSeal.Height);
                                childStampInfo.RelativePosX = Convert.ToDouble(fSeal.CorrectPosX);
                                childStampInfo.RelativePosY = Convert.ToDouble(fSeal.CorrectPosY);
                                stampInfo.ChildStampInfo.Add(childStampInfo);
                            }
                            stampInfos.Add(stampInfo);
                        }
                        #endregion
                    }

                    List<PdfImage> pdfImgs = new List<PdfImage>();
                    List<PdfText> pdfTxts = new List<PdfText>();
                    List<PdfAuthImg> pdfAuthImgs = new List<PdfAuthImg>();

                    BorderConfigInfo signConfig = null, cosignConfig = null, barcodeConfig = null, qrcodeConfig = null;
                    foreach (var item in signPosInfo.BorderConfigs)
                    {
                        if (item.Category == "Sign")
                            signConfig = item;
                        else if (item.Category == "CoSign")
                            cosignConfig = item;
                        else if (item.Category == "Barcode")
                            barcodeConfig = item;
                        else
                            qrcodeConfig = item;
                    }
                    for (int i = 0; i < signPosInfo.AuditRoles.Count; i++)
                    {
                        var key = signPosInfo.AuditRoles[i];
                        var isMajorCosignBlock = isMajorCosign(key);
                        double width = 0, height = 0, charHeight = 0, correctX = 0, correctY = 0;
                        int pageNum = 1, angle = 0;
                        bool visiable = true;
                        bool hasConfigFlag = false;
                        if (signPosInfo.AuditRolesConfig != null && signPosInfo.AuditRolesConfig.Count > i)
                        {
                            var bConfig = signPosInfo.AuditRolesConfig[i];
                            if (bConfig != null)
                            {
                                width = bConfig.Width;
                                height = bConfig.Height;
                                charHeight = bConfig.CharHeight;
                                angle = bConfig.Angle;
                                visiable = bConfig.Visiable;
                                hasConfigFlag = true;
                            }
                        }
                        if (visiable)
                        {
                            if (signPosInfo.AuditRolesPageNum != null && signPosInfo.AuditRolesPageNum.Count > i)
                                pageNum = signPosInfo.AuditRolesPageNum[i];
                            var auditRolesType = "";
                            if (signPosInfo.AuditRolesType != null && signPosInfo.AuditRolesType.Count > i)
                                if (!string.IsNullOrEmpty(signPosInfo.AuditRolesType[i]))
                                    auditRolesType = signPosInfo.AuditRolesType[i];

                            if (signPosInfo.PDFSignRoleConfig != null && signPosInfo.PDFSignRoleConfig.ContainsKey(auditRolesType))
                                visiable = signPosInfo.PDFSignRoleConfig[auditRolesType];
                            if (!visiable) continue;

                            if (key == "条码")
                            {

                            }
                            else if (key == "二维码")
                            {

                            }
                            else if (key.Contains("章"))
                            {
                                #region 图章
                                var stampInfo = stampInfos.FirstOrDefault(a => a.Name == key);
                                if (stampInfo != null)
                                {
                                    if (!hasConfigFlag)
                                    {
                                        width = stampInfo.Width;
                                        height = stampInfo.Height;
                                    }

                                    double attPtX = signPosInfo.PositionXs[i];
                                    double attPtY = signPosInfo.PositionYs[i];
                                    double left = attPtX;
                                    double bottom = attPtY;
                                    if (angle == 0 || angle == 180 || angle == -180)
                                    {
                                        left -= width / 2;
                                        bottom -= height / 2;
                                    }
                                    else
                                    {
                                        left -= height / 2;
                                        bottom -= width / 2;
                                    }
                                    PdfImage pdfImg = new PdfImage(stampInfo.LocalPath,
                                        (float)width, (float)height,
                                        (float)left, (float)bottom,
                                        (float)angle, pageNum);
                                    if (!string.IsNullOrEmpty(stampInfo.AuthInfo) && AppConfig.IsCA)
                                    {
                                        PdfAuthImg authImg = new PdfAuthImg(pdfImg);
                                        authImg.CertName = stampInfo.AuthInfo;
                                        pdfAuthImgs.Add(authImg);
                                    }
                                    else
                                        pdfImgs.Add(pdfImg);

                                    foreach (StampInfo childInfo in stampInfo.ChildStampInfo)
                                    {
                                        var childImgPosX = attPtX + childInfo.RelativePosX - childInfo.Width / 2;
                                        var childImgPosY = attPtY + childInfo.RelativePosY - childInfo.Height / 2;
                                        pdfImg = new PdfImage(childInfo.LocalPath,
                                            (float)childInfo.Width, (float)childInfo.Height,
                                            (float)childImgPosX, (float)childImgPosY,
                                            angle, pageNum);

                                        if (!string.IsNullOrEmpty(childInfo.AuthInfo) && AppConfig.IsCA)
                                        {
                                            PdfAuthImg authImg = new PdfAuthImg(pdfImg);
                                            authImg.CertName = childInfo.AuthInfo;
                                            pdfAuthImgs.Add(authImg);
                                        }
                                        else
                                            pdfImgs.Add(pdfImg);
                                    }
                                }
                                #endregion
                            }
                            else if (isMajorCosignBlock || auditRolesType.Contains("会签") || key.Contains("会签"))
                            {
                                #region 会签
                                if (!hasConfigFlag)
                                {
                                    width = cosignConfig.Width;
                                    height = cosignConfig.Height;
                                    charHeight = cosignConfig.CharHeight;
                                    angle = (int)cosignConfig.Angle;
                                    correctX = cosignConfig.CorrectPosX;
                                    correctY = cosignConfig.CorrectPosY;
                                }
                                double attPtX = signPosInfo.PositionXs[i] + correctX;
                                double attPtY = signPosInfo.PositionYs[i] + correctY;
                                double left = attPtX;
                                double bottom = attPtY;
                                var index = 1;
                                var indexStr = Regex.Replace(key, @"[^\d]*", "");
                                if (!string.IsNullOrEmpty(indexStr)) index = int.Parse(indexStr);
                                CoSignInfo cosignInfo = null;
                                if (isMajorCosignBlock)
                                {
                                    cosignInfo = coSignInfos.FirstOrDefault(a => key.Contains(a.Major));
                                }
                                else
                                {
                                    if (coSignInfos.Count >= index)
                                        cosignInfo = coSignInfos[index - 1];
                                }
                                if (cosignInfo != null)
                                {
                                    if (key.Contains("日期"))
                                    {
                                        pdfTxts.Add(new PdfText(cosignInfo.Date, "STSONG.TTF",
                                            (float)charHeight, 1,
                                            (float)attPtX, (float)attPtY,
                                            (float)angle, pageNum));
                                    }
                                    else if (key.Contains("专业"))
                                    {
                                        pdfTxts.Add(new PdfText(cosignInfo.Major, "STSONG.TTF",
                                            (float)charHeight, 1,
                                            (float)attPtX, (float)attPtY,
                                            (float)angle, pageNum));
                                    }
                                    else
                                    {
                                        if (angle == 0 || angle == 180 || angle == -180)
                                        {
                                            left -= width / 2;
                                            bottom -= height / 2;
                                        }
                                        else
                                        {
                                            left -= height / 2;
                                            bottom -= width / 2;
                                        }
                                        PdfImage pdfImg = new PdfImage(cosignInfo.LocalPath,
                                            (float)width, (float)height,
                                            (float)left, (float)bottom,
                                            (float)angle, pageNum);
                                        pdfImgs.Add(pdfImg);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 签名
                                if (!hasConfigFlag)
                                {
                                    width = signConfig.Width;
                                    height = signConfig.Height;
                                    charHeight = signConfig.CharHeight;
                                    angle = (int)signConfig.Angle;
                                    correctX = cosignConfig.CorrectPosX;
                                    correctY = cosignConfig.CorrectPosY;
                                }
                                double attPtX = signPosInfo.PositionXs[i] + correctX;
                                double attPtY = signPosInfo.PositionYs[i] + correctY;
                                double left = attPtX;
                                double bottom = attPtY;
                                var index = 1;
                                var indexStr = Regex.Replace(key, @"[^\d]*", "");
                                if (!string.IsNullOrEmpty(indexStr)) index = int.Parse(indexStr);
                                var roleName = Regex.Replace(key.Replace("签名", ""), @"\d", "");
                                var signInfo = signInfos.FirstOrDefault(a => a.BlockKeys.Contains(roleName + index.ToString()));
                                if (signInfo != null)
                                {
                                    if (key.Contains("日期"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.Date, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else if (key.Contains("签名角色"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.StepDes, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else if (!key.Contains("签名"))
                                    {
                                        pdfTxts.Add(new PdfText(signInfo.UserName, "STSONG.TTF",
                                                (float)charHeight, 1,
                                                (float)attPtX, (float)attPtY,
                                                (float)angle, pageNum));
                                    }
                                    else
                                    {
                                        if (angle == 0 || angle == 180 || angle == -180)
                                        {
                                            left -= width / 2;
                                            bottom -= height / 2;
                                        }
                                        else
                                        {
                                            left -= height / 2;
                                            bottom -= width / 2;
                                        }
                                        PdfImage pdfImg = new PdfImage(signInfo.LocalPath,
                                            (float)width, (float)height,
                                            (float)left, (float)bottom,
                                            (float)angle, pageNum);
                                        pdfImgs.Add(pdfImg);
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    if (File.Exists(pdfSignFilePath))
                    {
                        File.Delete(pdfSignFilePath);
                    }
                    float pageOneWidth = 0, pageOneHeight = 0;
                    if (signPosInfo.FrameProperty != null && signPosInfo.FrameProperty.Count > 0)
                    {
                        pageOneWidth = (float)signPosInfo.FrameProperty[0].PaperWidth;
                        pageOneHeight = (float)signPosInfo.FrameProperty[0].PaperHeight;
                    }
                    else
                    {
                        pageOneWidth = (float)signPosInfo.PaperWidth;
                        pageOneHeight = (float)signPosInfo.PaperHeight;
                    }
                    PdfOperate pdfOpe = new PdfOperate(pageOneWidth, pageOneHeight);
                    pdfOpe.PdfSign(pdfFilePath, pdfSignFilePath, pdfImgs, pdfTxts);
                    #endregion

                    var filestr = pdfSignFilePath;
                    if (AppConfig.IsCA)
                    {
                        var caOper = new PDFCAOper(pageOneWidth, pageOneHeight);
                        var tempPathWithoutExtension = Path.GetDirectoryName(pdfStampFilePath) + "/" + Path.GetFileNameWithoutExtension(pdfStampFilePath) + "Temp";
                        for (int i = 0; i < pdfAuthImgs.Count; i++)
                        {
                            var tempPath = tempPathWithoutExtension + i + ".pdf";
                            var basePdf = pdfSignFilePath;
                            var stampPdf = tempPath;
                            if (i != 0)
                                basePdf = tempList[tempList.Count - 1];
                            if (i == pdfAuthImgs.Count - 1)
                                stampPdf = pdfStampFilePath;

                            #region CA签章
                            caOper.Index = i;
                            caOper.BasePdf = basePdf;
                            caOper.StampPdf = stampPdf;
                            caOper.CASignProcess(pdfAuthImgs[i]);
                            tempList.Add(tempPath);
                            #endregion
                        }
                        filestr = pdfStampFilePath;
                    }
                    repo.UpdateTask(filestr, task);
                }
                catch (Exception ex)
                {
                    repo.TaskException(task, ex.Message);
                    //记录日志
                    LogWriter.Info(string.Format(ErrInfo, DateTime.Now.ToString(), task.ID, task.Name, ex.Message, ex.StackTrace));
                }
                finally
                {
                    foreach (var item in tempList)
                    {
                        try
                        {
                            if (File.Exists(item))
                                File.Delete(item);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
        }

        public SignInfo GetRoleSignInfo(RoleDefine roleDefine, string strUserName, string strUserID,
            string strDate, int signIndex, string imgPath, string stepDes)
        {
            SignInfo signInfo = new SignInfo { BlockKeys = new List<string>() };
            signInfo.UserName = strUserName;
            signInfo.UserID = strUserID;
            signInfo.LocalPath = imgPath;
            signInfo.Date = strDate;
            signInfo.StepDes = stepDes;
            signInfo.BlockKeys.Add(roleDefine.RoleName.Replace(" ", "") + signIndex.ToString());
            if (!string.IsNullOrEmpty(roleDefine.OtherName))
                foreach (var oName in roleDefine.OtherName.Replace("，", ",").Split(','))
                    if (!string.IsNullOrEmpty(oName))
                        signInfo.BlockKeys.Add(oName.Replace(" ", "") + signIndex.ToString());
            return signInfo;
        }

        public CoSignInfo GetRoleCoSignInfo(string strUserName,
            string strUserID, string strMajor,
            string strDate, string roleName, int coSignIndex, string imgPath)
        {
            CoSignInfo coSignInfo = new CoSignInfo();
            coSignInfo.RoleName = roleName + coSignIndex.ToString();
            coSignInfo.UserName = strUserName;
            coSignInfo.UserID = strUserID;
            coSignInfo.LocalPath = imgPath;
            coSignInfo.Date = strDate;
            coSignInfo.Major = strMajor;
            return coSignInfo;
        }

        public bool isMajorCosign(string key)
        {
            var major = GlobalData.AllMajor.FirstOrDefault(a => key.Contains(a.text));
            if (major != null)
                return true;
            return false;
        }

        private S_EP_PlotSealInfo CreateSealInfo(CASealDTO_Return dto)
        {
            var seal_Mis = new S_EP_PlotSealInfo();
            seal_Mis.ID = FormulaHelper.CreateGuid();
            seal_Mis.Name = dto.strategy_name;
            seal_Mis.Code = dto.strategy_name;
            seal_Mis.Type = ((picture_form)dto.picture_form).ToString();
            seal_Mis.BelongUser = "";
            seal_Mis.BelongUserName = "";
            seal_Mis.SealInfo = Convert.FromBase64String(dto.seal_thumbnail);
            seal_Mis.AuthInfo = "";
            seal_Mis.BlockKey = "出图章-" + dto.strategy_name;
            seal_Mis.QualityType = ((quality_type)dto.quality_type).GetDescription();
            seal_Mis.Remark = ((quality_type)dto.quality_type).GetDescription();
            seal_Mis.ExpireTime = FormatTime(dto.not_after);
            seal_Mis.Relational = dto.strategy_id;
            return seal_Mis;
        }

        private static DateTime FormatTime(string str)
        {
            DateTime dt = DateTime.Now;
            IFormatProvider ifp = new System.Globalization.CultureInfo("zh-CN", true);
            var formatStr = "yyyyMMddHHmmssfff";
            if (str.Length == 14)
                formatStr = "yyyyMMddHHmmss";
            DateTime.TryParseExact(str, formatStr, ifp, System.Globalization.DateTimeStyles.None, out dt);
            return dt;
        }
    }
}