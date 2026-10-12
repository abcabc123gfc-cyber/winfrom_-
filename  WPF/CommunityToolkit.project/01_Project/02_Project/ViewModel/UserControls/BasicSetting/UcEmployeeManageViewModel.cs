using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace _02_Project.ViewModel.UserControls.BasicSetting
{
    /// <summary>
    /// 员工管理 ViewModel（基础设置 - 员工管理）。
    /// 注入 IBasicSettingService / IDialogService；筛选支持“车间”联动查询；工号唯一由服务层校验。
    /// </summary>
    public partial class UcEmployeeManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IBasicSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcEmployeeManageViewModel(IBasicSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class EmployeeItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string EmployeeNo { get; set; } = string.Empty;
            public int WorkshopId { get; set; }
            public string WorkshopName { get; set; } = string.Empty;
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _employeeNo = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<EmployeeItem> _items = new();

        [ObservableProperty]
        private EmployeeItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        [ObservableProperty]
        private List<OptionDto> _filterWorkshopOptions = new();

        [ObservableProperty]
        private int? _filterWorkshopId;

        [ObservableProperty]
        private List<OptionDto> _workshopOptions = new();

        [ObservableProperty]
        private int _selectedWorkshopId;

        /// <summary>筛选车间变化：联动刷新（选择即查询）</summary>
        partial void OnFilterWorkshopIdChanged(int? value)
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
            FilterWorkshopOptions = await _service.GetWorkshopOptionsAsync();
            WorkshopOptions = FilterWorkshopOptions;
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
                HintText = "请输入数字员工ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 员工ID = {id}";
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            _filterId = null;
            QueryId = string.Empty;
            HintText = string.Empty;
            FilterWorkshopId = null;
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
            EmployeeNo = string.Empty;
            SelectedWorkshopId = WorkshopOptions.Count > 0 ? WorkshopOptions[0].Id : 0;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not EmployeeItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            BeginEdit(item);
        }

        [RelayCommand]
        private void EditRow(EmployeeItem? item)
        {
            if (!IsAdmin || item == null) return;
            BeginEdit(item);
        }

        private void BeginEdit(EmployeeItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            Name = item.Name;
            EmployeeNo = item.EmployeeNo;
            SelectedWorkshopId = item.WorkshopId;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var name = Name.Trim();
            var no = EmployeeNo.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                _dialog.ShowWarning("员工姓名不能为空。");
                return;
            }
            if (string.IsNullOrWhiteSpace(no))
            {
                _dialog.ShowWarning("工号不能为空。");
                return;
            }
            if (SelectedWorkshopId <= 0)
            {
                _dialog.ShowWarning("请选择所属车间。");
                return;
            }

            AuthResult result = _isEdit
                ? await _service.UpdateEmployeeAsync(_editingId, name, no, SelectedWorkshopId, Session.CurrentUserId)
                : await _service.AddEmployeeAsync(name, no, SelectedWorkshopId, Session.CurrentUserId);

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
        private async Task DeleteRowAsync(EmployeeItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除员工 [{item.Name}（{item.EmployeeNo}）] 吗？\n（存在报工记录或仍属于工组时禁止删除）")) return;

            var r = await _service.DeleteEmployeeAsync(item.Id);
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
                Title = "导入员工数据",
                Filter = "Excel/CSV 文件 (*.xlsx;*.csv)|*.xlsx;*.csv|所有文件 (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = dlg.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? ExcelHelper.ReadExcel(dlg.FileName)
                    : ExcelHelper.ReadCsv(dlg.FileName);
                var workshopOptions = await _service.GetWorkshopOptionsAsync();
                var workshopMap = workshopOptions.ToDictionary(o => o.Text, o => o.Id);
                var imported = ParseRows(rows, workshopMap);

                var added = 0;
                var updated = 0;
                foreach (var item in imported)
                {
                    if (item.Id > 0 && await _service.GetEmployeeCountAsync(item.Id, null) > 0)
                    {
                        var r2 = await _service.UpdateEmployeeAsync(item.Id, item.Name, item.EmployeeNo, item.WorkshopId, Session.CurrentUserId);
                        if (r2.Success) updated++;
                    }
                    else
                    {
                        var r1 = await _service.AddEmployeeAsync(item.Name, item.EmployeeNo, item.WorkshopId, Session.CurrentUserId);
                        if (r1.Success) added++;
                    }
                }
                _dialog.ShowInfo($"导入完成：新增 {added} 条，更新 {updated} 条。\n（车间名称不存在的行已跳过）");
                await ResetAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导入失败：{ex.Message}");
            }
        }

        /// <summary>解析导入行：识别表头（含“员工ID”），否则按位置 [ID,姓名,工号,车间名称]</summary>
        private List<EmployeeItem> ParseRows(List<string[]> rows, Dictionary<string, int> workshopMap)
        {
            var list = new List<EmployeeItem>();
            if (rows.Count == 0) return list;

            var idIdx = 0;
            var nameIdx = 1;
            var noIdx = 2;
            var wsIdx = 3;
            if (rows[0].Any(c => c.Trim().Contains("员工ID", StringComparison.OrdinalIgnoreCase)))
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    var h = rows[0][i].Trim();
                    if (h.Contains("员工ID", StringComparison.OrdinalIgnoreCase) || h == "ID") idIdx = i;
                    else if (h.Contains("姓名") || h.Contains("名称")) nameIdx = i;
                    else if (h.Contains("工号")) noIdx = i;
                    else if (h.Contains("车间")) wsIdx = i;
                }
                rows.RemoveAt(0);
            }

            foreach (var row in rows)
            {
                if (row.Length <= Math.Max(nameIdx, noIdx)) continue;
                if (string.IsNullOrWhiteSpace(row[nameIdx]) || string.IsNullOrWhiteSpace(row[noIdx])) continue;
                var wsName = row.Length > wsIdx ? row[wsIdx].Trim() : string.Empty;
                if (!workshopMap.TryGetValue(wsName, out var wsId)) continue;

                var item = new EmployeeItem { Name = row[nameIdx].Trim(), EmployeeNo = row[noIdx].Trim(), WorkshopId = wsId };
                if (idIdx >= 0 && row.Length > idIdx && int.TryParse(row[idIdx].Trim(), out var id))
                    item.Id = id;
                list.Add(item);
            }
            return list;
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出员工数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"员工管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetEmployeesAsync(_filterId, FilterWorkshopId, 1, 100000);
                var headers = new[] { "序号", "员工ID", "姓名", "工号", "所属车间" };
                var rows = all.Select((e, i) => new[]
                {
                    (i + 1).ToString(), e.Id.ToString(), e.Name, e.EmployeeNo, e.WorkshopName
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "员工管理", headers, rows);
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
                var total = await _service.GetEmployeeCountAsync(_filterId, FilterWorkshopId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载员工数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetEmployeeCountAsync(_filterId, FilterWorkshopId);
            var rows = await _service.GetEmployeesAsync(_filterId, FilterWorkshopId, page, PageSize);

            var items = new ObservableCollection<EmployeeItem>(
                rows.Select(e => new EmployeeItem
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmployeeNo = e.EmployeeNo,
                    WorkshopId = e.WorkshopId,
                    WorkshopName = e.WorkshopName
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
