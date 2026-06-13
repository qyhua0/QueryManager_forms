using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using QueryManager.Models;

namespace QueryManager.Services
{
    public class ConfigService
    {
        private readonly string _configPath;
        private AppConfig _config;

        public ConfigService(string configPath = null)
        {
            _configPath = configPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "queries.json");
        }

        public AppConfig LoadConfig()
        {
            string dir = Path.GetDirectoryName(_configPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(_configPath))
            {
                _config = CreateDefaultConfig();
                SaveConfig(_config);
            }
            else
            {
                string json = File.ReadAllText(_configPath, System.Text.Encoding.UTF8);
                _config = JsonConvert.DeserializeObject<AppConfig>(json) ?? new AppConfig();
            }
            return _config;
        }

        public void SaveConfig(AppConfig config)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(_configPath, json, System.Text.Encoding.UTF8);
            _config = config;
        }

        public string GetConfigPath() => _configPath;

        private AppConfig CreateDefaultConfig()
        {
            return new AppConfig
            {
                Connections = new List<ConnectionConfig>
                {
                    new ConnectionConfig
                    {
                        Id = "conn1",
                        Name = "示例数据库",
                        DbType = "MSSQL",
                        ConnectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=your_password;"
                    }
                },
                Reports = new List<QueryReport>
                {
                    new QueryReport
                    {
                        Id = "rpt1",
                        Name = "订单查询",
                        Description = "按日期范围和客户名称查询订单",
                        ConnectionId = "conn1",
                        BaseSql = "SELECT OrderID, CustomerID, OrderDate, ShippedDate, Freight FROM Orders WHERE 1=1",
                        Parameters = new List<QueryParameter>
                        {
                            new QueryParameter
                            {
                                Name = "StartDate",
                                Label = "开始日期",
                                ControlType = "DatePicker",
                                DefaultValue = "",
                                SqlFragment = "AND OrderDate >= @StartDate",
                                Required = false
                            },
                            new QueryParameter
                            {
                                Name = "EndDate",
                                Label = "结束日期",
                                ControlType = "DatePicker",
                                DefaultValue = "",
                                SqlFragment = "AND OrderDate <= @EndDate",
                                Required = false
                            },
                            new QueryParameter
                            {
                                Name = "CustomerID",
                                Label = "客户编号",
                                ControlType = "TextBox",
                                DefaultValue = "",
                                FuzzySearch = true,
                                FuzzyField = "CustomerID",
                                SqlFragment = "AND CustomerID LIKE @CustomerID",
                                Required = false
                            }
                        },
                        Columns = new List<ColumnConfig>
                        {
                            new ColumnConfig { Field = "OrderID",    Header = "订单ID",   Width = 80,  Align = "Center" },
                            new ColumnConfig { Field = "CustomerID", Header = "客户编号", Width = 100, Align = "Left" },
                            new ColumnConfig { Field = "OrderDate",  Header = "订单日期", Width = 120, Format = "yyyy-MM-dd", Align = "Center" },
                            new ColumnConfig { Field = "ShippedDate",Header = "发货日期", Width = 120, Format = "yyyy-MM-dd", Align = "Center" },
                            new ColumnConfig { Field = "Freight",    Header = "运费",     Width = 80,  Align = "Right" }
                        }
                    }
                }
            };
        }
    }
}
