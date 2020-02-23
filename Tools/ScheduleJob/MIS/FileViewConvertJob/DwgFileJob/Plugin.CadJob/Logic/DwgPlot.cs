using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Runtime;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Common.Logic;
using CsZoomify;

namespace Plugin.CadJob
{
    /// <summary>
    /// 打印主代码
    /// </summary>
    public class DwgPlot
    {
        public static ObjectId GetLayoutId(Database db, string name)
        {
            ObjectId layoutId = new ObjectId();
            BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
            foreach (ObjectId btrId in bt)
            {
                BlockTableRecord btr = (BlockTableRecord)btrId.GetObject(OpenMode.ForRead);
                if (btr.IsLayout)
                {
                    Layout layout = (Layout)btr.LayoutId.GetObject(OpenMode.ForRead);
                    if (layout.LayoutName.CompareTo(name) == 0)
                    {
                        layoutId = btr.LayoutId;
                        break;
                    }
                }
            }
            return layoutId;
        }
        public static void GetCurSpaceAllEntLength(ref double life, ref double right, ref double up, ref double down)
        {
            List<ObjectId> objIdLst = new List<ObjectId>();
            Document curDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = curDoc.Database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                BlockTableRecord curSpaceBlkRec = transactionManager.GetObject(curDoc.Database.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId objId in curSpaceBlkRec)
                {
                    Entity ent;
                    try
                    {
                        ent = transaction.GetObject(objId, OpenMode.ForRead) as Entity;
                    }
                    catch (System.Exception e)
                    {
                        continue;
                    }
                    if (ent == null)
                    {
                        continue;
                    }
                    if (ent is Line)
                    {
                        Line line = ent as Line;
                        if (line.StartPoint.X < life)
                        {
                            life = line.StartPoint.X;
                        }
                        if (line.StartPoint.X > right)
                        {
                            right = line.StartPoint.X;
                        }
                        if (line.StartPoint.Y > up)
                        {
                            up = line.StartPoint.Y;
                        }
                        if (line.StartPoint.Y < right)
                        {
                            down = line.StartPoint.Y;
                        }
                    }

                    if (ent is Circle)
                    {
                        Circle circle = ent as Circle;
                        if (circle.Center.X < life)
                        {
                            life = circle.Center.X;
                        }
                        if (circle.Center.X > right)
                        {
                            right = circle.Center.X;
                        }
                        if (circle.Center.Y > up)
                        {
                            up = circle.Center.Y;
                        }
                        if (circle.Center.Y < right)
                        {
                            down = circle.Center.Y;
                        }
                    }

                    if (ent is Polyline)
                    {
                        Polyline polyline = ent as Polyline;
                        for (int i = 0; i < polyline.NumberOfVertices; i++)
                        {
                            Point2d temPt = polyline.GetPoint2dAt(i);
                            if (temPt.X < life)
                            {
                                life = temPt.X;
                            }
                            if (temPt.X > right)
                            {
                                right = temPt.X;
                            }
                            if (temPt.Y > up)
                            {
                                up = temPt.Y;
                            }
                            if (temPt.Y < right)
                            {
                                down = temPt.Y;
                            }
                        }
                    }
                }
                transaction.Commit();
            }
        }

        public static ResultDTO plotTest(string id, string file, string fileID, bool isSnap = false)
        {
            try
            {
                Document doc = Application.DocumentManager.Open(file, false);
                Application.DocumentManager.MdiActiveDocument = doc;

                PlotSettings ps = null;//声明增强型打印设置对象
                Layout layout = null;//当前布局对象
                bool isTile = AppConfig.GetAppSettings("ViewMode").ToLower().Trim() == "tilepicviewer";
                using (Transaction trans = doc.Database.TransactionManager.StartTransaction())
                {
                    LayoutManager lm = LayoutManager.Current;//获取当前布局管理器
                    layout = (Layout)GetLayoutId(doc.Database, lm.CurrentLayout).GetObject(OpenMode.ForRead);

                    ps = new PlotSettings(layout.ModelType);
                    ps.CopyFrom(layout);//从已有打印设置中获取打印设置

                    //0.更新打印设备、图纸尺寸和打印样式表信息
                    PlotSettingsValidator validator = PlotSettingsValidator.Current;
                    validator.RefreshLists(ps);
                    //1.设置打印驱动
                    string plotConfigurationName = AppConfig.GetAppSettings("Printer");
                    if (isSnap)
                        plotConfigurationName = "PublishToWeb JPG.pc3";
                    validator.SetPlotConfigurationName(ps, plotConfigurationName, null);

                    //2.设置打印纸张
                    StringCollection cMNameLst = validator.GetCanonicalMediaNameList(ps);
                    string mediaName = AppConfig.GetAppSettings("CanonicalMediaName");
                    if (isSnap)
                        mediaName = "VGA (480.00 x 640.00 像素)";
                    bool isHas = false;
                    StringCollection canonicalMediaNames = validator.GetCanonicalMediaNameList(ps);
                    foreach (string canonicalMediaName in canonicalMediaNames)
                    {
                        string localeMediaName = validator.GetLocaleMediaName(ps, canonicalMediaName);
                        if (localeMediaName == mediaName)
                        {
                            validator.SetCanonicalMediaName(ps, canonicalMediaName);
                            isHas = true;
                            break;
                        }
                    }
                    if (!isHas)
                    {
                        throw new System.Exception("纸张:" + mediaName + "不存在！");
                    }
                    //3.设置打印样式表
                    string plotStyleSheet = AppConfig.GetAppSettings("PlotStyleSheet");
                    validator.SetCurrentStyleSheet(ps, plotStyleSheet);

                    validator.SetPlotType(ps, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);
                    validator.SetPlotCentered(ps, true);
                    validator.SetUseStandardScale(ps, true);
                    validator.SetStdScaleType(ps, StdScaleType.ScaleToFit);//设置打印时布满图纸

                    validator.SetPlotRotation(ps, PlotRotation.Degrees000);
                    trans.Commit();
                }

                PlotConfig config = PlotConfigManager.CurrentConfig;
                doc = AcadApp.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;
                //获取去除扩展名后的文件名（不含路径）
                string fileName = SymbolUtilityServices.GetSymbolNameFromPathName(doc.Name, "dwg");
                //存放在同目录下
                var basePath = AppConfig.GetAppSettings("CacheViewFilePath");
                if (!basePath.EndsWith("\\"))
                    basePath += "\\";

                int num = Convert.ToInt32(fileID) / 1000 + 1;
                string root = basePath + "Dwg\\" + string.Format("{0}", num.ToString("D8")) + "\\";
                if (!isTile)
                    root = Path.Combine(basePath, string.Format("{0}", num.ToString("D8")) + "\\");
                if (!Directory.Exists(@root))
                {
                    Directory.CreateDirectory(@root);
                }

                fileName = root + id + config.DefaultFileExtension;
                if (!isTile|| isSnap)
                    fileName = root + fileID + config.DefaultFileExtension;

                //为了防止后台打印问题，必须在调用打印API时设置BACKGROUNDPLOT系统变量为0
                short backPlot = (short)AcadApp.GetSystemVariable("BACKGROUNDPLOT");
                AcadApp.SetSystemVariable("BACKGROUNDPLOT", 0);

                PlotEngine plotEngine = PlotFactory.CreatePublishEngine();//创建一个打印引擎            
                PlotInfo pi = new PlotInfo();//创建打印信息
                pi.Layout = layout.ObjectId;//要打印的布局
                pi.OverrideSettings = ps;//使用ps中的打印设置

                //验证打印信息是否有效
                PlotInfoValidator piv = new PlotInfoValidator();
                piv.MediaMatchingPolicy = MatchingPolicy.MatchEnabled;
                piv.Validate(pi);

                PlotPageInfo ppi = new PlotPageInfo();
                plotEngine.BeginPlot(null, null);
                plotEngine.BeginDocument(pi, doc.Name, null, 1, true, fileName);
                plotEngine.BeginPage(ppi, pi, true, null);
                plotEngine.BeginGenerateGraphics(null);
                plotEngine.EndGenerateGraphics(null);
                plotEngine.EndPage(null);
                plotEngine.EndDocument(null);
                plotEngine.EndPlot(null);

                plotEngine.Destroy();//销毁打印引擎 

                //恢复BACKGROUNDPLOT系统变量的值
                AcadApp.SetSystemVariable("BACKGROUNDPLOT", backPlot);

                doc.CloseAndDiscard();

                if (!File.Exists(fileName))
                {
                    throw new System.Exception("打印文件生成失败！");
                }

                ResultDTO reVal = new ResultDTO();
                if (isTile&&!isSnap)//如果是图片就切图
                {
                    var img = new ZoomifyImage(fileName, 1024);
                    img.Zoomify(root + id + "\\");

                    reVal.status = true;
                    reVal.DirectoryPath = "/" + id + "/";
                    reVal.ZoomLevel = img.ZoomLevels - 1;
                }
                else
                {
                    reVal.status = true;
                }

                try
                {
                    //强制删除过程打印文件
                    if (File.Exists(file) && !isSnap)
                        File.Delete(file);
                }
                catch { }

                return reVal;
            }
            catch (System.Exception e)
            {
                return new ResultDTO
                {
                    status = false,
                    ErrInfo = e.Message,
                    FileName = file
                };
            }
        }
    }

    /// <summary>
    /// 输出结果
    /// </summary>
    public class ResultDTO
    {
        public bool IsWaiting;//是否等待
        public string FileName;//签名的文件名
        public string FsFileID;//签完名上传的文件ID
        public string ErrInfo; //详情

        public bool status;
        public string DirectoryPath;//存储路径
        public int ZoomLevel;//放大倍数
    }
}
