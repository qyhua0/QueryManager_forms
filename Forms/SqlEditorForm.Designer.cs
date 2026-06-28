using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QueryManager.Forms
{
    partial class SqlEditorForm
    {
        private IContainer components = null;
        private Panel toolbar;
        private Label lblConn;
        private ComboBox cmbConnections;
        private Button btnRun;
        private SplitContainer split;
        private Label lblSql;
        private RichTextBox txtSql;
        private DataGridView dgvResults;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolbar = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.cmbConnections = new System.Windows.Forms.ComboBox();
            this.lblConn = new System.Windows.Forms.Label();
            this.split = new System.Windows.Forms.SplitContainer();
            this.txtSql = new System.Windows.Forms.RichTextBox();
            this.lblSql = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.Controls.Add(this.btnRun);
            this.toolbar.Controls.Add(this.cmbConnections);
            this.toolbar.Controls.Add(this.lblConn);
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.toolbar.Size = new System.Drawing.Size(960, 36);
            this.toolbar.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(120)))), ((int)(((byte)(80)))));
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(272, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(110, 32);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "▶ 执行 (F5)";
            this.btnRun.UseVisualStyleBackColor = false;
            // 
            // cmbConnections
            // 
            this.cmbConnections.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnections.Location = new System.Drawing.Point(52, 4);
            this.cmbConnections.Name = "cmbConnections";
            this.cmbConnections.Size = new System.Drawing.Size(220, 20);
            this.cmbConnections.TabIndex = 1;
            // 
            // lblConn
            // 
            this.lblConn.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblConn.Location = new System.Drawing.Point(4, 4);
            this.lblConn.Name = "lblConn";
            this.lblConn.Size = new System.Drawing.Size(48, 32);
            this.lblConn.TabIndex = 2;
            this.lblConn.Text = "连接：";
            this.lblConn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 36);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.txtSql);
            this.split.Panel1.Controls.Add(this.lblSql);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.dgvResults);
            this.split.Size = new System.Drawing.Size(960, 580);
            this.split.SplitterDistance = 411;
            this.split.TabIndex = 0;
            // 
            // txtSql
            // 
            this.txtSql.AcceptsTab = true;
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSql.Location = new System.Drawing.Point(0, 22);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(960, 389);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "-- 在此输入SQL语句，按 F5 执行\nSELECT TOP 100 * FROM ";
            // 
            // lblSql
            // 
            this.lblSql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            this.lblSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSql.ForeColor = System.Drawing.Color.White;
            this.lblSql.Location = new System.Drawing.Point(0, 0);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(960, 22);
            this.lblSql.TabIndex = 1;
            this.lblSql.Text = " SQL语句（选中部分文本可单独执行）";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(80)))), ((int)(((byte)(140)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(960, 165);
            this.dgvResults.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 616);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(960, 24);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "就绪";
            // 
            // SqlEditorForm
            // 
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.split);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.lblStatus);
            this.Name = "SqlEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL 编辑器";
            this.toolbar.ResumeLayout(false);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
