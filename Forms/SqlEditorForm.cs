using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    public class SqlEditorForm : Form
    {
        private readonly AppConfig _config;
        private readonly DatabaseService _dbService;
        private ComboBox cmbConnections;
        private RichTextBox txtSql;
        private DataGridView dgvResults;
        private Button btnRun;
        private Label lblStatus;
        private SplitContainer split;

        public SqlEditorForm(AppConfig config, DatabaseService dbService)
        {
            _config = config;
            _dbService = dbService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "SQL 编辑器";
            Size = new Size(960, 640);
            StartPosition = FormStartPosition.CenterParent;

            var toolbar = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(4, 4, 4, 0) };
            var lblConn = new Label { Text = "连接：", Width = 48, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Left };
            cmbConnections = new ComboBox { Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Left };
            foreach (var c in _config.Connections)
                cmbConnections.Items.Add(c.Name ?? c.Id);
            if (cmbConnections.Items.Count > 0) cmbConnections.SelectedIndex = 0;

            btnRun = new Button
            {
                Text = "▶ 执行 (F5)",
                Width = 110, Height = 28,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(50, 120, 80), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微软雅黑", 9f, FontStyle.Bold)
            };
            btnRun.Click += (s, e) => RunSql();
            toolbar.Controls.Add(btnRun);
            toolbar.Controls.Add(cmbConnections);
            toolbar.Controls.Add(lblConn);
            Controls.Add(toolbar);

            split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 200
            };

            var lblSql = new Label { Dock = DockStyle.Top, Height = 22, Text = " SQL语句（选中部分文本可单独执行）", BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft };
            txtSql = new RichTextBox { Dock = DockStyle.Fill, Font = new Font("Consolas", 10f), AcceptsTab = true };
            txtSql.Text = "-- 在此输入SQL语句，按 F5 执行\r\nSELECT TOP 100 * FROM ";
            split.Panel1.Controls.Add(txtSql);
            split.Panel1.Controls.Add(lblSql);

            dgvResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false
            };
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 80, 140);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            split.Panel2.Controls.Add(dgvResults);
            Controls.Add(split);

            lblStatus = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 24,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "就绪",
                Padding = new Padding(4, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(lblStatus);
        }

        private void RunSql()
        {
            int idx = cmbConnections.SelectedIndex;
            if (idx < 0 || idx >= _config.Connections.Count)
            {
                MessageBox.Show("请选择一个数据库连接。");
                return;
            }
            string sql = txtSql.SelectedText.Trim();
            if (string.IsNullOrEmpty(sql))
                sql = txtSql.Text.Trim();
            if (string.IsNullOrEmpty(sql)) return;

            btnRun.Enabled = false;
            Cursor = Cursors.WaitCursor;
            lblStatus.Text = "执行中...";
            var sw = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var conn = _config.Connections[idx];
                var dt = _dbService.ExecuteRawSql(conn.ConnectionString, sql, out string error);
                sw.Stop();
                if (error != null)
                {
                    lblStatus.Text = "错误：" + error;
                    dgvResults.DataSource = null;
                }
                else
                {
                    dgvResults.DataSource = dt;
                    lblStatus.Text = $"执行成功，{dt.Rows.Count} 行，耗时 {sw.ElapsedMilliseconds}ms";
                }
            }
            finally { btnRun.Enabled = true; Cursor = Cursors.Default; }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) { RunSql(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
