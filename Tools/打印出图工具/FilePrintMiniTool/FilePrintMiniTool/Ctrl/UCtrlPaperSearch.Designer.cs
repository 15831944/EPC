namespace FilePrintMiniTool.Ctrl
{
    partial class UCtrlPaperSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvContent = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrm = new System.Windows.Forms.DateTimePicker();
            this.cBoxMajor = new System.Windows.Forms.ComboBox();
            this.cboxStage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtChargeDepartName = new System.Windows.Forms.TextBox();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.lblProjName = new System.Windows.Forms.Label();
            this.txtProjectManager = new System.Windows.Forms.TextBox();
            this.txtProjectCode = new System.Windows.Forms.TextBox();
            this.txtProjectInfoName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.uCtrlGridViewPager1 = new FilePrintMiniTool.Ctrl.UCtrlGridViewPager();
            this.checkCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjectInfoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjectCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjectManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMajorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStepName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubmitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCTNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaperSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsVerticalDescrib = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsVertical = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPdfFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlotFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvContent
            // 
            this.dgvContent.AllowUserToAddRows = false;
            this.dgvContent.AllowUserToDeleteRows = false;
            this.dgvContent.AllowUserToResizeColumns = false;
            this.dgvContent.AllowUserToResizeRows = false;
            this.dgvContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvContent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvContent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkCol,
            this.colProductCode,
            this.colProductName,
            this.colProjectInfoName,
            this.colProjectCode,
            this.colProjectManager,
            this.colProvider,
            this.colMajorCode,
            this.colStepName,
            this.colSubmitDate,
            this.colCTNum,
            this.colPaperSize,
            this.colCount,
            this.colIsVerticalDescrib,
            this.colID,
            this.colIsVertical,
            this.colPdfFile,
            this.colPlotFile});
            this.dgvContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContent.Location = new System.Drawing.Point(0, 89);
            this.dgvContent.Margin = new System.Windows.Forms.Padding(2);
            this.dgvContent.Name = "dgvContent";
            this.dgvContent.ReadOnly = true;
            this.dgvContent.RowHeadersVisible = false;
            this.dgvContent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dgvContent.RowTemplate.Height = 30;
            this.dgvContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContent.Size = new System.Drawing.Size(1285, 296);
            this.dgvContent.TabIndex = 20;
            this.dgvContent.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContent_CellMouseClick);
            this.dgvContent.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContent_ColumnHeaderMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uCtrlGridViewPager1);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 385);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1285, 32);
            this.panel1.TabIndex = 22;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(14, 11);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(41, 12);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "数量:0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.dtpFrm);
            this.groupBox1.Controls.Add(this.cBoxMajor);
            this.groupBox1.Controls.Add(this.cboxStage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtChargeDepartName);
            this.groupBox1.Controls.Add(this.txtProvider);
            this.groupBox1.Controls.Add(this.lblProjName);
            this.groupBox1.Controls.Add(this.txtProjectManager);
            this.groupBox1.Controls.Add(this.txtProjectCode);
            this.groupBox1.Controls.Add(this.txtProjectInfoName);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(1188, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1285, 89);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选";
            // 
            // dtpTo
            // 
            this.dtpTo.Checked = false;
            this.dtpTo.Location = new System.Drawing.Point(830, 52);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowCheckBox = true;
            this.dtpTo.Size = new System.Drawing.Size(122, 21);
            this.dtpTo.TabIndex = 22;
            // 
            // dtpFrm
            // 
            this.dtpFrm.Checked = false;
            this.dtpFrm.Location = new System.Drawing.Point(830, 20);
            this.dtpFrm.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFrm.Name = "dtpFrm";
            this.dtpFrm.ShowCheckBox = true;
            this.dtpFrm.Size = new System.Drawing.Size(122, 21);
            this.dtpFrm.TabIndex = 20;
            // 
            // cBoxMajor
            // 
            this.cBoxMajor.BackColor = System.Drawing.SystemColors.Window;
            this.cBoxMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxMajor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cBoxMajor.FormattingEnabled = true;
            this.cBoxMajor.Location = new System.Drawing.Point(609, 20);
            this.cBoxMajor.Name = "cBoxMajor";
            this.cBoxMajor.Size = new System.Drawing.Size(121, 20);
            this.cBoxMajor.TabIndex = 3;
            // 
            // cboxStage
            // 
            this.cboxStage.BackColor = System.Drawing.SystemColors.Window;
            this.cboxStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboxStage.FormattingEnabled = true;
            this.cboxStage.Location = new System.Drawing.Point(609, 52);
            this.cboxStage.Name = "cboxStage";
            this.cboxStage.Size = new System.Drawing.Size(121, 20);
            this.cboxStage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(571, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "阶段:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(571, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "专业:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "项目部门:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(381, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "图纸提供人:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "项目编号:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(744, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "提交终止日期:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(744, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "提交起始日期:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "项目负责人:";
            // 
            // txtChargeDepartName
            // 
            this.txtChargeDepartName.BackColor = System.Drawing.SystemColors.Window;
            this.txtChargeDepartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChargeDepartName.Location = new System.Drawing.Point(265, 52);
            this.txtChargeDepartName.Name = "txtChargeDepartName";
            this.txtChargeDepartName.Size = new System.Drawing.Size(100, 21);
            this.txtChargeDepartName.TabIndex = 1;
            // 
            // txtProvider
            // 
            this.txtProvider.BackColor = System.Drawing.SystemColors.Window;
            this.txtProvider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProvider.Location = new System.Drawing.Point(455, 52);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(100, 21);
            this.txtProvider.TabIndex = 1;
            // 
            // lblProjName
            // 
            this.lblProjName.AutoSize = true;
            this.lblProjName.Location = new System.Drawing.Point(27, 24);
            this.lblProjName.Name = "lblProjName";
            this.lblProjName.Size = new System.Drawing.Size(59, 12);
            this.lblProjName.TabIndex = 2;
            this.lblProjName.Text = "项目名称:";
            // 
            // txtProjectManager
            // 
            this.txtProjectManager.BackColor = System.Drawing.SystemColors.Window;
            this.txtProjectManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProjectManager.Location = new System.Drawing.Point(455, 20);
            this.txtProjectManager.Name = "txtProjectManager";
            this.txtProjectManager.Size = new System.Drawing.Size(101, 21);
            this.txtProjectManager.TabIndex = 1;
            // 
            // txtProjectCode
            // 
            this.txtProjectCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtProjectCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProjectCode.Location = new System.Drawing.Point(89, 52);
            this.txtProjectCode.Name = "txtProjectCode";
            this.txtProjectCode.Size = new System.Drawing.Size(108, 21);
            this.txtProjectCode.TabIndex = 1;
            // 
            // txtProjectInfoName
            // 
            this.txtProjectInfoName.BackColor = System.Drawing.SystemColors.Window;
            this.txtProjectInfoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProjectInfoName.Location = new System.Drawing.Point(89, 20);
            this.txtProjectInfoName.Name = "txtProjectInfoName";
            this.txtProjectInfoName.Size = new System.Drawing.Size(276, 21);
            this.txtProjectInfoName.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1208, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "下载(&D)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(1138, 21);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 49);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "打印(&S)";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(1069, 21);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 49);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清空(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(1000, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 49);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // uCtrlGridViewPager1
            // 
            this.uCtrlGridViewPager1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uCtrlGridViewPager1.Location = new System.Drawing.Point(1027, 5);
            this.uCtrlGridViewPager1.Name = "uCtrlGridViewPager1";
            this.uCtrlGridViewPager1.Size = new System.Drawing.Size(250, 22);
            this.uCtrlGridViewPager1.TabIndex = 3;
            this.uCtrlGridViewPager1.TotalCount = 0;
            // 
            // checkCol
            // 
            this.checkCol.Frozen = true;
            this.checkCol.HeaderText = "全选";
            this.checkCol.MinimumWidth = 40;
            this.checkCol.Name = "checkCol";
            this.checkCol.ReadOnly = true;
            this.checkCol.Width = 40;
            // 
            // colProductCode
            // 
            this.colProductCode.DataPropertyName = "ProductCode";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProductCode.Frozen = true;
            this.colProductCode.HeaderText = "图号";
            this.colProductCode.MinimumWidth = 120;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProductCode.Width = 120;
            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProductName.Frozen = true;
            this.colProductName.HeaderText = "图名";
            this.colProductName.MinimumWidth = 180;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProductName.Width = 180;
            // 
            // colProjectInfoName
            // 
            this.colProjectInfoName.DataPropertyName = "ProjectInfoName";
            this.colProjectInfoName.HeaderText = "项目名称";
            this.colProjectInfoName.MinimumWidth = 220;
            this.colProjectInfoName.Name = "colProjectInfoName";
            this.colProjectInfoName.ReadOnly = true;
            this.colProjectInfoName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProjectInfoName.Width = 220;
            // 
            // colProjectCode
            // 
            this.colProjectCode.DataPropertyName = "ProjectCode";
            this.colProjectCode.HeaderText = "项目编号";
            this.colProjectCode.MinimumWidth = 120;
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.ReadOnly = true;
            this.colProjectCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProjectCode.Width = 120;
            // 
            // colProjectManager
            // 
            this.colProjectManager.DataPropertyName = "ProjectManagerName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colProjectManager.DefaultCellStyle = dataGridViewCellStyle4;
            this.colProjectManager.HeaderText = "项目负责人";
            this.colProjectManager.MinimumWidth = 80;
            this.colProjectManager.Name = "colProjectManager";
            this.colProjectManager.ReadOnly = true;
            this.colProjectManager.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProjectManager.Width = 90;
            // 
            // colProvider
            // 
            this.colProvider.DataPropertyName = "DesingerName";
            this.colProvider.HeaderText = "图纸提供人";
            this.colProvider.MinimumWidth = 80;
            this.colProvider.Name = "colProvider";
            this.colProvider.ReadOnly = true;
            this.colProvider.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colProvider.Width = 90;
            // 
            // colMajorCode
            // 
            this.colMajorCode.DataPropertyName = "MajorName";
            this.colMajorCode.HeaderText = "专业";
            this.colMajorCode.MinimumWidth = 60;
            this.colMajorCode.Name = "colMajorCode";
            this.colMajorCode.ReadOnly = true;
            this.colMajorCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMajorCode.Width = 60;
            // 
            // colStepName
            // 
            this.colStepName.DataPropertyName = "StepName";
            this.colStepName.HeaderText = "阶段";
            this.colStepName.MinimumWidth = 80;
            this.colStepName.Name = "colStepName";
            this.colStepName.ReadOnly = true;
            this.colStepName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colStepName.Width = 80;
            // 
            // colSubmitDate
            // 
            this.colSubmitDate.DataPropertyName = "SubmitDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSubmitDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSubmitDate.HeaderText = "提交日期";
            this.colSubmitDate.MinimumWidth = 100;
            this.colSubmitDate.Name = "colSubmitDate";
            this.colSubmitDate.ReadOnly = true;
            this.colSubmitDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colCTNum
            // 
            this.colCTNum.DataPropertyName = "CTNum";
            this.colCTNum.HeaderText = "出图单编号";
            this.colCTNum.MinimumWidth = 100;
            this.colCTNum.Name = "colCTNum";
            this.colCTNum.ReadOnly = true;
            this.colCTNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colPaperSize
            // 
            this.colPaperSize.DataPropertyName = "PaperSize";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPaperSize.DefaultCellStyle = dataGridViewCellStyle6;
            this.colPaperSize.HeaderText = "图纸规格";
            this.colPaperSize.Name = "colPaperSize";
            this.colPaperSize.ReadOnly = true;
            this.colPaperSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colPaperSize.Width = 78;
            // 
            // colCount
            // 
            this.colCount.DataPropertyName = "Count";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colCount.DefaultCellStyle = dataGridViewCellStyle7;
            this.colCount.HeaderText = "累计出图份数";
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colCount.Width = 102;
            // 
            // colIsVerticalDescrib
            // 
            this.colIsVerticalDescrib.DataPropertyName = "IsVerticalDescrib";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colIsVerticalDescrib.DefaultCellStyle = dataGridViewCellStyle8;
            this.colIsVerticalDescrib.HeaderText = "纵向/横向";
            this.colIsVerticalDescrib.Name = "colIsVerticalDescrib";
            this.colIsVerticalDescrib.ReadOnly = true;
            this.colIsVerticalDescrib.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colIsVerticalDescrib.Width = 84;
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
            // colIsVertical
            // 
            this.colIsVertical.DataPropertyName = "IsVertical";
            this.colIsVertical.HeaderText = "IsVertical";
            this.colIsVertical.Name = "colIsVertical";
            this.colIsVertical.ReadOnly = true;
            this.colIsVertical.Visible = false;
            this.colIsVertical.Width = 90;
            // 
            // colPdfFile
            // 
            this.colPdfFile.DataPropertyName = "PdfFile";
            this.colPdfFile.HeaderText = "PdfFile";
            this.colPdfFile.Name = "colPdfFile";
            this.colPdfFile.ReadOnly = true;
            this.colPdfFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPdfFile.Visible = false;
            this.colPdfFile.Width = 53;
            // 
            // colPlotFile
            // 
            this.colPlotFile.DataPropertyName = "PlotFile";
            this.colPlotFile.HeaderText = "PlotFile";
            this.colPlotFile.Name = "colPlotFile";
            this.colPlotFile.ReadOnly = true;
            this.colPlotFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPlotFile.Visible = false;
            this.colPlotFile.Width = 59;
            // 
            // UCtrlPaperSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvContent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "UCtrlPaperSearch";
            this.Size = new System.Drawing.Size(1285, 417);
            this.Load += new System.EventHandler(this.UCtrlPaperSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBoxMajor;
        private System.Windows.Forms.ComboBox cboxStage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProjName;
        private System.Windows.Forms.TextBox txtProjectManager;
        private System.Windows.Forms.TextBox txtProjectCode;
        private System.Windows.Forms.TextBox txtProjectInfoName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private UCtrlGridViewPager uCtrlGridViewPager1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtChargeDepartName;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjectInfoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjectCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjectManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMajorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStepName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubmitDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCTNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaperSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsVerticalDescrib;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsVertical;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPdfFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlotFile;
    }
}
