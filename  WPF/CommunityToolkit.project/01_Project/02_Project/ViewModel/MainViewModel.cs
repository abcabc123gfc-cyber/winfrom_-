using _02_Project.DBContext;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.ViewModel
{
    /// <summary>
    /// 主窗口 ViewModel：提供底部状态栏“当前登录人”等信息。
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public MainViewModel(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>当前登录人（显示在底部状态栏）</summary>
        [ObservableProperty]
        private string _currentUserName = "未登录";

        /// <summary>从数据库加载当前登录用户（登录成功后 Session 中已有 CurrentUserId）</summary>
        public async Task LoadCurrentUserAsync()
        {
            try
            {
                if (Session.CurrentUserId <= 0)
                {
                    CurrentUserName = Session.CurrentUserName;
                    return;
                }
                await using var db = await _dbFactory.CreateDbContextAsync();
                var user = await db.Users.FindAsync(Session.CurrentUserId);
                CurrentUserName = user?.Account ?? $"用户#{Session.CurrentUserId}";

                // 用数据库最新数据刷新会话中的用户类型（权限判断依据）
                if (user != null)
                {
                    Session.CurrentUserName = user.Account;
                    Session.CurrentUserTypeId = user.UserTypeId;
                }
            }
            catch
            {
                // 数据库不可用时，使用登录时记录的账号兜底，不影响主界面使用
                CurrentUserName = Session.CurrentUserName;
            }
        }
    }
}
