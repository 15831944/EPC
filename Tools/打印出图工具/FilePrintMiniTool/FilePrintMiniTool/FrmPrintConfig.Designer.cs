namespace FilePrintMiniTool
{
    partial class FrmPrintConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rBtnHorizontal = new System.Windows.Forms.RadioButton();
            this.rBtnVertical = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxPaperSize = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(148, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBoxPaperSize);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印参数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rBtnHorizontal);
            this.groupBox2.Controls.Add(this.rBtnVertical);
            this.groupBox2.Location = new System.Drawing.Point(237, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(116, 42);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "纸张方向";
            // 
            // rBtnHorizontal
            // 
            this.rBtnHorizontal.AutoSize = true;
            this.rBtnHorizontal.Location = new System.Drawing.Point(62, 18);
            this.rBtnHorizontal.Name = "rBtnHorizontal";
            this.rBtnHorizontal.Size = new System.Drawing.Size(47, 16);
            this.rBtnHorizontal.TabIndex = 0;
            this.rBtnHorizontal.Text = "横向";
            this.rBtnHorizontal.UseVisualStyleBackColor = true;
            // 
            // rBtnVertical
            // 
            this.rBtnVertical.AutoSize = true;
            this.rBtnVertical.Checked = true;
            this.rBtnVertical.Location = new System.Drawing.Point(9, 18);
            this.rBtnVertical.Name = "rBtnVertical";
            this.rBtnVertical.Size = new System.Drawing.Size(47, 16);
            this.rBtnVertical.TabIndex = 0;
            this.rBtnVertical.TabStop = true;
            this.rBtnVertical.Text = "纵向";
            this.rBtnVertical.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "纸张大小(mm):";
            // 
            // cBoxPaperSize
            // 
            this.cBoxPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPaperSize.FormattingEnabled = true;
            this.cBoxPaperSize.Location = new System.Drawing.Point(99, 26);
            this.cBoxPaperSize.Name = "cBoxPaperSize";
            this.cBoxPaperSize.Size = new System.Drawing.Size(123, 20);
            this.cBoxPaperSize.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 39);
            this.panel1.TabIndex = 4;
            // 
            // FrmPrintConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(379, 109);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrintConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印参数设置";
            this.Load += new System.EventHandler(this.FrmPrintConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rBtnHorizontal;
        private System.Windows.Forms.RadioButton rBtnVertical;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxPaperSize;
        private System.Windows.Forms.Panel panel1;
    }
}