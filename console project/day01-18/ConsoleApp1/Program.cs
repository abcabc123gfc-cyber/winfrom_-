using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ========== 路由模式固定地址，不要修改 ==========
            string url = "http://127.0.0.1:15721/v1/chat/completions";
            // ⚠️ 任意字符串都行，CCSwitch自动替换面板里配置的66ai密钥
            string dummyKey = "test123";

            var body = new
            {
                model = "claude-3-5-sonnet-20241022",
                messages = new[]
                {
                    new { role = "user", content = "你好，介绍下自己" }
                }
            };

           HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(90);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dummyKey);

            string json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync(url, content);
            string result = await resp.Content.ReadAsStringAsync();

            Console.WriteLine("完整返回结果：");
            Console.WriteLine(result);
        }
    }
}