using _02_Project.DBContext;
using _02_Project.Model.UserManage;
using _02_Project.Share;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 用户管理服务实现：基于 EF Core 操作 User / UserType 表
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public UserService(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<UserDto>> GetUsersAsync(int? userId, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var query = db.Users
                .Join(db.UserTypes,
                    u => u.UserTypeId,
                    t => t.UserTypeId,
                    (u, t) => new UserDto
                    {
                        UserId = u.UserId,
                        Account = u.Account,
                        UserTypeId = u.UserTypeId,
                        UserTypeName = t.UserTypeName,
                        Status = u.Status,
                        CreateTime = u.CreateTime,
                        Remark = u.Remark
                    });

            if (userId.HasValue)
                query = query.Where(u => u.UserId == userId.Value);

            return await query
                .OrderBy(u => u.UserId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetUserCountAsync(int? userId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var query = db.Users.AsQueryable();
            if (userId.HasValue)
                query = query.Where(u => u.UserId == userId.Value);

            return await query.CountAsync();
        }

        public async Task<AuthResult> AddUserAsync(string account, string password, int userTypeId, short status, string? remark, int operatorUserId)
        {
            var name = account?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
                return AuthResult.Fail("账号不能为空");
            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("密码不能为空");
            if (userTypeId <= 0)
                return AuthResult.Fail("请选择用户类型");

            await using var db = await _dbFactory.CreateDbContextAsync();

            // 账号唯一性校验
            if (await db.Users.AnyAsync(u => u.Account == name))
                return AuthResult.Fail($"账号 [{name}] 已存在");

            var now = DateTime.Now;
            var user = new User
            {
                Account = name,
                Password = Cryptography.Encrypt(password),   // 与登录/注册一致的加密方式
                UserTypeId = userTypeId,
                Status = status,
                Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim(),
                CreateUserId = operatorUserId,
                CreateTime = now
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return AuthResult.Ok(user.UserId, $"新增用户 [{name}] 成功");
        }

        public async Task<AuthResult> UpdateUserAsync(int userId, string account, string? password, int userTypeId, short status, string? remark, int operatorUserId)
        {
            var name = account?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(name))
                return AuthResult.Fail("账号不能为空");
            if (userTypeId <= 0)
                return AuthResult.Fail("请选择用户类型");

            await using var db = await _dbFactory.CreateDbContextAsync();

            var user = await db.Users.FindAsync(userId);
            if (user == null)
                return AuthResult.Fail("用户不存在或已被删除");

            // 账号唯一性校验（排除自身）
            if (await db.Users.AnyAsync(u => u.Account == name && u.UserId != userId))
                return AuthResult.Fail($"账号 [{name}] 已存在");

            user.Account = name;
            if (!string.IsNullOrWhiteSpace(password))          // 密码留空表示不修改
                user.Password = Cryptography.Encrypt(password);
            user.UserTypeId = userTypeId;
            user.Status = status;
            user.Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim();
            user.LastUpdateUserId = operatorUserId;
            user.LastUpdateTime = DateTime.Now;

            await db.SaveChangesAsync();

            return AuthResult.Ok(user.UserId, $"修改用户 [{name}] 成功");
        }

        public async Task<AuthResult> DeleteUserAsync(int userId, int operatorUserId)
        {
            if (userId == operatorUserId)
                return AuthResult.Fail("不能删除当前登录账号");

            await using var db = await _dbFactory.CreateDbContextAsync();

            var user = await db.Users.FindAsync(userId);
            if (user == null)
                return AuthResult.Fail("用户不存在或已被删除");

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return AuthResult.Ok(userId, $"删除用户 [{user.Account}] 成功");
        }

        public async Task<List<UserTypeDto>> GetUserTypesAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            return await db.UserTypes
                .Where(t => t.Status == 0)     // 仅返回启用中的类型
                .OrderBy(t => t.UserTypeId)
                .Select(t => new UserTypeDto
                {
                    UserTypeId = t.UserTypeId,
                    UserTypeName = t.UserTypeName
                })
                .ToListAsync();
        }
    }
}
