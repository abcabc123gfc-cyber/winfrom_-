using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_作业
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string str = "F:\\C#软件开发14班\\2026-07-30\\b.png\\";
           string[] strs= str.Split('\\');

            foreach (var item in strs)
            {
                Console.WriteLine(item);
                
            }
            //b.png
            string s = strs[3].Split('.')[0];

         //    str.Replace();
        }
    }
}
