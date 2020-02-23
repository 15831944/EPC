namespace DataInterfaceSyn
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
            this.listBoxMsg = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDefaultValueSet = new System.Windows.Forms.Button();
            this.cbShowLog = new System.Windows.Forms.CheckBox();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnBegin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxMsg
            // 
            this.listBoxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMsg.FormattingEnabled = true;
            this.listBoxMsg.ItemHeight = 12;
            this.listBoxMsg.Location = new System.Drawing.Point(0, 0);
            this.listBoxMsg.Name = "listBoxMsg";
            this.listBoxMsg.Size = new System.Drawing.Size(595, 278);
            this.listBoxMsg.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDefaultValueSet);
            this.panel1.Controls.Add(this.cbShowLog);
            this.panel1.Controls.Add(this.btnLog);
            this.panel1.Controls.Add(this.btnBegin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 278);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 47);
            this.panel1.TabIndex = 6;
            // 
            // btnDefaultValueSet
            // 
            this.btnDefaultValueSet.Location = new System.Drawing.Point(12, 12);
            this.btnDefaultValueSet.Name = "btnDefaultValueSet";
            this.btnDefaultValueSet.Size = new System.Drawing.Size(75, 23);
            this.btnDefaultValueSet.TabIndex = 3;
            this.btnDefaultValueSet.Text = "默认值设置";
            this.btnDefaultValueSet.UseVisualStyleBackColor = true;
            this.btnDefaultValueSet.Click += new System.EventHandler(this.btnDefaultValueSet_Click);
            // 
            // cbShowLog
            // 
            this.cbShowLog.AutoSize = true;
            this.cbShowLog.Checked = true;
            this.cbShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowLog.Location = new System.Drawing.Point(510, 16);
            this.cbShowLog.Name = "cbShowLog";
            this.cbShowLog.Size = new System.Drawing.Size(72, 16);
            this.cbShowLog.TabIndex = 2;
            this.cbShowLog.Text = "显示日志";
            this.cbShowLog.UseVisualStyleBackColor = true;
            this.cbShowLog.CheckedChanged += new System.EventHandler(this.cbShowLog_CheckedChanged);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(421, 12);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "清空日志";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(274, 12);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 23);
            this.btnBegin.TabIndex = 0;
            this.btnBegin.Text = "开始同步";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(595, 325);
            this.Controls.Add(this.listBoxMsg);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "业财中间表同步";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.CheckBox cbShowLog;
        private System.Windows.Forms.Button btnDefaultValueSet;

    }
}

