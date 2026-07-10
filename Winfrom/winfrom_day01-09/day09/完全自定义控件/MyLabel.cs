using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace day09.完全自定义控件
{
    public class MyLabel : Control
    {
        private string myText = "文字";

        // 特性,用来给属性和事件添加描述信息(解释说明),点击属性窗口的时候,某个属性的提示信息
        [Description("这是MyLable标签的文本内容")]
        //用来设置属性进行分类, 默认分类放到 "杂项"
        [Category("自定义属性")]
        //用来设置当前属性是否显示在属性窗口,如果设置为false 会隐藏
        [Browsable(true)]
        //设置默认值 在属性窗口中, 如果属性的值,等于默认值,属性值则会显示成普通文本, 如果被修改,则会显示成粗体
        [DefaultValue("wuyifan")]
        public string MyText
        {
            get { return myText; }
            set { myText = value; }
        }
        private Font myFont = new Font("宋体", 12);

        public Font MyFont
        {
            get { return myFont; }
            set { myFont = value; }
        }
        /// <summary>
        /// 重新父类方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawArc(new Pen(Color.Blue, 5), 5, 5, this.Width - 5, this.Height - 5, 0, 30);
            //e.Graphics.fi
        }




    }
}
