using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using Formula.Helper;
using Config;
using Config.Logic;
using FileStore.Logic.BusinessFacade;
using DocSystem.Logic.Domain;
using System.Data;
using System.IO;
using FileStore;

namespace DocSystemTool
{
    public class ConvertTools
    {
        static OuterServiceFO fo = new OuterServiceFO();

        public static void Convert()
        {
            var entities = FormulaHelper.GetEntities<DocConfigEntities>();
            var list = entities.S_C_Space.ToList();
            foreach (var space in list)
            {
                var db = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                string sql = "SELECT * FROM S_ATTACHMENT WHERE PDFFILE IS NULL AND MAINFILE IS NOT NULL ";
                var dt = db.ExecuteDataTable(sql);
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Console.WriteLine("Start process file {0}", row["MainFile"].ToString());
                        var desFolder = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];
                        var mainFile = fo.LocalizeFile(row["MainFile"].ToString());
                        FileInfo file = new FileInfo(mainFile.FileFullPath);
                        if (!file.Exists) continue;

                        var pdfFullPath = ConvertPDF(file);
                        if (!String.IsNullOrEmpty(pdfFullPath) && File.Exists(pdfFullPath))
                        {
                            var pdfFile = new FileInfo(pdfFullPath);
                            string pdfName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + ".pdf";
                            var pdfPath = fo.AddLocalFile(pdfFullPath, pdfName, "", "", "", "PDF");
                            db.ExecuteNonQuery(String.Format("UPDATE S_ATTACHMENT SET PDFFILE ='{0}' WHERE ID='{1}'", pdfPath, row["ID"].ToString()));

                            var shotSnap = ConvertShotSnap(pdfFile);
                            db.ExecuteNonQuery(String.Format("UPDATE S_ATTACHMENT SET THUMBNAIL ='{0}' WHERE ID='{1}'", shotSnap, row["ID"].ToString()));

                            if (pdfFile.Extension.ToLower().IndexOf("pdf") >= 0)
                            {
                                var swfName = Guid.NewGuid().ToString() + ".swf";
                                var swfFilePath = desFolder.TrimEnd('\\') + "\\" + swfName;
                                Convertor.PDFToSWF(pdfFullPath, swfFilePath);
                                var swfPath = fo.AddLocalFile(swfFilePath, swfName, "", "", "", "SWF", true);
                                db.ExecuteNonQuery(String.Format("UPDATE S_ATTACHMENT SET SWFFILE ='{0}' WHERE ID='{1}'", swfPath, row["ID"].ToString()));
                            }
                            if (File.Exists(pdfFullPath))
                                File.Delete(pdfFullPath);
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }


        public static string ConvertPDF(FileInfo file)
        {
            string FileType = file.Extension.TrimStart('.').ToLower();
            var desFolder = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];
            string pdfName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + ".pdf";
            var pdfFullPath = desFolder.TrimEnd('\\') + "\\" + pdfName;
            if (FileType.IndexOf("doc") >= 0)
                Convertor.WordToPDF(file.FullName, pdfFullPath,true);
            else if (FileType.IndexOf("xls") >= 0)
                Convertor.ExcelToPDF(file.FullName, pdfFullPath,true);
            else if (FileType.IndexOf("pdf") >= 0)
            {
                file.CopyTo(pdfFullPath, true);
            }
            else if (FileType.IndexOf("bmp") >= 0 || FileType.IndexOf("png") >= 0 || FileType.IndexOf("gif") >= 0
                || FileType.IndexOf("jpg") >= 0 || FileType.IndexOf("tiff") >= 0)
            {
                pdfFullPath = file.Name.Substring(0, file.Name.LastIndexOf(".")) + "." + FileType;
                file.CopyTo(pdfFullPath, true);
            }
            else { return string.Empty; }
            return pdfFullPath;
        }

        public static string ConvertShotSnap(FileInfo  file)
        {
            var result = "";
            string FileType = file.Extension.TrimStart('.').ToLower();
            if (FileType.IndexOf("pdf") >= 0)
            {
                var desFolder = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];
                string pdfName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + ".pdf";
                var pdfFullPath = desFolder.TrimEnd('\\') + "\\" + pdfName;
                string bmpName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + ".bmp";
                var bmpFilePath = desFolder.TrimEnd('\\') + "\\" + bmpName;
                Convertor.PDFToBMP(pdfFullPath, bmpFilePath);
                var bmpPath = fo.AddLocalFile(bmpFilePath, bmpName, "", "", "", "ShotSnap", true);
                result = "/ShotSnap/" + FileHelper.GetFolderPath(bmpPath) + "/" + bmpPath;
            }
            else if(FileType.IndexOf("bmp") >= 0||FileType.IndexOf("png") >= 0||FileType.IndexOf("gif") >= 0
                ||FileType.IndexOf("jpg") >= 0||FileType.IndexOf("tiff") >= 0)
            {
                string bmpName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + ".bmp";
                var bmpPath = fo.AddLocalFile(file.FullName, bmpName, "", "", "", "ShotSnap", true);
                result = "/ShotSnap/" + FileHelper.GetFolderPath(bmpPath) + "/" + bmpPath;
            }
            return result;
        }

    }
}
