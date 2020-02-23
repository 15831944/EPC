using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterfaceSyn.Common;
using System.Windows.Forms;
using System.Threading;

namespace DataInterfaceSyn
{
    public class SynManager
    {
        private TargetTableStore _targetTableStore = null;
        private List<XmlTableMap> _xmlTableMap = null;
        private CancellationTokenSource _cts = null;
        public SynManager()
        {
            _targetTableStore = new TargetTableStore();
            _xmlTableMap = new List<XmlTableMap>();
        }

        public bool IsSyn { get; set; }

        public SynManager Prepare()
        {
            ReLoadTargetSourceTableMapDic();
            _targetTableStore.Prepare();
            IsSyn = false;
            return this;
        }

        public void BeginSyn()
        {
            Prepare();

            _cts = new CancellationTokenSource();
            //主表
            IEnumerable<TableModel> mainTables = _targetTableStore.GetMainTargetTables();
            foreach (var table in mainTables)
            {
                var tbMap = GetSourceTableName(table.TableName);
                if (tbMap == null) continue;

                int waitSec = 0;
                int.TryParse(tbMap.WaitSec, out waitSec);
                if (waitSec == 0)
                    LogWriter.Info(string.Format("从表【{0}】同步到表【{1}】的时间步为【{2}】", tbMap.SourceTable, tbMap.TargetTable, tbMap.WaitSec));

                Task.Run(() =>
                {
                    while (true)
                    {
                        _cts.Token.ThrowIfCancellationRequested();
                        SysnTargetTable(tbMap);
                        Thread.Sleep(waitSec * 1000);
                    }
                }, _cts.Token);
            }
            IsSyn = true;
        }

        //public void BeginSyn()
        //{
        //    Prepare();

        //    _cts = new CancellationTokenSource();
        //    //主表
        //    IEnumerable<TableModel> mainTables = _targetTableStore.GetMainTables();
        //    Task.Run(() =>
        //        {
        //            while (true)
        //            {
        //                _cts.Token.ThrowIfCancellationRequested();
        //                foreach (var table in mainTables)
        //                {
        //                    var tbMap = GetSourceTableName(table.TableName);
        //                    if (tbMap == null) continue;

        //                    int waitSec = 0;
        //                    int.TryParse(tbMap.WaitSec, out waitSec);
        //                    if (waitSec == 0)
        //                        LogWriter.Info(string.Format("从表【{0}】同步到表【{1}】的时间步为【{2}】", tbMap.SourceTable, tbMap.TargetTable, tbMap.WaitSec));

        //                    SysnTargetTable(tbMap);
        //                    //Thread.Sleep(waitSec * 1000);

        //                }
                        
        //            }
        //        }, _cts.Token);
        //    IsSyn = true;
        //}

        public void CancelSyn()
        {
            _cts.Cancel();
            IsSyn = false;
        }

        private void SysnTargetTable(XmlTableMap tbMap)
        {
            //同步数据并且获取该表的主键ID列表
            var resList = SynTableData(tbMap);
            //同步所有子表数据
            SysnSubTargetTable(tbMap.TargetTable, resList);//同步子表
        }

        /// <summary>
        /// 同步子表数据
        /// </summary>
        /// <param name="mainTargetTableName">目标主表名</param>
        /// <param name="mainTableKeyValList">目标主表的ID数组</param>
        private void SysnSubTargetTable(string mainTargetTableName, List<string> mainTableKeyValList = null)
        {
            IEnumerable<TableModel> subTableModels = _targetTableStore.GetSubTargetTables(mainTargetTableName);
            foreach (var subTable in subTableModels)
            {
                var tableMap = GetSourceTableName(subTable.TableName);
                if (tableMap == null) continue;

                SysnTargetTable(tableMap);
            }
        }

        /// <summary>
        /// 数据表数据同步
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="targetTable"></param>
        /// <returns>表主键(目标数据与源数据的id都相同)</returns>
        private List<string> SynTableData(XmlTableMap tableMap)
        {
            List<string> tableForeignKeyValList = new List<string>();
            var sourceDicList = SourceTableStore.GetTableData(tableMap.SourceTable, tableMap.SourceConnStr);
            //string sql = "";
            foreach (var sourceDic in sourceDicList)
            {
                //sql += _targetTableStore.GetExcuteSql(sourceDic, targetTableName) + " ";
                if (_targetTableStore.ExcuteSql(sourceDic, tableMap))
                    tableForeignKeyValList.Add(sourceDic.GetValue("ID"));
            }
            //SQLHelper.CreateSqlHelper(EnumConn.DataInterface).ExecuteNonQuery(sql);
            return tableForeignKeyValList;
        }

        /// <summary>
        /// 根据目标数据表名获取源数据表名
        /// </summary>
        /// <param name="targetTableName">目标数据表名</param>
        /// <returns>源数据表名</returns>
        private XmlTableMap GetSourceTableName(string targetTableName)
        {
            var tableMap = _xmlTableMap.FirstOrDefault(a => a.TargetTable == targetTableName);
            if (tableMap == null)
            {
                LogWriter.Info(targetTableName + "未找到对应可匹配的源表");
            }
            return tableMap;
        }

        public void ReLoadTargetSourceTableMapDic()
        {
            string fileName = Application.StartupPath + "\\tableMap.xml";

            if (System.IO.File.Exists(fileName))
            {
                _xmlTableMap = XmlTableMap.Create(fileName);
            }
            else
            {
                LogWriter.Info("未找到映射配置文件tableMap.xml");
            }
        }

        public List<TableModel> GetTableModel()
        {
            return _targetTableStore.TargetTableModels;
        }

        public void SaveTableModelDefalutValue()
        {
            _targetTableStore.SaveFile();
        }
    }
}
