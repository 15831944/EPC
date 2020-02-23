using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataInterfaceSyn.Common
{
    class DataManager
    {
        public static bool Save(string path, Object database)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(fs, database);
            }
            catch (Exception e)
            {
                Trace.TraceError("存储文件出现异常" + e.Message);
                throw new Exception("读取路径" + path + "文件出现异常", e);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return true;
        }

        public static Object Load(string path)
        {
            FileStream fs = null;
            Object database = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                fs.Seek(0, SeekOrigin.Begin);
                BinaryFormatter bFormatter = new BinaryFormatter();
                database = (Object)bFormatter.Deserialize(fs);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(FileNotFoundException))
                {
                    throw new Exception(path + "文件不存在", e);
                }
                string str = "读取路径" + path + "文件出现异常";
                throw new Exception(str, e);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

            return database;
        }
    }
}
