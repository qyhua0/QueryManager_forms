using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using QueryManager.Models;

namespace QueryManager.Forms
{
    /// <summary>
    /// 查询报表管理窗体（JSON 编辑模式）— 业务逻辑层。
    /// UI 控件定义及布局全部位于 ReportManagerForm.Designer.cs。
    /// </summary>
    public partial class ReportManagerForm : Form
    {
        private readonly AppConfig _config;

        /// <summary>当前正在编辑的报表。</summary>
        private QueryReport _currentReport;

        public ReportManagerForm(AppConfig config)
        {
            _config = config;
            InitializeComponent();
            LoadReports();
        }

        // ════════════════════════════════════════════════════════
        //  列表加载
        // ════════════════════════════════════════════════════════

        private void LoadReports()
        {
            lstReports.Items.Clear();
            foreach (var r in _config.Reports)
                lstReports.Items.Add(r.Name ?? r.Id);

            if (lstReports.Items.Count > 0)
                lstReports.SelectedIndex = 0;
        }

        // ════════════════════════════════════════════════════════
        //  事件处理
        // ════════════════════════════════════════════════════════

        private void lstReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReportJson();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var r = new QueryReport
            {
                Id           = Guid.NewGuid().ToString("N").Substring(0, 8),
                Name         = "新报表",
                ConnectionId = _config.Connections.Count > 0 ? _config.Connections[0].Id : "",
                BaseSql      = "SELECT * FROM TableName WHERE 1=1",
                Parameters   = new List<QueryParameter>
                {
                    new QueryParameter
                    {
                        Name        = "Keyword",
                        Label       = "关键字",
                        ControlType = "TextBox",
                        FuzzySearch = true,
                        FuzzyField  = "FieldName",
                        SqlFragment = "AND FieldName LIKE @Keyword"
                    }
                },
                Columns = new List<ColumnConfig>()
            };
            _config.Reports.Add(r);
            lstReports.Items.Add(r.Name);
            lstReports.SelectedIndex = lstReports.Items.Count - 1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0) return;

            if (MessageBox.Show("确定删除此报表？", "确认",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _config.Reports.RemoveAt(idx);
                lstReports.Items.RemoveAt(idx);
                _currentReport = null;
                txtJson.Text   = "";
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyCurrentJson();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 保存前先应用当前 JSON 编辑内容
            ApplyCurrentJson();
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            InsertTemplate();
        }

        // ════════════════════════════════════════════════════════
        //  业务逻辑
        // ════════════════════════════════════════════════════════

        private void LoadReportJson()
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0 || idx >= _config.Reports.Count) return;

            _currentReport = _config.Reports[idx];
            txtJson.Text   = JsonConvert.SerializeObject(_currentReport, Formatting.Indented);
        }

        /// <summary>将 txtJson 内容反序列化并写回 _config.Reports。</summary>
        private void ApplyCurrentJson()
        {
            int idx = lstReports.SelectedIndex;
            if (idx < 0) return;

            try
            {
                var rpt = JsonConvert.DeserializeObject<QueryReport>(txtJson.Text);
                if (rpt == null) throw new Exception("解析结果为空");

                _config.Reports[idx]  = rpt;
                lstReports.Items[idx] = rpt.Name;
                _currentReport        = rpt;

                MessageBox.Show("已应用JSON配置。", "成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON格式错误：\n" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>向 txtJson 插入一个完整的示例报表 JSON 模板。</summary>
        private void InsertTemplate()
        {
            var tmpl = new QueryReport
            {
                Id          = "rpt_example",
                Name        = "示例报表",
                Description = "按日期和关键字查询",
                ConnectionId = "conn1",
                BaseSql     = "SELECT * FROM YourTable WHERE 1=1",
                Parameters  = new List<QueryParameter>
                {
                    new QueryParameter { Name = "StartDate", Label = "开始日期", ControlType = "DatePicker", SqlFragment = "AND CreateDate >= @StartDate" },
                    new QueryParameter { Name = "EndDate",   Label = "结束日期", ControlType = "DatePicker", SqlFragment = "AND CreateDate <= @EndDate" },
                    new QueryParameter { Name = "Keyword",   Label = "关键字",   ControlType = "TextBox",    FuzzySearch = true, FuzzyField = "Name", SqlFragment = "AND Name LIKE @Keyword" },
                    new QueryParameter { Name = "Status",    Label = "状态",     ControlType = "ComboBox",   Options = new List<string> { "全部", "有效", "无效" }, SqlFragment = "AND Status = @Status" }
                },
                Columns = new List<ColumnConfig>
                {
                    new ColumnConfig { Field = "ID",         Header = "编号",     Width = 80,  Align = "Center" },
                    new ColumnConfig { Field = "Name",       Header = "名称",     Width = 150, Align = "Left"   },
                    new ColumnConfig { Field = "CreateDate", Header = "创建日期", Width = 120, Format = "yyyy-MM-dd", Align = "Center" },
                    new ColumnConfig { Field = "Status",     Header = "状态",     Width = 80,  Align = "Center" }
                }
            };
            txtJson.Text = JsonConvert.SerializeObject(tmpl, Formatting.Indented);
        }
    }
}
