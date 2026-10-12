using _02_Project.DBContext;
using _02_Project.Model.UserManage;
using _02_Project.Share;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 认证服务实现：基于 EF Core 操作 User 表
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public AuthService(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<AuthResult> LoginAsync(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account))
                return AuthResult.Fail("用户名不能为空");
            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("密码不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();

            // 联表查询：带出用户类型名称
            var matched = await db.Users
                .Join(db.UserTypes,
                    u => u.UserTypeId,
                    t => t.UserTypeId,
                    (u, t) => new { User = u, TypeName = t.UserTypeName })
                .FirstOrDefaultAsync(x => x.User.Account == account.Trim());
            if (matched == null)
                return AuthResult.Fail("用户不存在");

            if (matched.User.Status == 1)
                return AuthResult.Fail("该账号已被禁用");

            // 与注册时一致：对输入明文加密后再比对
            var encrypted = Cryptography.Encrypt(password);
            if (!string.Equals(matched.User.Password, encrypted, StringComparison.Ordinal))
                return AuthResult.Fail("用户名或密码错误");

            var result = AuthResult.Ok(matched.User.UserId, "登录成功");
            result.UserTypeId = matched.User.UserTypeId;
            result.UserTypeName = matched.TypeName;
            return result;
        }

        public async Task<AuthResult> RegisterAsync(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account))
                return AuthResult.Fail("用户名不能为空");
            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("密码不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();

            // 用户名不能重复
            var exists = await db.Users.AnyAsync(u => u.Account == account.Trim());
            if (exists)
                return AuthResult.Fail("该用户名已被注册");

            var now = DateTime.Now;
            var user = new User
            {
                Account = account.Trim(),
                Password = Cryptography.Encrypt(password),
                UserTypeId = 2,        // 注册默认为“员工”（UserType 表：1管理员、2员工），避免自助注册即获管理员权限
                Status = 0,            // 0 正常
                CreateUserId = 0,      // 自助注册
                CreateTime = now
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return AuthResult.Ok(user.UserId, "注册成功");
        }
    }
}
