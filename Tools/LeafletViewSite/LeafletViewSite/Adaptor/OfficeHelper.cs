using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Leaflet.Adaptor.DTO;
using Newtonsoft.Json;
using System.Collections;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Web;

namespace Leaflet.Adaptor
{
    public class OfficeHelper
    {
        public static string GetFolderPath(string fileID, string folder = "")
        {
            var path = HttpContext.Current.Server.MapPath("/Files");
            int num = Convert.ToInt32(fileID) / 1000 + 1;
            if (!string.IsNullOrEmpty(folder))
                path = System.IO.Path.Combine(path, folder);
            path = System.IO.Path.Combine(path, string.Format("{0}", num.ToString("D8")));
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            return path;
        }

        public static string GetFileName(string fileFullName)
        {
            return fileFullName.Substring(0, fileFullName.LastIndexOf("."));
        }

        public static string GetFileSuffix(string fileFullName)
        {
            return fileFullName.Substring(fileFullName.LastIndexOf("."), fileFullName.Length - fileFullName.LastIndexOf("."));
        }


        public static List<ImageDTO> GetFiles(string state = "")
        {
            var path = Path.Combine(HttpContext.Current.Server.MapPath("Files"), "Json");
            List<ImageDTO> lists = new List<ImageDTO>();
            if (!string.IsNullOrEmpty(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (var fileSystemInfo in dir.GetFileSystemInfos())
                {
                    DirectoryInfo di = new DirectoryInfo(Path.Combine(path, fileSystemInfo.Name));
                    FileInfo[] fis = di.GetFiles();
                    foreach (FileInfo fi in fis)
                    {
                        if (!string.IsNullOrEmpty(fi.Name) && fi.Name.ToLower().IndexOf("user") < 0)
                        {
                            StreamReader sr = fi.OpenText();
                            string str = sr.ReadToEnd();
                            if (!string.IsNullOrEmpty(str))
                            {
                                ImageDTO img = JsonHelper.ToObject<ImageDTO>(str);
                                if (img != null)
                                    lists.Add(img);
                            }
                            sr.Close();
                        }
                    }
                }
            }
            return string.IsNullOrEmpty(state) ? lists.OrderByDescending(c => c.State).ToList() : lists.Where(c => c.State == state).OrderByDescending(c => c.State).ToList();
        }

        public static ImageDTO GetFile(string fileID, string state = "")
        {
            var path = Path.Combine(GetFolderPath(fileID, "Json"), fileID + ".txt");
            ImageDTO image = new ImageDTO();
            if (!string.IsNullOrEmpty(path))
            {
                StreamReader sr = File.OpenText(path);
                string str = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(str))
                {
                    image = JsonHelper.ToObject<ImageDTO>(str);
                }
                sr.Close();
            }
            return image.State == state ? image : null;
        }


        public static bool ModifyFile(ImageDTO imgDto, string path)
        {
            if (imgDto != null)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        string json = JsonHelper.ToJson(imgDto);
                        if (!string.IsNullOrEmpty(json))
                        {
                            if (!File.Exists(path))
                            {
                                FileStream myFs = new FileStream(path, FileMode.Create);
                                StreamWriter mySw = new StreamWriter(myFs);
                                mySw.Write(json);
                                mySw.Close();
                                myFs.Close();
                            }
                        }
                    }
                }
                return true;
            }
            else
                return false;
        }

        private static string GetFileText(string fullName)
        {
            string fileStr = "";
            if (!string.IsNullOrEmpty(fullName))
            {
                if (File.Exists(fullName))
                {
                    StreamReader sr = File.OpenText(fullName);
                    string str = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(str))
                    {
                        //img = JsonHelper.ToObject<ImageDTO>(str);
                        fileStr = str;
                    }
                    sr.Close();
                }
            }
            return fileStr;
        }

        public static string GetFileByID(string fileID)
        {
            return GetFileText(GetFullPath(fileID));
        }

        private static string HumanReadableFilesize(double size)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }

        public static void CreateFile(string fileName, double size, string fileID = "")
        {
            int num = Convert.ToInt32(fileID) / 1000 + 1;
            if (string.IsNullOrEmpty(fileID))
                fileID = FormulaHelper.CreateGuid();
            string versionID = FormulaHelper.CreateGuid();
            string json = JsonHelper.ToJson(new ImageDTO()
            {
                ID = fileID,
                Name = GetFileName(fileName),
                Suffix = GetFileSuffix(fileName),
                State = Leaflet.Adaptor.Enum.EnumWork.New.ToString(),
                ErrorMessage = "新增文件",
                Version = versionID,
                Versions = new List<VersionDTO> { 
                    new VersionDTO{
                        ID = versionID,
                        FileID = fileID,
                        Name = "1.0",
                        FullPath = Path.Combine(GetFolderPath(fileID, "Files"), fileName),
                        Suffix = GetFileSuffix(fileName),
                        State = Leaflet.Adaptor.Enum.EnumWork.New.ToString(),
                        ImagePath = "",
                        ImageZoomLevel = 0,
                        ModifyTime = DateTime.Now,
                        HighHeightUnit = 0,
                        Size = HumanReadableFilesize(size),
                        Annotations = new List<AnnotationDTO>(),
                        Comments = new List<CommentDTO>()
                    }
                }
            });
            if (!string.IsNullOrEmpty(json))
            {
                string txtName = Path.Combine(GetFolderPath(fileID, "Json"), fileID + ".txt");
                if (!File.Exists(txtName))
                {
                    FileStream myFs = new FileStream(txtName, FileMode.Create);
                    StreamWriter mySw = new StreamWriter(myFs);
                    mySw.Write(json);
                    mySw.Close();
                    myFs.Close();
                }
            }
        }

        public static string FileUpgrade(string fileName, string root, string fileID, double size)
        {
            string fullName = GetFullPath(fileID);
            string fileText = GetFileText(fullName);
            string str = "";
            ImageDTO text = JsonHelper.ToObject<ImageDTO>(fileText);
            if (text != null)
            {
                string curVersion = "1.0";
                text.State = Leaflet.Adaptor.Enum.EnumWork.New.ToString();
                text.ErrorMessage = "新增文件";
                foreach (var item in text.Versions)
                {
                    if (Convert.ToDouble(item.Name) > Convert.ToDouble(curVersion))
                        curVersion = item.Name;
                }
                text.Versions.Add(new VersionDTO
                {
                    ID = FormulaHelper.CreateGuid(),
                    Name = string.Format("{0}.0", Convert.ToInt32(Convert.ToDouble(curVersion)) + 1),
                    FullPath = root + "\\" + fileName,
                    Suffix = OfficeHelper.GetFileSuffix(fileName),
                    State = Leaflet.Adaptor.Enum.EnumWork.New.ToString(),
                    ImagePath = "",
                    ImageZoomLevel = 0,
                    ModifyTime = DateTime.Now,
                    Size = HumanReadableFilesize(size),
                    Annotations = new List<AnnotationDTO>(),
                    Comments = new List<CommentDTO>()
                });
            }

            ModifyFile(text, fullName);
            return str;
        }

        public static string GetUsers()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Scripts") + "\\User.txt";
            return GetFileText(path);
        }

        public static bool SetUsers(List<UserDTO> userDTO)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Scripts") + "\\User.txt";
            string users = GetUsers();
            List<UserDTO> lists = JsonHelper.ToObject<List<UserDTO>>(users);
            foreach (var item in userDTO)
            {
                if (lists != null && lists.Where(c => c.UserID == item.UserID).Count() <= 0)
                    lists.Add(item);
                if (lists == null)
                {
                    lists = new List<UserDTO>();
                    lists.Add(item);
                }
            }
            if (File.Exists(path))
                File.Delete(path);
            FileStream myFs = new FileStream(path, FileMode.Create);
            StreamWriter mySw = new StreamWriter(myFs);
            mySw.Write(JsonHelper.ToJson(lists));
            mySw.Close();
            myFs.Close();
            return true;
        }

        private static string GetFullPath(string fileID)
        {
            return  Path.Combine(GetFolderPath(fileID, "Json"), fileID + ".txt");
        }

        public static string SetAnnotationAndComment(string jsonString, bool isAnnotation)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> data = (Dictionary<string, object>)serializer.DeserializeObject(jsonString);

            string fileID = Convert.ToString(data["DocumentID"]);
            string fileText = GetFileByID(fileID);
            string str = "";
            ImageDTO text = JsonHelper.ToObject<ImageDTO>(fileText);
            if (text != null)
            {
                string versionID = Convert.ToString(data["VersionID"]);
                foreach (var item in text.Versions)
                {
                    if (item.ID == versionID)
                    {
                        if (isAnnotation)
                        {
                            AnnotationDTO annotation = new AnnotationDTO();
                            annotation.ID = FormulaHelper.CreateGuid();
                            annotation.UserID = Convert.ToString(data["UserID"]);
                            annotation.PID = "";
                            annotation.Content = Convert.ToString(data["Content"]);
                            annotation.Time = DateTime.Now;
                            annotation.LayerID = Convert.ToString(data["LayerID"]);
                            annotation.LayerType = Convert.ToString(data["LayerType"]);
                            annotation.GraphData = JsonHelper.ToObject<GraphDataDTO>(Convert.ToString(data["GraphData"]));
                            item.Annotations.Add(annotation);
                            str = JsonHelper.ToJson(item.Annotations);
                        }
                        else
                        {
                            CommentDTO comment = new CommentDTO();
                            comment.ID = FormulaHelper.CreateGuid();
                            comment.UserID = Convert.ToString(data["UserID"]);
                            comment.PID = Convert.ToString(data["PID"]);
                            comment.Content = Convert.ToString(data["Content"]);
                            comment.Time = DateTime.Now;
                            item.Comments.Add(comment);
                            str = JsonHelper.ToJson(item.Comments);
                        }
                    }
                }
            }

            ModifyFile(text, GetFullPath(fileID));
            return str;
        }


        public static string DelAnnotationAndComment(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> data = (Dictionary<string, object>)serializer.DeserializeObject(jsonString);

            string fileID = Convert.ToString(data["DocumentID"]);
            string fileText = GetFileByID(fileID);
            string str = "";
            ImageDTO text = JsonHelper.ToObject<ImageDTO>(fileText);
            if (text != null)
            {
                string versionID = Convert.ToString(data["VersionID"]);
                string deleteID = Convert.ToString(data["DeleteID"]);
                foreach (var item in text.Versions)
                {
                    if (item.ID == versionID)
                    {
                        var annotation = item.Annotations.FirstOrDefault(c => c.ID == deleteID);
                        var comment = item.Comments.FirstOrDefault(c => c.ID == deleteID);
                        if (annotation != null)
                        {
                            item.Annotations.Remove(annotation);
                            str = JsonHelper.ToJson(item.Annotations);
                        }
                        if (comment != null)
                        {
                            item.Comments.Remove(comment);
                            str = JsonHelper.ToJson(item.Comments);
                        }
                    }
                }
            }

            ModifyFile(text, GetFullPath(fileID));
            return str;
        }

    }
}
