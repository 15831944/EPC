using QrCodeView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.IO;

namespace QrCodeView
{
    public class ProductService
    {
        public static QrCodeDTO GetProductDetail(string id, decimal versionnumber)
        {
            var formDic = new List<DicItem>();
            var subFormDic = new List<DicItem>();
            var result = new QrCodeDTO { AuditStep = "" };

            var SqlHelper = SQLHelper.CreateSqlHelper("Project");
            var sql = "select * from S_E_ProductVersion with(nolock) where ProductID='" + id + "' and Version='" + versionnumber + "'";
            var version = SqlHelper.ExecuteDataTable(sql).AsEnumerable().FirstOrDefault();
            if (version == null)
                throw new Exception("没有找到ID为【" + id + "】，版本为【" + versionnumber.ToString() + "】的成品图纸");

            sql = "select * from S_I_ProjectInfo where ID='" + version["ProjectInfoID"].ToString() + "'";
            var project = SqlHelper.ExecuteDataTable(sql).AsEnumerable().FirstOrDefault();
            if (project == null)
                throw new Exception("没有找到指定的成品图纸");

            sql = "select * from S_W_WBS with(nolock) where ProjectInfoID='" + version["ProjectInfoID"].ToString() + "'";
            var wbsList = SqlHelper.ExecuteDataTable(sql).AsEnumerable().Where(a => version["WBSFullID"].ToString().StartsWith(a["FullID"].ToString()));

            var FormDics = new List<Dictionary<string, string>>();
            try
            {
                string strDLLPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                strDLLPath = strDLLPath.Substring(8);
                string filePath = Path.GetDirectoryName(strDLLPath) + "\\QrCodeItems.json";
                if (System.IO.File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                    {
                        byte[] mybyte = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                        var jsonString = Encoding.UTF8.GetString(mybyte);
                        var json = JsonHelper.ToObject(jsonString);
                        FormDics = JsonHelper.ToObject<List<Dictionary<string, string>>>(json["FormDic_Product"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Json配置文件出错");
            }

            result.ID = version["ID"].ToString();
            result.Title = version["Name"].ToString();
            result.SubTitle = version["Code"].ToString();
            //获取子项、专业等WBS节点
            var wbsListDic = new Dictionary<string, DataRow>();
            var wbsTypes = Enum.GetNames(typeof(QrCodeView.Models.WBSType));
            foreach (var wbsType in wbsTypes)
            {
                var dataRow = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == wbsType);
                if (dataRow != null)
                    wbsListDic.Add(wbsType, dataRow);
            }
            var subproject = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == WBSType.SubProject.ToString());
            var area = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == WBSType.Area.ToString());
            var device = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == WBSType.Device.ToString());
            var major = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == WBSType.Major.ToString());
            var work = wbsList.FirstOrDefault(a => a["WBSType"].ToString() == WBSType.Work.ToString());
            //获取成果的图框信息
            var frameDic = new Dictionary<string, string>();
            var frameDicList = new List<AttburiteColInfo>();
            var isOld = true;
            if (version.Table.Columns.Contains("FrameAllAttInfo"))
            {
                if (!string.IsNullOrEmpty(version["FrameAllAttInfo"].ToString()))
                {
                    if (version["FrameAllAttInfo"].ToString().StartsWith("{"))
                    {
                        isOld = true;
                        frameDic = JsonHelper.ToObject<Dictionary<string, string>>(version["FrameAllAttInfo"].ToString());
                    }
                    else
                    {
                        isOld = false;
                        frameDicList = JsonHelper.ToObject<List<AttburiteColInfo>>(version["FrameAllAttInfo"].ToString());
                    }
                }
            }

            foreach (var f in FormDics)
            {
                var dic = new DicItem();
                dic.FieldName = f.GetValue("FieldName");

                var valueFrom = f.GetValue("ValueFrom");
                var fieldCode = f.GetValue("FieldCode");
                var frameItems = f.GetValue("FrameItems");

                if (valueFrom.Equals(ValueFrom.FieldValue.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    dic.FieldValue = f["FieldValue"];
                }
                else if (valueFrom.Equals(ValueFrom.FrameInfo.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    dic.FieldValue = f["FrameItems"];
                    //修订算法，支持找不到设为空 2019-3-31
                    if (dic.FieldValue.IndexOf('}') >= 0)
                    {
                        string[] _keyDics1 = dic.FieldValue.Split('}');
                        foreach (var key1 in _keyDics1)
                        {
                            string[] _keyDics2 = key1.Split('{');
                            if (_keyDics2.Length > -1)
                            {
                                string key2 = _keyDics2.Last();
                                if (isOld)
                                    dic.FieldValue = dic.FieldValue.Replace("{" + key2 + "}", frameDic.GetValue(key2));
                                else
                                {
                                    var frameValue = frameDicList.FirstOrDefault(a => a.Name == key2);
                                    dic.FieldValue = dic.FieldValue.Replace("{" + key2 + "}", frameValue == null ? "" : frameValue.Value);
                                }
                            }
                        }
                    }
                }
                else if (valueFrom.IndexOf('.') >= 0)
                {
                    var adapters = valueFrom.Split('.');
                    if (adapters.Length > 0)
                    {
                        var tableName = adapters[0];
                        var columnName = adapters.Last();
                        if (tableName.Equals(TableName.S_I_ProjectInfo.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (project.Table.Columns.Contains(columnName))
                                dic.FieldValue = project[columnName].ToString();
                        }
                        else if (tableName.Equals(TableName.S_E_ProductVersion.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (version.Table.Columns.Contains(columnName))
                                dic.FieldValue = version[columnName].ToString();
                        }
                        else if (adapters.Length > 1)
                        {
                            var type = adapters[1];
                            var wbs = wbsListDic.GetValue(type);
                            if (wbs != null && wbs.Table.Columns.Contains(columnName))
                                dic.FieldValue = wbs[columnName].ToString();
                        }

                    }
                }
                else
                {
                    switch (f["FieldCode"])
                    {
                        case "Customer":
                            if (project.Table.Columns.Contains("CustomerName"))
                                dic.FieldValue = project["CustomerName"].ToString();
                            break;
                        case "Name":
                            if (version.Table.Columns.Contains("Name"))
                                dic.FieldValue = version["Name"].ToString();
                            break;
                        case "Code":
                            if (version.Table.Columns.Contains("Code"))
                                dic.FieldValue = version["Code"].ToString();
                            break;
                        case "ProjectInfoName":
                            if (project.Table.Columns.Contains("Name"))
                                dic.FieldValue = project["Name"].ToString();
                            break;
                        case "ProjectInfoCode":
                            if (project.Table.Columns.Contains("Code"))
                                dic.FieldValue = project["Code"].ToString();
                            break;
                        case "SubProjectName":
                            if (subproject != null && subproject.Table.Columns.Contains("Name"))
                                dic.FieldValue = subproject["Name"].ToString();
                            break;
                        case "AreaName":
                            if (area != null && area.Table.Columns.Contains("Name"))
                                dic.FieldValue = area["Name"].ToString();
                            break;
                        case "DeviceName":
                            if (device != null && device.Table.Columns.Contains("Name"))
                                dic.FieldValue = device["Name"].ToString();
                            break;
                        case "MajorName":
                            if (major != null && major.Table.Columns.Contains("Name"))
                                dic.FieldValue = major["Name"].ToString();
                            break;
                        case "WorkName":
                            if (work != null && work.Table.Columns.Contains("Name"))
                                dic.FieldValue = work["Name"].ToString();
                            break;
                        case "ProjectChargerName":
                            if (project.Table.Columns.Contains("ChargeUserName"))
                                dic.FieldValue = project["ChargeUserName"].ToString();
                            break;
                        case "MajorChargerName":
                            if (major != null && major.Table.Columns.Contains("ChargeUserName"))
                                dic.FieldValue = major["ChargeUserName"].ToString();
                            break;
                        case "ApproverName":
                            if (version.Table.Columns.Contains("ApproverName"))
                                dic.FieldValue = version["ApproverName"].ToString();
                            break;
                        case "AuditorName":
                            if (version.Table.Columns.Contains("AuditorName"))
                                dic.FieldValue = version["AuditorName"].ToString();
                            break;
                        case "CollactorName":
                            if (version.Table.Columns.Contains("CollactorName"))
                                dic.FieldValue = version["CollactorName"].ToString();
                            break;
                        case "DesignerName":
                            if (version.Table.Columns.Contains("DesignerName"))
                                dic.FieldValue = version["DesignerName"].ToString();
                            break;
                        default:
                            break;
                    }
                }

                dic.IconCls = f.GetValue("IconCls");
                formDic.Add(dic);
            }
            result.FormDic = formDic;

            var subdic1 = new DicItem { FieldName = "Version", FieldValue = "版本:" + versionnumber };
            subFormDic.Add(subdic1);
            result.SubFormDic = subFormDic;
            return result;
        }
    }
}