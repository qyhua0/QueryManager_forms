using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    /// <summary>
    /// 主窗体 — 业务逻辑层。
    /// UI 控件定义及布局全部位于 MainForm.Designer.cs，
    /// 可在 Visual Studio 窗体设计器中直接打开编辑。
    /// </summary>
    public partial class MainForm : Form
    {
        // ── 服务 ─────────────────────────────────────────────────
        private readonly ConfigService    _configService;
        private readonly DatabaseService  _dbService;
        private readonly ExportService    _exportService;
        private AppConfig                 _config;

        // ── 运行时状态 ───────────────────────────────────────────
        /// <summary>当前选中的查询报表。</summary>
        private QueryReport _currentReport;

        /// <summary>动态生成的参数控件映射：参数名 → 控件。</summary>
        private readonly Dictionary<string, Control> _paramControls =
            new Dictionary<string, Control>();

        // ════════════════════════════════════════════════════════
        //  构造 & 初始化
        // ════════════════════════════════════════════════════════

        public MainForm()
        {
            _configService = new ConfigService();
            _dbService     = new DatabaseService();
            _exportService = new ExportService();

            InitializeComponent();   // 由 Designer.cs 生成

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // SplitterDistance 依赖实际窗体尺寸，在 Load 后设置更准确
            splitMain.SplitterDistance  = 210;
            splitRight.SplitterDistance = Math.Max(120,
                Math.Min(170, splitRight.Height - 200));

            LoadConfig();
        }

        // ════════════════════════════════════════════════════════
        //  菜单事件
        // ════════════════════════════════════════════════════════

        private void mnuReloadConfig_Click(object sender, EventArgs e) => LoadConfig();
        private void mnuOpenConfig_Click(object sender, EventArgs e)   => OpenConfigFile();
        private void mnuExit_Click(object sender, EventArgs e)         => Application.Exit();
        private void mnuConnections_Click(object sender, EventArgs e)  => ShowConnectionManager();
        private void mnuReports_Click(object sender, EventArgs e)      => ShowReportManager();
        private void mnuSqlEditor_Click(object sender, EventArgs e)    => ShowSqlEditor();
        private void mnuAbout_Click(object sender, EventArgs e)        => ShowAbout();

        // ════════════════════════════════════════════════════════
        //  工具栏 / 按钮事件（设计器和手写按钮共享同名处理器）
        // ════════════════════════════════════════════════════════

        private void btnQuery_Click(object sender, EventArgs e)       => ExecuteQuery();
        private void btnClear_Click(object sender, EventArgs e)       => ClearParams();
        private void btnExportExcel_Click(object sender, EventArgs e) => ExportToExcel();
        private void btnExportCsv_Click(object sender, EventArgs e)   => ExportToCsv();

        // ════════════════════════════════════════════════════════
        //  TreeView 事件
        // ════════════════════════════════════════════════════════

        private void tvReports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is QueryReport report)
                LoadReport(report);
        }

        // ════════════════════════════════════════════════════════
        //  配置加载
        // ════════════════════════════════════════════════════════

        private void LoadConfig()
        {
            try
            {
                _config = _configService.LoadConfig();
                BuildReportTree();
                SetStatus($"配置已加载，共 {_config.Reports.Count} 个报表，{_config.Connections.Count} 个连接");
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载配置失败：\n" + ex.Message,
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildReportTree()
        {
            tvReports.Nodes.Clear();
            var root = new TreeNode("所有报表") { Tag = null };
            tvReports.Nodes.Add(root);

            var groups = new Dictionary<string, TreeNode>();
            foreach (var report in _config.Reports)
            {
                var conn      = _config.Connections.Find(c => c.Id == report.ConnectionId);
                string grpName = conn?.Name ?? "未分组";

                if (!groups.TryGetValue(grpName, out TreeNode grpNode))
                {
                    grpNode = new TreeNode($"[{grpName}]")
                    {
                        ForeColor = Color.FromArgb(50, 80, 140),
                        NodeFont  = new Font("微软雅黑", 9f, FontStyle.Bold)
                    };
                    root.Nodes.Add(grpNode);
                    groups[grpName] = grpNode;
                }

                grpNode.Nodes.Add(new TreeNode(report.Name)
                {
                    Tag         = report,
                    ToolTipText = report.Description
                });
            }
            root.ExpandAll();
        }

        // ════════════════════════════════════════════════════════
        //  报表加载 & 参数控件动态生成
        // ════════════════════════════════════════════════════════

        private void LoadReport(QueryReport report)
        {
            _currentReport = report;

            lblReportName.Text = string.IsNullOrEmpty(report.Description)
                ? report.Name
                : $"{report.Name}  —  {report.Description}";

            BuildParamControls(report);
            dgvResults.DataSource = null;
            txtSqlPreview.Text    = report.BaseSql;
            lblRowCount.Text      = "";
        }

        /// <summary>
        /// 根据报表参数定义动态生成查询条件控件。
        /// 这部分本就需要在运行时根据配置生成，无法放入设计器，属于正常设计。
        /// </summary>
        private void BuildParamControls(QueryReport report)
        {
            pnlParams.Controls.Clear();
            _paramControls.Clear();

            if (report.Parameters == null || report.Parameters.Count == 0)
            {
                pnlParams.Controls.Add(new Label
                {
                    Text      = "（无查询条件，直接点击执行查询）",
                    ForeColor = Color.Gray,
                    AutoSize  = true,
                    Location  = new Point(4, 8)
                });
                return;
            }

            const int labelW = 90;
            const int ctrlW  = 180;
            const int rowH   = 34;
            int y = 4;

            foreach (var param in report.Parameters)
            {
                // 标签
                pnlParams.Controls.Add(new Label
                {
                    Text      = param.Label + (param.Required ? " *" : "") + "：",
                    Location  = new Point(4, y + 5),
                    Width     = labelW,
                    TextAlign = ContentAlignment.MiddleRight
                });

                // 控件
                Control ctrl = CreateParamControl(param, labelW, y, ctrlW);
                if (ctrl != null)
                {
                    ctrl.Tag = param;
                    pnlParams.Controls.Add(ctrl);
                    _paramControls[param.Name] = ctrl;
                }

                y += rowH;
            }
        }

        private Control CreateParamControl(QueryParameter param, int labelW, int y, int ctrlW)
        {
            switch (param.ControlType?.ToLower())
            {
                case "datepicker":
                {
                    var dtp = new DateTimePicker
                    {
                        Location  = new Point(labelW + 8, y),
                        Width     = ctrlW,
                        Format    = DateTimePickerFormat.Short,
                        Checked   = false,
                        ShowCheckBox = true
                    };
                    if (!string.IsNullOrEmpty(param.DefaultValue)
                        && DateTime.TryParse(param.DefaultValue, out DateTime defDt))
                    {
                        dtp.Value   = defDt;
                        dtp.Checked = true;
                    }
                    return dtp;
                }

                case "combobox":
                {
                    var cmb = new ComboBox
                    {
                        Location      = new Point(labelW + 8, y),
                        Width         = ctrlW,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    if (param.Options != null)
                        foreach (var opt in param.Options)
                            cmb.Items.Add(opt);
                    if (!string.IsNullOrEmpty(param.DefaultValue))
                        cmb.Text = param.DefaultValue;
                    return cmb;
                }

                case "checkbox":
                    return new CheckBox
                    {
                        Location = new Point(labelW + 8, y + 4),
                        Text     = param.DefaultValue ?? "",
                        AutoSize = true
                    };

                default: // TextBox
                {
                    var txt = new TextBox
                    {
                        Location = new Point(labelW + 8, y),
                        Width    = ctrlW,
                        Text     = param.DefaultValue ?? ""
                    };
                    if (param.FuzzySearch)
                        new ToolTip().SetToolTip(txt, "支持模糊查找");
                    return txt;
                }
            }
        }

        // ════════════════════════════════════════════════════════
        //  查询执行
        // ════════════════════════════════════════════════════════

        private void ExecuteQuery()
        {
            if (_currentReport == null)
            {
                MessageBox.Show("请先在左侧选择一个查询报表。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var conn = _config.Connections.Find(c => c.Id == _currentReport.ConnectionId);
            if (conn == null)
            {
                MessageBox.Show("找不到对应的数据库连接配置。",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 必填项校验
            foreach (var param in _currentReport.Parameters)
            {
                if (!param.Required) continue;
                if (_paramControls.TryGetValue(param.Name, out Control ctrl)
                    && string.IsNullOrWhiteSpace(GetControlValue(ctrl)))
                {
                    MessageBox.Show($"{param.Label} 为必填项。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ctrl.Focus();
                    return;
                }
            }

            // 收集参数值
            var paramValues = new Dictionary<string, object>();
            foreach (var kv in _paramControls)
            {
                string val = GetControlValue(kv.Value);
                if (!string.IsNullOrWhiteSpace(val))
                    paramValues[kv.Key] = val;
            }

            SetStatus("正在查询...");
            btnQuery.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                DataTable dt = _dbService.ExecuteQuery(
                    conn.ConnectionString, _currentReport, paramValues, out string finalSql);

                txtSqlPreview.Text    = finalSql;
                ApplyGridColumns(dt);
                dgvResults.DataSource = dt;
                lblRowCount.Text      = $"共 {dt.Rows.Count} 行";
                SetStatus($"查询完成，返回 {dt.Rows.Count} 行数据");
            }
            catch (Exception ex)
            {
                SetStatus("查询失败");
                MessageBox.Show("查询出错：\n" + ex.Message,
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnQuery.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void ApplyGridColumns(DataTable dt)
        {
            dgvResults.AutoGenerateColumns = true;
            dgvResults.DataSource          = null;
            dgvResults.Columns.Clear();

            if (_currentReport?.Columns == null || _currentReport.Columns.Count == 0)
                return;

            dgvResults.AutoGenerateColumns = false;
            foreach (var colCfg in _currentReport.Columns)
            {
                if (!dt.Columns.Contains(colCfg.Field)) continue;

                var col = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = colCfg.Field,
                    HeaderText       = colCfg.Header ?? colCfg.Field,
                    Width            = colCfg.Width,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = colCfg.Format ?? "" }
                };
                switch (colCfg.Align)
                {
                    case "Center":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Right":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
                dgvResults.Columns.Add(col);
            }
        }

        // ════════════════════════════════════════════════════════
        //  辅助：读取控件值 / 清空参数
        // ════════════════════════════════════════════════════════

        private static string GetControlValue(Control ctrl)
        {
            if (ctrl is DateTimePicker dtp)
                return dtp.Checked ? dtp.Value.ToString("yyyy-MM-dd") : "";
            if (ctrl is CheckBox chk)
                return chk.Checked ? "1" : "";
            return ctrl.Text?.Trim() ?? "";
        }

        private void ClearParams()
        {
            foreach (var kv in _paramControls)
            {
                switch (kv.Value)
                {
                    case TextBox tb:        tb.Text = ""; break;
                    case DateTimePicker dtp: dtp.Checked = false; break;
                    case ComboBox cmb:      cmb.SelectedIndex = -1; break;
                    case CheckBox chk:      chk.Checked = false; break;
                }
            }
            if (_currentReport != null)
                txtSqlPreview.Text = _currentReport.BaseSql;
        }

        // ════════════════════════════════════════════════════════
        //  导出
        // ════════════════════════════════════════════════════════

        private void ExportToExcel()
        {
            if (!HasResultData()) return;

            using (var dlg = new SaveFileDialog
            {
                Filter   = "Excel文件|*.xlsx",
                FileName = _currentReport?.Name ?? "查询结果"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    _exportService.ExportToExcel(
                        (DataTable)dgvResults.DataSource, dlg.FileName, _currentReport);
                    SetStatus($"已导出到 {dlg.FileName}");
                    if (MessageBox.Show("导出成功，是否打开文件？",
                            "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                        Process.Start(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败：" + ex.Message,
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv()
        {
            if (!HasResultData()) return;

            using (var dlg = new SaveFileDialog
            {
                Filter   = "CSV文件|*.csv",
                FileName = _currentReport?.Name ?? "查询结果"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    _exportService.ExportToCsv((DataTable)dgvResults.DataSource, dlg.FileName);
                    SetStatus($"已导出到 {dlg.FileName}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败：" + ex.Message,
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool HasResultData()
        {
            if (dgvResults.DataSource != null
                && (dgvResults.DataSource as DataTable)?.Rows.Count > 0)
                return true;

            MessageBox.Show("没有可导出的数据。",
                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        // ════════════════════════════════════════════════════════
        //  弹出子窗口
        // ════════════════════════════════════════════════════════

        private void ShowConnectionManager()
        {
            using (var f = new ConnectionManagerForm(_config, _dbService))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    _configService.SaveConfig(_config);
                    BuildReportTree();
                    SetStatus("连接配置已保存");
                }
            }
        }

        private void ShowReportManager()
        {
            using (var f = new ReportManagerForm(_config))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    _configService.SaveConfig(_config);
                    BuildReportTree();
                    SetStatus("报表配置已保存");
                }
            }
        }

        private void ShowSqlEditor()
        {
            using (var f = new SqlEditorForm(_config, _dbService))
                f.ShowDialog(this);
        }

        private void OpenConfigFile()
        {
            string path = _configService.GetConfigPath();
            if (File.Exists(path))
                Process.Start("notepad.exe", path);
            else
                MessageBox.Show("配置文件不存在：" + path,
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAbout()
        {
            MessageBox.Show(
                "通用查询数据管理器\n版本 1.0\n\n" +
                "支持 SQL Server 2000 及以上版本\n" +
                "通过JSON配置文件动态加载查询报表",
                "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ════════════════════════════════════════════════════════
        //  状态栏 & 快捷键
        // ════════════════════════════════════════════════════════

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
            statusStrip.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5) { ExecuteQuery(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
