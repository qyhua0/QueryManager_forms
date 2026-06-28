namespace QueryManager.Forms
{
    partial class ReportManagerForm
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
            this.components  = new System.ComponentModel.Container();

            // --- 实例化所有控件 ---
            this.splitMain   = new System.Windows.Forms.SplitContainer();
            this.pnlLeft     = new System.Windows.Forms.Panel();
            this.lstReports  = new System.Windows.Forms.ListBox();
            this.lblListHeader = new System.Windows.Forms.Label();
            this.btnAdd      = new System.Windows.Forms.Button();
            this.btnDelete   = new System.Windows.Forms.Button();
            this.pnlRight    = new System.Windows.Forms.Panel();
            this.txtJson     = new System.Windows.Forms.TextBox();
            this.lblJsonHeader = new System.Windows.Forms.Label();
            this.pnlBottom   = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.btnApply    = new System.Windows.Forms.Button();
            this.btnSave     = new System.Windows.Forms.Button();
            this.btnCancel   = new System.Windows.Forms.Button();

            // --- 挂起布局 ---
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // splitMain
            // ==========================================
            this.splitMain.Dock             = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location         = new System.Drawing.Point(0, 0);
            this.splitMain.Name             = "splitMain";
            this.splitMain.Panel1MinSize    = 120;
            this.splitMain.Panel2MinSize    = 300;
            this.splitMain.Size             = new System.Drawing.Size(900, 562);
            this.splitMain.SplitterDistance = 180;
            this.splitMain.TabIndex         = 0;
            // Panel1 内容（顺序见下方 pnlLeft / btnDelete / btnAdd 说明）
            this.splitMain.Panel1.Controls.Add(this.pnlLeft);
            this.splitMain.Panel1.Controls.Add(this.btnDelete);
            this.splitMain.Panel1.Controls.Add(this.btnAdd);
            // Panel2 内容
            this.splitMain.Panel2.Controls.Add(this.pnlRight);

            // ==========================================
            // splitMain.Panel1 子控件
            //
            // 视觉效果（从上到下）：
            //   [  连接列表 Header  ]
            //   [   lstReports      ]  ← Fill
            //   [     新增          ]  ← Bottom，后Add，压在最上
            //   [   删除选中        ]  ← Bottom，先Add，压在最下
            //
            // Dock=Bottom 的叠放规则：
            //   先 Add 的控件靠近窗体底边，后 Add 的控件在其上方。
            //   → btnDelete 先 Add（紧贴底边），btnAdd 后 Add（在 btnDelete 上方）
            // ==========================================

            // pnlLeft (Dock=Fill)
            this.pnlLeft.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Name     = "pnlLeft";
            this.pnlLeft.TabIndex = 0;
            // ★ Fill(lstReports) 先 Add，Top(lblListHeader) 后 Add
            this.pnlLeft.Controls.Add(this.lstReports);
            this.pnlLeft.Controls.Add(this.lblListHeader);

            // lstReports
            this.lstReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstReports.Dock        = System.Windows.Forms.DockStyle.Fill;
            this.lstReports.Location    = new System.Drawing.Point(0, 24);
            this.lstReports.Name        = "lstReports";
            this.lstReports.Size        = new System.Drawing.Size(178, 476);
            this.lstReports.TabIndex    = 1;
            this.lstReports.SelectedIndexChanged += new System.EventHandler(this.lstReports_SelectedIndexChanged);

            // lblListHeader
            this.lblListHeader.BackColor  = System.Drawing.Color.FromArgb(50, 80, 140);
            this.lblListHeader.Dock       = System.Windows.Forms.DockStyle.Top;
            this.lblListHeader.ForeColor  = System.Drawing.Color.White;
            this.lblListHeader.Location   = new System.Drawing.Point(0, 0);
            this.lblListHeader.Name       = "lblListHeader";
            this.lblListHeader.Size       = new System.Drawing.Size(178, 24);
            this.lblListHeader.TabIndex   = 0;
            this.lblListHeader.Text       = " 报表列表";
            this.lblListHeader.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;

            // btnDelete (Dock=Bottom, 先 Add → 紧贴底边)
            this.btnDelete.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Height   = 28;
            this.btnDelete.Name     = "btnDelete";
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text     = "删除选中";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click   += new System.EventHandler(this.btnDelete_Click);

            // btnAdd (Dock=Bottom, 后 Add → 在 btnDelete 上方)
            this.btnAdd.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.btnAdd.Height   = 28;
            this.btnAdd.Name     = "btnAdd";
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text     = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click   += new System.EventHandler(this.btnAdd_Click);

            // ==========================================
            // splitMain.Panel2 子控件
            // ==========================================

            // pnlRight (Dock=Fill)
            this.pnlRight.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Name     = "pnlRight";
            this.pnlRight.TabIndex = 0;
            // ★ Fill(txtJson) 先 Add，Top(lblJsonHeader) 后 Add
            this.pnlRight.Controls.Add(this.txtJson);
            this.pnlRight.Controls.Add(this.lblJsonHeader);

            // txtJson
            this.txtJson.AcceptsTab = true;
            this.txtJson.Dock       = System.Windows.Forms.DockStyle.Fill;
            this.txtJson.Font       = new System.Drawing.Font("Consolas", 9F);
            this.txtJson.Location   = new System.Drawing.Point(0, 24);
            this.txtJson.Multiline  = true;
            this.txtJson.Name       = "txtJson";
            this.txtJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtJson.Size       = new System.Drawing.Size(716, 538);
            this.txtJson.TabIndex   = 1;
            this.txtJson.WordWrap   = false;

            // lblJsonHeader
            this.lblJsonHeader.BackColor  = System.Drawing.Color.FromArgb(50, 80, 140);
            this.lblJsonHeader.Dock       = System.Windows.Forms.DockStyle.Top;
            this.lblJsonHeader.ForeColor  = System.Drawing.Color.White;
            this.lblJsonHeader.Location   = new System.Drawing.Point(0, 0);
            this.lblJsonHeader.Name       = "lblJsonHeader";
            this.lblJsonHeader.Size       = new System.Drawing.Size(716, 24);
            this.lblJsonHeader.TabIndex   = 0;
            this.lblJsonHeader.Text       = " 报表JSON配置（直接编辑后点击应用）";
            this.lblJsonHeader.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;

            // ==========================================
            // pnlBottom (Dock=Bottom, FlowDirection=RightToLeft)
            //
            // FlowDirection=RightToLeft 时，Controls 里靠前的控件出现在右侧。
            // 原代码 AddRange 顺序: btnCancel, btnSave, btnApply, btnTemplate
            // 视觉从右到左：[关闭] [保存全部] [应用JSON] [插入模板]
            // ==========================================
            this.pnlBottom.Dock          = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlBottom.Height        = 38;
            this.pnlBottom.Name          = "pnlBottom";
            this.pnlBottom.Padding       = new System.Windows.Forms.Padding(4);
            this.pnlBottom.TabIndex      = 1;
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Controls.Add(this.btnTemplate);

            // btnCancel (最右)
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location     = new System.Drawing.Point(826, 7);
            this.btnCancel.Name         = "btnCancel";
            this.btnCancel.Size         = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex     = 0;
            this.btnCancel.Text         = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;

            // btnSave
            this.btnSave.BackColor    = System.Drawing.Color.FromArgb(50, 80, 140);
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.FlatStyle    = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor    = System.Drawing.Color.White;
            this.btnSave.Location     = new System.Drawing.Point(732, 7);
            this.btnSave.Name         = "btnSave";
            this.btnSave.Size         = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex     = 1;
            this.btnSave.Text         = "保存全部";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click       += new System.EventHandler(this.btnSave_Click);

            // btnApply
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(50, 120, 80);
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location  = new System.Drawing.Point(638, 7);
            this.btnApply.Name      = "btnApply";
            this.btnApply.Size      = new System.Drawing.Size(90, 28);
            this.btnApply.TabIndex  = 2;
            this.btnApply.Text      = "应用JSON";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click    += new System.EventHandler(this.btnApply_Click);

            // btnTemplate (最左)
            this.btnTemplate.Location = new System.Drawing.Point(544, 7);
            this.btnTemplate.Name     = "btnTemplate";
            this.btnTemplate.Size     = new System.Drawing.Size(90, 28);
            this.btnTemplate.TabIndex = 3;
            this.btnTemplate.Text     = "插入模板";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click   += new System.EventHandler(this.btnTemplate_Click);

            // ==========================================
            // Form
            // ★ Dock=Fill(splitMain) 先 Add，Dock=Bottom(pnlBottom) 后 Add
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 600);
            this.MinimumSize         = new System.Drawing.Size(700, 480);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name                = "ReportManagerForm";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text                = "查询报表管理（JSON编辑）";
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlBottom);

            // --- 恢复布局 ---
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // ── 控件字段声明 ─────────────────────────────────────────
        private System.Windows.Forms.SplitContainer   splitMain;
        private System.Windows.Forms.Panel            pnlLeft;
        private System.Windows.Forms.ListBox          lstReports;
        private System.Windows.Forms.Label            lblListHeader;
        private System.Windows.Forms.Button           btnAdd;
        private System.Windows.Forms.Button           btnDelete;
        private System.Windows.Forms.Panel            pnlRight;
        private System.Windows.Forms.TextBox          txtJson;
        private System.Windows.Forms.Label            lblJsonHeader;
        private System.Windows.Forms.FlowLayoutPanel  pnlBottom;
        private System.Windows.Forms.Button           btnTemplate;
        private System.Windows.Forms.Button           btnApply;
        private System.Windows.Forms.Button           btnSave;
        private System.Windows.Forms.Button           btnCancel;
    }
}
