using DataInterfaceSyn.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataInterfaceSyn
{
    public partial class FrmMain : Form
    {
        SynManager manager = new SynManager();
        private const int ITEM_PADDING = 10;//各项之间的边距
        public FrmMain()
        {
            InitializeComponent();
            listBoxMsg.DrawMode = DrawMode.OwnerDrawVariable;
            listBoxMsg.DrawItem += ListBox1_DrawItem;
            listBoxMsg.MeasureItem += ListBox1_MeasureItem;
            LogWriter.onLogger = onLogger;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            manager.Prepare();
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            if (manager.IsSyn)
            {
                manager.CancelSyn();
                btnLog.Enabled = true;
                btnDefaultValueSet.Enabled = true;
                btnBegin.Text = "开始同步";
            }
            else
            {
                manager.BeginSyn();
                btnLog.Enabled = false;
                btnDefaultValueSet.Enabled = false;
                btnBegin.Text = "停止";
            }

        }

        private void onLogger(string msg)
        {
            listBoxMsg.Invoke(new Action<string, int>((a, b) =>
            {
                if (cbShowLog.Checked)
                    listBoxMsg.Items.Insert(0, msg);
            }), msg, 1);
        }

        private void ListBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            int index = e.Index;//获取当前要进行绘制的行的序号，从0开始。

            if (index < 0)
            {
                return;
            }

            string text = Convert.ToString(listBox.Items[index]);
            //超范围后自动换行
            Size size = TextRenderer.MeasureText(index + ":" + text, listBox.Font, listBox.Size, TextFormatFlags.WordBreak);
            e.ItemWidth = size.Width;

            e.ItemHeight = size.Height + ITEM_PADDING * 2;//适当多一点高度，避免太挤
        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index;//获取当前要进行绘制的行的序号，从0开始。
            if (index < 0)
            {
                return;
            }

            ListBox listBox = sender as ListBox;
            e.DrawBackground();//画背景颜色
            e.DrawFocusRectangle();//画聚焦项的边框
            Graphics g = e.Graphics;//获取Graphics对象。
            Rectangle itemBounds = e.Bounds;//获取当前要绘制的行的一个矩形范围。

            //文字绘制的区域，留出一定间隔
            Rectangle textBounds = new Rectangle(itemBounds.X, itemBounds.Y + ITEM_PADDING, itemBounds.Width, itemBounds.Height);

            string text = Convert.ToString(listBox.Items[index]);

            //因为文本可能会非常长，因此要用自绘实现ListBox项目的自动换行

            TextRenderer.DrawText(g, index + ":" + text, e.Font, textBounds, e.ForeColor,
                              TextFormatFlags.WordBreak);

            g.DrawRectangle(Pens.Blue, itemBounds);//画每一项的边框，这样清楚分出来各项。
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            listBoxMsg.Items.Clear();
        }

        private void cbShowLog_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowLog.Checked)
            {

            }
        }

        private void btnDefaultValueSet_Click(object sender, EventArgs e)
        {
            FrmSourceTableDefaultValue frm = new FrmSourceTableDefaultValue();
            if (frm.ShowDialog(manager.GetTableModel()) == System.Windows.Forms.DialogResult.OK) 
            {
                manager.SaveTableModelDefalutValue();
            }
        }
    }
}
