using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilePrintMiniTool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            uCtrlPaperSearchUnPrint.PrintedOrUnPrint = false;
            uCtrlPaperSearchPrinted.PrintedOrUnPrint = true;
        }
 
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtrl.SelectedTab == tPageUnPrint)
            {
                uCtrlPaperSearchUnPrint.ParaSearch();
            }
            else if (tabCtrl.SelectedTab == tPagePrinted)
            {
                uCtrlPaperSearchPrinted.ParaSearch();
            }
        }
    }
}
