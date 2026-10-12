using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace _02_Project.ViewModel.UserControls.BasicSetting
{
    /// <summary>
    /// 车间管理 ViewModel（基础设置 - 车间管理）。
    /// MVVM：业务逻辑全部在此，通过构造函数注入 IBasicSettingService / IDialogService；
    /// 页面 XAML 仅负责绑定与命令。数据基于 EF Core（Workshop 表）。
    /// </summary>
    public partial class UcWorkshopManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IBasicSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;          // 查询过滤（null = 全部）
        private bool _isEdit;             // 当前表单：true=修改，false=新增
        private int _editingId;           // 修改目标 Id（新增时为 0）
        private bool _suppressPager;      // 程序赋值页码时防重入
        private int _loadedPage;          // 已加载页码（幂等防重入）

        public UcWorkshopManageViewModel(IBasicSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        // ===== 表格行模型 =====
        public class WorkshopItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        // ===== 可绑定属性 =====

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<WorkshopItem> _items = new();

        [ObservableProperty]
        private WorkshopItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        // ===== 命令 =====

        /// <summary>页面加载后初始化（hc:EventTrigger Loaded -> LoadCommand）</summary>
        [RelayCommand]
        private async Task LoadAsync()
        {
            IsAdmin = Session.IsAdmin;
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
                HintText = "请输入数字车间ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 车间ID = {id}";
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            _filterId = null;
            QueryId = string.Empty;
            HintText = string.Empty;
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
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not WorkshopItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            BeginEdit(item);
        }

        [RelayCommand]
        private void EditRow(WorkshopItem? item)
        {
            if (!IsAdmin || item == null) return;
            BeginEdit(item);
        }

        private void BeginEdit(WorkshopItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            Name = item.Name;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var name = Name.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                _dialog.ShowWarning("车间名称不能为空。");
                return;
            }

            AuthResult result = _isEdit
                ? await _service.UpdateWorkshopAsync(_editingId, name, Session.CurrentUserId)
                : await _service.AddWorkshopAsync(name, Session.CurrentUserId);

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
        private async Task DeleteRowAsync(WorkshopItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除车间 [{item.Name}] 吗？\n（存在生产线/工位引用时禁止删除）")) return;

            var r = await _service.DeleteWorkshopAsync(item.Id);
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
                Title = "导入车间数据",
                Filter = "Excel/CSV 文件 (*.xlsx;*.csv)|*.xlsx;*.csv|所有文件 (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = dlg.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? ExcelHelper.ReadExcel(dlg.FileName)
                    : ExcelHelper.ReadCsv(dlg.FileName);
                var imported = ParseRows(rows);

                var added = 0;
                var updated = 0;
                foreach (var item in imported)
                {
                    if (item.Id > 0 && await _service.GetWorkshopCountAsync(item.Id) > 0)
                    {
                        var r2 = await _service.UpdateWorkshopAsync(item.Id, item.Name, Session.CurrentUserId);
                        if (r2.Success) updated++;
                    }
                    else
                    {
                        var r1 = await _service.AddWorkshopAsync(item.Name, Session.CurrentUserId);
                        if (r1.Success) added++;
                    }
                }
                _dialog.ShowInfo($"导入完成：新增 {added} 条，更新 {updated} 条。");
                await ResetAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"导入失败：{ex.Message}");
            }
        }

        /// <summary>解析导入行：识别表头（含“车间ID”），否则按位置 [ID,车间名称]</summary>
        private List<WorkshopItem> ParseRows(List<string[]> rows)
        {
            var list = new List<WorkshopItem>();
            if (rows.Count == 0) return list;

            var idIdx = 0;
            var nameIdx = 1;
            if (rows[0].Any(c => c.Trim().Contains("车间ID", StringComparison.OrdinalIgnoreCase)))
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    var h = rows[0][i].Trim();
                    if (h.Contains("车间ID", StringComparison.OrdinalIgnoreCase) || h == "ID") idIdx = i;
                    else if (h.Contains("车间名称") || h.Contains("名称")) nameIdx = i;
                }
                rows.RemoveAt(0);
            }

            foreach (var row in rows)
            {
                if (row.Length <= nameIdx || string.IsNullOrWhiteSpace(row[nameIdx])) continue;
                var item = new WorkshopItem { Name = row[nameIdx].Trim() };
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
                Title = "导出车间数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"车间管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetWorkshopsAsync(_filterId, 1, 100000);
                var headers = new[] { "序号", "车间ID", "车间名称" };
                var rows = all.Select((w, i) => new[]
                {
                    (i + 1).ToString(), w.Id.ToString(), w.Name
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "车间管理", headers, rows);
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
            if (e.Info == _loadedPage) return;   // 幂等：程序赋值页码触发的重复事件直接跳过
            PageIndex = e.Info;
            await LoadPageAsync();
        }

        private async Task ReloadPagerAsync()
        {
            try
            {
                var total = await _service.GetWorkshopCountAsync(_filterId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载车间数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetWorkshopCountAsync(_filterId);
            var rows = await _service.GetWorkshopsAsync(_filterId, page, PageSize);

            var items = new ObservableCollection<WorkshopItem>(
                rows.Select(w => new WorkshopItem
                {
                    Id = w.Id,
                    Name = w.Name
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
