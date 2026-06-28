using System;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    /// <summary>
    /// 数据库连接管理窗体 — 业务逻辑层。
    /// UI 控件定义及布局全部位于 ConnectionManagerForm.Designer.cs。
    /// </summary>
    public partial class ConnectionManagerForm : Form
    {
        private readonly AppConfig       _config;
        private readonly DatabaseService _dbService;

        /// <summary>当前正在编辑的连接配置。</summary>
        private ConnectionConfig _currentConn;

        public ConnectionManagerForm(AppConfig config, DatabaseService dbService)
        {
            _config    = config;
            _dbService = dbService;
            InitializeComponent();
            LoadConnections();
        }

        // ════════════════════════════════════════════════════════
        //  列表加载
        // ════════════════════════════════════════════════════════

        private void LoadConnections()
        {
            lstConnections.Items.Clear();
            foreach (var c in _config.Connections)
                lstConnections.Items.Add(c.Name ?? c.Id);

            if (lstConnections.Items.Count > 0)
                lstConnections.SelectedIndex = 0;
        }

        // ════════════════════════════════════════════════════════
        //  事件处理
        // ════════════════════════════════════════════════════════

        private void lstConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConnDetail();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var c = new ConnectionConfig
            {
                Id               = Guid.NewGuid().ToString("N").Substring(0, 8),
                Name             = "新连接",
                DbType           = "MSSQL",
                ConnectionString = ""
            };
            _config.Connections.Add(c);
            lstConnections.Items.Add(c.Name);
            lstConnections.SelectedIndex = lstConnections.Items.Count - 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = lstConnections.SelectedIndex;
            if (idx < 0) return;

            if (MessageBox.Show("确定删除此连接？", "确认",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _config.Connections.RemoveAt(idx);
                lstConnections.Items.RemoveAt(idx);
                _currentConn = null;
                ClearDetail();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConnStr.Text))
            {
                MessageBox.Show("请填写连接字符串。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTest.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                bool ok = _dbService.TestConnection(txtConnStr.Text, out string err);
                if (ok)
                    MessageBox.Show("连接成功！", "成功",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("连接失败：\n" + err, "失败",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTest.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int idx = lstConnections.SelectedIndex;
            if (idx < 0 || _currentConn == null) return;

            _currentConn.Id               = txtId.Text.Trim();
            _currentConn.Name             = txtName.Text.Trim();
            _currentConn.ConnectionString = txtConnStr.Text.Trim();
            _currentConn.DbType           = cmbDbType.Text;

            // 同步列表显示名称
            lstConnections.Items[idx] = _currentConn.Name;
        }

        // ════════════════════════════════════════════════════════
        //  辅助
        // ════════════════════════════════════════════════════════

        private void LoadConnDetail()
        {
            int idx = lstConnections.SelectedIndex;
            if (idx < 0 || idx >= _config.Connections.Count) return;

            _currentConn      = _config.Connections[idx];
            txtId.Text        = _currentConn.Id;
            txtName.Text      = _currentConn.Name;
            txtConnStr.Text   = _currentConn.ConnectionString;
            cmbDbType.Text    = _currentConn.DbType ?? "MSSQL";
        }

        private void ClearDetail()
        {
            txtId.Text      = "";
            txtName.Text    = "";
            txtConnStr.Text = "";
            cmbDbType.SelectedIndex = 0;
        }
    }
}
