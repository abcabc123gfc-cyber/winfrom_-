using System;
using System.Collections.Generic;

namespace day13
{
    #region 抽象类_图书馆
    abstract class LibraryItem
    {
        /// <summary>
        /// 图书ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 图书名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 剩余库存
        /// </summary>
        public int Stock { get; set; }

        

       

        
        public LibraryItem()
        {

        }
        public LibraryItem(string iID, string iName, int iStock )
        {
            ID = iID;
            Name = iName;
            Stock = iStock;
          
        }

        /// <summary>
        /// 是否可借
        /// </summary>
        /// <param name="Stock"></param>
        /// <returns></returns>
        public virtual bool IsAvailable(int Stock,User user,int Quantity)
        {
            if (user.GetMaxBorrowCount() <= 0)
            {
                return false;
            }
            if (Stock >= Quantity && user.GetMaxBorrowCount()>user.BorrowingQuantity )
            {
                return true;
            }
            return default;
        }
        /// <summary>
        /// 获取资料详情
        /// </summary>
        public virtual void GetInfo(DateTime dateTime)
        {
            Console.WriteLine("编号：{0}，名称：{1}，剩余库存：{2}", ID, Name, Stock);
        }
        /// <summary>
        /// 最大借阅天数
        /// </summary>
        /// <returns></returns>
        abstract public int GetMaxBorrowDays();
        /// <summary>
        /// j计算罚金
        /// </summary>
        /// <param name="iBorrowDate"></param>
        /// <returns></returns>
        abstract public double CalculateFine(DateTime iBorrowDate,DateTime dateTime);


    }
    #endregion

    #region Book
    class Book : LibraryItem
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 国籍标准书号
        /// </summary>
        public int ISBN { get; set; }

        public Book(string iID, string iName, int iStock , string iAuthor, int iISBN) : base(iID, iName, iStock )
        {
            Author = iAuthor;
            ISBN = iISBN;
        }


        public override double CalculateFine(DateTime dateTime,DateTime BorrowDate)
        {
            int days = (dateTime - BorrowDate).Days - 30 > 0 ? (dateTime - BorrowDate).Days : 0;
            return 0.5 * days;
        }
        /// <summary>
        /// 获取图书信息
        /// </summary>

        public override void GetInfo(DateTime BorrowDate)
        {
            //创建归还日期
            DateTime returnDate = BorrowDate.AddDays(GetMaxBorrowDays());
            Console.WriteLine($"图书编号:{ID},名称:{Name},库存:{Stock},,应应归还日期:{returnDate},作者:{Author},ISBN:{ISBN},逾期金额{CalculateFine(returnDate, BorrowDate)}");
        }

        public override int GetMaxBorrowDays()
        {
            return 30;
        }
    }
    #endregion

    #region 期刊类
    class Magazine : LibraryItem
    {
        /// <summary>
        /// 期号
        /// </summary>
        public int IssueNumber { get; set; }

        public Magazine(string iID, string iName, int iStock,  int iIssueNumber) : base(iID, iName, iStock )
        {
            IssueNumber = iIssueNumber;
        }

        public override int GetMaxBorrowDays()
        {
            return 7;
        }

        public override bool IsAvailable(int Stock,User user,int Quantity)
        {
            return base.IsAvailable(Stock,user,Quantity);
        }

        public override double CalculateFine(DateTime iBorrowDate,DateTime BorrowDate)
        {
            int days = (iBorrowDate - BorrowDate).Days - 7 > 0 ? (iBorrowDate - BorrowDate).Days : 0;
            return 1.0 * days;

        }
        public override void GetInfo(DateTime BorrowDate )
        {
            //创建归还日期
            DateTime returnDate = BorrowDate.AddDays(GetMaxBorrowDays());
        
            Console.WriteLine($"图书编号:{ID},名称:{Name},库存:{Stock},借出日期:{BorrowDate},应归还日期:{returnDate},期刊:{IssueNumber},逾期金额:{CalculateFine(returnDate, BorrowDate)}");
        }
    }
    #endregion

    #region  DVD 类
    class DvD : LibraryItem
    {
        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }


        public DvD(string iID, string iName, int iStock,  string iDirector) : base(iID, iName, iStock)
        {
            Director = iDirector;
        }

        public override double CalculateFine(DateTime iBorrowDate, DateTime BorrowDate)
        {
            int days = (iBorrowDate - BorrowDate).Days - 3 > 0 ? (iBorrowDate - BorrowDate).Days : 0;
            return 2 * days;
        }

        public override int GetMaxBorrowDays()
        {
            return 3;
        }
        public override bool IsAvailable(int Stock, User user, int Quantity)
        {
            return base.IsAvailable(Stock, user,Quantity);
        }
        public override void GetInfo(DateTime BorrowDate)   
        {
            //创建归还日期
            DateTime returnDate = BorrowDate.AddDays(GetMaxBorrowDays());
         
            Console.WriteLine($"图书编号:{ID},名称:{Name},库存:{Stock},借出日期:{BorrowDate},应归还日期:{returnDate},导演{Director},逾期金额:{CalculateFine(returnDate, BorrowDate)}");

        }
    }
    #endregion



    
}
