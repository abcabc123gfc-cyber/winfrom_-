using System.IO;

namespace _01_VisionPro_联合开发
{
    internal class Config
    {
        // 单例模式
        private static Config config = null;
        private static readonly object obj = new object();
        public static Config GetConfig()
        {
            if (config == null)
            {
                lock (obj)
                {
                    if (config == null)
                    {
                        config = new Config();
                    }

                }
            }
            // 返回单例 确保全局引用唯一
            return config;
        }
        /// <summary>
        /// 总数
        /// </summary>
        public double ProZhong { get; set; } = 0;
        /// <summary>
        /// ok 的数量
        /// </summary>
        public double ProOk { get; set; } = 0;
        /// <summary>
        /// 配置通信 保存地址
        /// </summary>
        public string DeployPath = Directory.GetCurrentDirectory() + "\\配置文件\\deploy.ini";
        /// <summary>
        /// TCP是否开启 
        /// </summary>
        public int TcpOpen { get; set; } = 1;
        /// <summary>
        /// IP 地址
        /// </summary>
        public string TcpIp { get; set; }
        /// <summary>
        /// 储存端口号
        /// </summary>
        public int TcpPort { get; set; }

        public void LoadDeploy()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\\配置文件"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() +@"\\配置文件");
            }
            if (!File.Exists(DeployPath))
            {
                //需要即时释放资源
                using (File.Create(DeployPath)) { }
            }
            // 读“是否开启”，读不到就返回默认值 1
            TcpOpen = Ini.IniAPI.GetPrivateProfileInt("网口通信", "是否开启", 1, DeployPath);
            // 读“IP地址”，读不到就返回默认值 "127.0.0.1"
            TcpIp = Ini.IniAPI.GetPrivateProfileString("网口通信", "IP地址", "127.0.0.1", DeployPath);
            TcpPort = Ini.IniAPI.GetPrivateProfileInt("网口通信", "端口号", 60000, DeployPath);
        }
    }
}
