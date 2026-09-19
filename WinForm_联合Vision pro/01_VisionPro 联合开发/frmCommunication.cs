using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_VisionPro_联合开发
{
    public partial class frmCommunication : Form
    {
       Config config = Config.GetConfig();
        public frmCommunication()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmCommunication_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// TCP 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            config.LoadDeploy();
            // 点击保存的时候把数据保存到配置文件里面
            //判断是否勾选 
            config.TcpOpen = checkBox2.Checked ? 1 : 0;
            config.TcpIp = textBoxIP.Text;
            config.TcpPort = Convert.ToInt32(textBoxPort.Text);
            //保存到INI中
            Ini.IniAPI.INIWriteValue(config.DeployPath, "网口参数", "是否开启", config.TcpOpen.ToString());
            Ini.IniAPI.INIWriteValue(config.DeployPath, "网口参数", "IP地址", config.TcpIp.ToString());
            Ini.IniAPI.INIWriteValue(config.DeployPath, "网口参数", "端口号", config.TcpPort.ToString());
            if (File.Exists(config.DeployPath))
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
