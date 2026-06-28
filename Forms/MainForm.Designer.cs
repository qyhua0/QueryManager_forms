namespace QueryManager.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReloadConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMgr = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSqlEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnQuery = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsBtnCsv = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.tvReports = new System.Windows.Forms.TreeView();
            this.lblTreeHeader = new System.Windows.Forms.Label();
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlParams = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.txtSqlPreview = new System.Windows.Forms.TextBox();
            this.lblSqlPreviewLabel = new System.Windows.Forms.Label();
            this.lblReportName = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuMgr,
            this.mnuTools,
            this.mnuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(1097, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReloadConfig,
            this.mnuOpenConfig,
            this.mnuSep1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(58, 22);
            this.mnuFile.Text = "文件(&F)";
            // 
            // mnuReloadConfig
            // 
            this.mnuReloadConfig.Name = "mnuReloadConfig";
            this.mnuReloadConfig.Size = new System.Drawing.Size(166, 22);
            this.mnuReloadConfig.Text = "重新加载配置(&R)";
            this.mnuReloadConfig.Click += new System.EventHandler(this.mnuReloadConfig_Click);
            // 
            // mnuOpenConfig
            // 
            this.mnuOpenConfig.Name = "mnuOpenConfig";
            this.mnuOpenConfig.Size = new System.Drawing.Size(166, 22);
            this.mnuOpenConfig.Text = "打开配置文件(&O)";
            this.mnuOpenConfig.Click += new System.EventHandler(this.mnuOpenConfig_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(163, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(166, 22);
            this.mnuExit.Text = "退出(&X)";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuMgr
            // 
            this.mnuMgr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnections,
            this.mnuReports});
            this.mnuMgr.Name = "mnuMgr";
            this.mnuMgr.Size = new System.Drawing.Size(64, 22);
            this.mnuMgr.Text = "管理(&M)";
            // 
            // mnuConnections
            // 
            this.mnuConnections.Name = "mnuConnections";
            this.mnuConnections.Size = new System.Drawing.Size(142, 22);
            this.mnuConnections.Text = "连接管理(&C)";
            this.mnuConnections.Click += new System.EventHandler(this.mnuConnections_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(142, 22);
            this.mnuReports.Text = "报表管理(&Q)";
            this.mnuReports.Click += new System.EventHandler(this.mnuReports_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSqlEditor});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(59, 22);
            this.mnuTools.Text = "工具(&T)";
            // 
            // mnuSqlEditor
            // 
            this.mnuSqlEditor.Name = "mnuSqlEditor";
            this.mnuSqlEditor.Size = new System.Drawing.Size(150, 22);
            this.mnuSqlEditor.Text = "SQL编辑器(&S)";
            this.mnuSqlEditor.Click += new System.EventHandler(this.mnuSqlEditor_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(61, 22);
            this.mnuHelp.Text = "帮助(&H)";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(180, 22);
            this.mnuAbout.Text = "关于(&A)";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnQuery,
            this.tsSep1,
            this.tsBtnClear,
            this.tsSep2,
            this.tsBtnExcel,
            this.tsBtnCsv});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1097, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // tsBtnQuery
            // 
            this.tsBtnQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnQuery.ForeColor = System.Drawing.Color.DarkGreen;
            this.tsBtnQuery.Name = "tsBtnQuery";
            this.tsBtnQuery.Size = new System.Drawing.Size(74, 22);
            this.tsBtnQuery.Text = "▶ 执行查询";
            this.tsBtnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(74, 22);
            this.tsBtnClear.Text = "✕ 清空条件";
            this.tsBtnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnExcel
            // 
            this.tsBtnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnExcel.Name = "tsBtnExcel";
            this.tsBtnExcel.Size = new System.Drawing.Size(85, 22);
            this.tsBtnExcel.Text = "📊 导出Excel";
            this.tsBtnExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // tsBtnCsv
            // 
            this.tsBtnCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnCsv.Name = "tsBtnCsv";
            this.tsBtnCsv.Size = new System.Drawing.Size(77, 22);
            this.tsBtnCsv.Text = "📄 导出CSV";
            this.tsBtnCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblRowCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 543);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(1097, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1080, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "就绪";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRowCount
            // 
            this.lblRowCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(4, 17);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 49);
            this.splitMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlLeft);
            this.splitMain.Panel1MinSize = 160;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitRight);
            this.splitMain.Panel2MinSize = 400;
            this.splitMain.Size = new System.Drawing.Size(1097, 494);
            this.splitMain.SplitterDistance = 179;
            this.splitMain.SplitterWidth = 3;
            this.splitMain.TabIndex = 3;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tvReports);
            this.pnlLeft.Controls.Add(this.lblTreeHeader);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(179, 494);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvReports
            // 
            this.tvReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReports.FullRowSelect = true;
            this.tvReports.HotTracking = true;
            this.tvReports.Location = new System.Drawing.Point(0, 20);
            this.tvReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvReports.Name = "tvReports";
            this.tvReports.Size = new System.Drawing.Size(179, 474);
            this.tvReports.TabIndex = 1;
            this.tvReports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReports_AfterSelect);
            // 
            // lblTreeHeader
            // 
            this.lblTreeHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this.lblTreeHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTreeHeader.ForeColor = System.Drawing.Color.White;
            this.lblTreeHeader.Location = new System.Drawing.Point(0, 0);
            this.lblTreeHeader.Name = "lblTreeHeader";
            this.lblTreeHeader.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblTreeHeader.Size = new System.Drawing.Size(179, 20);
            this.lblTreeHeader.TabIndex = 0;
            this.lblTreeHeader.Text = "查询报表";
            this.lblTreeHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitRight
            // 
            this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRight.Location = new System.Drawing.Point(0, 0);
            this.splitRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitRight.Name = "splitRight";
            this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.Controls.Add(this.pnlTop);
            this.splitRight.Panel1MinSize = 120;
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.dgvResults);
            this.splitRight.Panel2MinSize = 200;
            this.splitRight.Size = new System.Drawing.Size(915, 494);
            this.splitRight.SplitterDistance = 137;
            this.splitRight.SplitterWidth = 3;
            this.splitRight.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlParams);
            this.pnlTop.Controls.Add(this.pnlButtons);
            this.pnlTop.Controls.Add(this.txtSqlPreview);
            this.pnlTop.Controls.Add(this.lblSqlPreviewLabel);
            this.pnlTop.Controls.Add(this.lblReportName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pnlTop.Size = new System.Drawing.Size(915, 137);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlParams
            // 
            this.pnlParams.AutoScroll = true;
            this.pnlParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParams.Location = new System.Drawing.Point(5, 27);
            this.pnlParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlParams.Name = "pnlParams";
            this.pnlParams.Size = new System.Drawing.Size(905, 37);
            this.pnlParams.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnQuery);
            this.pnlButtons.Controls.Add(this.btnClear);
            this.pnlButtons.Controls.Add(this.btnExportExcel);
            this.pnlButtons.Controls.Add(this.btnExportCsv);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(5, 64);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlButtons.Size = new System.Drawing.Size(905, 30);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(120)))), ((int)(((byte)(80)))));
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(3, 6);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(86, 21);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "▶ 执行查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(95, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 21);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空条件";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Location = new System.Drawing.Point(170, 6);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(77, 21);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "导出 Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Location = new System.Drawing.Point(253, 6);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(69, 21);
            this.btnExportCsv.TabIndex = 3;
            this.btnExportCsv.Text = "导出 CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // txtSqlPreview
            // 
            this.txtSqlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSqlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSqlPreview.Location = new System.Drawing.Point(5, 94);
            this.txtSqlPreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSqlPreview.Multiline = true;
            this.txtSqlPreview.Name = "txtSqlPreview";
            this.txtSqlPreview.ReadOnly = true;
            this.txtSqlPreview.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSqlPreview.Size = new System.Drawing.Size(905, 26);
            this.txtSqlPreview.TabIndex = 1;
            // 
            // lblSqlPreviewLabel
            // 
            this.lblSqlPreviewLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSqlPreviewLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblSqlPreviewLabel.Location = new System.Drawing.Point(5, 120);
            this.lblSqlPreviewLabel.Name = "lblSqlPreviewLabel";
            this.lblSqlPreviewLabel.Size = new System.Drawing.Size(905, 13);
            this.lblSqlPreviewLabel.TabIndex = 2;
            this.lblSqlPreviewLabel.Text = "预览SQL：";
            // 
            // lblReportName
            // 
            this.lblReportName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReportName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this.lblReportName.Location = new System.Drawing.Point(5, 4);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(905, 23);
            this.lblReportName.TabIndex = 4;
            this.lblReportName.Text = "（请在左侧选择报表）";
            this.lblReportName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dgvResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(915, 354);
            this.dgvResults.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 565);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(774, 435);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "振华通用查询数据管理器";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // ── 控件字段声明 ─────────────────────────────────────────
        private System.Windows.Forms.MenuStrip              menuStrip;
        private System.Windows.Forms.ToolStripMenuItem      mnuFile;
        private System.Windows.Forms.ToolStripMenuItem      mnuReloadConfig;
        private System.Windows.Forms.ToolStripMenuItem      mnuOpenConfig;
        private System.Windows.Forms.ToolStripSeparator     mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem      mnuExit;
        private System.Windows.Forms.ToolStripMenuItem      mnuMgr;
        private System.Windows.Forms.ToolStripMenuItem      mnuConnections;
        private System.Windows.Forms.ToolStripMenuItem      mnuReports;
        private System.Windows.Forms.ToolStripMenuItem      mnuTools;
        private System.Windows.Forms.ToolStripMenuItem      mnuSqlEditor;
        private System.Windows.Forms.ToolStripMenuItem      mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem      mnuAbout;
        private System.Windows.Forms.ToolStrip              toolStrip;
        private System.Windows.Forms.ToolStripButton        tsBtnQuery;
        private System.Windows.Forms.ToolStripSeparator     tsSep1;
        private System.Windows.Forms.ToolStripButton        tsBtnClear;
        private System.Windows.Forms.ToolStripSeparator     tsSep2;
        private System.Windows.Forms.ToolStripButton        tsBtnExcel;
        private System.Windows.Forms.ToolStripButton        tsBtnCsv;
        private System.Windows.Forms.StatusStrip            statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel   lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel   lblRowCount;
        private System.Windows.Forms.SplitContainer         splitMain;
        private System.Windows.Forms.Panel                  pnlLeft;
        private System.Windows.Forms.Label                  lblTreeHeader;
        private System.Windows.Forms.TreeView               tvReports;
        private System.Windows.Forms.SplitContainer         splitRight;
        private System.Windows.Forms.Panel                  pnlTop;
        private System.Windows.Forms.Label                  lblReportName;
        private System.Windows.Forms.Panel                  pnlParams;
        private System.Windows.Forms.FlowLayoutPanel        pnlButtons;
        private System.Windows.Forms.Button                 btnQuery;
        private System.Windows.Forms.Button                 btnClear;
        private System.Windows.Forms.Button                 btnExportExcel;
        private System.Windows.Forms.Button                 btnExportCsv;
        private System.Windows.Forms.Label                  lblSqlPreviewLabel;
        private System.Windows.Forms.TextBox                txtSqlPreview;
        private System.Windows.Forms.DataGridView           dgvResults;
    }
}
