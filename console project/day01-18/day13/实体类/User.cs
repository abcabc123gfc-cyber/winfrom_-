using System.Collections.Generic;

namespace day13
{
    abstract class User
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string Name;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        public User(string name, int useID,BorrowRecord borrowRecord)
        {
            Name = name;
            UserID = useID;
           
            BorrowRecodes.Add( borrowRecord);
        }
        public User (string name , int useID)
        {
            Name = name;
            UserID = useID;
        }

        /// <summary>
        /// 借阅记录
        /// </summary>
        public List<BorrowRecord> BorrowRecodes = new List<BorrowRecord>();


        /// <summary>
        /// 获取最大可借数量
        /// </summary>
        /// <returns></returns>
        abstract public int GetMaxBorrowCount();

        /// <summary>
        /// 实际借阅数量
        /// </summary>
        public int BorrowingQuantity { get; set; }

        

    }
    /// <summary>
    /// 普通读者
    /// </summary>
    class Menber : User
    {
        public Menber(string name, int useID,BorrowRecord borrowRecord) : base(name, useID, borrowRecord)
        {

        }
        public Menber(string name, int useID) : base(name, useID)
        {

        }
        public override int GetMaxBorrowCount()
        {
            return 5;
        }

    }
    /// <summary>
    /// 管理员
    /// </summary>
    class Librarian : User
    {
        public Librarian(string name, int useID, BorrowRecord borrowRecord) : base(name, useID, borrowRecord)
        {

        }
        public Librarian(string name, int useID) : base(name, useID)
        {

        }
        public override int GetMaxBorrowCount()
        {
            return 20;
        }
    }
}
