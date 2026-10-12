using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace _02_Project.ViewModel.UserControls.Salary
{
    /// <summary>
    /// 薪资统计 ViewModel（薪资报表 - 薪资统计）。
    /// 按员工分组，累加报工工资；支持时间段 / 员工 / 工序过滤与合计、导出。
    /// </summary>
    public partial class UcSalaryReportViewModel : ObservableObject
    {
        private readonly ISalaryReportService _service;
        private readonly IDialogService _dialog;

        public UcSalaryReportViewModel(ISalaryReportService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class SalaryItem
        {
            public int Index { get; set; }
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; } = string.Empty;
            public string WorkGroupNames { get; set; } = string.Empty;
            public int Times { get; set; }
            public int ReportCount { get; set; }
            public decimal Wage { get; set; }
        }

        [ObservableProperty]
        private DateTime? _filterStart;

        [ObservableProperty]
        private DateTime? _filterEnd;

        [ObservableProperty]
        private List<OptionDto> _employeeOptions = new();

        [ObservableProperty]
        private int? _selectedEmployeeId;

        [ObservableProperty]
        private List<OptionDto> _processOptions = new();

        [ObservableProperty]
        private int? _selectedProcessId;

        [ObservableProperty]
        private ObservableCollection<SalaryItem> _items = new();

        [ObservableProperty]
        private string _totalText = "共 0 人次，工资合计 0 元";

        [RelayCommand]
        private async Task LoadAsync()
        {
            EmployeeOptions = await _service.GetEmployeeOptionsAsync();
            ProcessOptions = await _service.GetProcessOptionsAsync();
            await QueryAsync();
        }

        [RelayCommand]
        private async Task QueryAsync()
        {
            try
            {
                var rows = await _service.GetSalarySummaryAsync(FilterStart, FilterEnd, SelectedEmployeeId, SelectedProcessId);
                var items = new ObservableCollection<SalaryItem>(
                    rows.Select(r => new SalaryItem
                    {
                        EmployeeId = r.EmployeeId,
                        EmployeeName = r.EmployeeName,
                        WorkGroupNames = r.WorkGroupNames,
                        Times = r.Times,
                        ReportCount = r.ReportCount,
                        Wage = r.Wage
                    }));
                for (var i = 0; i < items.Count; i++)
                    items[i].Index = i + 1;
                Items = items;

                var total = await _service.GetTotalAsync(FilterStart, FilterEnd, SelectedEmployeeId, SelectedProcessId);
                TotalText = $"共 {total.Times} 人次，报工数量合计 {total.ReportCount}，工资合计 {total.Wage:0.##} 元";
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"统计失败：{ex.Message}");
            }
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            FilterStart = null;
            FilterEnd = null;
            SelectedEmployeeId = null;
            SelectedProcessId = null;
            await QueryAsync();
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出薪资统计",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"薪资统计_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = await _service.GetSalarySummaryAsync(FilterStart, FilterEnd, SelectedEmployeeId, SelectedProcessId);
                var headers = new[] { "序号", "员工ID", "员工姓名", "所属工组", "报工次数", "数量合计", "工资合计(元)" };
                var data = rows.Select((r, i) => new[]
                {
                    (i + 1).ToString(), r.EmployeeId.ToString(), r.EmployeeName,
                    r.WorkGroupNames, r.Times.ToString(), r.ReportCount.ToString(), r.Wage.ToString("0.##")
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, data);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "薪资统计", headers, data);
                _dialog.ShowInfo($"导出成功：{dlg.FileName}");
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导出失败：{ex.Message}");
            }
        }
    }
}
