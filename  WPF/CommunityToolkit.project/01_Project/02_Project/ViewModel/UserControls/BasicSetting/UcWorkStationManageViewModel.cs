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
    /// 工位管理 ViewModel（基础设置 - 工位管理）。
    /// 联动：表单选择车间 -> 生产线下拉只显示该车间生产线（服务层保存时再校验“生产线必须属于所选车间”）。
    /// </summary>
    public partial class UcWorkStationManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IBasicSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcWorkStationManageViewModel(IBasicSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class StationItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int WorkshopId { get; set; }
            public string WorkshopName { get; set; } = string.Empty;
            public int ProductionLineId { get; set; }
            public string ProductionLineName { get; set; } = string.Empty;
            public string? Remark { get; set; }
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _remark = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<StationItem> _items = new();

        [ObservableProperty]
        private StationItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        // ===== 筛选下拉 =====

        [ObservableProperty]
        private List<OptionDto> _filterWorkshopOptions = new();

        [ObservableProperty]
        private int? _filterWorkshopId;

        // ===== 表单下拉（车间 -> 生产线联动） =====

        [ObservableProperty]
        private List<OptionDto> _workshopOptions = new();

        [ObservableProperty]
        private int _selectedWorkshopId;

        [ObservableProperty]
        private List<OptionDto> _formLineOptions = new();

        [ObservableProperty]
        private int _selectedLineId;

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

        /// <summary>表单选择车间：生产线下拉联动刷新</summary>
        partial void OnSelectedWorkshopIdChanged(int value)
        {
            _ = HandleWorkshopChangedAsync(value);
        }

        private async Task HandleWorkshopChangedAsync(int value)
        {
            if (value <= 0)
            {
                FormLineOptions = new List<OptionDto>();
                SelectedLineId = 0;
                return;
            }
            var lines = await _service.GetProductionLineOptionsAsync(value);
            FormLineOptions = lines;
            if (lines.Count > 0) SelectedLineId = lines[0].Id;
            else SelectedLineId = 0;
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
                HintText = "请输入数字工位ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 工位ID = {id}";
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
            Remark = string.Empty;
            SelectedWorkshopId = WorkshopOptions.Count > 0 ? WorkshopOptions[0].Id : 0;   // 触发生产线联动
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not StationItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            BeginEdit(item);
        }

        [RelayCommand]
        private void EditRow(StationItem? item)
        {
            if (!IsAdmin || item == null) return;
            BeginEdit(item);
        }

        private void BeginEdit(StationItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            Name = item.Name;
            Remark = item.Remark ?? string.Empty;
            SelectedWorkshopId = item.WorkshopId;    // 触发生产线联动
            SelectedLineId = item.ProductionLineId;  // 联动后回填原生产线
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var name = Name.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                _dialog.ShowWarning("工位名称不能为空。");
                return;
            }
            if (SelectedWorkshopId <= 0)
            {
                _dialog.ShowWarning("请选择所属车间。");
                return;
            }
            if (SelectedLineId <= 0)
            {
                _dialog.ShowWarning("请选择所属生产线。");
                return;
            }
            var remark = string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim();

            AuthResult result = _isEdit
                ? await _service.UpdateWorkStationAsync(_editingId, name, SelectedWorkshopId, SelectedLineId, remark, Session.CurrentUserId)
                : await _service.AddWorkStationAsync(name, SelectedWorkshopId, SelectedLineId, remark, Session.CurrentUserId);

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
        private async Task DeleteRowAsync(StationItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除工位 [{item.Name}] 吗？\n（被工序绑定或存在报工记录时禁止删除）")) return;

            var r = await _service.DeleteWorkStationAsync(item.Id);
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
                Title = "导入工位数据",
                Filter = "Excel/CSV 文件 (*.xlsx;*.csv)|*.xlsx;*.csv|所有文件 (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = dlg.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? ExcelHelper.ReadExcel(dlg.FileName)
                    : ExcelHelper.ReadCsv(dlg.FileName);
                var workshopMap = (await _service.GetWorkshopOptionsAsync()).ToDictionary(o => o.Text, o => o.Id);
                var lineMap = (await _service.GetProductionLineOptionsAsync(null)).ToDictionary(o => o.Text, o => o.Id);
                var imported = ParseRows(rows, workshopMap, lineMap);

                var added = 0;
                var updated = 0;
                foreach (var item in imported)
                {
                    if (item.Id > 0 && await _service.GetWorkStationCountAsync(item.Id, null, null) > 0)
                    {
                        var r2 = await _service.UpdateWorkStationAsync(item.Id, item.Name, item.WorkshopId, item.ProductionLineId, item.Remark, Session.CurrentUserId);
                        if (r2.Success) updated++;
                    }
                    else
                    {
                        var r1 = await _service.AddWorkStationAsync(item.Name, item.WorkshopId, item.ProductionLineId, item.Remark, Session.CurrentUserId);
                        if (r1.Success) added++;
                    }
                }
                _dialog.ShowInfo($"导入完成：新增 {added} 条，更新 {updated} 条。\n（车间/生产线名称不存在的行已跳过）");
                await ResetAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导入失败：{ex.Message}");
            }
        }

        /// <summary>解析导入行：识别表头（含“工位ID”），否则按位置 [ID,工位名称,车间名称,生产线名称,备注]</summary>
        private List<StationItem> ParseRows(List<string[]> rows, Dictionary<string, int> workshopMap, Dictionary<string, int> lineMap)
        {
            var list = new List<StationItem>();
            if (rows.Count == 0) return list;

            var idIdx = 0;
            var nameIdx = 1;
            var wsIdx = 2;
            var lineIdx = 3;
            var remarkIdx = 4;
            if (rows[0].Any(c => c.Trim().Contains("工位ID", StringComparison.OrdinalIgnoreCase)))
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    var h = rows[0][i].Trim();
                    if (h.Contains("工位ID", StringComparison.OrdinalIgnoreCase) || h == "ID") idIdx = i;
                    else if (h.Contains("工位名称") || h.Contains("名称")) nameIdx = i;
                    else if (h.Contains("车间")) wsIdx = i;
                    else if (h.Contains("生产线")) lineIdx = i;
                    else if (h == "备注") remarkIdx = i;
                }
                rows.RemoveAt(0);
            }

            foreach (var row in rows)
            {
                if (row.Length <= nameIdx || string.IsNullOrWhiteSpace(row[nameIdx])) continue;
                var wsName = row.Length > wsIdx ? row[wsIdx].Trim() : string.Empty;
                var lineName = row.Length > lineIdx ? row[lineIdx].Trim() : string.Empty;
                if (!workshopMap.TryGetValue(wsName, out var wsId)) continue;
                if (!lineMap.TryGetValue(lineName, out var lineId)) continue;

                var item = new StationItem
                {
                    Name = row[nameIdx].Trim(),
                    WorkshopId = wsId,
                    ProductionLineId = lineId
                };
                if (idIdx >= 0 && row.Length > idIdx && int.TryParse(row[idIdx].Trim(), out var id))
                    item.Id = id;
                if (row.Length > remarkIdx && !string.IsNullOrWhiteSpace(row[remarkIdx]))
                    item.Remark = row[remarkIdx].Trim();
                list.Add(item);
            }
            return list;
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出工位数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"工位管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetWorkStationsAsync(_filterId, FilterWorkshopId, null, 1, 100000);
                var headers = new[] { "序号", "工位ID", "工位名称", "所属车间", "所属生产线", "备注" };
                var rows = all.Select((s, i) => new[]
                {
                    (i + 1).ToString(), s.Id.ToString(), s.Name, s.WorkshopName, s.ProductionLineName,
                    s.Remark ?? string.Empty
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "工位管理", headers, rows);
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
                var total = await _service.GetWorkStationCountAsync(_filterId, FilterWorkshopId, null);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载工位数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetWorkStationCountAsync(_filterId, FilterWorkshopId, null);
            var rows = await _service.GetWorkStationsAsync(_filterId, FilterWorkshopId, null, page, PageSize);

            var items = new ObservableCollection<StationItem>(
                rows.Select(s => new StationItem
                {
                    Id = s.Id,
                    Name = s.Name,
                    WorkshopId = s.WorkshopId,
                    WorkshopName = s.WorkshopName,
                    ProductionLineId = s.ProductionLineId,
                    ProductionLineName = s.ProductionLineName,
                    Remark = s.Remark
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
