using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using 测试.Model;
using 测试_WebAPI.Model;
namespace 测试_WebAPI
{
    public partial class Form1 : Form
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["BaseURL"];
        HttpClient httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var v = await httpClient.GetAsync("https://localhost:7237/User/selectAll");
            
            string v1 = await v.Content.ReadAsStringAsync();
            RespondResult obj = JsonConvert.DeserializeObject<RespondResult>(v1);
            if (obj.Code.Equals("200")) { 
            
            dataGridView1.DataSource = obj.Data;
            }
            //Console.WriteLine(obj);
        }
        private async void button11_Click(object sender, EventArgs e)
        {
            //如何访问其他项目中定义的接口?
            //Get请求:
            //1.实例化HttpClient 用于发起请求
            //2.准备接口地址
            //3.发起get请求
            //4.获取响应结果
            //5.解析响应结果

            string url = $"{baseUrl}/UserInfo/GetList";

            //查询的接口
            // https://localhost:7074/UserInfo/GetList
            //请求接口
            //发起get请求获取响应
            var response = await httpClient.GetAsync(url);
            //响应中获取结果  string 
            string res = await response.Content.ReadAsStringAsync();
            //方便打点调用
            RespondResult obj = JsonConvert.DeserializeObject<RespondResult>(res);
            Console.WriteLine(obj.Code);
            dataGridView1.DataSource = obj.Data;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //如何访问其他项目中定义的接口?
            //Post请求:
            //1.实例化HttpClient 用于发起请求
            //2.准备接口地址
            //4.准备参数(需要添加的对象)
            //5.把对象转换成json字符传格式
            //6.发起Post请求
            //7.获取响应结果
            //8.解析响应结果


            //添加的接口
            // https://localhost:7074/UserInfo/Add
            string url = $"{baseUrl}/UserInfo/Add";

            var model = new
            {
                account = "吴亦凡",
                password = "123213",
                createUserId = 0,
                createTime = DateTime.Now,
                lastUpdateUserId = 0,
                lastUpdateTime = DateTime.Now,
                status = 0
            };
            string jsonstring = JsonConvert.SerializeObject(model);

            HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(jsonstring, Encoding.UTF8, "application/json"));
            string result = await response.Content.ReadAsStringAsync();

            Result res = JsonConvert.DeserializeObject<Result>(result);
            MessageBox.Show(res.Msg);


        }
    }
}
