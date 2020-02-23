namespace FilePrintMiniTool.Ctrl
{
    partial class UCtrlGridViewPager
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblGoto = new System.Windows.Forms.Label();
            this.lblPrePage = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblMaxPage = new System.Windows.Forms.Label();
            this.txtCountPerPage = new System.Windows.Forms.TextBox();
            this.txtDgvPage = new System.Windows.Forms.TextBox();
            this.lblNextPage = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lblGoto);
            this.panel2.Controls.Add(this.lblPrePage);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.lblMaxPage);
            this.panel2.Controls.Add(this.txtCountPerPage);
            this.panel2.Controls.Add(this.txtDgvPage);
            this.panel2.Controls.Add(this.lblNextPage);
            this.panel2.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 22);
            this.panel2.TabIndex = 33;
            // 
            // lblGoto
            // 
            this.lblGoto.AutoSize = true;
            this.lblGoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblGoto.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGoto.Location = new System.Drawing.Point(6, 5);
            this.lblGoto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGoto.Name = "lblGoto";
            this.lblGoto.Size = new System.Drawing.Size(29, 12);
            this.lblGoto.TabIndex = 12;
            this.lblGoto.Text = "当前";
            // 
            // lblPrePage
            // 
            this.lblPrePage.AutoSize = true;
            this.lblPrePage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPrePage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrePage.Location = new System.Drawing.Point(72, 5);
            this.lblPrePage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrePage.Name = "lblPrePage";
            this.lblPrePage.Size = new System.Drawing.Size(41, 12);
            this.lblPrePage.TabIndex = 12;
            this.lblPrePage.Text = "上一页";
            this.lblPrePage.Click += new System.EventHandler(this.lblPrePage_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(168, 5);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 15;
            this.label21.Text = "每页";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(229, 5);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 15;
            this.label23.Text = "行";
            // 
            // lblMaxPage
            // 
            this.lblMaxPage.AutoSize = true;
            this.lblMaxPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMaxPage.Location = new System.Drawing.Point(113, 5);
            this.lblMaxPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxPage.Name = "lblMaxPage";
            this.lblMaxPage.Size = new System.Drawing.Size(11, 12);
            this.lblMaxPage.TabIndex = 8;
            this.lblMaxPage.Text = "/";
            // 
            // txtCountPerPage
            // 
            this.txtCountPerPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCountPerPage.Location = new System.Drawing.Point(198, 1);
            this.txtCountPerPage.Margin = new System.Windows.Forms.Padding(2);
            this.txtCountPerPage.Name = "txtCountPerPage";
            this.txtCountPerPage.Size = new System.Drawing.Size(30, 21);
            this.txtCountPerPage.TabIndex = 14;
            this.txtCountPerPage.Text = "10";
            this.txtCountPerPage.TextChanged += new System.EventHandler(this.txtCountPerPage_TextChanged);
            // 
            // txtDgvPage
            // 
            this.txtDgvPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDgvPage.Location = new System.Drawing.Point(39, 1);
            this.txtDgvPage.Margin = new System.Windows.Forms.Padding(2);
            this.txtDgvPage.Name = "txtDgvPage";
            this.txtDgvPage.Size = new System.Drawing.Size(30, 21);
            this.txtDgvPage.TabIndex = 9;
            this.txtDgvPage.Text = "1";
            this.txtDgvPage.TextChanged += new System.EventHandler(this.txtDgvPage_TextChanged);
            // 
            // lblNextPage
            // 
            this.lblNextPage.AutoSize = true;
            this.lblNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNextPage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNextPage.Location = new System.Drawing.Point(124, 5);
            this.lblNextPage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNextPage.Name = "lblNextPage";
            this.lblNextPage.Size = new System.Drawing.Size(41, 12);
            this.lblNextPage.TabIndex = 13;
            this.lblNextPage.Text = "下一页";
            this.lblNextPage.Click += new System.EventHandler(this.lblNextPage_Click);
            // 
            // UCtrlGridViewPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "UCtrlGridViewPager";
            this.Size = new System.Drawing.Size(250, 22);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblGoto;
        private System.Windows.Forms.Label lblPrePage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblMaxPage;
        private System.Windows.Forms.TextBox txtCountPerPage;
        private System.Windows.Forms.TextBox txtDgvPage;
        private System.Windows.Forms.Label lblNextPage;
    }
}
