using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace _02_Project.ViewModel.UserControls.BasicSetting
{
    /// <summary>
    /// 工组管理 ViewModel（基础设置 - 工组管理）。
    /// 子表单[员工姓名]：ObservableCollection&lt;SelectableOption&gt;，订阅勾选变化实时刷新“员工数量”。
    /// </summary>
    public partial class UcWorkGroupManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IBasicSettingService _service;
        private readonly IDialogService _dialog;

        private int? _filterId;
        private bool _isEdit;
        private int _editingId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcWorkGroupManageViewModel(IBasicSettingService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class GroupItem
        {
            public int Index { get; set; }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int MemberCount { get; set; }
            public string MembersText { get; set; } = string.Empty;
        }

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
        private ObservableCollection<GroupItem> _items = new();

        [ObservableProperty]
        private GroupItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        // ===== 子表单[员工姓名] =====

        [ObservableProperty]
        private ObservableCollection<SelectableOption> _employeeOptions = new();

        [ObservableProperty]
        private int _memberCount;

        /// <summary>加载全部员工勾选列表；selectedIds 为编辑时已选员工（回显勾选）</summary>
        private async Task LoadEmployeeOptionsAsync(List<int>? selectedIds)
        {
            // 退订旧选项，避免内存泄漏与误触发
            foreach (var o in EmployeeOptions)
                o.PropertyChanged -= OnEmployeeOptionChanged;

            var selected = selectedIds ?? new List<int>();
            var options = (await _service.GetEmployeeOptionsAsync(null))
                .Select(o => new SelectableOption
                {
                    Id = o.Id,
                    Text = o.Text,
                    IsSelected = selected.Contains(o.Id)
                })
                .ToList();

            var list = new ObservableCollection<SelectableOption>(options);
            foreach (var o in list)
                o.PropertyChanged += OnEmployeeOptionChanged;

            EmployeeOptions = list;
            RefreshMemberCount();
        }

        private void OnEmployeeOptionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectableOption.IsSelected))
                RefreshMemberCount();
        }

        /// <summary>员工数量 = 子表单勾选行数</summary>
        private void RefreshMemberCount()
        {
            MemberCount = EmployeeOptions.Count(x => x.IsSelected);
        }

        private List<int> GetSelectedEmployeeIds()
        {
            return EmployeeOptions.Where(x => x.IsSelected).Select(x => x.Id).ToList();
        }

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
                HintText = "请输入数字工组ID";
                return;
            }
            _filterId = id;
            HintText = $"仅显示 工组ID = {id}";
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
            Name = string.Empty;
            await LoadEmployeeOptionsAsync(selectedIds: null);
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not GroupItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            _ = BeginEditAsync(item);
        }

        [RelayCommand]
        private void EditRow(GroupItem? item)
        {
            if (!IsAdmin || item == null) return;
            _ = BeginEditAsync(item);
        }

        private async Task BeginEditAsync(GroupItem item)
        {
            _editingId = item.Id;
            _isEdit = true;
            Name = item.Name;
            var members = await _service.GetWorkGroupEmployeeOptionsAsync(item.Id);
            await LoadEmployeeOptionsAsync(members.Select(m => m.Id).ToList());
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var name = Name.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                _dialog.ShowWarning("工组名称不能为空。");
                return;
            }

            var employeeIds = GetSelectedEmployeeIds();
            AuthResult result = _isEdit
                ? await _service.UpdateWorkGroupAsync(_editingId, name, employeeIds, Session.CurrentUserId)
                : await _service.AddWorkGroupAsync(name, employeeIds, Session.CurrentUserId);

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
        private async Task DeleteRowAsync(GroupItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除工组 [{item.Name}] 吗？\n（存在报工单引用时禁止删除）")) return;

            var r = await _service.DeleteWorkGroupAsync(item.Id);
            if (r.Success) _dialog.ShowInfo(r.Message);
            else _dialog.ShowWarning(r.Message);
            if (r.Success) await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ExportAsync()
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出工组数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"工组管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetWorkGroupsAsync(_filterId, 1, 100000);
                var headers = new[] { "序号", "工组ID", "工组名称", "员工数量", "组内员工" };
                var rows = all.Select((g, i) => new[]
                {
                    (i + 1).ToString(), g.Id.ToString(), g.Name, g.MemberCount.ToString(), g.MembersText
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "工组管理", headers, rows);
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
                var total = await _service.GetWorkGroupCountAsync(_filterId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载工组数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetWorkGroupCountAsync(_filterId);
            var rows = await _service.GetWorkGroupsAsync(_filterId, page, PageSize);

            var items = new ObservableCollection<GroupItem>(
                rows.Select(g => new GroupItem
                {
                    Id = g.Id,
                    Name = g.Name,
                    MemberCount = g.MemberCount,
                    MembersText = g.MembersText
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
