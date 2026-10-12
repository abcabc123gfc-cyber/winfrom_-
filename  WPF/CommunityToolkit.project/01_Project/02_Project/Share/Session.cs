namespace _02_Project.Share
{
    /// <summary>
    /// 登录会话（静态持有当前登录用户信息，主窗口底部状态栏读取）
    /// </summary>
    public static class Session
    {
        /// <summary>当前登录用户 Id（登录成功后由 LoginViewModel 写入）</summary>
        public static int CurrentUserId { get; set; }

        /// <summary>当前登录用户账号（数据库不可用时兜底显示）</summary>
        public static string CurrentUserName { get; set; } = "未登录";

        /// <summary>当前登录用户类型 Id（UserType 表：1=管理员，其余为非管理员）</summary>
        public static int CurrentUserTypeId { get; set; }

        /// <summary>当前登录用户类型名称（如 管理员/员工/经理/总经理/总裁）</summary>
        public static string CurrentUserTypeName { get; set; } = string.Empty;

        /// <summary>是否管理员：只有管理员允许在“用户管理”页执行添加/修改/删除</summary>
        public static bool IsAdmin => CurrentUserTypeId == 1;
    }
}
