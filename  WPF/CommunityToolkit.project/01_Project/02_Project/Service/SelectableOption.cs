using CommunityToolkit.Mvvm.ComponentModel;

namespace _02_Project.Service
{
    /// <summary>
    /// 可勾选选项（多选子表单 ListBox 数据源）：
    /// 通过 ListBoxItem.IsSelected 双向绑定实现勾选回显，规避虚拟化容器未生成导致回显失效的问题。
    /// </summary>
    public partial class SelectableOption : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _text = string.Empty;

        [ObservableProperty]
        private bool _isSelected;
    }
}
