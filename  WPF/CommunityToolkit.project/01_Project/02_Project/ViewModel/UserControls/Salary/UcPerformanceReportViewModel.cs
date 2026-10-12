using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace _02_Project.ViewModel.UserControls.Salary
{
    /// <summary>
    /// 业绩统计 ViewModel（薪资报表 - 业绩统计）。
    /// 按时间段过滤，支持按员工 / 按工序分组；分组方式变化时对象下拉联动刷新。
    /// </summary>
    public partial class UcPerformanceReportViewModel : ObservableObject
    {
        private readonly ISalaryReportService _service;
        private readonly IDialogService _dialog;

        public UcPerformanceReportViewModel(ISalaryReportService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class PerformanceItem
        {
            public int Index { get; set; }
            public int GroupId { get; set; }
            public string GroupName { get; set; } = string.Empty;
            public int Times { get; set; }
            public int ReportCount { get; set; }
            public decimal Wage { get; set; }
        }

        // 分组方式下拉：1 = 按员工(Employee)，2 = 按工序(Process)
        public List<OptionDto> GroupByOptions { get; } = new()
        {
            new OptionDto { Id = 1, Text = "按员工" },
            new OptionDto { Id = 2, Text = "按工序" }
        };

        private string CurrentGroupBy => SelectedGroupBy == 2 ? "Process" : "Employee";

        [ObservableProperty]
        private DateTime? _filterStart;

        [ObservableProperty]
        private DateTime? _filterEnd;

        [ObservableProperty]
        private int _selectedGroupBy = 1;

        [ObservableProperty]
        private List<OptionDto> _objectOptions = new();

        [ObservableProperty]
        private int? _selectedObjectId;

        [ObservableProperty]
        private ObservableCollection<PerformanceItem> _items = new();

        [ObservableProperty]
        private string _totalText = "共 0 人次，工资合计 0 元";

        /// <summary>分组方式变化：对象下拉联动刷新（员工或工序）</summary>
        partial void OnSelectedGroupByChanged(int value)
        {
            _ = HandleGroupByChangedAsync();
        }

        private async Task HandleGroupByChangedAsync()
        {
            SelectedObjectId = null;
            await RefreshObjectOptionsAsync();
            await QueryAsync();
        }

        private async Task RefreshObjectOptionsAsync()
        {
            ObjectOptions = CurrentGroupBy == "Process"
                ? await _service.GetProcessOptionsAsync()
                : await _service.GetEmployeeOptionsAsync();
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
            await RefreshObjectOptionsAsync();
            await QueryAsync();
        }

        [RelayCommand]
        private async Task QueryAsync()
        {
            try
            {
                var filterId = SelectedObjectId;
                var rows = await _service.GetPerformanceSummaryAsync(FilterStart, FilterEnd, CurrentGroupBy, filterId);
                var items = new ObservableCollection<PerformanceItem>(
                    rows.Select(r => new PerformanceItem
                    {
                        GroupId = r.GroupId,
                        GroupName = r.GroupName,
                        Times = r.Times,
                        ReportCount = r.ReportCount,
                        Wage = r.Wage
                    }));
                for (var i = 0; i < items.Count; i++)
                    items[i].Index = i + 1;
                Items = items;

                var employeeId = CurrentGroupBy == "Employee" ? filterId : null;
                var processId = CurrentGroupBy == "Process" ? filterId : null;
                var total = await _service.GetTotalAsync(FilterStart, FilterEnd, employeeId, processId);
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
            SelectedGroupBy = 1;   // 触发联动刷新对象下拉
            SelectedObjectId = null;
            await QueryAsync();
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出业绩统计",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"业绩统计_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = await _service.GetPerformanceSummaryAsync(FilterStart, FilterEnd, CurrentGroupBy, SelectedObjectId);
                var headers = new[] { "序号", "对象ID", "对象名称", "报工次数", "数量合计", "工资合计(元)" };
                var data = rows.Select((r, i) => new[]
                {
                    (i + 1).ToString(), r.GroupId.ToString(), r.GroupName,
                    r.Times.ToString(), r.ReportCount.ToString(), r.Wage.ToString("0.##")
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, data);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "业绩统计", headers, data);
                _dialog.ShowInfo($"导出成功：{dlg.FileName}");
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导出失败：{ex.Message}");
            }
        }
    }
}
