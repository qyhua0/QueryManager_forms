using System;
using System.Drawing;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    public class ConnectionManagerForm : Form
    {
        private readonly AppConfig _config;
        private readonly DatabaseService _dbService;

        private ListBox lstConnections;
        private TextBox txtId, txtName, txtConnStr;
        private ComboBox cmbDbType;
        private Button btnAdd, btnDelete, btnTest, btnSave, btnCancel;
        private ConnectionConfig _currentConn;

        public ConnectionManagerForm(AppConfig config, DatabaseService dbService)
        {
            _config = config;
            _dbService = dbService;
            InitializeComponent();
            LoadConnections();
        }

        private void InitializeComponent()
        {
            Text = "数据库连接管理";
            Size = new Size(680, 420);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Left list
            var pnlLeft = new Panel { Left = 8, Top = 8, Width = 180, Height = 340, BorderStyle = BorderStyle.FixedSingle };
            var lblList = new Label { Dock = DockStyle.Top, Height = 24, Text = " 连接列表", BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft };
            lstConnections = new ListBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
            lstConnections.SelectedIndexChanged += (s, e) => LoadConnDetail();
            pnlLeft.Controls.Add(lstConnections);
            pnlLeft.Controls.Add(lblList);
            Controls.Add(pnlLeft);

            btnAdd = new Button { Text = "新增", Left = 8, Top = 355, Width = 60, Height = 26 };
            btnAdd.Click += BtnAdd_Click;
            btnDelete = new Button { Text = "删除", Left = 76, Top = 355, Width = 60, Height = 26 };
            btnDelete.Click += BtnDelete_Click;
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);

            // Right detail
            int lx = 200, ex = 310, ew = 320, ly = 20;
            AddField("ID：", ref lx, ref ex, ref ew, ly, out txtId);
            AddField("名称：", ref lx, ref ex, ref ew, ly + 40, out txtName);

            var lblType = new Label { Text = "数据库类型：", Left = lx, Top = ly + 80, Width = 100, TextAlign = ContentAlignment.MiddleRight };
            cmbDbType = new ComboBox { Left = ex, Top = ly + 80, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbDbType.Items.AddRange(new object[] { "MSSQL", "MSSQL2000" });
            cmbDbType.SelectedIndex = 0;
            Controls.Add(lblType);
            Controls.Add(cmbDbType);

            var lblCs = new Label { Text = "连接字符串：", Left = lx, Top = ly + 120, Width = 100, TextAlign = ContentAlignment.MiddleRight };
            txtConnStr = new TextBox { Left = ex, Top = ly + 120, Width = ew, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Consolas", 8.5f) };
            Controls.Add(lblCs);
            Controls.Add(txtConnStr);

            var lblHint = new Label
            {
                Text = "示例（SQL2000）：\nServer=192.168.1.1,1433;Database=MyDB;User Id=sa;Password=pwd;\n\nPacket Size=4096 （MSSQL2000建议添加此项）",
                Left = ex, Top = ly + 210, Width = ew, Height = 60,
                ForeColor = Color.Gray, Font = new Font("微软雅黑", 7.5f)
            };
            Controls.Add(lblHint);

            btnTest = new Button { Text = "测试连接", Left = ex, Top = ly + 278, Width = 90, Height = 28, BackColor = Color.FromArgb(50, 120, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnTest.Click += BtnTest_Click;
            Controls.Add(btnTest);

            btnSave = new Button { Text = "保存", Left = 490, Top = 360, Width = 70, Height = 28, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;
            btnCancel = new Button { Text = "取消", Left = 570, Top = 360, Width = 70, Height = 28, DialogResult = DialogResult.Cancel };
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private void AddField(string label, ref int lx, ref int ex, ref int ew, int y, out TextBox txt)
        {
            var lbl = new Label { Text = label, Left = lx, Top = y, Width = 100, TextAlign = ContentAlignment.MiddleRight };
            txt = new TextBox { Left = ex, Top = y, Width = ew };
            Controls.Add(lbl);
            Controls.Add(txt);
        }

        private void LoadConnections()
        {
            lstConnections.Items.Clear();
            foreach (var c in _config.Connections)
                lstConnections.Items.Add(c.Name ?? c.Id);
            if (lstConnections.Items.Count > 0)
                lstConnections.SelectedIndex = 0;
        }

        private void LoadConnDetail()
        {
            int idx = lstConnections.SelectedIndex;
            if (idx < 0 || idx >= _config.Connections.Count) return;
            _currentConn = _config.Connections[idx];
            txtId.Text = _currentConn.Id;
            txtName.Text = _currentConn.Name;
            txtConnStr.Text = _currentConn.ConnectionString;
            cmbDbType.Text = _currentConn.DbType ?? "MSSQL";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var c = new ConnectionConfig { Id = Guid.NewGuid().ToString("N").Substring(0, 8), Name = "新连接", DbType = "MSSQL", ConnectionString = "" };
            _config.Connections.Add(c);
            lstConnections.Items.Add(c.Name);
            lstConnections.SelectedIndex = lstConnections.Items.Count - 1;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int idx = lstConnections.SelectedIndex;
            if (idx < 0) return;
            if (MessageBox.Show("确定删除此连接？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _config.Connections.RemoveAt(idx);
                lstConnections.Items.RemoveAt(idx);
                _currentConn = null;
            }
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConnStr.Text)) { MessageBox.Show("请填写连接字符串。"); return; }
            btnTest.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                bool ok = _dbService.TestConnection(txtConnStr.Text, out string err);
                if (ok) MessageBox.Show("连接成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("连接失败：\n" + err, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { btnTest.Enabled = true; Cursor = Cursors.Default; }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int idx = lstConnections.SelectedIndex;
            if (idx >= 0 && _currentConn != null)
            {
                _currentConn.Id = txtId.Text.Trim();
                _currentConn.Name = txtName.Text.Trim();
                _currentConn.ConnectionString = txtConnStr.Text.Trim();
                _currentConn.DbType = cmbDbType.Text;
                lstConnections.Items[idx] = _currentConn.Name;
            }
        }
    }
}
