namespace QueryManager.Forms
{
    partial class ConnectionManagerForm
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
            this.components = new System.ComponentModel.Container();

            // --- 实例化所有控件 ---
            this.pnlLeft        = new System.Windows.Forms.Panel();
            this.lstConnections  = new System.Windows.Forms.ListBox();
            this.lblListHeader   = new System.Windows.Forms.Label();
            this.btnAdd          = new System.Windows.Forms.Button();
            this.btnDelete       = new System.Windows.Forms.Button();
            this.lblId           = new System.Windows.Forms.Label();
            this.txtId           = new System.Windows.Forms.TextBox();
            this.lblName         = new System.Windows.Forms.Label();
            this.txtName         = new System.Windows.Forms.TextBox();
            this.lblDbType       = new System.Windows.Forms.Label();
            this.cmbDbType       = new System.Windows.Forms.ComboBox();
            this.lblConnStr      = new System.Windows.Forms.Label();
            this.txtConnStr      = new System.Windows.Forms.TextBox();
            this.lblHint         = new System.Windows.Forms.Label();
            this.btnTest         = new System.Windows.Forms.Button();
            this.btnSave         = new System.Windows.Forms.Button();
            this.btnCancel       = new System.Windows.Forms.Button();

            // --- 挂起布局 ---
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // pnlLeft  (Left=8,Top=8, 180x340)
            // ★ Dock顺序：Fill(lstConnections)先，Top(lblListHeader)后
            // ==========================================
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeft.Location    = new System.Drawing.Point(8, 8);
            this.pnlLeft.Name        = "pnlLeft";
            this.pnlLeft.Size        = new System.Drawing.Size(180, 340);
            this.pnlLeft.TabIndex    = 0;
            // Fill 先 Add
            this.pnlLeft.Controls.Add(this.lstConnections);
            // Top 后 Add
            this.pnlLeft.Controls.Add(this.lblListHeader);

            // lstConnections
            this.lstConnections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstConnections.Dock        = System.Windows.Forms.DockStyle.Fill;
            this.lstConnections.Location    = new System.Drawing.Point(0, 24);
            this.lstConnections.Name        = "lstConnections";
            this.lstConnections.Size        = new System.Drawing.Size(178, 314);
            this.lstConnections.TabIndex    = 1;
            this.lstConnections.SelectedIndexChanged += new System.EventHandler(this.lstConnections_SelectedIndexChanged);

            // lblListHeader
            this.lblListHeader.BackColor  = System.Drawing.Color.FromArgb(50, 80, 140);
            this.lblListHeader.Dock       = System.Windows.Forms.DockStyle.Top;
            this.lblListHeader.ForeColor  = System.Drawing.Color.White;
            this.lblListHeader.Location   = new System.Drawing.Point(0, 0);
            this.lblListHeader.Name       = "lblListHeader";
            this.lblListHeader.Size       = new System.Drawing.Size(178, 24);
            this.lblListHeader.TabIndex   = 0;
            this.lblListHeader.Text       = " 连接列表";
            this.lblListHeader.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;

            // ==========================================
            // btnAdd / btnDelete
            // ==========================================
            this.btnAdd.Location = new System.Drawing.Point(8, 355);
            this.btnAdd.Name     = "btnAdd";
            this.btnAdd.Size     = new System.Drawing.Size(60, 26);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text     = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click   += new System.EventHandler(this.btnAdd_Click);

            this.btnDelete.Location = new System.Drawing.Point(76, 355);
            this.btnDelete.Name     = "btnDelete";
            this.btnDelete.Size     = new System.Drawing.Size(60, 26);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text     = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click   += new System.EventHandler(this.btnDelete_Click);

            // ==========================================
            // 右侧详情区 — 基准坐标: lx=200, ex=310, ew=320, ly=20
            // ==========================================

            // lblId / txtId  (y = ly = 20)
            this.lblId.Location  = new System.Drawing.Point(200, 20);
            this.lblId.Name      = "lblId";
            this.lblId.Size      = new System.Drawing.Size(100, 23);
            this.lblId.TabIndex  = 3;
            this.lblId.Text      = "ID：";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtId.Location = new System.Drawing.Point(310, 20);
            this.txtId.Name     = "txtId";
            this.txtId.Size     = new System.Drawing.Size(320, 23);
            this.txtId.TabIndex = 4;

            // lblName / txtName  (y = ly+40 = 60)
            this.lblName.Location  = new System.Drawing.Point(200, 60);
            this.lblName.Name      = "lblName";
            this.lblName.Size      = new System.Drawing.Size(100, 23);
            this.lblName.TabIndex  = 5;
            this.lblName.Text      = "名称：";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtName.Location = new System.Drawing.Point(310, 60);
            this.txtName.Name     = "txtName";
            this.txtName.Size     = new System.Drawing.Size(320, 23);
            this.txtName.TabIndex = 6;

            // lblDbType / cmbDbType  (y = ly+80 = 100)
            this.lblDbType.Location  = new System.Drawing.Point(200, 100);
            this.lblDbType.Name      = "lblDbType";
            this.lblDbType.Size      = new System.Drawing.Size(100, 23);
            this.lblDbType.TabIndex  = 7;
            this.lblDbType.Text      = "数据库类型：";
            this.lblDbType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.cmbDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbType.Items.AddRange(new object[] { "MSSQL", "MSSQL2000" });
            this.cmbDbType.Location     = new System.Drawing.Point(310, 100);
            this.cmbDbType.Name         = "cmbDbType";
            this.cmbDbType.Size         = new System.Drawing.Size(180, 23);
            this.cmbDbType.TabIndex     = 8;
            this.cmbDbType.SelectedIndex = 0;

            // lblConnStr / txtConnStr  (y = ly+120 = 140)
            this.lblConnStr.Location  = new System.Drawing.Point(200, 140);
            this.lblConnStr.Name      = "lblConnStr";
            this.lblConnStr.Size      = new System.Drawing.Size(100, 23);
            this.lblConnStr.TabIndex  = 9;
            this.lblConnStr.Text      = "连接字符串：";
            this.lblConnStr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtConnStr.Font        = new System.Drawing.Font("Consolas", 8.5F);
            this.txtConnStr.Location    = new System.Drawing.Point(310, 140);
            this.txtConnStr.Multiline   = true;
            this.txtConnStr.Name        = "txtConnStr";
            this.txtConnStr.ScrollBars  = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConnStr.Size        = new System.Drawing.Size(320, 80);
            this.txtConnStr.TabIndex    = 10;

            // lblHint  (y = ly+210 = 230)
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Font      = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.lblHint.Location  = new System.Drawing.Point(310, 230);
            this.lblHint.Name      = "lblHint";
            this.lblHint.Size      = new System.Drawing.Size(320, 60);
            this.lblHint.TabIndex  = 11;
            this.lblHint.Text      = "示例（SQL2000）：\r\nServer=192.168.1.1,1433;Database=MyDB;User Id=sa;Password=pwd;\r\n\r\nPacket Size=4096 （MSSQL2000建议添加此项）";

            // btnTest  (y = ly+278 = 298)
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(50, 120, 80);
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location  = new System.Drawing.Point(310, 298);
            this.btnTest.Name      = "btnTest";
            this.btnTest.Size      = new System.Drawing.Size(90, 28);
            this.btnTest.TabIndex  = 12;
            this.btnTest.Text      = "测试连接";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click    += new System.EventHandler(this.btnTest_Click);

            // btnSave / btnCancel  (y = 360)
            this.btnSave.BackColor    = System.Drawing.Color.FromArgb(50, 80, 140);
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.FlatStyle    = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor    = System.Drawing.Color.White;
            this.btnSave.Location     = new System.Drawing.Point(490, 360);
            this.btnSave.Name         = "btnSave";
            this.btnSave.Size         = new System.Drawing.Size(70, 28);
            this.btnSave.TabIndex     = 13;
            this.btnSave.Text         = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click       += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location     = new System.Drawing.Point(570, 360);
            this.btnCancel.Name         = "btnCancel";
            this.btnCancel.Size         = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex     = 14;
            this.btnCancel.Text         = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;

            // ==========================================
            // Form
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(664, 400);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.MinimizeBox         = false;
            this.Name                = "ConnectionManagerForm";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                = "数据库连接管理";
            // 绝对定位窗体，按控件 TabIndex 顺序 Add 即可（无 Dock 冲突）
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDbType);
            this.Controls.Add(this.cmbDbType);
            this.Controls.Add(this.lblConnStr);
            this.Controls.Add(this.txtConnStr);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            // --- 恢复布局 ---
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── 控件字段声明 ─────────────────────────────────────────
        private System.Windows.Forms.Panel       pnlLeft;
        private System.Windows.Forms.ListBox     lstConnections;
        private System.Windows.Forms.Label       lblListHeader;
        private System.Windows.Forms.Button      btnAdd;
        private System.Windows.Forms.Button      btnDelete;
        private System.Windows.Forms.Label       lblId;
        private System.Windows.Forms.TextBox     txtId;
        private System.Windows.Forms.Label       lblName;
        private System.Windows.Forms.TextBox     txtName;
        private System.Windows.Forms.Label       lblDbType;
        private System.Windows.Forms.ComboBox    cmbDbType;
        private System.Windows.Forms.Label       lblConnStr;
        private System.Windows.Forms.TextBox     txtConnStr;
        private System.Windows.Forms.Label       lblHint;
        private System.Windows.Forms.Button      btnTest;
        private System.Windows.Forms.Button      btnSave;
        private System.Windows.Forms.Button      btnCancel;
    }
}
