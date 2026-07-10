using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class _06_DriveInfo类
    {
        public void DriveInfoTest()
        {
            DriveInfo driveInfo = new DriveInfo(@"H://");
            //获取磁盘总大小
            Console.WriteLine("总大小：{0}", driveInfo.TotalSize);
            Console.WriteLine(driveInfo.Name);
            //获取磁盘可用空间 以字节为单位
            Console.WriteLine(driveInfo.AvailableFreeSpace);
            //Fixed 固定的
            //Removable 可移动的
            Console.WriteLine(driveInfo.DriveType);
            //获取所有磁盘信息 返回DriveInfo[] 数组
            DriveInfo.GetDrives();
            
        }
    }
}
