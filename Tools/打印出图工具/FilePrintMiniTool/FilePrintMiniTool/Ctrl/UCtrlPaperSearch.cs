using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using FilePrintMiniTool.CS;
using System.Collections;

namespace FilePrintMiniTool.Ctrl
{
    public partial class UCtrlPaperSearch : UserControl
    {
        /// <summary>
        /// true—已打印 false—未打印
        /// </summary>
        private bool _printedOrUnPrint;

        public bool PrintedOrUnPrint
        {
            get { return _printedOrUnPrint; }
            set
            {
                _printedOrUnPrint = value;
                btnPrint.Text = value ? "重打(&P)" : "打印(&P)";
            }
        }

        private FrmPrint _frmPrint;
        private FrmPrintConfig _frmPrintConfig;
        private FrmMultiDownLoad _frmMultiDownLoad;

        public UCtrlPaperSearch()
        {
            InitializeComponent();
            _frmPrint = new FrmPrint();
            _frmPrintConfig = new FrmPrintConfig();
            _frmMultiDownLoad = new FrmMultiDownLoad();
            _frmMultiDownLoad.AfterDownLoadSuccess += Print;
            _frmPrint.AfterSendToPrinter += Print;
            uCtrlGridViewPager1.ActionNewPageSearch = PagerSearch;
            dgvContent.AutoGenerateColumns = false;
            cboxStage.Items.Add("全部");
            cBoxMajor.Items.Add("全部");
            ClearCtrl();
        }

        private void UCtrlPaperSearch_Load(object sender, EventArgs e)
        {
            ParaSearch();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ParaSearch();
        }

        /// <summary>
        /// 分页控件发起的查询
        /// </summary>
        public void PagerSearch()
        {
            Search();
        }

        /// <summary>
        /// 查询界面发起的查询
        /// </summary>
        public void ParaSearch()
        {
            //将分页控件的当前页、每页显示条数重置为初值
            uCtrlGridViewPager1.ReSetPager();
            Search();
        }

        /// <summary>
        /// 调用api查询
        /// </summary>
        private void Search()
        {
            #region 参数准备
            Dictionary<string, string> searchDic = new Dictionary<string, string>();
            FillSearchPara(ref searchDic);
            //SearchPara para = GetSearchPara();
            #endregion

            #region 查询执行
            EnableBtnAndPageCtrl(false);
            Helper.CallApi(EnumApi.GetPublishDetail, (senderobj, es) =>
            {
                if (es.Error != null)
                {
                    EnableBtnAndPageCtrl(true);
                    throw es.Error;
                }
                else if (es.Result != null)
                {                     
                    Encoding enc = Encoding.GetEncoding("UTF-8");
                    var jsonReult = JsonConvert.DeserializeObject<JsonAjaxResult>(enc.GetString(es.Result));
                    if (jsonReult.Success)
                    {
                        var definition = new { PageList = new SortableBindingList<S_E_PublishInfoDetail>(), Total = 1 };
                        var result = JsonConvert.DeserializeAnonymousType(jsonReult.Data.ToString(), definition);
                        uCtrlGridViewPager1.TotalCount = result.Total;
                        lblTotal.Text = "数量:" + uCtrlGridViewPager1.TotalCount;
                        dgvContent.DataSource = result.PageList.OrderByDescending(a => a.ID).ToList();
                        dgvContent.ClearSelection();
                    }
                    else
                    {
                        MessageBox.Show(jsonReult.Message);
                        Helper.WriteLog(jsonReult.Message);
                    }

                    EnableBtnAndPageCtrl(true);
                }
                else
                {
                    Helper.WriteLog(es.Error.Message);
                    throw new Exception("无法获取列表数据", es.Error);
                }
            }, searchDic);
            #endregion
        }

        /// <summary>
        /// 准备查询参数
        /// </summary>
        /// <param name="searchDic"></param>
        private void FillSearchPara(ref Dictionary<string, string> searchDic)
        {
            if (searchDic == null)
            {
                searchDic = new Dictionary<string, string>();
            }

            if (PrintedOrUnPrint)
            {
                searchDic.Add("Printed", "true");
            }
            else
            {
                searchDic.Add("Printed", "false");
            }
            searchDic.Add("ProjectInfoName", txtProjectInfoName.Text.Trim());
            searchDic.Add("ProjectCode", txtProjectCode.Text.Trim());
            searchDic.Add("ProjectManager", txtProjectManager.Text.Trim());

            searchDic.Add("ChargeDeptName", txtChargeDepartName.Text.Trim());
            searchDic.Add("DesignerName", txtProvider.Text.Trim());

            if (dtpFrm.Checked)
            {
                searchDic.Add("SubmitDateFrm", new DateTime(dtpFrm.Value.Year, dtpFrm.Value.Month, dtpFrm.Value.Day).ToString());
            }

            if (dtpTo.Checked)
            {
                searchDic.Add("SubmitDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day).ToString());
            }

            //分页
            searchDic.Add("CurPage", uCtrlGridViewPager1.CurPage.ToString());
            searchDic.Add("PageSize", uCtrlGridViewPager1.PageSize.ToString());
        }

        /// <summary>
        /// 准备查询参数
        /// </summary>
        /// <param name="searchDic"></param>
        private SearchPara GetSearchPara()
        {
            SearchPara para = new SearchPara();
            para.Printed = PrintedOrUnPrint;
            para.ProjectInfoName = txtProjectInfoName.Text.Trim();
            para.ProjectCode = txtProjectCode.Text.Trim();
            para.ProjectManager = txtProjectManager.Text.Trim();
            para.ChargeDeptName = txtChargeDepartName.Text.Trim();
            para.DesignerName = txtProvider.Text.Trim();

            if (dtpFrm.Checked)
            {
                para.SubmitDateFrm = dtpFrm.Value;
            }

            if (dtpTo.Checked)
            {
                para.SubmitDateTo = dtpTo.Value;
            }

            para.CurPage = uCtrlGridViewPager1.CurPage;
            para.PageSize = uCtrlGridViewPager1.PageSize;
            return para;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowSelect = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvContent.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    bool bChecked = Convert.ToBoolean(row.Cells[0].Value);
                    if (bChecked)
                    {
                        rowSelect.Add(row);
                    }
                }
            }

            if (rowSelect.Count == 0)
            {
                MessageBox.Show("请选择要打印的行");
                return;
            }

            #region 打印部分发送打印界面
            List<FrmPrint.MutltiPrintData> dataList = new List<FrmPrint.MutltiPrintData>();
            foreach (DataGridViewRow row in rowSelect)
            {
                FrmPrint.MutltiPrintData data = new FrmPrint.MutltiPrintData();
                data.ID = row.Cells["colID"].Value.ToSafeString();
                data.ProductName = row.Cells["colProductName"].Value.ToSafeString();
                data.Count = 1;
                //TODO
                //data.FileName = 
                data.IsPrinted = false;
                data.PaperSize = row.Cells["colPaperSize"].Value.ToSafeString();
                data.PdfFile = row.Cells["colPdfFile"].Value.ToSafeString();
                data.PlotFile = row.Cells["colPlotFile"].Value.ToSafeString();
                data.IsVertical = Convert.ToBoolean(row.Cells["colIsVertical"].Value);
                dataList.Add(data);
            }
            FrmPrint frmConfig = _frmPrint;
            DialogResult dr = frmConfig.ShowDialog(dataList);
            #endregion
        }

        /// <summary>
        /// 打印成功后调用api执行更新数据库相关数据
        /// </summary>
        /// <param name="obj"></param>
        private void Print(string id, int count)
        {
            #region 数据准备
            Dictionary<string, string> detail = new Dictionary<string, string>();
            detail["ID"] = id;
            detail["Count"] = count.ToString();
            #endregion

            #region 执行更新
            Helper.CallApi(EnumApi.Print, (senderobj, es) =>
            {
                if (es.Error == null)
                {
                    Encoding enc = Encoding.GetEncoding("UTF-8");
                    var jsonReult = JsonConvert.DeserializeObject<JsonAjaxResult>(enc.GetString(es.Result));
                    if (jsonReult.Success)
                    {
                        Search();
                        //DataGridViewRow tmpRow = GetRow(obj.ID);
                        ////需要显示数量时再放开
                        //int currCount = Convert.ToInt32(tmpRow.Cells["colCount"].Value);
                        //tmpRow.Cells["colCount"].Value = obj.Count + currCount;
                    }
                    else
                    {
                        MessageBox.Show(jsonReult.Message);
                        Helper.WriteLog(jsonReult.Message);
                    }
                }
                else
                {
                    MessageBox.Show(es.Error.Message);
                    Helper.WriteLog(es.Error.Message);
                }
               
            }, detail);
            #endregion
        }
        private DataGridViewRow GetRow(string ID)
        {
            foreach (DataGridViewRow row in dgvContent.Rows)
            {
                if (ID == row.Cells["colID"].Value.ToString())
                {
                    return row;
                }
            }
            return null;
        }

        private void EnableBtnAndPageCtrl(bool enable)
        {
            btnSearch.Enabled = enable;
            uCtrlGridViewPager1.Enabled = enable;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearCtrl();
        }

        private void ClearCtrl()
        {
            txtProjectInfoName.Text = "";
            txtProjectCode.Text = "";
            txtProjectManager.Text = "";
            txtProvider.Text = "";
            txtChargeDepartName.Text = "";
            cboxStage.SelectedIndex = 0;
            cBoxMajor.SelectedIndex = 0;
            dtpFrm.Checked = false;
            dtpTo.Checked = false;
        }

        private void btnPrintConfig_Click(object sender, EventArgs e)
        {
            if (dgvContent.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要设置的行");
                return;
            }

            List<FrmPrintConfig.PrintConfigData> dataList = new List<FrmPrintConfig.PrintConfigData>();
            foreach (DataGridViewRow row in dgvContent.SelectedRows)
            {
                FrmPrintConfig.PrintConfigData data = new FrmPrintConfig.PrintConfigData();
                data.ID = row.Cells["colID"].Value.ToString();
                data.PaperSize = row.Cells["colPaperSize"].Value.ToSafeString(); ;
                data.IsVertical = Convert.ToBoolean(row.Cells["colIsVertical"].Value);
                dataList.Add(data);
            }

            DialogResult dr = _frmPrintConfig.ShowDialog(dataList);
            if (dr == DialogResult.OK)
            {                
                #region 执行更新
                Helper.CallApi(EnumApi.UpdatePrintConfig, (senderobj, es) =>
                {
                    if (es.Error == null)
                    {
                        Encoding enc = Encoding.GetEncoding("UTF-8");
                        var jsonReult = JsonConvert.DeserializeObject<JsonAjaxResult>(enc.GetString(es.Result));
                        if (jsonReult.Success)
                        {
                            foreach (FrmPrintConfig.PrintConfigData configData in _frmPrintConfig.ConfigDatas)
                            {
                                DataGridViewRow row = GetRow(configData.ID);
                                row.Cells["colPaperSize"].Value = configData.PaperSize;
                                row.Cells["colIsVertical"].Value = configData.IsVertical;
                                row.Cells["colIsVerticalDescrib"].Value = configData.IsVerticalDescrib;
                                row.Selected = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show(jsonReult.Message);
                            Helper.WriteLog(jsonReult.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(es.Error.Message);
                        Helper.WriteLog(es.Error.Message);
                    }

                }, _frmPrintConfig.ConfigDatas);
                #endregion
            }
        }

        private void dgvContent_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = this.dgvContent.Columns[e.ColumnIndex];
            if (col.SortMode == DataGridViewColumnSortMode.Programmatic)
            {
                if (this.dgvContent.Columns[e.ColumnIndex].Tag == null || this.dgvContent.Columns[e.ColumnIndex].Tag.ToString() == "0")
                {
                    this.dgvContent.Sort(col, ListSortDirection.Ascending);
                    col.Tag = "1";
                }
                else
                {
                    this.dgvContent.Sort(col, ListSortDirection.Descending);
                    col.Tag = "0";
                }
            }            
        }

        private void dgvContent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                for (int i = 0; i < this.dgvContent.RowCount; i++)
                {
                    this.dgvContent.Rows[i].Cells[0].Value = true;//如果为true则为选中,false未选中
                }
            }
            else
            {
                //checkbox 勾上
                if ((bool)dgvContent.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                {
                    this.dgvContent.Rows[e.RowIndex].Cells[0].Value = false;
                }
                else
                {
                    this.dgvContent.Rows[e.RowIndex].Cells[0].Value = true;
                }
            }
            
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowSelect = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvContent.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    bool bChecked = Convert.ToBoolean(row.Cells[0].Value);
                    if (bChecked)
                    {
                        rowSelect.Add(row);
                    }
                }
            }

            if (rowSelect.Count == 0)
            {
                MessageBox.Show("请选择要下载的行");
                return;
            }

            List<FrmMultiDownLoad.MultiDownLoadData> dataList = new List<FrmMultiDownLoad.MultiDownLoadData>();
            foreach (DataGridViewRow row in rowSelect)
            {
                FrmMultiDownLoad.MultiDownLoadData data = new FrmMultiDownLoad.MultiDownLoadData();
                data.ID = row.Cells["colID"].Value.ToSafeString();
                data.ProjectInfoName = row.Cells["colProjectInfoName"].Value.ToSafeString();
                data.ProductName = row.Cells["colProductName"].Value.ToSafeString();
                data.PdfFile = row.Cells["colPdfFile"].Value.ToSafeString();
                data.SubmitDate = row.Cells["colSubmitDate"].Value.ToSafeString();
                data.IsDownLoad = false;
                dataList.Add(data);
            }

            _frmMultiDownLoad.ShowDialog(dataList);
        }
    }
}
