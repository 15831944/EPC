namespace DataInitTool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.PanTopArea = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnConfigEdit = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tbExcelFilePath = new System.Windows.Forms.TextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnValidateData = new System.Windows.Forms.Button();
            this.btnDownTemplate = new System.Windows.Forms.Button();
            this.btnOpenTemplate = new System.Windows.Forms.Button();
            this.panInfoArea = new System.Windows.Forms.Panel();
            this.lbxInfo = new System.Windows.Forms.ListBox();
            this.ofdExcelFile = new System.Windows.Forms.OpenFileDialog();
            this.PanTopArea.SuspendLayout();
            this.panInfoArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanTopArea
            // 
            this.PanTopArea.Controls.Add(this.btnImport);
            this.PanTopArea.Controls.Add(this.btnConfigEdit);
            this.PanTopArea.Controls.Add(this.btnSelectFile);
            this.PanTopArea.Controls.Add(this.tbExcelFilePath);
            this.PanTopArea.Controls.Add(this.lbTitle);
            this.PanTopArea.Controls.Add(this.btnValidateData);
            this.PanTopArea.Controls.Add(this.btnDownTemplate);
            this.PanTopArea.Controls.Add(this.btnOpenTemplate);
            this.PanTopArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanTopArea.Location = new System.Drawing.Point(0, 0);
            this.PanTopArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanTopArea.Name = "PanTopArea";
            this.PanTopArea.Size = new System.Drawing.Size(619, 157);
            this.PanTopArea.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(361, 89);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 40);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "导入数据";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnConfigEdit
            // 
            this.btnConfigEdit.Location = new System.Drawing.Point(477, 89);
            this.btnConfigEdit.Name = "btnConfigEdit";
            this.btnConfigEdit.Size = new System.Drawing.Size(100, 40);
            this.btnConfigEdit.TabIndex = 7;
            this.btnConfigEdit.Text = "修改配置";
            this.btnConfigEdit.UseVisualStyleBackColor = true;
            this.btnConfigEdit.Click += new System.EventHandler(this.btnConfigEdit_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(517, 36);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(60, 30);
            this.btnSelectFile.TabIndex = 6;
            this.btnSelectFile.Text = "选择...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tbExcelFilePath
            // 
            this.tbExcelFilePath.Enabled = false;
            this.tbExcelFilePath.Location = new System.Drawing.Point(144, 38);
            this.tbExcelFilePath.Name = "tbExcelFilePath";
            this.tbExcelFilePath.Size = new System.Drawing.Size(372, 27);
            this.tbExcelFilePath.TabIndex = 5;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(39, 41);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(99, 20);
            this.lbTitle.TabIndex = 4;
            this.lbTitle.Text = "选择导入文件";
            // 
            // btnValidateData
            // 
            this.btnValidateData.Enabled = false;
            this.btnValidateData.Location = new System.Drawing.Point(255, 89);
            this.btnValidateData.Name = "btnValidateData";
            this.btnValidateData.Size = new System.Drawing.Size(100, 40);
            this.btnValidateData.TabIndex = 3;
            this.btnValidateData.Text = "校验数据";
            this.btnValidateData.UseVisualStyleBackColor = true;
            this.btnValidateData.Click += new System.EventHandler(this.btnValidateData_Click);
            // 
            // btnDownTemplate
            // 
            this.btnDownTemplate.Location = new System.Drawing.Point(149, 89);
            this.btnDownTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDownTemplate.Name = "btnDownTemplate";
            this.btnDownTemplate.Size = new System.Drawing.Size(100, 40);
            this.btnDownTemplate.TabIndex = 1;
            this.btnDownTemplate.Text = "下载模板";
            this.btnDownTemplate.UseVisualStyleBackColor = true;
            this.btnDownTemplate.Click += new System.EventHandler(this.btnDownTemplate_Click);
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenTemplate.Location = new System.Drawing.Point(43, 89);
            this.btnOpenTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(100, 40);
            this.btnOpenTemplate.TabIndex = 0;
            this.btnOpenTemplate.Text = "编辑模板";
            this.btnOpenTemplate.UseVisualStyleBackColor = true;
            this.btnOpenTemplate.Click += new System.EventHandler(this.btnOpenTemplate_Click);
            // 
            // panInfoArea
            // 
            this.panInfoArea.Controls.Add(this.lbxInfo);
            this.panInfoArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panInfoArea.Location = new System.Drawing.Point(0, 157);
            this.panInfoArea.Name = "panInfoArea";
            this.panInfoArea.Size = new System.Drawing.Size(619, 388);
            this.panInfoArea.TabIndex = 1;
            // 
            // lbxInfo
            // 
            this.lbxInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxInfo.FormattingEnabled = true;
            this.lbxInfo.ItemHeight = 20;
            this.lbxInfo.Location = new System.Drawing.Point(0, 0);
            this.lbxInfo.Name = "lbxInfo";
            this.lbxInfo.Size = new System.Drawing.Size(619, 388);
            this.lbxInfo.TabIndex = 0;
            // 
            // ofdExcelFile
            // 
            this.ofdExcelFile.Filter = "Excel文件(*.xlsx,*.xls)|*.xlsx;*.xls";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 545);
            this.Controls.Add(this.panInfoArea);
            this.Controls.Add(this.PanTopArea);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "金慧系统数据初始化工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.PanTopArea.ResumeLayout(false);
            this.PanTopArea.PerformLayout();
            this.panInfoArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanTopArea;
        private System.Windows.Forms.Button btnDownTemplate;
        private System.Windows.Forms.Button btnOpenTemplate;
        private System.Windows.Forms.Button btnValidateData;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox tbExcelFilePath;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnConfigEdit;
        private System.Windows.Forms.Panel panInfoArea;
        private System.Windows.Forms.ListBox lbxInfo;
        private System.Windows.Forms.OpenFileDialog ofdExcelFile;
    }
}