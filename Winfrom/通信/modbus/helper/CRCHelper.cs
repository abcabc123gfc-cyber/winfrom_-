using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Helper
{
    public class CRCHelper
    {
        public static byte[] CRC16(byte[] buffer, int start = 0, int len = 0)
        {
            if(buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            ushort crc = 0;// Initial value
            for (int i = start; i < length; i++)
            {
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);// 0xA001 = reverse 0x8005
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            byte[] ret = BitConverter.GetBytes(crc);
            Array.Reverse(ret);
            return ret;
        }
        public static byte[] CRC16(byte[] data)
        {
            int crc = 0xffff;//初始化一个CRC寄存器

            //CRC 是根据传入的数组的前6位生成,要循环遍历数组,并且 -2 排除字节数组中最后两个字节
            for (int i = 0; i < data.Length - 2; i++)
            {
                //将当前字节与CRC寄存器进行异或操作(crc是16位, data[i]是8位 隐式提升为32位异或后,实际只影响低8八位)
                crc = crc ^ data[i];//异或

                //外层循环循环一次 内层循环 循环8次
                //为什么循环8次 一个字节有8bit
                for (int j = 0; j < 8; j++)
                {
                    int temp = 0; 
                    temp = crc & 1;//取出最低位(0或者1)
                    crc = crc >> 1;//右移
                    crc = crc & 0x7fff;//清除低16位(确保移位后的最高位位00)
                    if (temp == 1)
                    {
                        crc = crc ^ 0xa001;//如果移除位是1 则与0xa001异或
                    }
                    crc = crc & 0xffff;//确保只有16位有效
                }
            }
            //CRC寄存器高低位互换 因为协议要求  低字节在前 高字节在后
            byte[] crc16 = new byte[2];
            crc16[1] = (byte)((crc >> 8) & 0xff);
            crc16[0] = (byte)(crc & 0xff);
            return crc16;
        }
    }

    

}
