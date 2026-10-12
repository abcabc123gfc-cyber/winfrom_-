using System.Windows;
using System.Windows.Controls;

namespace _02_Project.View.UserControls
{
    /// <summary>
    /// 通用占位页：菜单已接入、页面内容待实现时显示模块标题。
    /// </summary>
    public partial class UcPlaceholder : UserControl
    {
        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            nameof(PageTitle), typeof(string), typeof(UcPlaceholder), new PropertyMetadata("模块"));

        /// <summary>页面标题（由 Main 导航切换时赋值）</summary>
        public string PageTitle
        {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }

        public UcPlaceholder()
        {
            InitializeComponent();
        }
    }
}
