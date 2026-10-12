using MVVM_Demo.Common;


namespace 手写MVVM.Models
{
    //模型
    internal class Student : ViewModelBase
    {
        public int Id { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }


        public int Age { get; set; }
    }
}
