namespace _02_Project.Service
{
    /// <summary>
    /// 认证操作返回结果（登录 / 注册通用）
    /// </summary>
    public class AuthResult
    {
        /// <summary>是否成功</summary>
        public bool Success { get; set; }

        /// <summary>提示信息（失败原因 / 成功文案）</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>成功时对应的用户 Id</summary>
        public int UserId { get; set; }

        /// <summary>用户类型Id（登录时带出，1=管理员）</summary>
        public int UserTypeId { get; set; }

        /// <summary>用户类型名称（登录时带出，如 管理员/员工/经理）</summary>
        public string UserTypeName { get; set; } = string.Empty;

        public static AuthResult Ok(int userId, string message = "操作成功")
            => new() { Success = true, UserId = userId, Message = message };

        public static AuthResult Fail(string message)
            => new() { Success = false, Message = message };
    }
}
