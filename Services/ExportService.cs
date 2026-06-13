using System;
using System.Data;
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QueryManager.Models;

namespace QueryManager.Services
{
    public class ExportService
    {
        static ExportService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// 导出到 Excel (.xlsx)
        /// </summary>
        public void ExportToExcel(DataTable dt, string filePath, QueryReport report = null)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add(report?.Name ?? "查询结果");

                // 表头
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    var cell = sheet.Cells[1, c + 1];
                    // 使用列配置的标题（若有）
                    string header = dt.Columns[c].ColumnName;
                    if (report != null)
                    {
                        var col = report.Columns.Find(x => x.Field == header);
                        if (col != null) header = col.Header;
                    }
                    cell.Value = header;
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(68, 114, 196));
                    cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                // 数据行
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        var cell = sheet.Cells[r + 2, c + 1];
                        object val = dt.Rows[r][c];
                        if (val is DBNull) { cell.Value = ""; continue; }

                        string fieldName = dt.Columns[c].ColumnName;
                        string fmt = null;
                        if (report != null)
                        {
                            var col = report.Columns.Find(x => x.Field == fieldName);
                            fmt = col?.Format;
                        }

                        if (val is DateTime dtVal && !string.IsNullOrEmpty(fmt))
                        {
                            cell.Value = dtVal.ToString(fmt);
                        }
                        else
                        {
                            cell.Value = val;
                        }
                    }
                }

                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                // 斑马纹
                for (int r = 2; r <= dt.Rows.Count + 1; r++)
                {
                    if (r % 2 == 0)
                    {
                        for (int c = 1; c <= dt.Columns.Count; c++)
                        {
                            sheet.Cells[r, c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            sheet.Cells[r, c].Style.Fill.BackgroundColor.SetColor(
                                System.Drawing.Color.FromArgb(235, 241, 251));
                        }
                    }
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        /// <summary>
        /// 导出到 CSV
        /// </summary>
        public void ExportToCsv(DataTable dt, string filePath)
        {
            var sb = new StringBuilder();
            // 表头
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(QuoteCsvField(dt.Columns[i].ColumnName));
                if (i < dt.Columns.Count - 1) sb.Append(",");
            }
            sb.AppendLine();

            // 数据
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(QuoteCsvField(row[i]?.ToString() ?? ""));
                    if (i < dt.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private string QuoteCsvField(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            return field;
        }
    }
}
