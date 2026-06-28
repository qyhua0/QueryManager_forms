using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using QueryManager.Models;
using QueryManager.Services;

namespace QueryManager.Forms
{
    public partial class SqlEditorForm:Form
    {
        private AppConfig _config;
        private DatabaseService _dbService;

        public SqlEditorForm(){InitializeComponent();}
        public SqlEditorForm(AppConfig config,DatabaseService dbService):this(){_config=config;_dbService=dbService;}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(LicenseManager.UsageMode==LicenseUsageMode.Designtime) return;
            LoadConnections();
        }

        private void LoadConnections()
        {
            if(_config==null) return;
            cmbConnections.Items.Clear();
            foreach(var c in _config.Connections) cmbConnections.Items.Add(c.Name??c.Id);
            if(cmbConnections.Items.Count>0) cmbConnections.SelectedIndex=0;
        }

        private void btnRun_Click(object sender,EventArgs e)=>RunSql();

        private void RunSql()
        {
            if(_config==null||_dbService==null)return;
            int idx=cmbConnections.SelectedIndex;
            if(idx<0||idx>=_config.Connections.Count){MessageBox.Show("请选择一个数据库连接。");return;}
            string sql=string.IsNullOrWhiteSpace(txtSql.SelectedText)?txtSql.Text.Trim():txtSql.SelectedText.Trim();
            if(string.IsNullOrWhiteSpace(sql))return;
            btnRun.Enabled=false; Cursor=Cursors.WaitCursor; lblStatus.Text="执行中...";
            var sw=System.Diagnostics.Stopwatch.StartNew();
            try{
                var conn=_config.Connections[idx];
                DataTable dt=_dbService.ExecuteRawSql(conn.ConnectionString,sql,out string error);
                sw.Stop();
                if(error!=null){dgvResults.DataSource=null; lblStatus.Text="错误："+error;}
                else{dgvResults.DataSource=dt; lblStatus.Text=$"执行成功，{dt.Rows.Count} 行，耗时 {sw.ElapsedMilliseconds}ms";}
            }finally{btnRun.Enabled=true; Cursor=Cursors.Default;}
        }

        protected override bool ProcessCmdKey(ref Message msg,Keys keyData)
        {
            if(keyData==Keys.F5){RunSql(); return true;}
            return base.ProcessCmdKey(ref msg,keyData);
        }
    }
}
