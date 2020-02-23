using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Colors;
using System.IO;

namespace Plugin.CadJob
{
    /// <summary>
    /// CAD基础类库
    /// </summary>
    public class CADBase
    {
        #region CAD文档操作
        /// <summary>
        /// DWG文件是否已经被打开
        /// </summary>
        /// <param name="dwgFileName"></param>
        /// <returns></returns>
        public static Document IsDwgOpen(string dwgFileName)
        {
            DocumentCollection docman = Application.DocumentManager;
            foreach (Document doc in docman)
            {
                if (0 == string.Compare(dwgFileName, doc.Name, true))
                {
                    return doc;
                }
            }
            return null;
        }

        /// <summary>
        /// 打开DWG文件
        /// </summary>
        /// <param name="dwgFileName"></param>
        /// <returns></returns>
        public static Document OpenDwg(string dwgFileName, bool forReadonly)
        {
            Document doc = IsDwgOpen(dwgFileName);
            if (null == doc)
            {
                try
                {
                    doc = Application.DocumentManager.Open(dwgFileName, forReadonly);
                }
                catch (System.Exception ex)
                {
                	throw ex;
                }
            }
            return doc;
        }

        /// <summary>
        /// 将传入的文档置为当前文档
        /// </summary>
        /// <param name="doc"></param>
        public static void ActiveDocment(Document curDoc)
        {
            try
            {
                if (curDoc != Application.DocumentManager.MdiActiveDocument)
                {
                    Application.DocumentManager.MdiActiveDocument = curDoc;
                } 
                                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭DWG文件
        /// </summary>
        /// <param name="dwgFileName"></param>
        /// <returns></returns>
        public static void  CloseDwg(string dwgFileName, bool save)
        {
            Document doc = IsDwgOpen(dwgFileName);
            if (null != doc)
            {
                if (save)
                {
                    doc.CloseAndSave(dwgFileName);
                }
                else
                {
                    doc.CloseAndDiscard();
                }
            }
            else
            {
                throw new Exception(dwgFileName + " 未被打开.");
            }
        }

        /// <summary>
        /// 保存指定文档
        /// </summary>
        /// <param name="dwgDoc"></param>
        public static void Save(Document dwgDoc)
        {
            if (dwgDoc == null)
                return;

            if (Application.DocumentManager.MdiActiveDocument != dwgDoc)
            {
                Application.DocumentManager.MdiActiveDocument = dwgDoc;
            }
            int isModified = System.Convert.ToInt32(Application.GetSystemVariable("DBMOD"));

            // No need to save if not modified
            if (isModified != 0)
            {
                using (DocumentLock acLockDoc = dwgDoc.LockDocument())
                {
#if ACAD2008
                    dwgDoc.Database.SaveAs(dwgDoc.Name, dwgDoc.Database.SecurityParameters);
#else
                    dwgDoc.Database.SaveAs(dwgDoc.Name, true, DwgVersion.AC1021, dwgDoc.Database.SecurityParameters);
#endif
                }
            }
        }

        /// <summary>
        /// 保存并关闭文档
        /// </summary>
        /// <param name="dwgDoc"></param>
        /// <param name="IsSave">是否保存</param> 
        public static void CloseAndSave(Document dwgDoc, bool IsSave)
        {
            try
            {
                if (dwgDoc.CommandInProgress != "" && dwgDoc.CommandInProgress != "CD")
                {
                    dwgDoc.SendStringToExecute("\x03\x03", true, false, false);
                }

                if (dwgDoc.IsReadOnly)
                {
                    dwgDoc.CloseAndDiscard();
                }
                else
                {
                    // Activate the document, so we can check DBMOD
                    if (Application.DocumentManager.MdiActiveDocument != dwgDoc)
                    {
                        Application.DocumentManager.MdiActiveDocument = dwgDoc;
                    }
                    int isModified = System.Convert.ToInt32(Application.GetSystemVariable("DBMOD"));

                    // No need to save if not modified
                    if (isModified != 0 && IsSave)
                    {
#if ACAD2008
                        dwgDoc.Database.SaveAs(dwgDoc.Name, DwgVersion.Current);
#else
                        dwgDoc.Database.SaveAs(dwgDoc.Name, true, DwgVersion.AC1021, dwgDoc.Database.SecurityParameters);
#endif
                        dwgDoc.CloseAndDiscard();
                    }
                    else
                    {
                        dwgDoc.CloseAndDiscard();
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw new System.Exception("->Close:" + ex.Message);
            }
        }

        /// <summary>
        /// 新建文件并打开
        /// </summary>
        public static Document NewDwg()
        {
            try
            {
                return Application.DocumentManager.Add("acad.dwt");
            }
            catch (System.Exception ex)
            {
            	throw ex;
            }
        }

        /// <summary>
        /// 在目标路径根据acad.dwt创建新文件
        /// </summary>
        /// <param name="targetFile"></param>
        /// <returns></returns>
        public static bool NewDwgFromAcadDwt(string targetFile)
        {
            try
            {
                string sourceFile = null;
                string sourcePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\");
                bool exist = true;
                while (!System.IO.File.Exists(sourcePath + @"\CoDesign.dwt"))
                {
                    int pos = sourcePath.LastIndexOf("\\", sourcePath.Length - 2);
                    if (pos < 0)
                    {
                        exist = false;
                        break;
                    }
                    sourcePath = sourcePath.Substring(0, pos + 1);
                }
                if (exist)
                {
                    sourceFile = sourcePath + @"\CoDesign.dwt";
                }
                else
                {
                    throw new Exception("模板文件未找到.");
                }
                if(sourceFile != null)
                {
                    System.IO.File.Copy(sourceFile, targetFile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新建空文件,保存到指定位置并打开
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <returns></returns>
        public static Document NewDwg(string newFilePath)
        {
            try
            {
                Document newDoc = NewDwg();
#if ACAD2008
                newDoc.Database.SaveAs(newFilePath, DwgVersion.Current);
#else
                newDoc.Database.SaveAs(newFilePath, true, DwgVersion.AC1021, newDoc.Database.SecurityParameters);
#endif
                return newDoc;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 外部参照相关
        /// <summary>
        /// 将一个DWG文件做为外部参照插入到当前图纸里
        /// </summary>
        /// <param name="dwgPath">DWG文件路径,绝对路径相对路径都可以</param>
        /// <param name="attach">是否为附加型</param>
        /// <returns>外部参照的块表记录ID</returns>
        public static ObjectId AddXrefBlockTable(string dwgPath, bool attach)
        {
            try
            {
                if (string.IsNullOrEmpty(dwgPath))
                {
                    throw new Exception("引用文件不能为空！");
                }
                ObjectId idRet = ObjectId.Null;
                Database db = Application.DocumentManager.MdiActiveDocument.Database;
                string insertName = Path.GetFileNameWithoutExtension(dwgPath);
                //ObjectId lastEntId = CppImport.EntLast();
                if (attach)
                {
                    idRet = db.AttachXref(dwgPath, insertName);
                }
                else
                {
                    idRet = db.OverlayXref(dwgPath, insertName);
                }
                return idRet;
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

        /// <summary>
        /// 获得外部参照的类型
        /// </summary>
        public enum GetXrefType
        {
            Attach,
            Overlay,
            All
        }

        public static Dictionary<string,ObjectId> GetAllXrefExt(Database db, GetXrefType xrefType)
        {
            Dictionary<string, ObjectId> xrefArr = new Dictionary<string,ObjectId>();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                foreach (ObjectId btrId in bt)
                {
                    BlockTableRecord btr = trans.GetObject(btrId, OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsFromExternalReference)
                    {
                        if (btr.IsFromOverlayReference)
                        {
                            if (xrefType == GetXrefType.Overlay || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btr.PathName.ToLower(),btrId );
                            }
                        }
                        else
                        {
                            if (xrefType == GetXrefType.Attach || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btr.PathName.ToLower(),btrId);
                            }
                        }
                    }

                    foreach (ObjectId id in btr)
                    {
                        Entity ent = trans.GetObject(id, OpenMode.ForRead) as Entity;
                        if (ent is RasterImage)
                        {
                            RasterImage image = ent as RasterImage;
                            xrefArr.Add(image.Path.ToLower(), id);
                        }
#if !ACAD2008
                        if (ent is PdfReference)
                        {
                            PdfReference pdf = ent as PdfReference;
                            xrefArr.Add(pdf.Path.ToLower(), id);
                        }
#endif
                    }
                }
            }
            return xrefArr;
        }

        /// <summary>
        /// 获得数据库中的所有外部参照，无论加载与否。
        /// </summary>
        /// <returns>id, 路径</returns>
        public static Dictionary<ObjectId, string> GetAllXref(Database db, GetXrefType xrefType)
        {
            Dictionary<ObjectId, string> xrefArr = new Dictionary<ObjectId, string>();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                foreach (ObjectId btrId in bt)
                {
                    BlockTableRecord btr = trans.GetObject(btrId, OpenMode.ForRead) as BlockTableRecord;
                    if (btr.IsFromExternalReference)
                    {
                        if (btr.IsFromOverlayReference)
                        {
                            if (xrefType == GetXrefType.Overlay || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btrId, btr.PathName);
                            }
                        }
                        else
                        {
                            if (xrefType == GetXrefType.Attach || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btrId, btr.PathName);
                            }
                        }
                    }
                }
            }
            return xrefArr;
        }

        /// <summary>
        /// 获得数据库的已加载的外部参照
        /// </summary>
        /// <returns>id, 路径。当图纸没打开，获得空</returns>
        public static Dictionary<ObjectId, string> GetOpenDwgResolvedAllXref(Database db, GetXrefType xrefType)
        {
            Dictionary<ObjectId, string> xrefArr = new Dictionary<ObjectId, string>();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                foreach (ObjectId btrId in bt)
                {
                    BlockTableRecord btr = trans.GetObject(btrId, OpenMode.ForRead) as BlockTableRecord;
                    if (btr.XrefStatus == XrefStatus.Resolved)
                    {
                        if (btr.IsFromOverlayReference)
                        {
                            if (xrefType == GetXrefType.Overlay || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btrId, btr.PathName);
                            }
                        }
                        else
                        {
                            if (xrefType == GetXrefType.Attach || xrefType == GetXrefType.All)
                            {
                                xrefArr.Add(btrId, btr.PathName);
                            }
                        }
                    }
                }
            }
            return xrefArr;
        }
        #endregion


        #region 添加实体

        /// <summary>
        /// 将实体加入当前空间
        /// </summary>
        /// <param name="ent"></param>
        /// <returns>实体ID</returns>
        public static ObjectId AddToCurrentSpace(Entity ent)
        {
            return AddToCurrentSpace(ent, HostApplicationServices.WorkingDatabase);
        }
        public static ObjectId AddToCurrentSpace(Entity ent, Autodesk.AutoCAD.DatabaseServices.Database database)
        {
            ObjectId id;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                id = ((BlockTableRecord)transactionManager.GetObject(database.CurrentSpaceId, OpenMode.ForWrite, false)).AppendEntity(ent);
                transactionManager.AddNewlyCreatedDBObject(ent, true);
                transaction.Commit();
            }
            return id;
        }
        public static ObjectId AddToCurrentSpace(Entity ent, Autodesk.AutoCAD.DatabaseServices.Database database,ObjectId spaceid)
        {
            ObjectId id;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                id = ((BlockTableRecord)transactionManager.GetObject(spaceid, OpenMode.ForWrite, false)).AppendEntity(ent);
                transactionManager.AddNewlyCreatedDBObject(ent, true);
                transaction.Commit();
            }
            return id;
        }
        /// <summary>
        /// 取得模型空间ID
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static ObjectId ModelSpaceId(Autodesk.AutoCAD.DatabaseServices.Database db)
        {
            return SymbolUtilityServices.GetBlockModelSpaceId(db);
        }

        /// <summary>
        /// 取得图纸空间ID
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static ObjectId PaperSpaceId(Autodesk.AutoCAD.DatabaseServices.Database db)
        {
            return SymbolUtilityServices.GetBlockPaperSpaceId(db);
        }

        /// <summary>
        /// 将实体加入模型空间
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static ObjectId AddToModelSpace(Entity ent)
        {
            Autodesk.AutoCAD.DatabaseServices.Database workingDatabase = HostApplicationServices.WorkingDatabase;
            return AddToModelSpace(ent, workingDatabase);
        }
        public static ObjectId AddToModelSpace(Entity ent, Autodesk.AutoCAD.DatabaseServices.Database database)
        {
            ObjectId id = new ObjectId() ;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                id = ((BlockTableRecord)transactionManager.GetObject(ModelSpaceId(database), OpenMode.ForWrite, false)).AppendEntity(ent);
                transactionManager.AddNewlyCreatedDBObject(ent, true);
                transaction.Commit();
            }
            return id;
        }

        /// <summary>
        /// 将实体加入图纸空间
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static ObjectId AddToPaperSpace(Entity ent)
        {
            Autodesk.AutoCAD.DatabaseServices.Database workingDatabase = HostApplicationServices.WorkingDatabase;
            return AddToPaperSpace(ent, workingDatabase);
        }
        public static ObjectId AddToPaperSpace(Entity ent, Autodesk.AutoCAD.DatabaseServices.Database database)
        {
            ObjectId id;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                id = ((BlockTableRecord)transactionManager.GetObject(PaperSpaceId(database), OpenMode.ForWrite, false)).AppendEntity(ent);
                transactionManager.AddNewlyCreatedDBObject(ent, true);
                transaction.Commit();
            }
            return id;
        }
        /// <summary>
        /// 创建多线段
        /// </summary>
        /// <param name="pts"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Polyline Polyline(Point2dCollection pts, double width)
        {
            try
            {
                Polyline ent = new Polyline();
                for (int i = 0; i < pts.Count; i++)
                {
                    ent.AddVertexAt(i, pts[i], 0, width, width);
                }
                return ent;
            }
            catch
            {
                return new Polyline();
            }
        }
        /// <summary>
        /// 创建多行文字
        /// </summary>
        /// <param name="textString"></param>
        /// <param name="location"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static MText Mtext(string textString, Point3d location, double height, double width)
        {
            MText txt = new MText();
            txt.Location = location;
            txt.TextHeight = height;
            txt.Width = width;
            txt.Contents = textString;
            return txt;
        }
        public static MText Mtext(string textString, Point3d location, double height, double width, bool isfield) 
        { 
            MText txt = new MText(); 
            txt.Location = location; 
            txt.TextHeight = height; 
            txt.Width = width;
            //txt.Height = Pheight;
            if (isfield) 
            { 
                Field field = new Field(textString); 
                txt.SetField(field); 
            } 
            else 
                txt.Contents = textString; 
            return txt;
        }
        #endregion

        #region 图层
        public static ObjectId GetLayerId(string strLayerName)
        {
            return GetLayerId(strLayerName, HostApplicationServices.WorkingDatabase);
        }

        public static ObjectId GetLayerId(string strLayerName, Database db)
        {
            if (string.IsNullOrEmpty(strLayerName))
            {
                return ObjectId.Null;
            }
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable laTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (laTab != null)
                {
                    if (laTab.Has(strLayerName))
                    {
                        return laTab[strLayerName];
                    }
                }
            }
            return ObjectId.Null;
        }

        public static ObjectId AddLayer(LayerTableRecord laRec)
        {
            return AddLayer(laRec, HostApplicationServices.WorkingDatabase);
        }

        public static void SetCurrentFileLayerIsOff(string layerName, bool isOff)
        {
            Document curDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock acLockDoc = curDoc.LockDocument())
            {
                CADBase.SetLayerIsoff(layerName, isOff, HostApplicationServices.WorkingDatabase);
            }
        }

        public static void SetLayerIsoff(string layerName, bool isOff, Database db)
        {
            ObjectId layerId = GetLayerId(layerName);
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                if (layerId != ObjectId.Null)
                {
                    LayerTableRecord layTabRec = trans.GetObject(layerId, OpenMode.ForRead) as LayerTableRecord;
                    if (layTabRec != null)
                    {
                        layTabRec.UpgradeOpen();
                        layTabRec.IsOff = isOff;
                    }
                }
                trans.Commit();
            }
        }

        public static ObjectId AddLayer(LayerTableRecord laRec, Database db)
        {
            if (laRec == null)
            {
                return ObjectId.Null;
            }
            ObjectId layerId = GetLayerId(laRec.Name);
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                if (layerId == ObjectId.Null)
                {
                    LayerTable laTab = trans.GetObject(db.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    if (laTab != null)
                    {
                        layerId = laTab.Add(laRec);
                        trans.AddNewlyCreatedDBObject(laRec, true);
                    }
                }
                else
                {
                    LayerTableRecord laRecOld = trans.GetObject(layerId, OpenMode.ForWrite) as LayerTableRecord;
                    if (laRecOld != null)
                    {
                        laRecOld.Name = laRec.Name;
                        laRecOld.IsOff = laRec.IsOff;
                        laRecOld.IsFrozen = laRec.IsFrozen;
                        laRecOld.IsLocked = laRec.IsLocked;
                        laRecOld.Color = laRec.Color;
                        laRecOld.LinetypeObjectId = laRec.LinetypeObjectId;
                        laRecOld.LineWeight = laRec.LineWeight;
                        laRecOld.IsPlottable = laRec.IsPlottable;
                        laRecOld.ViewportVisibilityDefault = laRec.ViewportVisibilityDefault;
                        laRecOld.Description = laRec.Description;
                    }
                }
                trans.Commit();
            }
            return layerId;
        }

        public static List<LayerTableRecord> GetLayerTable()
        {
            return GetLayerTable(HostApplicationServices.WorkingDatabase);
        }

        public static List<LayerTableRecord> GetLayerTable(Database db)
        {
            List<LayerTableRecord> resultList = new List<LayerTableRecord>();
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable laTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                foreach (ObjectId recId in laTab)
                {
                    LayerTableRecord record = trans.GetObject(recId, OpenMode.ForRead) as LayerTableRecord;
                    resultList.Add(record);
                }
                trans.Commit();
            }
            return resultList;
        }

        /// <summary>
        /// 建立指定层
        /// </summary>
        /// <param name="strLayerName">图层名</param>
        /// <param name="db">数据库</param>
        /// <returns>图层ID objectId</returns>
        public static ObjectId AddLayerByName(string strLayerName, short ColorIndex, Database db)
        {
            try
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    LayerTable lt = trans.GetObject(db.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    ObjectId layerId = ObjectId.Null;
                    LayerTableRecord ltr = new LayerTableRecord();
                    if (lt.Has(strLayerName) == false)
                    {
                        ltr.Name = strLayerName;
                        ltr.Color = Color.FromColorIndex(ColorMethod.ByColor, (short)(ColorIndex % 256));
                        ltr.IsOff = false;
                        layerId = lt.Add(ltr);
                        trans.AddNewlyCreatedDBObject(ltr, true);
                    }
                    else
                    {
                        LayerTableRecord curLTR = trans.GetObject(db.Clayer, OpenMode.ForRead) as LayerTableRecord;
                        if (!curLTR.Name.ToLower().Equals(strLayerName.ToLower()))
                        {
                            ltr = trans.GetObject(lt[strLayerName], OpenMode.ForWrite) as LayerTableRecord;
                            ltr.Color = Color.FromColorIndex(ColorMethod.ByColor, (short)(ColorIndex % 256));
                            ltr.IsOff = false;
                            layerId = db.Clayer;
                        }
                    }
                    trans.Commit();
                    return layerId;
                }
            }
            catch (Exception ex)
            {

            }
            return ObjectId.Null;
        }


        /// <summary>
        /// 清理指定图层
        /// </summary>
        /// <param name="strLayer">图层名</param>
        /// <param name="isErase">是否删除图层</param>
        public static void ClearLayer(string strLayer,bool isErase)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction trans = transactionManager.StartTransaction())
            {
                BlockTable blkTab = trans.GetObject(db.BlockTableId,OpenMode.ForRead) as BlockTable;
                foreach (ObjectId blkRecId in blkTab)
                {
                    BlockTableRecord btr = trans.GetObject(blkRecId, OpenMode.ForRead) as BlockTableRecord;
                    foreach (ObjectId id in btr)
                    {
                        Entity ent = trans.GetObject(id, OpenMode.ForRead) as Entity;
                        if (ent.Layer == strLayer)
                        {
                            LayerTableRecord entLayer = trans.GetObject(ent.LayerId, OpenMode.ForRead) as LayerTableRecord;
                            if (!(entLayer.IsFrozen || entLayer.IsLocked))
                            {
                                ent.UpgradeOpen();
                                ent.Erase(true);
                            }
                        }
                    }
                }
                trans.Commit();
            }
            if (!isErase)
            {
                return;
            }
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable laTab = trans.GetObject(db.LayerTableId, OpenMode.ForWrite) as LayerTable;
                if (laTab != null)
                {
                    if (laTab.Has(strLayer))
                    {
                        ObjectId layerTabRecId = laTab[strLayer];
                        LayerTableRecord layerTabRec = trans.GetObject(layerTabRecId, OpenMode.ForWrite) as LayerTableRecord;
                        if (layerTabRec!=null)
                        {
                            layerTabRec.Erase();
                        }   
                    }
                }
                trans.Commit();
            }
        }
        /// <summary>
        /// 从Datebase清理指定图层
        /// </summary>
        /// <param name="strLayer">图层名</param>
        /// <param name="isErase">是否删除图层</param>
        public static void ClearLayer(Database db, string strLayer, bool isErase)
        {
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction trans = transactionManager.StartTransaction())
            {
                BlockTableRecord btr = trans.GetObject(db.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId id in btr)
                {
                    Entity ent = trans.GetObject(id, OpenMode.ForRead) as Entity;
                    if (ent.Layer == strLayer)
                    {
                        LayerTableRecord entLayer = trans.GetObject(ent.LayerId, OpenMode.ForRead) as LayerTableRecord;
                        if (!(entLayer.IsFrozen || entLayer.IsLocked))
                        {
                            ent.UpgradeOpen();
                            ent.Erase();
                        }
                    }
                }
                trans.Commit();
            }
            if (!isErase)
            {
                return;
            }
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable laTab = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (laTab != null)
                {
                    if (laTab.Has(strLayer))
                    {
                        ObjectId layerTabRecId = laTab[strLayer];
                        LayerTableRecord layerTabRec = trans.GetObject(layerTabRecId, OpenMode.ForWrite) as LayerTableRecord;
                        if (layerTabRec != null)
                        {
                            layerTabRec.Erase();
                        }
                    }
                }
                trans.Commit();
            }
        }
        #endregion

        #region 线型
        public static string GetLineTypeName(ObjectId lineTypeId)
        {
            return GetLineTypeName(lineTypeId, HostApplicationServices.WorkingDatabase);
        }
        private static string GetLineTypeName(ObjectId lineTypeId, Database db)
        {
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTableRecord lineRec = trans.GetObject(lineTypeId, OpenMode.ForRead) as LinetypeTableRecord;
                if (lineRec != null)
                {
                    return lineRec.Name;
                }
            }
            return null;
        }

        public static ObjectId GetLineTypeId(string strLineType)
        {
            return GetLineTypeId(strLineType, HostApplicationServices.WorkingDatabase);
        }
        public static ObjectId GetLineTypeId(string strLineType, Database db)
        {
            if (string.IsNullOrEmpty(strLineType))
            {
                return ObjectId.Null;
            }
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LinetypeTable ltTab = trans.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                if (ltTab != null)
                {
                    if (ltTab.Has(strLineType))
                    {
                        return ltTab[strLineType];
                    }
                }
            }
            return ObjectId.Null;
        }
        #endregion

        #region 判断是否为图框块
        public static bool IsTemplateEntity(ObjectId entId)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction trans = transactionManager.StartTransaction())
            {
                BlockReference block = trans.GetObject(entId, OpenMode.ForRead, false, true) as BlockReference;
                if (null != block)
                {
                    BlockTableRecord blockTab = trans.GetObject(block.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                    if (null != blockTab)
                    {
                        string blockName = blockTab.Name;

                        string[] s = blockName.Split(new char[] { '_' });
                        if (s.Length == 8 && String.Compare("template", s[s.Length - 1], true) == 0)
                        {                           
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        public static void SetEntLayer(ObjectId objId,string strLayer)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using(Transaction trans = db.TransactionManager.StartTransaction())
            {
                Entity ent = trans.GetObject(objId,OpenMode.ForWrite) as Entity;
                if (ent!=null)
                {
                    ent.Layer = strLayer;
                }
                trans.Commit();
            }
        }

        public static List<ObjectId> GetLayerEnt(string layerName)
        {
            List<ObjectId> objIdLst = new List<ObjectId>();
            Document curDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = curDoc.Database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                BlockTableRecord curSpaceBlkRec = transactionManager.GetObject(curDoc.Database.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId objId in curSpaceBlkRec)
                {
                    Entity ent = transaction.GetObject(objId, OpenMode.ForRead) as Entity;
                    if (ent != null)
                    {
                       if (ent.Layer==layerName)
                       {
                           objIdLst.Add(objId);
                       }
                    }
                }
                transaction.Commit();
            }
            return objIdLst;
        }


        //得到当前空间的所有实体
        public static List<ObjectId> GetCurSpaceAllEnt()
        {
            List<ObjectId> objIdLst = new List<ObjectId>();
            Document curDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = curDoc.Database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                BlockTableRecord curSpaceBlkRec = transactionManager.GetObject(curDoc.Database.CurrentSpaceId, OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId objId in curSpaceBlkRec)
                {
                    Entity ent = transaction.GetObject(objId, OpenMode.ForRead) as Entity;
                    if (ent != null)
                    {
                        objIdLst.Add(objId);
                    }
                }
                transaction.Commit();
            }
            return objIdLst;
        }

        //根据线型名称得到线型ID
        public static ObjectId GetLineType(string lineTypeName)
        {
            ObjectId lineTypeId = ObjectId.Null;
            Document curDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager = curDoc.Database.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction transaction = transactionManager.StartTransaction())
            {
                LinetypeTable curLineTypeTab = transactionManager.GetObject(curDoc.Database.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                lineTypeId = curLineTypeTab[lineTypeName];
                transaction.Commit();
            }
            return lineTypeId;
        }

        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        public static extern int SetFocus(IntPtr hWnd);
    }

    /// <summary>
    /// 封装C++中的ads_name
    /// </summary>
    internal struct ads_name
    {
        public unsafe void copyFrom(ads_name src)
        {
            this.item[0] = src.item[0];
            this.item[1] = src.item[1];
        }

        public unsafe void Clear()
        {
            this.item[0] = 0;
            this.item[1] = 0;
        }

        public unsafe bool isNull
        {
            get
            {
                return ((this.item[0] == 0) && (this.item[1] == 0));
            }
        }

        public unsafe int* item
        {
            get
            {
                fixed (ads_name* _nameRef = &this)
                {
                    return (int*)_nameRef;
                }
            }
        }

        public unsafe int this[int i]
        {
            get
            {
                return this.item[i];
            }
            set
            {
                this.item[i] = value;
            }
        }
    }

    /// <summary>
    /// 封装C++编程中的一些常用的函数
    /// </summary>
    public class CppImport
    {
        private static Hashtable resTypes = new Hashtable();
        public const string PauseToken = @"\";
        private const short RT3DPOINT = 0x1391;
        private const short RTENAME = 0x138e;
        private const short RTLONG = 0x1392;
        private const short RTNONE = 0x1388;
        private const short RTNORM = 0x13ec;
        private const short RTPOINT = 0x138a;
        private const short RTREAL = 0x1389;
        private const short RTSHORT = 0x138b;
        private const short RTSTR = 0x138d;

        static CppImport()
        {
            resTypes[typeof(string)] = (short)0x138d;
            resTypes[typeof(double)] = (short)0x1389;
            resTypes[typeof(Point3d)] = (short)0x1391;
            resTypes[typeof(ObjectId)] = (short)0x138e;
            resTypes[typeof(int)] = (short)0x1392;
            resTypes[typeof(short)] = (short)0x138b;
            resTypes[typeof(Point2d)] = (short)0x138a;
        }
        
        [DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int acdbEntLast(ads_name* ename);
        [DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int acdbEntNext(ads_name* ent, ads_name* result);
        [SuppressUnmanagedCodeSecurity, DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl)]
        private static extern int acedCmd(IntPtr resbuf);
        [DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl)]
        private static extern int acedTrans(double[] point, IntPtr fromRb, IntPtr toRb, int disp, double[] result);
        [SuppressUnmanagedCodeSecurity, DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int acedRedraw(ads_name* ent, int mode);
        [DllImport("acad.exe", EntryPoint = "acedFindFile", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int acedFindFile(char* strFileName, char* strFullPath);

#if (!ACAD2010)
        //AutoCAD 2008
        [DllImport("acdb17.dll", EntryPoint = "?acdbGetAdsName@@YA?AW4ErrorStatus@Acad@@AAY01JVAcDbObjectId@@@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void acdbGetAdsName(ads_name* objName, ObjectId objId);
        [DllImport("acdb17.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void acdbGetObjectId(out ObjectId objId, ads_name* ename);
#else
        //ACAD 2010
        [DllImport("acdb18.dll", EntryPoint = "?acdbGetAdsName@@YA?AW4ErrorStatus@Acad@@AAY01JVAcDbObjectId@@@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void acdbGetAdsName(ads_name* objName, ObjectId objId);
        [DllImport("acdb18.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void acdbGetObjectId(out ObjectId objId, ads_name* ename);
        [DllImport("acdb18.dll", EntryPoint = "acutAngle", CallingConvention = CallingConvention.Cdecl)]
        public static extern double acutAngle(double[] pt1, double[] pt2);
#endif
        /// <summary>
        /// acedCmd
        /// eg. 
        /// CppImport.RunCommand(false, new object[] { "draworder", id, "", "f" });
        /// CppImport.RunCommand(false, new object[] { "._zoom", "e" });
        /// </summary>
        public static void RunCommand(bool echoCommand, params object[] args)
        {
            if (!Application.DocumentManager.IsApplicationContext)
            {
                int num = 0;
                int num2 = Convert.ToInt32(Application.GetSystemVariable("CMDECHO"));
                using (ResultBuffer buffer = new ResultBuffer())
                {
                    foreach (object obj2 in args)
                    {
                        buffer.Add(TypedValueFromObject(obj2));
                        num++;
                    }
                    if (num > 0)
                    {
                        if (!echoCommand)
                        {
                            Application.SetSystemVariable("CMDECHO", 0);
                        }
                        else
                        {
                            Application.SetSystemVariable("CMDECHO", 1);
                        }
                        acedCmd(buffer.UnmanagedObject);
                    }
                }
                Application.SetSystemVariable("CMDECHO", num2);
            }
        }

        private static TypedValue TypedValueFromObject(object val)
        {
            Type key = val.GetType();
            if (!resTypes.ContainsKey(key))
            {
                throw new InvalidOperationException("Unsupported type in Command() method");
            }
            return new TypedValue((short)resTypes[key], val);
        }

        /// <summary>
        /// acdbEntLast
        /// </summary>
        public static unsafe ObjectId EntLast()
        {
            ObjectId id;
            ads_name ename = new ads_name();
            acdbEntLast(&ename);
            acdbGetObjectId(out id, &ename);
            return id;
        }

        /// <summary>
        /// acdbEntNext
        /// </summary>
        public static unsafe ObjectId EntNext(ObjectId entId)
        {
            ObjectId id;
            ads_name objName = new ads_name();
            ads_name result = new ads_name();
            acdbGetAdsName(&objName, entId);
            acdbEntNext(&objName, &result);
            acdbGetObjectId(out id, &result);
            return id;
        }

        /// <summary>
        /// acedFindFile
        /// </summary>
        public static unsafe string FindFile(string strFileName)
        {
            fixed (char* pStrFillName = &strFileName.ToCharArray()[0])
            {
                fixed (char* pStrFullpath = &(new char[256])[0])
                {
                    int res = acedFindFile(pStrFillName, pStrFullpath);
                    if (res == RTNORM)
                    {
                        return new string(pStrFullpath);
                    }
                }
            }
            return null;
        }

        public enum CoordinateSystem
        {
            WCS,
            UCS,
            DisplayDCS,
            PaperSpaceDCS
        }

        /// <summary>
        /// acedTrans
        /// Warning:The paper space DCS (PSDCS) can be transformed only to or from the model space DCS.
        /// ZJ 20091007
        /// </summary>
        public static Point3d TranslateCoordinates(Point3d originalPoint, CoordinateSystem from, CoordinateSystem to)
        {
            TypedValue[] values = new TypedValue[] { new TypedValue(RTSHORT, (int)from) };
            ResultBuffer buffer = new ResultBuffer(values);
            TypedValue[] valueArray2 = new TypedValue[] { new TypedValue(RTSHORT, (int)to) };
            ResultBuffer buffer2 = new ResultBuffer(valueArray2);
            double[] result = new double[3];
            int res = acedTrans(originalPoint.ToArray(), buffer.UnmanagedObject, buffer2.UnmanagedObject, 0, result);
            return new Point3d(result);
        }
        public static Vector3d TranslateCoordinates(Vector3d originalVector, CoordinateSystem from, CoordinateSystem to)
        {
            TypedValue[] values = new TypedValue[] { new TypedValue(RTSHORT, (int)from) };
            ResultBuffer buffer = new ResultBuffer(values);
            TypedValue[] valueArray2 = new TypedValue[] { new TypedValue(RTSHORT, (int)to) };
            ResultBuffer buffer2 = new ResultBuffer(valueArray2);
            double[] result = new double[3];
            acedTrans(originalVector.ToArray(), buffer.UnmanagedObject, buffer2.UnmanagedObject, 1, result);
            return new Vector3d(result);
        }


        public enum RedrawMode
        {
            HighlightEntity = 3,
            RedrawEntity = 1,
            UndrawEntity = 2,
            UnHighlightEntity = 4
        }

        public static unsafe void ReDraw(Autodesk.AutoCAD.EditorInput.Editor ed, ObjectId entityId, RedrawMode mode)
        {
            ads_name objName = new ads_name();
            acdbGetAdsName(&objName, entityId);
            acedRedraw(&objName, (int)mode);
        }
        //判断文件是否打开,若打开是否激活为当前文档 
        public static bool IsOpened(string filePath,bool bActive = false)
        {
            bool bFound = false;
            Document document = null;
            DocumentCollection docColl = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
            foreach (Document curDoc in docColl)
            {
                if (string.Compare(filePath, curDoc.Name, true) == 0)
                {
                    bFound = true;
                    if (bActive)
                    {
                        if (curDoc != docColl.MdiActiveDocument)
                        {
                            docColl.MdiActiveDocument = curDoc;
                        }
                    }
                    document = curDoc;
                    break;
                }
            }
            return bFound;
        }

  



    }
}
