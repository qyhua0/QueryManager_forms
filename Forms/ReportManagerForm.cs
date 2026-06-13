using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using QueryManager.Models;

namespace QueryManager.Forms
{
    public class ReportManagerForm : Form
    {
        private readonly AppConfig _config;
        private ListBox lstReports;
        private TextBox txtJson;
        private Button btnAdd, btnDelete, btnApply, btnSave, btnCancel;
        private QueryReport _currentReport;

        public ReportManagerForm(AppConfig config)
        {
            _config = config;
            InitializeComponent();
            LoadReports();
        }

        private void InitializeComponent()
        {
            Text = "查询报表管理（JSON编辑）";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;

            var split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 180 };

            var pnlLeft = new Panel { Dock = DockStyle.Fill };
            var lblList = new Label { Dock = DockStyle.Top, Height = 24, Text = " 报表列表", BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft };
            lstReports = new ListBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
            lstReports.SelectedIndexChanged += (s, e) => LoadReportJson();
            pnlLeft.Controls.Add(lstReports);
            pnlLeft.Controls.Add(lblList);
            split.Panel1.Controls.Add(pnlLeft);

            btnAdd = new Button { Text = "新增", Dock = DockStyle.Bottom, Height = 28 };
            btnAdd.Click += BtnAdd_Click;
            btnDelete = new Button { Text = "删除选中", Dock = DockStyle.Bottom, Height = 28 };
            btnDelete.Click += BtnDelete_Click;
            split.Panel1.Controls.Add(btnDelete);
            split.Panel1.Controls.Add(btnAdd);

            var pnlRight = new Panel { Dock = DockStyle.Fill };
            var lblJson = new Label { Dock = DockStyle.Top, Height = 24, Text = " 报表JSON配置（直接编辑后点击应用）", BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft };
            txtJson = new TextBox { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Both, Font = new Font("Consolas", 9f), AcceptsTab = true, WordWrap = false };
            pnlRight.Controls.Add(txtJson);
            pnlRight.Controls.Add(lblJson);
            split.Panel2.Controls.Add(pnlRight);

            Controls.Add(split);

            var pnlBottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 38, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(4) };
            btnCancel = new Button { Text = "关闭", Width = 70, Height = 28, DialogResult = DialogResult.Cancel };
            btnSave = new Button { Text = "保存全部", Width = 90, Height = 28, DialogResult = DialogResult.OK, BackColor = Color.FromArgb(50, 80, 140), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.Click += BtnSave_Click;
            btnApply = new Button { Text = "应用JSON", Width = 90, Height = 28, BackColor = Color.FromArgb(50, 120, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnApply.Click += BtnApply_Click;
            var btnTemplate = new Button { Text = "插入模板", Width = 90, Height = 28 };
            btnTemplate.Click += (s, e) => InsertTemplate();
            pnlBottom.Controls.AddRange(new Control[] { btnCancel, btnSave, btnApply, btnTemplate });
            Controls.Add(pnlBottom);
        }

        private void LoadReports()
        {
            lstReports.Items.Clear();
            foreach (var r in _config.Reports)
                lstReports.Items.Add(r.Name ?? r.Id);
            if (lstReports.Items.Count > 0)
                lstReports.SelectedIndex = 0;
        }

        private void LoadReportJson()
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0 || idx >= _config.Reports.Count) return;
            _currentReport = _config.Reports[idx];
            txtJson.Text = JsonConvert.SerializeObject(_currentReport, Formatting.Indented);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var r = new QueryReport
            {
                Id = Guid.NewGuid().ToString("N").Substring(0, 8),
                Name = "新报表",
                ConnectionId = _config.Connections.Count > 0 ? _config.Connections[0].Id : "",
                BaseSql = "SELECT * FROM TableName WHERE 1=1",
                Parameters = new List<QueryParameter>
                {
                    new QueryParameter { Name = "Keyword", Label = "关键字", ControlType = "TextBox", FuzzySearch = true, FuzzyField = "FieldName", SqlFragment = "AND FieldName LIKE @Keyword" }
                },
                Columns = new List<ColumnConfig>()
            };
            _config.Reports.Add(r);
            lstReports.Items.Add(r.Name);
            lstReports.SelectedIndex = lstReports.Items.Count - 1;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0) return;
            if (MessageBox.Show("确定删除此报表？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _config.Reports.RemoveAt(idx);
                lstReports.Items.RemoveAt(idx);
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0) return;
            try
            {
                var rpt = JsonConvert.DeserializeObject<QueryReport>(txtJson.Text);
                if (rpt == null) throw new Exception("解析结果为空");
                _config.Reports[idx] = rpt;
                lstReports.Items[idx] = rpt.Name;
                MessageBox.Show("已应用JSON配置。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON格式错误：\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // auto apply current json before save
            BtnApply_Click(sender, e);
        }

        private void InsertTemplate()
        {
            var tmpl = new QueryReport
            {
                Id = "rpt_example",
                Name = "示例报表",
                Description = "按日期和关键字查询",
                ConnectionId = "conn1",
                BaseSql = "SELECT * FROM YourTable WHERE 1=1",
                Parameters = new List<QueryParameter>
                {
                    new QueryParameter { Name = "StartDate", Label = "开始日期", ControlType = "DatePicker", SqlFragment = "AND CreateDate >= @StartDate" },
                    new QueryParameter { Name = "EndDate",   Label = "结束日期", ControlType = "DatePicker", SqlFragment = "AND CreateDate <= @EndDate" },
                    new QueryParameter { Name = "Keyword",   Label = "关键字",   ControlType = "TextBox", FuzzySearch = true, FuzzyField = "Name", SqlFragment = "AND Name LIKE @Keyword" },
                    new QueryParameter { Name = "Status",    Label = "状态",     ControlType = "ComboBox", Options = new List<string>{ "全部","有效","无效" }, SqlFragment = "AND Status = @Status" }
                },
                Columns = new List<ColumnConfig>
                {
                    new ColumnConfig { Field = "ID",         Header = "编号",   Width = 80,  Align = "Center" },
                    new ColumnConfig { Field = "Name",       Header = "名称",   Width = 150, Align = "Left" },
                    new ColumnConfig { Field = "CreateDate", Header = "创建日期", Width = 120, Format = "yyyy-MM-dd", Align = "Center" },
                    new ColumnConfig { Field = "Status",     Header = "状态",   Width = 80,  Align = "Center" }
                }
            };
            txtJson.Text = JsonConvert.SerializeObject(tmpl, Formatting.Indented);
        }
    }
}
