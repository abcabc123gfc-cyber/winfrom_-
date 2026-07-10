using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dong.Model
{
    /// <summary>
    /// 给自定义控件UCStoreAreaBox使用的
    /// </summary>
    public class StoreAreaBoxModel
    {
        /// <summary>
        /// 仓库区域Id
        /// </summary>
        public int StoreAreaId { get; set; }
        /// <summary>
        /// 仓库区域编号
        /// </summary>
        public string StoreAreaNo { get; set; }
        /// <summary>
        /// 仓库分区名称
        /// </summary>
        public string StoreAreaName { get; set; }
        /// <summary>
        /// 仓库Id
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 仓库区域当前温度
        /// </summary>
        public decimal Temperature { get; set; }
        /// <summary>
        /// 仓库区域最小温度
        /// </summary>
        public decimal MinTemperature { get; set; }
        /// <summary>
        /// 仓库区域最大温度
        /// </summary>
        public decimal MaxTemperature { get; set; }
        /// <summary>
        /// 仓库区域温度范围
        /// </summary>
        public string TemperatureRange { get; set; }
        /// <summary>
        /// 温度状态（0正常，1高预警，2低预警）
        /// </summary>
        public int TemperatureState { get; set; }
        /// <summary>
        /// 指示灯颜色
        /// </summary>
        public Color StateColor { get; set; }
        /// <summary>
        /// 设置按钮是否显示
        /// </summary>
        public bool BtnSetVisible { get; set; }
        /// <summary>
        /// 设置按钮的文本
        /// </summary>
        public string BtnSetText { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int ProductCount { get; set; }
    }
}
