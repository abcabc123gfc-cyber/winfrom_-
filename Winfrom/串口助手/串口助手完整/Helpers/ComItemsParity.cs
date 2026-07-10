using System.Collections.Generic;
using System.Windows.Forms;
using 串口助手完整.Model;

namespace 串口助手完整.Helpers
{
    internal class ComItemsParity
    {
        /// <summary>
        /// 设置校验
        /// </summary>
        /// <param name="comboBox"></param>
        internal static void SetParoty(ref ComboBox comboBox)
        {
            comboBox.DataSource = new List<ComItemsParitys>
            {
                new ComItemsParitys{Name="无校验",Value="None"},
                new ComItemsParitys{Name="奇校验",Value="Odd"},
                new ComItemsParitys{Name="偶校验",Value="Even"},
                new ComItemsParitys{Name="标记校验",Value="Mark"},
                new ComItemsParitys{Name="空间校验",Value="Space"}

            };
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Value";
        }
        /// <summary>
        /// 设置停止位
        /// </summary>
        /// <param name="comboBox"></param>
        public static void SetStopBit(ref ComboBox comboBox)
        {
            comboBox.DataSource = new List<StopBitModel>
            {
               new StopBitModel(){Name="0",Value="None"},
            new StopBitModel(){Name="1",Value="One"},
            new StopBitModel(){Name="2",Value="Two"},
            new StopBitModel(){Name="1.5",Value="OnePointFive"}
            };
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Value";
            comboBox.SelectedIndex = 1;
        }
        //public static void 
    }
}
