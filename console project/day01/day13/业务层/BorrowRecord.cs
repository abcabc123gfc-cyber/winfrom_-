using System;

namespace day13
{
    internal class BorrowRecord
    {

        /// <summary>
        /// 流水编号
        /// </summary>
        public int RecordID { get; set; }
        /// <summary>
        /// 借阅时间
        /// </summary>
        public DateTime? BorrowDate { get; set; }

        /// <summary>
        /// 借阅物品id
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// 归还时间_实际
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// 本次借阅数量
        /// </summary>
        public int Quantity { get; set; }

       
        public BorrowRecord()
        {
            RecordID = 0;
            ItemID = "";
            ReturnDate = null;
        }
        public BorrowRecord(int recordID, string itemID, DateTime? BorrowDate, int quantity)
        {
            RecordID = recordID;
            ItemID = itemID;
            this.BorrowDate = BorrowDate;
            Quantity = quantity;
        }

        public BorrowRecord(string itemID, DateTime? BorrowDate, int quantity)
        {
            ItemID = itemID;
            this.BorrowDate = BorrowDate;
            this.Quantity = quantity;
        }


        /// <summary>
        /// 借阅记录,包括借出记录,逾期金额等 遍历所有图书和用户显示信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="user"></param>
        public void Return() 
        {
            //foreach (LibraryItem item2 in LibrarySystem.LibraryItems)
            //{
            //    foreach (var item in LibrarySystem.listUsers)
            //    {
            //        item2.GetInfo((DateTime)item.BorrowRecodes.Find(x=>x.BorrowDate is DateTime&& x.BorrowDate!=null).BorrowDate);
            //    }
            //}
            foreach (var item in LibrarySystem.listUsers)
            {
                foreach (var item1 in item.BorrowRecodes)
                {
                    Console.WriteLine(item1.ItemID);
                }
                
            }
        }

    }
}
