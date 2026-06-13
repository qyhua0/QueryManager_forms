using System.Collections.Generic;

namespace QueryManager.Models
{
    public class AppConfig
    {
        public List<ConnectionConfig> Connections { get; set; } = new List<ConnectionConfig>();
        public List<QueryReport> Reports { get; set; } = new List<QueryReport>();
    }

    public class ConnectionConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string DbType { get; set; } = "MSSQL";
    }

    public class QueryReport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConnectionId { get; set; }
        public string BaseSql { get; set; }
        public List<QueryParameter> Parameters { get; set; } = new List<QueryParameter>();
        public List<ColumnConfig> Columns { get; set; } = new List<ColumnConfig>();
    }

    public class QueryParameter
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string ControlType { get; set; }   // TextBox / DatePicker / ComboBox / CheckBox
        public string DefaultValue { get; set; }
        public string SqlFragment { get; set; }   // e.g. "AND OrderDate >= @StartDate"
        public bool FuzzySearch { get; set; }
        public string FuzzyField { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public bool Required { get; set; }
    }

    public class ColumnConfig
    {
        public string Field { get; set; }
        public string Header { get; set; }
        public int Width { get; set; } = 100;
        public string Format { get; set; }
        public string Align { get; set; } = "Left";
    }
}
