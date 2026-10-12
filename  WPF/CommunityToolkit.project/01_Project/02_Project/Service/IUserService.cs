using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _02_Project.Service
{
    /// <summary>
    /// 用户管理服务接口：用户表（User）的增删改查（CRUD）
    /// 实现类位于 Service/lmp/UserService.cs
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 分页查询用户列表（可按用户ID精确过滤）
        /// </summary>
        /// <param name="userId">用户ID过滤，null 表示不过滤</param>
        /// <param name="pageIndex">页码（从 1 开始）</param>
        /// <param name="pageSize">每页条数</param>
        Task<List<UserDto>> GetUsersAsync(int? userId, int pageIndex, int pageSize);

        /// <summary>
        /// 获取用户总数（可按用户ID精确过滤，用于分页计算）
        /// </summary>
        Task<int> GetUserCountAsync(int? userId);

        /// <summary>
        /// 新增用户：账号查重 + 密码加密存储
        /// </summary>
        /// <param name="password">明文密码（内部加密后入库）</param>
        Task<AuthResult> AddUserAsync(string account, string password, int userTypeId, short status, string? remark, int operatorUserId);

        /// <summary>
        /// 修改用户：账号查重（排除自身）+ 可选重置密码（password 为空表示不修改密码）
        /// </summary>
        Task<AuthResult> UpdateUserAsync(int userId, string account, string? password, int userTypeId, short status, string? remark, int operatorUserId);

        /// <summary>
        /// 删除用户（不允许删除当前登录账号）
        /// </summary>
        Task<AuthResult> DeleteUserAsync(int userId, int operatorUserId);

        /// <summary>
        /// 获取全部用户类型（UserType 表，如 管理员/员工/经理/总经理/总裁）
        /// </summary>
        Task<List<UserTypeDto>> GetUserTypesAsync();
    }

    /// <summary>用户列表展示模型（对应 User 表 + UserType 名称）</summary>
    public class UserDto
    {
        public int UserId { get; set; }
        public string Account { get; set; } = string.Empty;
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = string.Empty;
        /// <summary>状态（0正常，1禁用）</summary>
        public short Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Remark { get; set; }
    }

    /// <summary>用户类型展示模型</summary>
    public class UserTypeDto
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = string.Empty;
    }
}
