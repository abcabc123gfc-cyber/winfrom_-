using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 播放
        private void button3_Click(object sender, EventArgs e)
        {
            xWindowsMediaPlayer1.Ctlcontrols.play();
        }
        // 暂停
        private void button4_Click(object sender, EventArgs e)
        {
            xWindowsMediaPlayer1.Ctlcontrols.pause();
        }
        // 停止
        private void button5_Click(object sender, EventArgs e)
        {
            xWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //程序初始化,取消自动播放功能
            xWindowsMediaPlayer1.settings.autoStart = false;

            xWindowsMediaPlayer1.URL = "D:\\Download\\BOOK\\计算机网络\\导论课程视频\\吉他独奏MP3.mp3";
            //取消循环播放
            xWindowsMediaPlayer1.settings.setMode("loop", false);
        }
    }
}
