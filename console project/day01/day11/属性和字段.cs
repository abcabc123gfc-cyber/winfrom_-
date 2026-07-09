namespace day11
{
    internal class 属性和字段
    {
        //当不需要对输入值进行判断时, 可以使用自动属性
        public int Name { get; set; }

        //声明一个Int 类型的属性
        private int _age;
        public int Age
        {
            get => _age; set
            {
               _age=value>0?value:_age;
            }
        }

        //属性可以设置默认值
        //语法糖写法
        public string Sex { get; set; } = "男";

        private int _height=180;
        public int Height { get; set; }

        //只读属性
        public int Weight { get; } = 80;


       
        public void PropertyDemo()
        {
            //类里面写的东西都称之为类成员: 成员又分为三大类, 属性 字段 方法
            //属性和字段都是成员变量,变量就是用来存储信息


        }
    }
}
