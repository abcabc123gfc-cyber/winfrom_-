using System;
using System.Collections.Generic;
using System.Linq;

namespace day13
{
    class LibrarySystem
    {
        public static List<LibraryItem> LibraryItems = new List<LibraryItem>();
        public static List<User> listUsers = new List<User>();


        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="libraryItem"></param>
        /// <param name="user"></param>
        public void Borrow(LibraryItem libraryItem, User user, BorrowRecord borrowRecord)
        {
            //判断库存
            if (libraryItem.IsAvailable(libraryItem.Stock, user,borrowRecord.Quantity))
            {
               
                //更新库存
                Update(libraryItem, borrowRecord.Quantity);
                //自动生成借阅 编号
                borrowRecord.RecordID = user.BorrowRecodes.Count +1;
                //添加借阅记录
                user.BorrowRecodes.Add(borrowRecord);
                // 更新借阅数量
                user.BorrowingQuantity += borrowRecord.Quantity;
                //添加用户
                LibrarySystem.listUsers.Add(user);
                
            }
        }
        //加入书籍
        public void AddCommon(LibraryItem book)
        {
            LibrarySystem.LibraryItems.Add(book);
        }
        /// <summary>
        /// 更新书籍库存
        /// </summary>
        /// <param name="book"></param>
        /// <param name="number"></param>
        public void Update(LibraryItem book, int number)
        {
            for (int i = 0; i < LibrarySystem.LibraryItems.Count; i++)
            {

                if (LibrarySystem.LibraryItems[i].Stock > 0 && LibrarySystem.LibraryItems[i].ID == book.ID)
                {
                    LibrarySystem.LibraryItems[i].Stock -= number;
                   
                }
            }
        }

    }
}
