namespace DataInitTool
{
    partial class frmConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.panTop = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbDefaultUserName = new System.Windows.Forms.TextBox();
            this.tbDefautUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDefautUser = new System.Windows.Forms.Label();
            this.panDock = new System.Windows.Forms.Panel();
            this.dgvConfig = new System.Windows.Forms.DataGridView();
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnEdit = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.SheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Required = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectorFields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panTop.SuspendLayout();
            this.panDock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).BeginInit();
            this.tsToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnSave);
            this.panTop.Controls.Add(this.tbDefaultUserName);
            this.panTop.Controls.Add(this.tbDefautUserID);
            this.panTop.Controls.Add(this.label2);
            this.panTop.Controls.Add(this.lbDefautUser);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(842, 111);
            this.panTop.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(693, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存设置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbDefaultUserName
            // 
            this.tbDefaultUserName.Location = new System.Drawing.Point(130, 54);
            this.tbDefaultUserName.Name = "tbDefaultUserName";
            this.tbDefaultUserName.Size = new System.Drawing.Size(195, 27);
            this.tbDefaultUserName.TabIndex = 3;
            // 
            // tbDefautUserID
            // 
            this.tbDefautUserID.Location = new System.Drawing.Point(130, 18);
            this.tbDefautUserID.Name = "tbDefautUserID";
            this.tbDefautUserID.Size = new System.Drawing.Size(195, 27);
            this.tbDefautUserID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "默认用户名";
            // 
            // lbDefautUser
            // 
            this.lbDefautUser.AutoSize = true;
            this.lbDefautUser.Location = new System.Drawing.Point(26, 24);
            this.lbDefautUser.Name = "lbDefautUser";
            this.lbDefautUser.Size = new System.Drawing.Size(84, 20);
            this.lbDefautUser.TabIndex = 0;
            this.lbDefautUser.Text = "默认用户ID";
            // 
            // panDock
            // 
            this.panDock.Controls.Add(this.dgvConfig);
            this.panDock.Controls.Add(this.tsToolBar);
            this.panDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panDock.Location = new System.Drawing.Point(0, 111);
            this.panDock.Name = "panDock";
            this.panDock.Size = new System.Drawing.Size(842, 456);
            this.panDock.TabIndex = 1;
            // 
            // dgvConfig
            // 
            this.dgvConfig.AllowUserToAddRows = false;
            this.dgvConfig.AllowUserToDeleteRows = false;
            this.dgvConfig.AllowUserToResizeColumns = false;
            this.dgvConfig.AllowUserToResizeRows = false;
            this.dgvConfig.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SheetName,
            this.TableName,
            this.DB,
            this.Required,
            this.Unique,
            this.ID,
            this.SelectorFields});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConfig.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConfig.Location = new System.Drawing.Point(0, 27);
            this.dgvConfig.Name = "dgvConfig";
            this.dgvConfig.RowHeadersVisible = false;
            this.dgvConfig.RowTemplate.Height = 27;
            this.dgvConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConfig.Size = new System.Drawing.Size(842, 429);
            this.dgvConfig.TabIndex = 1;
            // 
            // tsToolBar
            // 
            this.tsToolBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAdd,
            this.tsBtnEdit,
            this.tsBtnDelete});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(842, 27);
            this.tsToolBar.TabIndex = 0;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAdd.Image")));
            this.tsBtnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Size = new System.Drawing.Size(59, 24);
            this.tsBtnAdd.Text = "新增";
            this.tsBtnAdd.Click += new System.EventHandler(this.tsBtnAdd_Click);
            // 
            // tsBtnEdit
            // 
            this.tsBtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnEdit.Image")));
            this.tsBtnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnEdit.Name = "tsBtnEdit";
            this.tsBtnEdit.Size = new System.Drawing.Size(59, 24);
            this.tsBtnEdit.Text = "编辑";
            this.tsBtnEdit.Click += new System.EventHandler(this.tsBtnEdit_Click);
            // 
            // tsBtnDelete
            // 
            this.tsBtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnDelete.Image")));
            this.tsBtnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDelete.Name = "tsBtnDelete";
            this.tsBtnDelete.Size = new System.Drawing.Size(59, 24);
            this.tsBtnDelete.Text = "删除";
            this.tsBtnDelete.Click += new System.EventHandler(this.tsBtnDelete_Click);
            // 
            // SheetName
            // 
            this.SheetName.Frozen = true;
            this.SheetName.HeaderText = "工作簿名称";
            this.SheetName.Name = "SheetName";
            this.SheetName.ReadOnly = true;
            this.SheetName.Width = 150;
            // 
            // TableName
            // 
            this.TableName.Frozen = true;
            this.TableName.HeaderText = "数据表名称";
            this.TableName.Name = "TableName";
            this.TableName.ReadOnly = true;
            this.TableName.Width = 150;
            // 
            // DB
            // 
            this.DB.Frozen = true;
            this.DB.HeaderText = "数据库";
            this.DB.Name = "DB";
            this.DB.ReadOnly = true;
            // 
            // Required
            // 
            this.Required.Frozen = true;
            this.Required.HeaderText = "必填字段";
            this.Required.Name = "Required";
            this.Required.ReadOnly = true;
            this.Required.Width = 200;
            // 
            // Unique
            // 
            this.Unique.Frozen = true;
            this.Unique.HeaderText = "唯一字段";
            this.Unique.Name = "Unique";
            this.Unique.ReadOnly = true;
            this.Unique.Width = 200;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // SelectorFields
            // 
            this.SelectorFields.Frozen = true;
            this.SelectorFields.HeaderText = "选择器";
            this.SelectorFields.Name = "SelectorFields";
            this.SelectorFields.ReadOnly = true;
            this.SelectorFields.Visible = false;
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 567);
            this.Controls.Add(this.panDock);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具配置";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panDock.ResumeLayout(false);
            this.panDock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).EndInit();
            this.tsToolBar.ResumeLayout(false);
            this.tsToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.TextBox tbDefaultUserName;
        private System.Windows.Forms.TextBox tbDefautUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDefautUser;
        private System.Windows.Forms.Panel panDock;
        private System.Windows.Forms.DataGridView dgvConfig;
        private System.Windows.Forms.ToolStrip tsToolBar;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnEdit;
        private System.Windows.Forms.ToolStripButton tsBtnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Required;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unique;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectorFields;
    }
}