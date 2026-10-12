using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace _02_Project.ViewModel.UserControls.UserManage
{
    /// <summary>
    /// 用户管理 ViewModel（系统用户账号管理）。
    /// 注入 IUserService / IDialogService。密码字段为 PasswordBox 例外：
    /// 由于 WPF PasswordBox 不支持绑定，由 code-behind 的 PasswordChanged 事件桥接到 PasswordInput 属性
    /// （业务校验仍全部在 ViewModel 完成）。
    /// </summary>
    public partial class UcUserManageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IUserService _service;
        private readonly IDialogService _dialog;

        private int? _filterUserId;
        private bool _isEdit;
        private int _editingUserId;
        private bool _suppressPager;
        private int _loadedPage;

        public UcUserManageViewModel(IUserService service, IDialogService dialog)
        {
            _service = service;
            _dialog = dialog;
        }

        public class UserItem
        {
            public int Index { get; set; }
            public int UserId { get; set; }
            public string Account { get; set; } = string.Empty;
            public int UserTypeId { get; set; }
            public string UserTypeName { get; set; } = string.Empty;
            public short Status { get; set; }
            public string StatusText => Status == 0 ? "正常" : "禁用";
            public DateTime CreateTime { get; set; }
            public string? Remark { get; set; }
        }

        /// <summary>导入行（解析用）</summary>
        private class ImportRow
        {
            public int UserId { get; set; }
            public string Account { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public int UserTypeId { get; set; }
            public short Status { get; set; }
            public string? Remark { get; set; }
        }

        [ObservableProperty]
        private string _queryId = string.Empty;

        [ObservableProperty]
        private string _hintText = string.Empty;

        [ObservableProperty]
        private string _account = string.Empty;

        /// <summary>密码（由 code-behind PasswordChanged 桥接；修改时留空表示不修改密码）</summary>
        [ObservableProperty]
        private string _passwordInput = string.Empty;

        [ObservableProperty]
        private string _pwdHintText = "必填";

        [ObservableProperty]
        private List<UserTypeDto> _typeOptions = new();

        [ObservableProperty]
        private int _selectedTypeId = 2;

        [ObservableProperty]
        private List<OptionDto> _statusOptions = new()
        {
            new OptionDto { Id = 0, Text = "正常" },
            new OptionDto { Id = 1, Text = "禁用" }
        };

        [ObservableProperty]
        private short _selectedStatus;

        [ObservableProperty]
        private string _remark = string.Empty;

        [ObservableProperty]
        private bool _editPanelVisible;

        [ObservableProperty]
        private bool _isAdmin = Session.IsAdmin;

        [ObservableProperty]
        private ObservableCollection<UserItem> _items = new();

        [ObservableProperty]
        private UserItem? _selectedItem;

        [ObservableProperty]
        private string _totalText = "共 0 条记录";

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private int _maxPageCount = 1;

        [RelayCommand]
        private async Task LoadAsync()
        {
            IsAdmin = Session.IsAdmin;
            TypeOptions = await _service.GetUserTypesAsync();
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
                HintText = "请输入数字用户ID";
                return;
            }
            _filterUserId = id;
            HintText = $"仅显示 用户ID = {id}";
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private async Task ResetAsync()
        {
            _filterUserId = null;
            QueryId = string.Empty;
            HintText = string.Empty;
            PageIndex = 1;
            await ReloadPagerAsync();
        }

        [RelayCommand]
        private void Add()
        {
            if (!IsAdmin) return;
            _editingUserId = 0;
            _isEdit = false;
            Account = string.Empty;
            PasswordInput = string.Empty;   // 通过属性桥接清空 PasswordBox
            PwdHintText = "必填";
            SelectedTypeId = 2;             // 默认员工
            SelectedStatus = 0;             // 默认正常
            Remark = string.Empty;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private void Edit()
        {
            if (!IsAdmin) return;
            if (SelectedItem is not UserItem item)
            {
                _dialog.ShowInfo("请先在表格中选中一行数据。");
                return;
            }
            BeginEdit(item);
        }

        [RelayCommand]
        private void EditRow(UserItem? item)
        {
            if (!IsAdmin || item == null) return;
            BeginEdit(item);
        }

        private void BeginEdit(UserItem item)
        {
            _editingUserId = item.UserId;
            _isEdit = true;
            Account = item.Account;
            PasswordInput = string.Empty;   // 修改时留空 = 不修改密码
            PwdHintText = "留空不改";
            SelectedTypeId = item.UserTypeId;
            SelectedStatus = item.Status;
            Remark = item.Remark ?? string.Empty;
            EditPanelVisible = true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var account = Account.Trim();
            if (string.IsNullOrWhiteSpace(account))
            {
                _dialog.ShowWarning("账号不能为空。");
                return;
            }

            AuthResult result;
            if (_isEdit)
            {
                // 修改：密码留空表示不修改
                var password = string.IsNullOrWhiteSpace(PasswordInput) ? null : PasswordInput.Trim();
                result = await _service.UpdateUserAsync(_editingUserId, account, password, SelectedTypeId, SelectedStatus, string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim(), Session.CurrentUserId);
            }
            else
            {
                var password = PasswordInput.Trim();
                if (string.IsNullOrWhiteSpace(password))
                {
                    _dialog.ShowWarning("新增用户必须填写密码。");
                    return;
                }
                result = await _service.AddUserAsync(account, password, SelectedTypeId, SelectedStatus, string.IsNullOrWhiteSpace(Remark) ? null : Remark.Trim(), Session.CurrentUserId);
            }

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
            _editingUserId = 0;
            _isEdit = false;
            EditPanelVisible = false;
            PasswordInput = string.Empty;
        }

        [RelayCommand]
        private async Task DeleteRowAsync(UserItem? item)
        {
            if (!IsAdmin || item == null) return;
            if (!_dialog.Confirm($"确定删除用户 [{item.Account}] 吗？\n（不允许删除当前登录账号）")) return;

            var r = await _service.DeleteUserAsync(item.UserId, Session.CurrentUserId);
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
                Title = "导入用户数据",
                Filter = "Excel/CSV 文件 (*.xlsx;*.csv)|*.xlsx;*.csv|所有文件 (*.*)|*.*"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var rows = dlg.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)
                    ? ExcelHelper.ReadExcel(dlg.FileName)
                    : ExcelHelper.ReadCsv(dlg.FileName);
                var types = await _service.GetUserTypesAsync();
                var typeMap = types.ToDictionary(t => t.UserTypeName, t => t.UserTypeId);
                var imported = ParseRows(rows, typeMap);

                var added = 0;
                var updated = 0;
                foreach (var item in imported)
                {
                    var exists = await _service.GetUsersAsync(item.UserId, 1, 1);
                    if (exists.Count > 0)
                    {
                        // 已存在则更新（密码不改）
                        var r2 = await _service.UpdateUserAsync(item.UserId, item.Account, null,
                            item.UserTypeId, item.Status, item.Remark, Session.CurrentUserId);
                        if (r2.Success) updated++;
                    }
                    else
                    {
                        // 不存在则新增（导入无密码列时使用默认密码）
                        var r1 = await _service.AddUserAsync(item.Account, item.Password,
                            item.UserTypeId, item.Status, item.Remark, Session.CurrentUserId);
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

        /// <summary>
        /// 解析导入行：优先识别表头（含“用户ID”），否则按位置 [ID,账号,密码,类型,状态,备注]；
        /// 密码列缺失时使用默认密码 “123456”；类型按名称匹配 UserType 表，未知类型默认“员工”。
        /// </summary>
        private List<ImportRow> ParseRows(List<string[]> rows, Dictionary<string, int> typeMap)
        {
            var list = new List<ImportRow>();
            if (rows.Count == 0) return list;

            var idIdx = 0; var accIdx = 1; var pwdIdx = -1;
            var typeIdx = 2; var statusIdx = 3; var remarkIdx = 4;
            if (rows[0].Any(c => c.Trim().Contains("用户ID", StringComparison.OrdinalIgnoreCase)))
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    var h = rows[0][i].Trim();
                    if (h.Contains("用户ID", StringComparison.OrdinalIgnoreCase)) idIdx = i;
                    else if (h == "账号" || h.Contains("用户名")) accIdx = i;
                    else if (h == "密码") pwdIdx = i;
                    else if (h.Contains("用户类型") || h.Contains("类型")) typeIdx = i;
                    else if (h == "状态") statusIdx = i;
                    else if (h == "备注") remarkIdx = i;
                }
                rows.RemoveAt(0);
            }

            foreach (var row in rows)
            {
                if (row.Length <= Math.Max(idIdx, accIdx)) continue;
                if (!int.TryParse(row[idIdx].Trim(), out var id)) continue;
                var account = row[accIdx].Trim();
                if (string.IsNullOrWhiteSpace(account)) continue;

                var item = new ImportRow { UserId = id, Account = account };
                item.Password = pwdIdx >= 0 && row.Length > pwdIdx && !string.IsNullOrWhiteSpace(row[pwdIdx])
                    ? row[pwdIdx].Trim()
                    : "123456";
                var typeName = row.Length > typeIdx ? row[typeIdx].Trim() : string.Empty;
                item.UserTypeId = typeMap.TryGetValue(typeName, out var tid) ? tid
                    : (typeMap.TryGetValue("员工", out var empId) ? empId : 2);
                var statusText = row.Length > statusIdx ? row[statusIdx].Trim() : string.Empty;
                item.Status = statusText == "禁用" || statusText == "1" ? (short)1 : (short)0;
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
                Title = "导出用户数据",
                Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv",
                FileName = $"用户管理_{DateTime.Now:yyyyMMddHHmmss}"
            };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var all = await _service.GetUsersAsync(_filterUserId, 1, 100000);
                var headers = new[] { "序号", "用户ID", "账号", "用户类型", "状态", "创建时间", "备注" };
                var rows = all.Select((u, i) => new[]
                {
                    (i + 1).ToString(), u.UserId.ToString(), u.Account, u.UserTypeName,
                    u.Status == 0 ? "正常" : "禁用",
                    u.CreateTime.ToString("yyyy-MM-dd HH:mm"), u.Remark ?? string.Empty
                });
                if (dlg.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    ExcelHelper.ExportToCsv(dlg.FileName, headers, rows);
                else
                    ExcelHelper.ExportToExcel(dlg.FileName, "用户管理", headers, rows);
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
                var total = await _service.GetUserCountAsync(_filterUserId);
                var pages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                _suppressPager = true;
                MaxPageCount = pages;
                PageIndex = Math.Clamp(PageIndex, 1, pages);
                _suppressPager = false;
                await LoadPageAsync();
            }
            catch (Exception ex)
            {
                _dialog.ShowError($"加载用户数据失败：{ex.Message}");
            }
        }

        private async Task LoadPageAsync()
        {
            var page = Math.Clamp(PageIndex, 1, Math.Max(1, MaxPageCount));
            _loadedPage = page;
            var total = await _service.GetUserCountAsync(_filterUserId);
            var rows = await _service.GetUsersAsync(_filterUserId, page, PageSize);

            var items = new ObservableCollection<UserItem>(
                rows.Select(u => new UserItem
                {
                    UserId = u.UserId,
                    Account = u.Account,
                    UserTypeId = u.UserTypeId,
                    UserTypeName = u.UserTypeName,
                    Status = u.Status,
                    CreateTime = u.CreateTime,
                    Remark = u.Remark
                }));
            for (var i = 0; i < items.Count; i++)
                items[i].Index = (page - 1) * PageSize + i + 1;

            Items = items;
            TotalText = $"共 {total} 条记录，当前第 {page} / {Math.Max(1, MaxPageCount)} 页";
        }
    }
}
