using System;
using System.Configuration;
using System.Net.Http;

namespace Web测试.Models
{
    internal class single
    {

        private static HttpClient _single;
        private static readonly object _lock = new object();
        private single()
        {

        }
        public static HttpClient GetInstance()
        {
            if (_single == null)
            {
                lock (_lock)
                {
                    if (_single == null)
                    {
                        _single = new HttpClient()
                        {
                            BaseAddress = new Uri(ConfigurationManager.AppSettings["MesApi"])
                        };
                    }
                }
            }
            return _single;
        }
    }
}
