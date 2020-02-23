using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilePrintMiniTool.Ctrl
{
    /// <summary>
    /// 分页用户控件
    /// </summary>
    public partial class UCtrlGridViewPager : UserControl
    {
        /// <summary>
        /// 由分页控件发起的查询操作
        /// </summary>
        public Action ActionNewPageSearch;
        private int _totalCount;
        public int TotalCount
        {
            get 
            {
                return _totalCount;
            }
            set
            {
                _totalCount = value;
            }
        }

        public int CurPage
        {
            get;
            private set;
        }
           
        public int PageSize
        {
            get;
            private set;
        }

        public UCtrlGridViewPager()
        {
            InitializeComponent();
            CurPage = Convert.ToInt32(txtDgvPage.Text);
            PageSize = Convert.ToInt32(txtCountPerPage.Text);
        }

        public void ReSetPager()
        {
            txtCountPerPage.Text = "50";
        }

        private void lblPrePage_Click(object sender, EventArgs e)
        {
            txtDgvPage.Text = (CurPage - 1).ToString();
        }

        private void lblNextPage_Click(object sender, EventArgs e)
        {
            txtDgvPage.Text = (CurPage + 1).ToString();
        }

        private void txtCountPerPage_TextChanged(object sender, EventArgs e)
        {
            int changedVal = 0;
            if (Int32.TryParse(txtCountPerPage.Text, out changedVal))
            {
                //越界
                if (changedVal <= 0 )//|| changedVal > _totalCount)
                {
                    txtCountPerPage.Text = PageSize.ToString();
                }
                else
                {
                    PageSize = changedVal;
                    //改变每页显示数量后，当前页重置为第一页
                    txtDgvPage.Text = "1";
                    NewSearch();
                }
            }
            //格式错误
            else
            {
                txtCountPerPage.Text = PageSize.ToString();
            }
        }

        private void txtDgvPage_TextChanged(object sender, EventArgs e)
        {
            int changedVal = 0;
            if (Int32.TryParse(txtDgvPage.Text, out changedVal))
            {
                //越界
                if (changedVal <= 0 || changedVal > Math.Ceiling((double)_totalCount / PageSize))
                {
                    txtDgvPage.Text = CurPage.ToString();
                }
                else
                {
                    CurPage = changedVal;
                    NewSearch();
                }
            }
            //格式错误
            else
            {
                txtDgvPage.Text = CurPage.ToString();
            }
        }

        private void NewSearch()
        {
            if (ActionNewPageSearch != null)
            {
                ActionNewPageSearch();
            }
        }
    }
}
