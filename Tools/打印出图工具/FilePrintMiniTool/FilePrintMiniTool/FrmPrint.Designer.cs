namespace FilePrintMiniTool
{
    partial class FrmPrint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cBoxPrint = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReloadPrinter = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaperSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsVerticalDescrib = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDownLoad = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrintInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsPrinted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsVertical = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(143, 320);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(176, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cBoxPrint
            // 
            this.cBoxPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPrint.FormattingEnabled = true;
            this.cBoxPrint.Items.AddRange(new object[] {
            "HP 打印机",
            "Brother MFC"});
            this.cBoxPrint.Location = new System.Drawing.Point(65, 9);
            this.cBoxPrint.Name = "cBoxPrint";
            this.cBoxPrint.Size = new System.Drawing.Size(275, 20);
            this.cBoxPrint.TabIndex = 1;
            this.cBoxPrint.SelectedIndexChanged += new System.EventHandler(this.cBoxPrint_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "打印机:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReloadPrinter);
            this.panel2.Controls.Add(this.cBoxPrint);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(31, 269);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 40);
            this.panel2.TabIndex = 5;
            // 
            // btnReloadPrinter
            // 
            this.btnReloadPrinter.Location = new System.Drawing.Point(346, 8);
            this.btnReloadPrinter.Name = "btnReloadPrinter";
            this.btnReloadPrinter.Size = new System.Drawing.Size(22, 23);
            this.btnReloadPrinter.TabIndex = 2;
            this.btnReloadPrinter.Text = "刷";
            this.btnReloadPrinter.UseVisualStyleBackColor = true;
            this.btnReloadPrinter.Click += new System.EventHandler(this.btnReloadPrinter_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvContent);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(438, 263);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打印的文件";
            // 
            // dgvContent
            // 
            this.dgvContent.AllowUserToAddRows = false;
            this.dgvContent.AllowUserToDeleteRows = false;
            this.dgvContent.AllowUserToResizeColumns = false;
            this.dgvContent.AllowUserToResizeRows = false;
            this.dgvContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCount,
            this.colProductName,
            this.colPaperSize,
            this.colIsVerticalDescrib,
            this.colDownLoad,
            this.colPrintInfo,
            this.colID,
            this.colIsPrinted,
            this.colIsVertical,
            this.colLocalFilePath});
            this.dgvContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContent.Location = new System.Drawing.Point(3, 17);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContent.MultiSelect = false;
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.RowHeadersVisible = false;
            this.dgvContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvContent.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvContent.RowTemplate.Height = 30;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContent.Size = new System.Drawing.Size(432, 243);
            this.dgvContent.TabIndex = 21;
            this.dgvContent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContent_CellContentClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 320);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // colCount
            // 
            this.colCount.DataPropertyName = "Count";
            this.colCount.HeaderText = "打印份数";
            this.colCount.MinimumWidth = 60;
            this.colCount.Name = "colCount";
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCount.Width = 60;
            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "图名";
            this.colProductName.MinimumWidth = 103;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProductName.Width = 103;
            // 
            // colPaperSize
            // 
            this.colPaperSize.DataPropertyName = "PaperSize";
            this.colPaperSize.HeaderText = "图纸规格";
            this.colPaperSize.Name = "colPaperSize";
            this.colPaperSize.ReadOnly = true;
            this.colPaperSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPaperSize.Width = 59;
            // 
            // colIsVerticalDescrib
            // 
            this.colIsVerticalDescrib.DataPropertyName = "IsVerticalDescrib";
            this.colIsVerticalDescrib.HeaderText = "纵向/横向";
            this.colIsVerticalDescrib.Name = "colIsVerticalDescrib";
            this.colIsVerticalDescrib.ReadOnly = true;
            this.colIsVerticalDescrib.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIsVerticalDescrib.Width = 65;
            // 
            // colDownLoad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "下载";
            this.colDownLoad.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDownLoad.HeaderText = "操作";
            this.colDownLoad.MinimumWidth = 60;
            this.colDownLoad.Name = "colDownLoad";
            this.colDownLoad.Text = "下载";
            this.colDownLoad.Width = 60;
            // 
            // colPrintInfo
            // 
            this.colPrintInfo.DataPropertyName = "PrintInfo";
            this.colPrintInfo.HeaderText = "提示";
            this.colPrintInfo.MinimumWidth = 120;
            this.colPrintInfo.Name = "colPrintInfo";
            this.colPrintInfo.ReadOnly = true;
            this.colPrintInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPrintInfo.Width = 120;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colID.Visible = false;
            this.colID.Width = 23;
            // 
            // colIsPrinted
            // 
            this.colIsPrinted.DataPropertyName = "IsPrinted";
            this.colIsPrinted.HeaderText = "是否已打印";
            this.colIsPrinted.Name = "colIsPrinted";
            this.colIsPrinted.ReadOnly = true;
            this.colIsPrinted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIsPrinted.Visible = false;
            this.colIsPrinted.Width = 71;
            // 
            // colIsVertical
            // 
            this.colIsVertical.DataPropertyName = "IsVertical";
            this.colIsVertical.HeaderText = "IsVertical";
            this.colIsVertical.Name = "colIsVertical";
            this.colIsVertical.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIsVertical.Visible = false;
            this.colIsVertical.Width = 71;
            // 
            // colLocalFilePath
            // 
            this.colLocalFilePath.HeaderText = "LocalFilePath";
            this.colLocalFilePath.Name = "colLocalFilePath";
            this.colLocalFilePath.Visible = false;
            this.colLocalFilePath.Width = 108;
            // 
            // FrmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(438, 352);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印";
            this.Load += new System.EventHandler(this.FrmPrintConfig_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cBoxPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.Button btnReloadPrinter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaperSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsVerticalDescrib;
        private System.Windows.Forms.DataGridViewButtonColumn colDownLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrintInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsPrinted;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsVertical;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalFilePath;
    }
}