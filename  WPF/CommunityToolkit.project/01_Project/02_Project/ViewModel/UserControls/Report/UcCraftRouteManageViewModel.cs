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
    /// 工艺路线管理 ViewModel（报工管理 - 工艺路线管理）。
    /// 子表单[工艺流程]：工序 + 顺序 + 备注 的可编辑明细行（DataGrid 内直接编辑，保存时整体提交）。
    /// </summary>
    public partial class UcCraftRouteManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IReportSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcCraftRouteManageViewModel(IReportSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class RouteItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string RouteNo { get; set; } = string.Empty;
            public string RouteName { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public string? Remark { get; set; }
            public int DetailCount { get; set; }
        }

        /// <summary>工艺流程子表单行模型（DataGrid 行内编辑）</summary>
        public partial class CraftRouteDetailRow : ObservableObject
        {
            [ObservableProperty]
            private int _processId;

            [ObservableProperty]
            private string _processName = string.Empty;

            [ObservableProperty]
            private int _sortOrder;

            [ObservableProperty]
            private string? _remark;

            public CraftRouteDetailDto ToDto() => new()
            {
                ProcessId = ProcessId,
                ProcessName = ProcessName,
                SortOrder = SortOrder,
                Remark = string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim()
            };
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _routeNo = string.Empty;

        [ObservableProperty]
        private string _routeName = string.Empty;

        [ObservableProperty]
        private string _productName = string.Empty;

        [ObservableProperty]
        private string _remark = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<RouteItem> _items = new();

        [ObservableProperty]
        private RouteItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        // ===== 工艺流程子表单 =====

        [ObservableProperty]
        private List<OptionDto> _processOptions = new();

        [ObservableProperty]
        private ObservableCollection<CraftRouteDetailRow> _details = new();

        [ObservableProperty]
        private CraftRouteDetailRow? _selectedDetail;

        [RelayCommand]
        private async Task LoadAsync()
        {
            IsAdmin = Session.IsAdmin;
            ProcessOptions = await _service.GetProcessOptionsAsync();
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
                HintText = "请输入数字路线ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 路线ID = {id}";
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
        private async Task AddAsync()
        {
            if (!IsAdmin) return;
            _editingId = 0;
            _isEdit = false;
            RouteNo = string.Empty;
            RouteName = string.Empty;
            ProductName = string.Empty;
            Remark = string.Empty;
            Details = new ObservableCollection<CraftRouteDetailRow>();
            await EnsureProcessOptionsAsync();
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not RouteItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            _ = BeginEditAsync(item);
        }

        [RelayCommand]
        private void EditRow(RouteItem? item)
        {
            if (!IsAdmin || item == null) return;
            _ = BeginEditAsync(item);
        }

        private async Task BeginEditAsync(RouteItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            RouteNo = item.RouteNo;
            RouteName = item.RouteName;
            ProductName = item.ProductName;
            Remark = item.Remark ?? string.Empty;
            await EnsureProcessOptionsAsync();
            var rows = await _service.GetCraftRouteDetailsAsync(item.Id);
            Details = new ObservableCollection<CraftRouteDetailRow>(
                rows.Select(r => new CraftRouteDetailRow
                {
                    ProcessId = r.ProcessId,
                    ProcessName = r.ProcessName,
                    SortOrder = r.SortOrder,
                    Remark = r.Remark
                }));
            EditPanelVisible = true;
        }

        private async Task EnsureProcessOptionsAsync()
        {
            if (ProcessOptions.Count == 0)
                ProcessOptions = await _service.GetProcessOptionsAsync();
        }

        [RelayCommand]
        private void AddDetail()
        {
            var nextOrder = (Details.Count == 0) ? 1 : Details.Max(d => d.SortOrder) + 1;
            Details.Add(new CraftRouteDetailRow
            {
                ProcessId = ProcessOptions.Count > 0 ? ProcessOptions[0].Id : 0,
                ProcessName = ProcessOptions.Count > 0 ? ProcessOptions[0].Text : string.Empty,
                SortOrder = nextOrder
            });
        }

        [RelayCommand]
        private void RemoveDetail()
        {
            if (SelectedDetail is null)
            {
                _dialog.ShowInfo("请先在工艺流程子表单中选中一行。");
                return;
            }
            Details.Remove(SelectedDetail);
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(RouteNo.Trim()))
            {
                _dialog.ShowWarning("路线编号不能为空。");
                return;
            }
            if (string.IsNullOrWhiteSpace(RouteName.Trim()))
            {
                _dialog.ShowWarning("工艺名称不能为空。");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProductName.Trim()))
            {
                _dialog.ShowWarning("成品名称不能为空。");
                return;
            }

            var details = Details.Select(d => d.ToDto()).ToList();
            if (details.Any(d => d.ProcessId <= 0))
            {
                _dialog.ShowWarning("工艺流程中存在未选择工序的行，请补全或删除。");
                return;
            }

            var remark = string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim();
            AuthResult result = _isEdit
                ? await _service.UpdateCraftRouteAsync(_editingId, RouteNo.Trim(), RouteName.Trim(), ProductName.Trim(), remark, details, Session.CurrentUserId)
                : await _service.AddCraftRouteAsync(RouteNo.Trim(), RouteName.Trim(), ProductName.Trim(), remark, details, Session.CurrentUserId);

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
            Details = new ObservableCollection<CraftRouteDetailRow>();
        }

        [RelayCommand]
        private async Task DeleteRowAsync(RouteItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除工艺路线 [{item.RouteName}] 吗？\n（明细随主表级联删除）")) return;

            var r = await _service.DeleteCraftRouteAsync(item.Id);
            if (r.Success) _dialog.ShowInfo(r.Message);
            else _dialog.ShowWarning(r.Message);
            if (r.Success) await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出工艺路线数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"工艺路线管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetCraftRoutesAsync(_filterId, 1, 100000);
                var headers = new[] { "序号", "路线ID", "路线编号", "工艺名称", "成品名称", "工序数", "备注" };
                var rows = all.Select((r, i) => new[]
                {
                    (i + 1).ToString(), r.Id.ToString(), r.RouteNo, r.RouteName,
                    r.ProductName, r.DetailCount.ToString(), r.Remark ?? string.Empty
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "工艺路线管理", headers, rows);
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
                var total = await _service.GetCraftRouteCountAsync(_filterId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载工艺路线数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetCraftRouteCountAsync(_filterId);
            var rows = await _service.GetCraftRoutesAsync(_filterId, page, PageSize);

            var items = new ObservableCollection<RouteItem>(
                rows.Select(r => new RouteItem
                {
                    Id = r.Id,
                    RouteNo = r.RouteNo,
                    RouteName = r.RouteName,
                    ProductName = r.ProductName,
                    Remark = r.Remark,
                    DetailCount = r.DetailCount
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
