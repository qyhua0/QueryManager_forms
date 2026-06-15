using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    public class MainForm : Form
    {
        private readonly ConfigService _configService;
        private readonly DatabaseService _dbService;
        private readonly ExportService _exportService;
        private AppConfig _config;

        // UI Controls
        private TreeView tvReports;
        private Panel pnlQuery;
        private Panel pnlParams;
        private DataGridView dgvResults;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblRowCount;
        private Label lblSqlPreview;
        private TextBox txtSqlPreview;
        private Button btnQuery;
        private Button btnClear;
        private Button btnExportExcel;
        private Button btnExportCsv;
        private Label lblReportName;
        private Panel pnlTop;
        private SplitContainer splitMain;
        private SplitContainer splitRight;

        // Dynamic param controls
        private Dictionary<string, Control> _paramControls = new Dictionary<string, Control>();
        private QueryReport _currentReport;

        public MainForm()
        {
            _configService = new ConfigService();
            _dbService = new DatabaseService();
            _exportService = new ExportService();
            InitializeComponent();

            this.Load += (s, e) =>
            {
                splitMain.Panel1MinSize = 160;
                splitMain.Panel2MinSize = 400;
                splitMain.SplitterDistance = 210;

                splitRight.Panel1MinSize = 120;
                splitRight.Panel2MinSize = 200;
                splitRight.SplitterDistance = Math.Max(120, Math.Min(170, splitRight.Height - 200));
            };

            LoadConfig();
        }

        private void InitializeComponent()
        {
            this.Text = "振华通用查询数据管理器";
            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("微软雅黑", 9f);

            // ── Menu ──────────────────────────────────────────────
            var menu = new MenuStrip();
            var mnuFile = new ToolStripMenuItem("文件(&F)");
            var mnuReloadConfig = new ToolStripMenuItem("重新加载配置(&R)", null, (s, e) => LoadConfig());
            var mnuOpenConfig = new ToolStripMenuItem("打开配置文件(&O)", null, (s, e) => OpenConfigFile());
            var mnuExit = new ToolStripMenuItem("退出(&X)", null, (s, e) => Application.Exit());
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuReloadConfig, mnuOpenConfig, new ToolStripSeparator(), mnuExit });

            var mnuMgr = new ToolStripMenuItem("管理(&M)");
            var mnuConnections = new ToolStripMenuItem("连接管理(&C)", null, (s, e) => ShowConnectionManager());
            var mnuReports = new ToolStripMenuItem("报表管理(&Q)", null, (s, e) => ShowReportManager());
            mnuMgr.DropDownItems.AddRange(new ToolStripItem[] { mnuConnections, mnuReports });

            var mnuTools = new ToolStripMenuItem("工具(&T)");
            var mnuSqlEditor = new ToolStripMenuItem("SQL编辑器(&S)", null, (s, e) => ShowSqlEditor());
            mnuTools.DropDownItems.Add(mnuSqlEditor);

            var mnuHelp = new ToolStripMenuItem("帮助(&H)");
            var mnuAbout = new ToolStripMenuItem("关于(&A)", null, (s, e) => ShowAbout());
            mnuHelp.DropDownItems.Add(mnuAbout);

            menu.Items.AddRange(new ToolStripItem[] { mnuFile, mnuMgr, mnuTools, mnuHelp });
            this.MainMenuStrip = menu;

            // ── Toolbar ───────────────────────────────────────────
            var toolbar = new ToolStrip();
            toolbar.Padding = new Padding(4, 2, 0, 2);
            var tsBtnQuery = new ToolStripButton("▶ 执行查询") { DisplayStyle = ToolStripItemDisplayStyle.Text, Font = new Font("微软雅黑", 9f, FontStyle.Bold), ForeColor = Color.DarkGreen };
            tsBtnQuery.Click += (s, e) => ExecuteQuery();
            var tsBtnClear = new ToolStripButton("✕ 清空条件") { DisplayStyle = ToolStripItemDisplayStyle.Text };
            tsBtnClear.Click += (s, e) => ClearParams();
            var tsBtnExcel = new ToolStripButton("📊 导出Excel") { DisplayStyle = ToolStripItemDisplayStyle.Text };
            tsBtnExcel.Click += (s, e) => ExportToExcel();
            var tsBtnCsv = new ToolStripButton("📄 导出CSV") { DisplayStyle = ToolStripItemDisplayStyle.Text };
            tsBtnCsv.Click += (s, e) => ExportToCsv();
            toolbar.Items.AddRange(new ToolStripItem[] {
                tsBtnQuery, new ToolStripSeparator(),
                tsBtnClear, new ToolStripSeparator(),
                tsBtnExcel, tsBtnCsv
            });

            // ── Status bar ────────────────────────────────────────
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel("就绪") { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            lblRowCount = new ToolStripStatusLabel("") { BorderSides = ToolStripStatusLabelBorderSides.Left };
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus, lblRowCount });

            // ── Main split ────────────────────────────────────────
            splitMain = new SplitContainer
            {
                Dock = DockStyle.Fill,

            };

            // Left: report tree
            var pnlLeft = new Panel { Dock = DockStyle.Fill };
            var lblTree = new Label
            {
                Text = "📋 查询报表",
                Dock = DockStyle.Top,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0),
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                BackColor = Color.FromArgb(50, 80, 140),
                ForeColor = Color.White
            };
            tvReports = new TreeView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("微软雅黑", 9f),
                ShowLines = true,
                FullRowSelect = true,
                HotTracking = true
            };
            tvReports.AfterSelect += TvReports_AfterSelect;
            pnlLeft.Controls.Add(tvReports);
            pnlLeft.Controls.Add(lblTree);

            // Right: split top/bottom
            splitRight = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,

            };

            // Top-right: param panel
            pnlTop = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };

            lblReportName = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("微软雅黑", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 80, 140),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlParams = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 4, 0, 0)
            };

            var pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 4, 0, 0)
            };

            btnQuery = new Button { Text = "▶ 执行查询", Width = 100, Height = 30, BackColor = Color.FromArgb(50, 120, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("微软雅黑", 9f, FontStyle.Bold) };
            btnQuery.Click += (s, e) => ExecuteQuery();
            btnClear = new Button { Text = "清空条件", Width = 80, Height = 30, FlatStyle = FlatStyle.Flat };
            btnClear.Click += (s, e) => ClearParams();
            btnExportExcel = new Button { Text = "导出Excel", Width = 90, Height = 30, FlatStyle = FlatStyle.Flat };
            btnExportExcel.Click += (s, e) => ExportToExcel();
            btnExportCsv = new Button { Text = "导出CSV", Width = 80, Height = 30, FlatStyle = FlatStyle.Flat };
            btnExportCsv.Click += (s, e) => ExportToCsv();

            pnlButtons.Controls.AddRange(new Control[] { btnQuery, btnClear, btnExportExcel, btnExportCsv });

            pnlTop.Controls.Add(pnlParams);
            pnlTop.Controls.Add(pnlButtons);
            pnlTop.Controls.Add(lblReportName);

            // SQL preview at bottom of top panel
            txtSqlPreview = new TextBox
            {
                Dock = DockStyle.Bottom,
                Height = 36,
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245),
                Font = new Font("Consolas", 8.5f),
                ScrollBars = ScrollBars.Horizontal,
                BorderStyle = BorderStyle.FixedSingle
            };
            lblSqlPreview = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 18,
                Text = "预览SQL：",
                Font = new Font("微软雅黑", 8f),
                ForeColor = Color.Gray
            };
            pnlTop.Controls.Add(txtSqlPreview);
            pnlTop.Controls.Add(lblSqlPreview);

            splitRight.Panel1.Controls.Add(pnlTop);

            // Bottom-right: data grid
            dgvResults = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowTemplate = { Height = 24 },
                GridColor = Color.FromArgb(220, 220, 220),
                EnableHeadersVisualStyles = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(240, 245, 255) }
            };
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 80, 140);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 9f, FontStyle.Bold);

            splitRight.Panel2.Controls.Add(dgvResults);

            splitMain.Panel1.Controls.Add(pnlLeft);
            splitMain.Panel2.Controls.Add(splitRight);

            this.Controls.Add(splitMain);
            this.Controls.Add(toolbar);
            this.Controls.Add(menu);
            this.Controls.Add(statusStrip);
        }

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
                MessageBox.Show("加载配置失败：\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildReportTree()
        {
            tvReports.Nodes.Clear();
            var root = new TreeNode("所有报表") { Tag = null };
            tvReports.Nodes.Add(root);

            // Group by connection name
            var groups = new Dictionary<string, TreeNode>();
            foreach (var report in _config.Reports)
            {
                var conn = _config.Connections.Find(c => c.Id == report.ConnectionId);
                string groupName = conn?.Name ?? "未分组";
                if (!groups.ContainsKey(groupName))
                {
                    var groupNode = new TreeNode($"[{groupName}]")
                    {
                        ForeColor = Color.FromArgb(50, 80, 140),
                        NodeFont = new Font("微软雅黑", 9f, FontStyle.Bold)
                    };
                    root.Nodes.Add(groupNode);
                    groups[groupName] = groupNode;
                }
                var rptNode = new TreeNode(report.Name) { Tag = report, ToolTipText = report.Description };
                groups[groupName].Nodes.Add(rptNode);
            }
            root.ExpandAll();
        }

        private void TvReports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is QueryReport report)
            {
                LoadReport(report);
            }
        }

        private void LoadReport(QueryReport report)
        {
            _currentReport = report;
            lblReportName.Text = report.Name;
            if (!string.IsNullOrEmpty(report.Description))
                lblReportName.Text += "  —  " + report.Description;

            BuildParamControls(report);
            dgvResults.DataSource = null;
            txtSqlPreview.Text = report.BaseSql;
            lblRowCount.Text = "";
        }

        private void BuildParamControls(QueryReport report)
        {
            pnlParams.Controls.Clear();
            _paramControls.Clear();

            if (report.Parameters == null || report.Parameters.Count == 0)
            {
                pnlParams.Controls.Add(new Label { Text = "（无查询条件，直接点击执行查询）", ForeColor = Color.Gray, AutoSize = true, Location = new Point(4, 8) });
                return;
            }

            int y = 4;
            int labelW = 90;
            int ctrlW = 180;
            int rowH = 34;

            foreach (var param in report.Parameters)
            {
                var lbl = new Label
                {
                    Text = param.Label + (param.Required ? " *" : "") + "：",
                    Location = new Point(4, y + 5),
                    Width = labelW,
                    TextAlign = ContentAlignment.MiddleRight
                };
                pnlParams.Controls.Add(lbl);

                Control ctrl = null;
                switch (param.ControlType?.ToLower())
                {
                    case "datepicker":
                        var dtp = new DateTimePicker
                        {
                            Location = new Point(labelW + 8, y),
                            Width = ctrlW,
                            Format = DateTimePickerFormat.Short,
                            Checked = false,
                            ShowCheckBox = true
                        };
                        if (!string.IsNullOrEmpty(param.DefaultValue) && DateTime.TryParse(param.DefaultValue, out DateTime defDt))
                        {
                            dtp.Value = defDt;
                            dtp.Checked = true;
                        }
                        ctrl = dtp;
                        break;

                    case "combobox":
                        var cmb = new ComboBox
                        {
                            Location = new Point(labelW + 8, y),
                            Width = ctrlW,
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        if (param.Options != null)
                            foreach (var opt in param.Options)
                                cmb.Items.Add(opt);
                        if (!string.IsNullOrEmpty(param.DefaultValue))
                            cmb.Text = param.DefaultValue;
                        ctrl = cmb;
                        break;

                    case "checkbox":
                        var chk = new CheckBox
                        {
                            Location = new Point(labelW + 8, y + 4),
                            Text = param.DefaultValue ?? "",
                            AutoSize = true
                        };
                        ctrl = chk;
                        break;

                    default: // TextBox
                        var txt = new TextBox
                        {
                            Location = new Point(labelW + 8, y),
                            Width = ctrlW,
                            Text = param.DefaultValue ?? ""
                        };
                        if (param.FuzzySearch)
                        {
                            var tip = new ToolTip();
                            tip.SetToolTip(txt, "支持模糊查找");
                            // txt.PlaceholderText = "支持模糊查找...";
                        }
                        ctrl = txt;
                        break;
                }

                if (ctrl != null)
                {
                    ctrl.Tag = param;
                    pnlParams.Controls.Add(ctrl);
                    _paramControls[param.Name] = ctrl;
                }
                y += rowH;
            }
        }

        private void ExecuteQuery()
        {
            if (_currentReport == null)
            {
                MessageBox.Show("请先在左侧选择一个查询报表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var conn = _config.Connections.Find(c => c.Id == _currentReport.ConnectionId);
            if (conn == null)
            {
                MessageBox.Show("找不到对应的数据库连接配置。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate required params
            foreach (var param in _currentReport.Parameters)
            {
                if (!param.Required) continue;
                if (_paramControls.TryGetValue(param.Name, out Control ctrl))
                {
                    string val = GetControlValue(ctrl);
                        MessageBox.Show($"{param.Label}为必填项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    {
                        MessageBox.Show("${param.Label}为必填项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ctrl.Focus();
                        return;
                    }
                }
            }

            // Collect param values
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
                DataTable dt = _dbService.ExecuteQuery(conn.ConnectionString, _currentReport, paramValues, out string finalSql);
                txtSqlPreview.Text = finalSql;
                ApplyGridColumns(dt);
                dgvResults.DataSource = dt;
                lblRowCount.Text = $"共 {dt.Rows.Count} 行";
                SetStatus($"查询完成，返回 {dt.Rows.Count} 行数据");
            }
            catch (Exception ex)
            {
                SetStatus("查询失败");
                MessageBox.Show("查询出错：\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgvResults.DataSource = null;
            dgvResults.Columns.Clear();

            if (_currentReport?.Columns == null || _currentReport.Columns.Count == 0) return;

            dgvResults.AutoGenerateColumns = false;
            foreach (var colCfg in _currentReport.Columns)
            {
                if (dt.Columns.Contains(colCfg.Field))
                {
                    var col = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = colCfg.Field,
                        HeaderText = colCfg.Header ?? colCfg.Field,
                        Width = colCfg.Width,
                        DefaultCellStyle = new DataGridViewCellStyle { Format = colCfg.Format ?? "" }
                    };
                    if (colCfg.Align == "Center") col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    else if (colCfg.Align == "Right") col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvResults.Columns.Add(col);
                }
            }
        }

        private string GetControlValue(Control ctrl)
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
                if (kv.Value is TextBox tb) tb.Text = "";
                else if (kv.Value is DateTimePicker dtp) { dtp.Checked = false; }
                else if (kv.Value is ComboBox cmb) cmb.SelectedIndex = -1;
                else if (kv.Value is CheckBox chk) chk.Checked = false;
            }
            if (_currentReport != null) txtSqlPreview.Text = _currentReport.BaseSql;
        }

        private void ExportToExcel()
        {
            if (dgvResults.DataSource == null || (dgvResults.DataSource as DataTable)?.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var dlg = new SaveFileDialog { Filter = "Excel文件|*.xlsx", FileName = _currentReport?.Name ?? "查询结果" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _exportService.ExportToExcel((DataTable)dgvResults.DataSource, dlg.FileName, _currentReport);
                        SetStatus($"已导出到 {dlg.FileName}");
                        if (MessageBox.Show("导出成功，是否打开文件？", "成功", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            System.Diagnostics.Process.Start(dlg.FileName);
                    }
                    catch (Exception ex) { MessageBox.Show("导出失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void ExportToCsv()
        {
            if (dgvResults.DataSource == null || (dgvResults.DataSource as DataTable)?.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var dlg = new SaveFileDialog { Filter = "CSV文件|*.csv", FileName = _currentReport?.Name ?? "查询结果" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _exportService.ExportToCsv((DataTable)dgvResults.DataSource, dlg.FileName);
                        SetStatus($"已导出到 {dlg.FileName}");
                    }
                    catch (Exception ex) { MessageBox.Show("导出失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

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
                System.Diagnostics.Process.Start("notepad.exe", path);
            else
                MessageBox.Show("配置文件不存在：" + path, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAbout()
        {
            MessageBox.Show(
                "通用查询数据管理器\n版本 1.0\n\n支持 SQL Server 2000 及以上版本\n通过JSON配置文件动态加载查询报表",
                "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
