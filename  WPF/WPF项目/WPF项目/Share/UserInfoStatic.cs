using Models;
using System.Collections.Generic;

namespace WPF项目.Share
{
    internal class UserInfoStatic<T>
    {
        public static List<T> InfoList { get; set; } = new List<T>();
        public static User user { get; set; } = new User();
    }
}
