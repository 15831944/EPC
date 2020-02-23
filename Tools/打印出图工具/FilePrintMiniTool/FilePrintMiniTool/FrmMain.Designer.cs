namespace FilePrintMiniTool
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tPageUnPrint = new System.Windows.Forms.TabPage();
            this.uCtrlPaperSearchUnPrint = new FilePrintMiniTool.Ctrl.UCtrlPaperSearch();
            this.tPagePrinted = new System.Windows.Forms.TabPage();
            this.uCtrlPaperSearchPrinted = new FilePrintMiniTool.Ctrl.UCtrlPaperSearch();
            this.tabCtrl.SuspendLayout();
            this.tPageUnPrint.SuspendLayout();
            this.tPagePrinted.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tPageUnPrint);
            this.tabCtrl.Controls.Add(this.tPagePrinted);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(0, 0);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(1264, 662);
            this.tabCtrl.TabIndex = 0;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.tabCtrl_SelectedIndexChanged);
            // 
            // tPageUnPrint
            // 
            this.tPageUnPrint.Controls.Add(this.uCtrlPaperSearchUnPrint);
            this.tPageUnPrint.Location = new System.Drawing.Point(4, 22);
            this.tPageUnPrint.Name = "tPageUnPrint";
            this.tPageUnPrint.Padding = new System.Windows.Forms.Padding(3);
            this.tPageUnPrint.Size = new System.Drawing.Size(1256, 636);
            this.tPageUnPrint.TabIndex = 0;
            this.tPageUnPrint.Text = "未打印";
            this.tPageUnPrint.UseVisualStyleBackColor = true;
            // 
            // uCtrlPaperSearchUnPrint
            // 
            this.uCtrlPaperSearchUnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uCtrlPaperSearchUnPrint.Location = new System.Drawing.Point(3, 3);
            this.uCtrlPaperSearchUnPrint.Name = "uCtrlPaperSearchUnPrint";
            this.uCtrlPaperSearchUnPrint.PrintedOrUnPrint = false;
            this.uCtrlPaperSearchUnPrint.Size = new System.Drawing.Size(1250, 630);
            this.uCtrlPaperSearchUnPrint.TabIndex = 0;
            // 
            // tPagePrinted
            // 
            this.tPagePrinted.Controls.Add(this.uCtrlPaperSearchPrinted);
            this.tPagePrinted.Location = new System.Drawing.Point(4, 22);
            this.tPagePrinted.Name = "tPagePrinted";
            this.tPagePrinted.Padding = new System.Windows.Forms.Padding(3);
            this.tPagePrinted.Size = new System.Drawing.Size(1256, 636);
            this.tPagePrinted.TabIndex = 1;
            this.tPagePrinted.Text = "已打印";
            this.tPagePrinted.UseVisualStyleBackColor = true;
            // 
            // uCtrlPaperSearchPrinted
            // 
            this.uCtrlPaperSearchPrinted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uCtrlPaperSearchPrinted.Location = new System.Drawing.Point(3, 3);
            this.uCtrlPaperSearchPrinted.Name = "uCtrlPaperSearchPrinted";
            this.uCtrlPaperSearchPrinted.PrintedOrUnPrint = false;
            this.uCtrlPaperSearchPrinted.Size = new System.Drawing.Size(1250, 630);
            this.uCtrlPaperSearchPrinted.TabIndex = 0;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 662);
            this.Controls.Add(this.tabCtrl);
            this.MinimumSize = new System.Drawing.Size(1280, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件打印小工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabCtrl.ResumeLayout(false);
            this.tPageUnPrint.ResumeLayout(false);
            this.tPagePrinted.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tPageUnPrint;
        private Ctrl.UCtrlPaperSearch uCtrlPaperSearchUnPrint;
        private System.Windows.Forms.TabPage tPagePrinted;
        private Ctrl.UCtrlPaperSearch uCtrlPaperSearchPrinted;


    }
}

