using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Common.Logic.DTO;
using Newtonsoft.Json;
using System.Collections;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Common.Logic
{
    public class OfficeHelper
    {

        static OfficeHelper()
        {
            //增加的连接判断，支持\\方式
            var path = AppConfig.GetAppSettings("CacheViewFilePath");
            if (path.StartsWith("\\"))
            {
                string localpath = AppConfig.GetAppSettings("SmbDriver") ?? "X:";
                string serverPath = path.TrimEnd('\\');
                string loginUser = AppConfig.GetAppSettings("SmbUserName");
                string loginPassword = AppConfig.GetAppSettings("SmbPassword");

                int status = NetworkConnection.Connect(serverPath, localpath, loginUser, loginPassword);
                if (status != (int)ERROR_ID.ERROR_SUCCESS)
                {
                    // 连接失败
                    Console.WriteLine(status);
                }
            }
        }

        public static string GetFolderPath(string fileID, string folder = "")
        {
            var path = AppConfig.GetAppSettings("CacheViewFilePath");
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

        /*
        public static List<ImageDTO> GetFiles( string state = "", bool isCAD = false)
        {
            var path = Path.Combine(AppConfig.GetAppSettings("CacheViewFilePath"), "Json");          
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
        */

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

        public static bool WriteJsonFile(ImageDTO imgDTO)
        {
            if (imgDTO != null)
            {
                var path = Path.Combine(GetFolderPath(imgDTO.ID, "Json"), imgDTO.ID + ".txt");
                if (File.Exists(path))
                    File.Delete(path);

                if (!File.Exists(path))
                {
                    string json = JsonHelper.ToJson(imgDTO);
                    FileStream myFs = new FileStream(path, FileMode.Create);
                    StreamWriter mySw = new StreamWriter(myFs);
                    mySw.Write(json);
                    mySw.Close();
                    myFs.Close();

                    return true;
                }
            }

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

        public static ImageDTO InitDTO(string fileName, double size, string fileID)
        {
            int num = 0;
            if (!int.TryParse(fileID, out num))
                return null;

            num = num / 1000 + 1;
            if(string.IsNullOrEmpty(fileID))
                fileID = FormulaHelper.CreateGuid();
            string versionID = FormulaHelper.CreateGuid();

            var imgDTO = new ImageDTO()
            {
                ID = fileID,
                Name = GetFileName(fileName),
                Suffix = GetFileSuffix(fileName),
                State = Common.Logic.Enum.EnumWork.New.ToString(),
                ErrorMessage = "",
                Version = versionID,
                Versions = new List<VersionDTO> { 
                    new VersionDTO{
                        ID = versionID,
                        FileID = fileID,
                        Name = "1.0",
                        FullPath = Path.Combine(GetFolderPath(fileID, "Files"), fileName),
                        Suffix = GetFileSuffix(fileName),
                        State = Common.Logic.Enum.EnumWork.New.ToString(),
                        ImagePath = "Files/"+(AppConfig.IsCad?"Dwg":"Office")+"/" + string.Format("{0}", num.ToString("D8"))+"/"+versionID + "/",
                        ImageZoomLevel = 0,
                        ModifyTime = DateTime.Now,
                        Size = HumanReadableFilesize(size),
                        Annotations = new List<AnnotationDTO>(),
                        Comments = new List<CommentDTO>()
                    }
                }
            };
            
            string json = JsonHelper.ToJson(imgDTO);
            return imgDTO;
        }

        /*
        public static string FileUpgrade(string fileName, string root, string fileID, double size)
        {
            string fullName = GetFullPath(fileID);
            string fileText = GetFileText(fullName);
            string str = "";
            ImageDTO text = JsonHelper.ToObject<ImageDTO>(fileText);
            if (text != null)
            {
                string curVersion = "1.0";
                text.State = Common.Logic.Enum.EnumWork.New.ToString();
                text.ErrorMessage = "新增文件";
                foreach (var item in text.Versions)
                {
                    if(Convert.ToDouble(item.Name) > Convert.ToDouble(curVersion))
                        curVersion = item.Name;
                }
                 text.Versions.Add(new VersionDTO{
                        ID = FormulaHelper.CreateGuid(),
                        Name = string.Format("{0}.0", Convert.ToInt32(Convert.ToDouble(curVersion)) + 1),
                        FullPath = root + "\\" + fileName,
                        Suffix = OfficeHelper.GetFileSuffix(fileName),
                        State = Common.Logic.Enum.EnumWork.New.ToString(),
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
        */
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
            string fileFullPath = Path.Combine(GetFolderPath(fileID, "Json"), fileID + ".txt");
            if (File.Exists(fileFullPath))
            {
                return fileFullPath;
            }
            else {
                var db = SqlHelper.Create(AppConfig.GetConnectionStrings("FileStore"));
                db.ExecuteNonQuery(string.Format("update FsFile set ConvertResult=null,ConvertTime=null where id='{0}'", fileID));
                return "";
            }
        }

        public static string SetAnnotationAndComment(string jsonString, bool isAnnotation, bool isCAD = false)
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
                        else {
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

            WriteJsonFile(text);
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

            WriteJsonFile(text);
            return str;
        }

        #region Define NetWare Connect Class

        public enum ERROR_ID
        {
            ERROR_SUCCESS = 0,  // Success
            ERROR_BUSY = 170,
            ERROR_MORE_DATA = 234,
            ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
            ERROR_INVALID_LEVEL = 124,
            ERROR_ACCESS_DENIED = 5,
            ERROR_INVALID_PASSWORD = 86,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_BAD_DEV_TYPE = 66,
            ERROR_NOT_ENOUGH_MEMORY = 8,
            ERROR_NETWORK_BUSY = 54,
            ERROR_BAD_NETPATH = 53,
            ERROR_NO_NETWORK = 1222,
            ERROR_INVALID_HANDLE_STATE = 1609,
            ERROR_EXTENDED_ERROR = 1208,
            ERROR_DEVICE_ALREADY_REMEMBERED = 1202,
            ERROR_NO_NET_OR_BAD_PATH = 1203,
            ERROR_SESSION_CREDENTIAL_CONFLICT = 1219
        }

        public enum RESOURCE_SCOPE
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET = 2,
            RESOURCE_REMEMBERED = 3,
            RESOURCE_RECENT = 4,
            RESOURCE_CONTEXT = 5
        }

        public enum RESOURCE_TYPE
        {
            RESOURCETYPE_ANY = 0,
            RESOURCETYPE_DISK = 1,
            RESOURCETYPE_PRINT = 2,
            RESOURCETYPE_RESERVED = 8,
        }

        public enum RESOURCE_USAGE
        {
            RESOURCEUSAGE_CONNECTABLE = 1,
            RESOURCEUSAGE_CONTAINER = 2,
            RESOURCEUSAGE_NOLOCALDEVICE = 4,
            RESOURCEUSAGE_SIBLING = 8,
            RESOURCEUSAGE_ATTACHED = 16,
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
        }

        public enum RESOURCE_DISPLAYTYPE
        {
            RESOURCEDISPLAYTYPE_GENERIC = 0,
            RESOURCEDISPLAYTYPE_DOMAIN = 1,
            RESOURCEDISPLAYTYPE_SERVER = 2,
            RESOURCEDISPLAYTYPE_SHARE = 3,
            RESOURCEDISPLAYTYPE_FILE = 4,
            RESOURCEDISPLAYTYPE_GROUP = 5,
            RESOURCEDISPLAYTYPE_NETWORK = 6,
            RESOURCEDISPLAYTYPE_ROOT = 7,
            RESOURCEDISPLAYTYPE_SHAREADMIN = 8,
            RESOURCEDISPLAYTYPE_DIRECTORY = 9,
            RESOURCEDISPLAYTYPE_TREE = 10,
            RESOURCEDISPLAYTYPE_NDSCONTAINER = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NETRESOURCE
        {
            public RESOURCE_SCOPE dwScope;
            public RESOURCE_TYPE dwType;
            public RESOURCE_DISPLAYTYPE dwDisplayType;
            public RESOURCE_USAGE dwUsage;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpLocalName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpRemoteName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpComment;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpProvider;
        }

        public class NetworkConnection
        {

            [DllImport("mpr.dll")]
            public static extern int WNetAddConnection2A(NETRESOURCE[] lpNetResource, string lpPassword, string lpUserName, int dwFlags);

            [DllImport("mpr.dll")]
            public static extern int WNetCancelConnection2A(string sharename, int dwFlags, int fForce);


            public static int Connect(string remotePath, string localPath, string username, string password)
            {
                NETRESOURCE[] share_driver = new NETRESOURCE[1];
                share_driver[0].dwScope = RESOURCE_SCOPE.RESOURCE_GLOBALNET;
                share_driver[0].dwType = RESOURCE_TYPE.RESOURCETYPE_DISK;
                share_driver[0].dwDisplayType = RESOURCE_DISPLAYTYPE.RESOURCEDISPLAYTYPE_SHARE;
                share_driver[0].dwUsage = RESOURCE_USAGE.RESOURCEUSAGE_CONNECTABLE;
                share_driver[0].lpLocalName = localPath;
                share_driver[0].lpRemoteName = remotePath;

                Disconnect(localPath);
                int ret = WNetAddConnection2A(share_driver, password, username, 1);

                return ret;
            }

            public static int Disconnect(string localpath)
            {
                return WNetCancelConnection2A(localpath, 1, 1);
            }

        }

        #endregion

    }
}
