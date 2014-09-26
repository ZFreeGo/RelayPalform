using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFreeGo.IntelligentControlPlatform.Modbus
{
    public class ReciveRtuFrame
    {
        /// <summary>
        /// 从字节流中判断是否为相应帧
        /// </summary>
        /// <param name="reciveData">接收的字节流</param>
        /// <param name="sendFrame">发送的帧数据</param>
        /// <returns></returns>
        public bool JudgeResponseFrame(List<byte> reciveData, RTUFrame sendFrame,RTUFrame reciveFrame)
        {
            //首先判断这一帧是否已经完成响应
            //if (!sendFrame.CompleteFlag)
            { //测试注销
                //判断 查询地址 与 发送地址 是否一致
                if (reciveData[0] != sendFrame.Address)
                    return false;

                if (reciveData[1] != sendFrame.Function)
                    return false;
                int len = reciveData[2]; //获取数据字节数

                //再紧接着进行CRC校验
                ushort crc = GenCRC.CRC16(reciveData.ToArray(), (ushort)(len + 3));
               
                var low = (byte)(crc & 0xFF); //低8位
                if (low != reciveData[2 + len - 1])
                {
                    return false;
                }

                var hig = (byte)(crc & 0xFF00 >> 8);//高8位
                if (low != reciveData[2 + len ])
                {
                    return false;
                }
                List<byte> tmp = new List<byte>();
                tmp.AddRange(reciveData);
                tmp.RemoveRange(0, 3); //去除头
                tmp.RemoveRange(len, 2); //去除尾巴
                reciveFrame = new RTUFrame(reciveData[0], reciveData[1], tmp.ToArray(), (byte)len);

                sendFrame.CompleteFlag = true; //防止重复响应
                return true;
            }
            //else
            //{
            //    return false;
            //}
            
        }

        //适应一个一个字节的接收
        private byte GetByte()
        {
            if (ReciveAbyte != null)
            {
                return ReciveAbyte();
            }
            
            return 0x00;
        }
        public  Func<byte>  ReciveAbyte;
        public RTUFrame ReciveFrame;
        public bool JudgeGetByte(RTUFrame sendFrame)
        {
            //首先判断这一帧是否已经完成响应
          //  if (!sendFrame.CompleteFlag)
            {
                //判断 查询地址 与 发送地址 是否一致
                if (GetByte() != sendFrame.Address)
                    return false;

                //if (GetByte() != sendFrame.Function)
                //    return false;
                var fun = GetByte();
                
                int len = GetByte(); //获取数据字节数

                byte[] array = new byte[len + 3];
                array[0] = sendFrame.Address;
                //array[1] = sendFrame.Function;
                array[1] = fun;
                array[2] = (byte)len;
                for (int i = 0; i < len; i++ )
                {
                    array[i + 3] = GetByte();
                }

                ushort crc = GenCRC.CRC16(array, (ushort)(len + 3));

                var low = (byte)(crc & 0xFF); //低8位
                if (low != GetByte())
                {
                    return false;
                }

                var hig = (byte)(crc & 0xFF00 >> 8);//高8位
                if (low != GetByte())
                {
                    return false;
                }
                List<byte> tmp = new List<byte>();
                tmp.AddRange(array);
                tmp.RemoveRange(0, 3); //去除头

                ReciveFrame = new RTUFrame(array[0], array[1], tmp.ToArray(),
                    (byte)len, low, hig);

                sendFrame.CompleteFlag = true; //防止重复响应
                return true;

            }
          //  else
          //  {
          //      return false;
          //  }
        }
    }
}
