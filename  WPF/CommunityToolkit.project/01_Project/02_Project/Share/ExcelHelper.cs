using OfficeOpenXml;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace _02_Project.Share
{
    /// <summary>
    /// Excel 导入导出工具（基于第三方库 EPPlus 4.5.3.1，参考课程纪要 day12「导出为Excel」章节）。
    /// 导出：.xlsx（ExcelPackage 写实体文件，写入前无需手工创建文件，见课程纪要说明）；
    /// 导入：.xlsx（EPPlus 读取第一个工作表）/ .csv（简易解析）。
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>导出为 Excel(.xlsx)：headers 为表头，rows 为数据行（每行 string[]）</summary>
        public static void ExportToExcel(string filePath, string sheetName, string[] headers, IEnumerable<string[]> rows)
        {
            // EPPlus 在写入数据之前不能创建实体文件：由 ExcelPackage 统一管理（FileInfo 传入）
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excel = new ExcelPackage(fileInfo))
            {
                // 若工作表已存在，先删除再重建，保证导出结果干净
                if (excel.Workbook.Worksheets[sheetName] != null)
                {
                    excel.Workbook.Worksheets.Delete(sheetName);
                }

                // 创建工作表
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(sheetName);

                // 表头
                for (int c = 0; c < headers.Length; c++)
                {
                    worksheet.Cells[1, c + 1].Value = headers[c];
                }

                // 数据行
                int row = 2;
                foreach (var line in rows)
                {
                    for (int c = 0; c < line.Length; c++)
                    {
                        worksheet.Cells[row, c + 1].Value = line[c];
                    }
                    row++;
                }

                // 表头样式：加粗 + 浅蓝底，与界面风格一致
                using (var headerRange = worksheet.Cells[1, 1, 1, Math.Max(1, headers.Length)])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 236, 255));
                }

                // 保存到 fileInfo 对应路径
                excel.Save();
            }
        }

        /// <summary>读取 .xlsx 第一个工作表，返回每行单元格文本</summary>
        public static List<string[]> ReadExcel(string filePath)
        {
            var list = new List<string[]>();
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excel = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet? worksheet = excel.Workbook.Worksheets[0];
                if (worksheet?.Dimension == null) return list;   // 空表

                int rowCount = worksheet.Dimension.End.Row;
                int colCount = worksheet.Dimension.End.Column;
                for (int r = 1; r <= rowCount; r++)
                {
                    var line = new string[colCount];
                    for (int c = 1; c <= colCount; c++)
                    {
                        line[c - 1] = worksheet.Cells[r, c].Text ?? string.Empty;
                    }
                    if (line.Any(s => !string.IsNullOrWhiteSpace(s)))
                    {
                        list.Add(line);
                    }
                }
            }
            return list;
        }

        /// <summary>导出为 CSV（UTF-8 BOM，Excel 双击可直接打开）</summary>
        public static void ExportToCsv(string filePath, string[] headers, IEnumerable<string[]> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Join(",", headers.Select(CsvEncode)));
            foreach (var row in rows)
            {
                sb.AppendLine(string.Join(",", row.Select(CsvEncode)));
            }
            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        /// <summary>读取 CSV（按行解析，支持引号包裹）</summary>
        public static List<string[]> ReadCsv(string filePath)
        {
            var list = new List<string[]>();
            foreach (var line in File.ReadAllLines(filePath, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                list.Add(SplitCsvLine(line));
            }
            return list;
        }

        private static string CsvEncode(string s)
        {
            if (s == null) return string.Empty;
            if (s.Contains(',') || s.Contains('"') || s.Contains('\n') || s.Contains('\r'))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        private static string[] SplitCsvLine(string line)
        {
            var cols = new List<string>();
            var cur = new StringBuilder();
            var inQuotes = false;
            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (inQuotes)
                {
                    if (c == '"' && i + 1 < line.Length && line[i + 1] == '"') { cur.Append('"'); i++; }
                    else if (c == '"') inQuotes = false;
                    else cur.Append(c);
                }
                else
                {
                    if (c == '"') inQuotes = true;
                    else if (c == ',') { cols.Add(cur.ToString()); cur.Clear(); }
                    else cur.Append(c);
                }
            }
            cols.Add(cur.ToString());
            return cols.ToArray();
        }
    }
}
