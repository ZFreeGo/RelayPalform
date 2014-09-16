using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFreeGo.IntelligentControlPlatform.Modbus
{
    class RTUFrame
    {
        //完整一帧
        private byte[] frame;
        public byte[] Frame
        {
            public get { return frame; } 
        }

        private byte address;
        public byte Address
        {
            public get { return address; }
        }
        private byte function;
        public byte Function
        {
            public get { return function; }
        }
        private byte[] framedata;
        public byte[] FrameData
        {
            public get { return framedata; }
        }


        private bool completeFlag;
        public bool CompleteFlag
        {
            get { return completeFlag; }
            set { completeFlag = value; }
        }

        private byte  dataLen;
        public byte DataLen
        {
            get { return dataLen; }
            //set { dataLen = value; }
        }

        public  RTUFrame(byte addr, FunEnum funcode,
                        byte[] sendData, byte datalen)
        {
            //addrss(1) funcode(1) + bytecount(1) sendData(datalen) CRC(2)
            int len = 1 + 1 +  + 1 + datalen + 2;
            frame = new byte[len];

            this.dataLen = datalen;
            this.address = addr;
            this.function = (byte)funcode;
            this.framedata = new byte[datalen];
            for (int i = 0; i < datalen;  i++)
            {
                this.framedata[i] = sendData[i];
                frame[i + 3] = sendData[i];
            }
            frame[0] = addr;
            frame[1] = (byte)funcode;
            frame[2] = (byte)datalen;

            ushort crc =  GenCRC.CRC16(frame, (ushort)(len - 2));
            frame[len - 2] = (byte)(crc & 0xFF); //低8位
            frame[len - 1] = (byte)(crc & 0xFF00 >> 8);//高8位

            completeFlag = false;
        }
        

        private RTUFrame()
        {

        }

      
    }

}
