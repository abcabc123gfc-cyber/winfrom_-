
namespace WebAPI_练习.Model
{

    public class UserInfo
    {
        

     
        public int Id { get; set; }
        // required 在实例化时,必须赋值
        //public required string Name { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Account { get; set; }
        public int Password { get; set; }
        public int Grade { get; set; }
        public int State { get; set; }
    }
}
