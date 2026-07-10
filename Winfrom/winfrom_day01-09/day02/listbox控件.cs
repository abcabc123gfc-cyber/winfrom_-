using System.Windows.Forms;

namespace day02
{
    public partial class listbox控件 : Form
    {
        public listbox控件()
        {
            InitializeComponent();
            listbox控件_Load();
        }
        public void listbox控件_Load()
        {
            listBox1.Items.Add("张三");
            //多选
            listBox1.SelectionMode = SelectionMode.MultiSimple;
            //当选中的项改变时,触发
            listBox1.SelectedIndexChanged += (e, s) =>
            {
                //listbox 获取选中项的值
                MessageBox.Show(listBox1.SelectedItem.ToString());
                //listbox 获取选中项的索引
                MessageBox.Show(listBox1.SelectedIndex.ToString());
            };
            //遍历
            foreach (var item in listBox1.Items)
            {
                MessageBox.Show(item.ToString());
            }

            // 添加
            listBox1.Items.Add("王五");
            // 删除
            listBox1.Items.Remove("王五");
            listBox1.Items.AddRange(new string[] { "张三", "王五", "赵六" });
            listBox1.Size = new System.Drawing.Size(100, 100);

        }

    }
}
