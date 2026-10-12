using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace _02_Project.ViewModel.UserControls.Report
{
    /// <summary>
    /// 报工管理 ViewModel（报工管理 - 报工单）。
    /// 业务规则：选工序 -> 工价下拉只显示该工序工价；选工价 -> 自动带出单价（只读）；
    /// 工资 = 数量 × 单价（实时重算）；子表单[员工姓名]按工组过滤，员工个数 = 勾选行数。
    /// 保存时主表 + 子表单一同入库（服务层事务）。
    /// </summary>
    public partial class UcWorkReportManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IReportSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressCascade;          // 编辑回显时抑制联动
        private bool _suppressPager;
        private int _loadedPage;
        private decimal _currentUnitPrice;      // 当前表单单价（用于工资实时计算）

        public UcWorkReportManageViewModel(IReportSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class ReportItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; } = string.Empty;
            public int WorkGroupId { get; set; }
            public string WorkGroupName { get; set; } = string.Empty;
            public int ProcessId { get; set; }
            public string ProcessName { get; set; } = string.Empty;
            public int WageSettingId { get; set; }
            public string WageSettingName { get; set; } = string.Empty;
            public int ReportCount { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Wage { get; set; }
            public int EmployeeCount { get; set; }
            public string EmployeeNames { get; set; } = string.Empty;
            public string? Remark { get; set; }
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<ReportItem> _items = new();

        [ObservableProperty]
        private ReportItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        // ===== 筛选 =====

        [ObservableProperty]
        private List<OptionDto> _filterEmployeeOptions = new();

        [ObservableProperty]
        private int? _filterEmployeeId;

        [ObservableProperty]
        private DateTime? _filterStart;

        [ObservableProperty]
        private DateTime? _filterEnd;

        // ===== 表单主表字段 =====

        [ObservableProperty]
        private List<OptionDto> _employeeOptions = new();

        [ObservableProperty]
        private int _selectedEmployeeId;

        [ObservableProperty]
        private List<OptionDto> _groupOptions = new();

        [ObservableProperty]
        private int _selectedGroupId;

        [ObservableProperty]
        private List<OptionDto> _processOptions = new();

        [ObservableProperty]
        private int _selectedProcessId;

        [ObservableProperty]
        private List<OptionDto> _wageOptions = new();

        [ObservableProperty]
        private int _selectedWageId;

        [ObservableProperty]
        private string _reportCountText = string.Empty;

        [ObservableProperty]
        private string _unitPriceText = "0";

        [ObservableProperty]
        private string _wageText = "0";

        [ObservableProperty]
        private string _remark = string.Empty;

        [ObservableProperty]
        private DateTime? _startTime = DateTime.Now;

        [ObservableProperty]
        private DateTime? _endTime = DateTime.Now;

        // ===== 子表单[员工姓名] =====

        [ObservableProperty]
        private ObservableCollection<SelectableOption> _reportEmployees = new();

        [ObservableProperty]
        private int _employeeCount;

        /// <summary>工序变化：刷新工价下拉（只显示该工序的工价）</summary>
        partial void OnSelectedProcessIdChanged(int value)
        {
            if (_suppressCascade) return;
            _ = RefreshWageOptionsAsync(value, selectId: 0);
        }

        private async Task RefreshWageOptionsAsync(int processId, int selectId)
        {
            var wages = processId > 0
                ? await _service.GetWageSettingOptionsAsync(processId)
                : new List<OptionDto>();
            WageOptions = wages;
            if (wages.Count > 0)
            {
                SelectedWageId = selectId > 0 ? selectId : wages[0].Id;   // 触发工价联动带出单价
            }
            else
            {
                SelectedWageId = 0;
                _currentUnitPrice = 0;
                UnitPriceText = "0";
                RecalcWage();
            }
        }

        /// <summary>工价变化：自动查询 WageSetting 带出单价（只读）</summary>
        partial void OnSelectedWageIdChanged(int value)
        {
            if (_suppressCascade) return;
            if (value > 0)
                _ = FillUnitPriceAsync(value);
            else
            {
                _currentUnitPrice = 0;
                UnitPriceText = "0";
                RecalcWage();
            }
        }

        private async Task FillUnitPriceAsync(int wageId)
        {
            var wages = await _service.GetWageSettingsAsync(wageId, null, 1, 1);
            if (wages.Count > 0)
            {
                _currentUnitPrice = wages[0].UnitPrice;
                UnitPriceText = _currentUnitPrice.ToString("0.##");
            }
            RecalcWage();
        }

        /// <summary>数量变化：工资 = 数量 × 单价（实时重算）</summary>
        partial void OnReportCountTextChanged(string value)
        {
            if (_suppressCascade) return;
            RecalcWage();
        }

        private void RecalcWage()
        {
            var count = int.TryParse(ReportCountText.Trim(), out var c) ? c : 0;
            WageText = (count * _currentUnitPrice).ToString("0.##");
        }

        /// <summary>工组变化：子表单[员工姓名]只显示该工组成员（0 = 全部）</summary>
        partial void OnSelectedGroupIdChanged(int value)
        {
            if (_suppressCascade) return;
            _ = LoadReportEmployeeListAsync(selectedIds: null, workGroupId: value > 0 ? value : null);
        }

        /// <summary>加载子表单员工勾选列表（selectedIds 为编辑回显；workGroupId 为 null 用当前选中工组）</summary>
        private async Task LoadReportEmployeeListAsync(List<int>? selectedIds, int? workGroupId = null)
        {
            foreach (var o in ReportEmployees)
                o.PropertyChanged -= OnReportEmployeeChanged;

            var groupId = workGroupId ?? (SelectedGroupId > 0 ? SelectedGroupId : null);
            var employees = await _service.GetEmployeeOptionsAsync(groupId);
            var selected = selectedIds ?? new List<int>();
            var list = new ObservableCollection<SelectableOption>(
                employees.Select(e => new SelectableOption
                {
                    Id = e.Id,
                    Text = e.Text,
                    IsSelected = selected.Contains(e.Id)
                }));
            foreach (var o in list)
                o.PropertyChanged += OnReportEmployeeChanged;

            ReportEmployees = list;
            RefreshEmployeeCount();
        }

        private void OnReportEmployeeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectableOption.IsSelected))
                RefreshEmployeeCount();
        }

        /// <summary>员工个数 = 子表单勾选行数</summary>
        private void RefreshEmployeeCount()
        {
            EmployeeCount = ReportEmployees.Count(x => x.IsSelected);
        }

        private List<int> GetSelectedEmployeeIds()
        {
            return ReportEmployees.Where(x => x.IsSelected).Select(x => x.Id).ToList();
        }

        // ===== 命令 =====

        [RelayCommand]
        private async Task LoadAsync()
        {
            IsAdmin = Session.IsAdmin;
            FilterEmployeeOptions = await _service.GetEmployeeOptionsAsync(null);
            EmployeeOptions = FilterEmployeeOptions;
            GroupOptions = await _service.GetWorkGroupOptionsAsync();
            ProcessOptions = await _service.GetProcessOptionsAsync();
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task QueryAsync()
        {
            var keyword = QueryId.Trim();
            if (!string.IsNullOrWhiteSpace(keyword) && !int.TryParse(keyword, out _))
            {
                _dialog.ShowInfo("请输入数字报工单ID");
                return;
            }
            _filterId = int.TryParse(keyword, out var id) ? id : null;
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            _filterId = null;
            QueryId = string.Empty;
            FilterEmployeeId = null;
            FilterStart = null;
            FilterEnd = null;
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            if (!IsAdmin) return;
            _editingId = 0;
            _isEdit = false;
            _suppressCascade = true;

            SelectedEmployeeId = EmployeeOptions.Count > 0 ? EmployeeOptions[0].Id : 0;
            SelectedGroupId = 0;
            SelectedProcessId = ProcessOptions.Count > 0 ? ProcessOptions[0].Id : 0;
            SelectedWageId = 0;
            ReportCountText = string.Empty;
            Remark = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            _currentUnitPrice = 0;
            UnitPriceText = "0";
            WageText = "0";

            _suppressCascade = false;
            await LoadReportEmployeeListAsync(selectedIds: null);
            await RefreshWageOptionsAsync(SelectedProcessId, selectId: 0);
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not ReportItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            _ = BeginEditAsync(item);
        }

        [RelayCommand]
        private void EditRow(ReportItem? item)
        {
            if (!IsAdmin || item == null) return;
            _ = BeginEditAsync(item);
        }

        private async Task BeginEditAsync(ReportItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            _suppressCascade = true;

            SelectedEmployeeId = item.EmployeeId;
            SelectedGroupId = item.WorkGroupId;
            SelectedProcessId = item.ProcessId;
            SelectedWageId = item.WageSettingId;
            ReportCountText = item.ReportCount.ToString();
            Remark = item.Remark ?? string.Empty;
            StartTime = item.StartTime;
            EndTime = item.EndTime;
            _currentUnitPrice = item.UnitPrice;
            UnitPriceText = item.UnitPrice.ToString("0.##");
            WageText = item.Wage.ToString("0.##");

            _suppressCascade = false;
            var reportEmployees = await _service.GetWorkReportEmployeeOptionsAsync(item.Id);
            await LoadReportEmployeeListAsync(reportEmployees.Select(x => x.Id).ToList());
            await RefreshWageOptionsAsync(item.ProcessId, selectId: item.WageSettingId);
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (SelectedEmployeeId <= 0)
            {
                _dialog.ShowWarning("请选择员工。");
                return;
            }
            if (SelectedProcessId <= 0)
            {
                _dialog.ShowWarning("请选择工序。");
                return;
            }
            if (SelectedWageId <= 0)
            {
                _dialog.ShowWarning("请选择工价。");
                return;
            }
            if (!int.TryParse(ReportCountText.Trim(), out var count) || count <= 0)
            {
                _dialog.ShowWarning("报工数量必须是正整数。");
                return;
            }
            if (!StartTime.HasValue || !EndTime.HasValue)
            {
                _dialog.ShowWarning("请选择报工起止时间。");
                return;
            }
            if (EndTime.Value < StartTime.Value)
            {
                _dialog.ShowWarning("结束时间不能早于开始时间。");
                return;
            }

            var input = new WorkReportInput
            {
                StartTime = StartTime.Value,
                EndTime = EndTime.Value,
                EmployeeId = SelectedEmployeeId,
                WorkGroupId = SelectedGroupId,
                ProcessId = SelectedProcessId,
                WageSettingId = SelectedWageId,
                ReportCount = count,
                Remark = string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim(),
                EmployeeIds = GetSelectedEmployeeIds()
            };

            AuthResult result = _isEdit
                ? await _service.UpdateWorkReportAsync(_editingId, input, Session.CurrentUserId)
                : await _service.AddWorkReportAsync(input, Session.CurrentUserId);

            if (!result.Success)
            {
                _dialog.ShowWarning(result.Message);
                return;
            }
            _dialog.ShowInfo(result.Message);
            Cancel();
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private void Cancel()
        {
            _editingId = 0;
            _isEdit = false;
            EditPanelVisible = false;
            foreach (var o in ReportEmployees)
                o.PropertyChanged -= OnReportEmployeeChanged;
        }

        [RelayCommand]
        private async Task DeleteRowAsync(ReportItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除报工单 [{item.Id}]（{item.EmployeeName}，工资 {item.Wage:0.##} 元）吗？\n（子表单员工随主表级联删除）")) return;

            var r = await _service.DeleteWorkReportAsync(item.Id);
            if (r.Success) _dialog.ShowInfo(r.Message);
            else _dialog.ShowWarning(r.Message);
            if (r.Success) await ReloadPagerAsync();
        }

        [RelayCommand]
        private void SelectAll()
        {
            foreach (var o in ReportEmployees)
                o.IsSelected = true;
        }

        [RelayCommand]
        private void ClearAll()
        {
            foreach (var o in ReportEmployees)
                o.IsSelected = false;
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出报工数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"报工管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetWorkReportsAsync(_filterId, FilterEmployeeId, FilterStart, FilterEnd, 1, 100000);
                var headers = new[] { "序号", "报工单ID", "员工", "工组", "工序", "工价", "数量", "单价", "工资", "员工个数", "开始时间", "结束时间", "子表单员工", "备注" };
                var rows = all.Select((w, i) => new[]
                {
                    (i + 1).ToString(), w.Id.ToString(), w.EmployeeName, w.WorkGroupName,
                    w.ProcessName, w.WageSettingName, w.ReportCount.ToString(),
                    w.UnitPrice.ToString("0.##"), w.Wage.ToString("0.##"), w.EmployeeCount.ToString(),
                    w.StartTime.ToString("yyyy-MM-dd HH:mm"), w.EndTime.ToString("yyyy-MM-dd HH:mm"),
                    w.EmployeeNames, w.Remark ?? string.Empty
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "报工管理", headers, rows);
                _dialog.ShowInfo($"导出成功：{dlg.FileName}");
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导出失败：{ex.Message}");
            }
        }

        // ===== 分页 =====

        [RelayCommand]
        private async Task PageUpdatedAsync(FunctionEventArgs<int>? e)
        {
            if (_suppressPager || e == null) return;
            if (e.Info == _loadedPage) return;
            PageIndex = e.Info;
            await LoadPageAsync();
        }

        private async Task ReloadPagerAsync()
        {
            try
            {
                var total = await _service.GetWorkReportCountAsync(_filterId, FilterEmployeeId, FilterStart, FilterEnd);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载报工数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetWorkReportCountAsync(_filterId, FilterEmployeeId, FilterStart, FilterEnd);
            var rows = await _service.GetWorkReportsAsync(_filterId, FilterEmployeeId, FilterStart, FilterEnd, page, PageSize);

            var items = new ObservableCollection<ReportItem>(
                rows.Select(w => new ReportItem
                {
                    Id = w.Id,
                    StartTime = w.StartTime,
                    EndTime = w.EndTime,
                    EmployeeId = w.EmployeeId,
                    EmployeeName = w.EmployeeName,
                    WorkGroupId = w.WorkGroupId,
                    WorkGroupName = w.WorkGroupName,
                    ProcessId = w.ProcessId,
                    ProcessName = w.ProcessName,
                    WageSettingId = w.WageSettingId,
                    WageSettingName = w.WageSettingName,
                    ReportCount = w.ReportCount,
                    UnitPrice = w.UnitPrice,
                    Wage = w.Wage,
                    EmployeeCount = w.EmployeeCount,
                    EmployeeNames = w.EmployeeNames,
                    Remark = w.Remark
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
