using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using QueryManager.Models;

namespace QueryManager.Services
{
    public class DatabaseService
    {
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        public bool TestConnection(string connectionString, out string error)
        {
            error = null;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 动态构建并执行查询SQL
        /// </summary>
        public DataTable ExecuteQuery(
     string connectionString,
     QueryReport report,
     Dictionary<string, object> paramValues,
     out string finalSql)
        {
            string baseSql = report.BaseSql.Trim();
            var sqlParams = new List<SqlParameter>();
            var fragments = new System.Text.StringBuilder();

            foreach (var param in report.Parameters)
            {
                if (!paramValues.TryGetValue(param.Name, out object val)) continue;
                string strVal = val?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(strVal)) continue;

                if (!string.IsNullOrWhiteSpace(param.SqlFragment))
                {
                    fragments.Append(" " + param.SqlFragment);

                    if (param.FuzzySearch)
                        sqlParams.Add(new SqlParameter("@" + param.Name, "%" + strVal + "%"));
                    else if (param.ControlType == "DatePicker")
                    {
                        if (DateTime.TryParse(strVal, out DateTime dt1))
                            sqlParams.Add(new SqlParameter("@" + param.Name, dt1));
                        else
                            sqlParams.Add(new SqlParameter("@" + param.Name, strVal));
                    }
                    else
                        sqlParams.Add(new SqlParameter("@" + param.Name, strVal));
                }
            }

            // 将动态条件插入到 ORDER BY 之前
            string orderByClause = "";
            string sqlNoOrder = baseSql;
            int orderByIdx = baseSql.LastIndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase);
            if (orderByIdx >= 0)
            {
                orderByClause = " " + baseSql.Substring(orderByIdx);
                sqlNoOrder = baseSql.Substring(0, orderByIdx).TrimEnd();
            }

            finalSql = sqlNoOrder + fragments.ToString() + orderByClause;

            var dt = new DataTable();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(finalSql, conn))
                {
                    // MSSQL2000 兼容：CommandTimeout 延长，不使用新特性
                    cmd.CommandTimeout = 120;
                    foreach (var p in sqlParams)
                        cmd.Parameters.Add(p);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 执行自由SQL（供SQL编辑器使用）
        /// </summary>
        public DataTable ExecuteRawSql(string connectionString, string sql, out string error)
        {
            error = null;
            try
            {
                var dt = new DataTable();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 120;
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取数据库所有表名（兼容SQL Server 2000）
        /// </summary>
        public List<string> GetTableNames(string connectionString)
        {
            var tables = new List<string>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // sysobjects 兼容 SQL Server 2000
                    var cmd = new SqlCommand(
                        "SELECT name FROM sysobjects WHERE xtype='U' ORDER BY name", conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            tables.Add(reader.GetString(0));
                    }
                }
            }
            catch { }
            return tables;
        }
    }
}
