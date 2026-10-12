using System.Threading.Tasks;

namespace _02_Project.Service
{
    /// <summary>
    /// 认证服务接口：登录 / 注册
    /// 实现类位于 Service/lmp/AuthService.cs
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 登录校验：用户必须存在、未被禁用且密码正确
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">明文密码（内部加密后与库中密文比对）</param>
        Task<AuthResult> LoginAsync(string account, string password);

        /// <summary>
        /// 注册：用户名不能重复
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">明文密码（入库前加密）</param>
        Task<AuthResult> RegisterAsync(string account, string password);
    }
}
