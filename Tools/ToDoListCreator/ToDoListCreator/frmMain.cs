using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDoListCreator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            var timeTick = 1000;
            var timer = System.Configuration.ConfigurationManager.AppSettings["Timer"];
            if (!String.IsNullOrEmpty(timer))
            {
                timeTick = Convert.ToInt32(timer) * 1000;
            }
            this.timer1.Interval = timeTick;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!this.timer1.Enabled)
            {
                this.timer1.Start();
            }
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            var creator = new Creator.Logic.ToDoListCreator();
            creator.CreatorToDoList();

            this.timer1.Enabled = true;
        }
    }
}
