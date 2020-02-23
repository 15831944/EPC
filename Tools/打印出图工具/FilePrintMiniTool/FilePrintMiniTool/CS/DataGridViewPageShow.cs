using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FilePrintMiniTool.CS
{
    public class DataGridViewPageShow
    {
        private DataTable _dt;
        private int _pageSize;//每页显示行数
        private int _currentPage;

        private int _nMax;
        private int _pageCount;
        //private int m_nCurrent;
        public DataGridViewPageShow(DataTable dt, int pageSize)
        {
            _dt = dt;

            _pageSize = pageSize;
            _currentPage = 0;
            InitDataSet();
        }

        private void InitDataSet()
        {
            _nMax = _dt.Rows.Count;
   
            if (_pageSize > _nMax)
            {
                _pageSize = _nMax;
            }
            if (_pageSize > 0)
            {
                _pageCount = (_nMax / _pageSize);
                if ((_nMax % _pageSize) > 0)
                {
                    _pageCount++;
                }
            }
            
            _currentPage = 1;
            //m_nCurrent = 0;
        }

        public void SetPageSize(int pageSize)
        {
            _pageSize = pageSize;          
            InitDataSet();
        }

        public DataTable LoadData()
        {
            int nStartPos = 0;
            int nEndPos = 0;
            DataTable dtTmp = _dt.Clone();

            if (_pageCount == _currentPage)
            {
                nEndPos = _nMax;
            }
            else
            {
                nEndPos = _pageSize * _currentPage;
            }

            //nStartPos = m_nCurrent;
            nStartPos = (_currentPage - 1) * _pageSize;
            for (int i = nStartPos; i < nEndPos; i++)
            {
                dtTmp.ImportRow(_dt.Rows[i]);
                //m_nCurrent++;
            }
            return dtTmp;
        }

        public DataTable NextPage()
        {
            if (_currentPage >= _pageCount)
            {
                return null;
            }

            _currentPage++;
            DataTable dt = LoadData();            
            return dt;
        }

        public DataTable PrePage()
        {
            if (_currentPage <= 1)
            {
                return null;
            }

            _currentPage--;            
            DataTable dt = LoadData();            
            return dt;
        }

        public DataTable GotoPage(int page)
        {
            if (page < 1 || page > _pageCount)
            {
                return null;
            }

            _currentPage = page;
            DataTable dt = LoadData();            
            return dt;
        }

        public int GetPageCount()
        {
            return _pageCount;
        }

        public int GetCurrentPage()
        {
            return _currentPage;
        }
    }
}
