using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace _02_Project.ViewModel.UserControls.Report
{
    /// <summary>
    /// 工价管理 ViewModel（报工管理 - 工价管理）。
    /// 筛选支持“工序”联动查询；单价输入用文本字段（decimal 解析在保存时校验）。
    /// </summary>
    public partial class UcWageSettingManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IReportSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcWageSettingManageViewModel(IReportSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class WageItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int ProcessId { get; set; }
            public string ProcessName { get; set; } = string.Empty;
            public decimal UnitPrice { get; set; }
            public string? Description { get; set; }
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _unitPriceText = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<WageItem> _items = new();

        [ObservableProperty]
        private WageItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        [ObservableProperty]
        private List<OptionDto> _filterProcessOptions = new();

        [ObservableProperty]
        private int? _filterProcessId;

        [ObservableProperty]
        private List<OptionDto> _processOptions = new();

        [ObservableProperty]
        private int _selectedProcessId;

        /// <summary>筛选工序变化：联动刷新（选择即查询）</summary>
        partial void OnFilterProcessIdChanged(int? value)
        {
            _ = ReloadAfterFilterChangedAsync();
        }

        private async Task ReloadAfterFilterChangedAsync()
        {
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task LoadAsync()
        {
            IsAdmin = Session.IsAdmin;
            FilterProcessOptions = await _service.GetProcessOptionsAsync();
            ProcessOptions = FilterProcessOptions;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task QueryAsync()
        {
            var keyword = QueryId.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                await ResetAsync();
                return;
            }
            if (!int.TryParse(keyword, out var id))
            {
                HintText = "请输入数字工价ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 工价ID = {id}";
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            _filterId = null;
            QueryId = string.Empty;
            HintText = string.Empty;
            FilterProcessId = null;
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private void Add()
        {
            if (!IsAdmin) return;
            _editingId = 0;
            _isEdit = false;
            Name = string.Empty;
            UnitPriceText = string.Empty;
            Description = string.Empty;
            SelectedProcessId = ProcessOptions.Count > 0 ? ProcessOptions[0].Id : 0;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not WageItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            BeginEdit(item);
        }

        [RelayCommand]
        private void EditRow(WageItem? item)
        {
            if (!IsAdmin || item == null) return;
            BeginEdit(item);
        }

        private void BeginEdit(WageItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            Name = item.Name;
            UnitPriceText = item.UnitPrice.ToString("0.##");
            Description = item.Description ?? string.Empty;
            SelectedProcessId = item.ProcessId;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var name = Name.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                _dialog.ShowWarning("工价名称不能为空。");
                return;
            }
            if (SelectedProcessId <= 0)
            {
                _dialog.ShowWarning("请选择工序。");
                return;
            }
            if (!decimal.TryParse(UnitPriceText.Trim(), out var unitPrice) || unitPrice < 0)
            {
                _dialog.ShowWarning("单价必须是非负数字。");
                return;
            }
            var description = string.IsNullOrWhiteSpace(Description) ? null : Description.Trim();

            AuthResult result = _isEdit
                ? await _service.UpdateWageSettingAsync(_editingId, name, SelectedProcessId, unitPrice, description, Session.CurrentUserId)
                : await _service.AddWageSettingAsync(name, SelectedProcessId, unitPrice, description, Session.CurrentUserId);

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
        }

        [RelayCommand]
        private async Task DeleteRowAsync(WageItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除工价 [{item.Name}] 吗？\n（被报工单引用时禁止删除）")) return;

            var r = await _service.DeleteWageSettingAsync(item.Id);
            if (r.Success) _dialog.ShowInfo(r.Message);
            else _dialog.ShowWarning(r.Message);
            if (r.Success) await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ImportAsync()
        {
            if (!IsAdmin) return;
            var dlg = new OpenFileDialog
            {
                Title = "导入工价数据",
                Filter = "Excel/CSV 文件 (*.xlsx;*.csv)|*.xlsx;*.csv|所有文件 (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = dlg.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? ExcelHelper.ReadExcel(dlg.FileName)
                    : ExcelHelper.ReadCsv(dlg.FileName);
                var processMap = (await _service.GetProcessOptionsAsync()).ToDictionary(o => o.Text, o => o.Id);
                var imported = ParseRows(rows, processMap);

                var added = 0;
                var updated = 0;
                foreach (var item in imported)
                {
                    if (item.Id > 0 && await _service.GetWageSettingCountAsync(item.Id, null) > 0)
                    {
                        var r2 = await _service.UpdateWageSettingAsync(item.Id, item.Name, item.ProcessId, item.UnitPrice, item.Description, Session.CurrentUserId);
                        if (r2.Success) updated++;
                    }
                    else
                    {
                        var r1 = await _service.AddWageSettingAsync(item.Name, item.ProcessId, item.UnitPrice, item.Description, Session.CurrentUserId);
                        if (r1.Success) added++;
                    }
                }
                _dialog.ShowInfo($"导入完成：新增 {added} 条，更新 {updated} 条。\n（工序名称不存在的行已跳过）");
                await ResetAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导入失败：{ex.Message}");
            }
        }

        /// <summary>解析导入行：识别表头（含“工价ID”），否则按位置 [ID,工价名称,工序名称,单价,说明]</summary>
        private List<WageItem> ParseRows(List<string[]> rows, Dictionary<string, int> processMap)
        {
            var list = new List<WageItem>();
            if (rows.Count == 0) return list;

            var idIdx = 0;
            var nameIdx = 1;
            var processIdx = 2;
            var priceIdx = 3;
            var descIdx = 4;
            if (rows[0].Any(c => c.Trim().Contains("工价ID", StringComparison.OrdinalIgnoreCase)))
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    var h = rows[0][i].Trim();
                    if (h.Contains("工价ID", StringComparison.OrdinalIgnoreCase) || h == "ID") idIdx = i;
                    else if (h.Contains("工价名称") || h.Contains("名称")) nameIdx = i;
                    else if (h.Contains("工序")) processIdx = i;
                    else if (h.Contains("单价")) priceIdx = i;
                    else if (h.Contains("说明")) descIdx = i;
                }
                rows.RemoveAt(0);
            }

            foreach (var row in rows)
            {
                if (row.Length <= nameIdx || string.IsNullOrWhiteSpace(row[nameIdx])) continue;
                var processName = row.Length > processIdx ? row[processIdx].Trim() : string.Empty;
                if (!processMap.TryGetValue(processName, out var processId)) continue;
                if (!decimal.TryParse(row.Length > priceIdx ? row[priceIdx].Trim() : string.Empty, out var price)) continue;

                var item = new WageItem { Name = row[nameIdx].Trim(), ProcessId = processId, UnitPrice = price };
                if (idIdx >= 0 && row.Length > idIdx && int.TryParse(row[idIdx].Trim(), out var id))
                    item.Id = id;
                if (row.Length > descIdx && !string.IsNullOrWhiteSpace(row[descIdx]))
                    item.Description = row[descIdx].Trim();
                list.Add(item);
            }
            return list;
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出工价数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"工价管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetWageSettingsAsync(_filterId, FilterProcessId, 1, 100000);
                var headers = new[] { "序号", "工价ID", "工价名称", "工序", "单价(元/件)", "工价说明" };
                var rows = all.Select((w, i) => new[]
                {
                    (i + 1).ToString(), w.Id.ToString(), w.Name, w.ProcessName,
                    w.UnitPrice.ToString("0.##"), w.Description ?? string.Empty
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "工价管理", headers, rows);
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
                var total = await _service.GetWageSettingCountAsync(_filterId, FilterProcessId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载工价数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetWageSettingCountAsync(_filterId, FilterProcessId);
            var rows = await _service.GetWageSettingsAsync(_filterId, FilterProcessId, page, PageSize);

            var items = new ObservableCollection<WageItem>(
                rows.Select(w => new WageItem
                {
                    Id = w.Id,
                    Name = w.Name,
                    ProcessId = w.ProcessId,
                    ProcessName = w.ProcessName,
                    UnitPrice = w.UnitPrice,
                    Description = w.Description
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
