using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    class Tool
    {
        /// <summary>
        /// 32bit无符号整型转换为字节数组，由低位到高位排列
        /// </summary>
        /// <param name="num">32bit 无符号整型</param>
        /// <returns>字节数组</returns>
        static public byte[] Uint32ToByte(uint num)
        {
            byte[] array = new byte[4];
            array[0] = (byte)(num & 0x000000FF);
            array[1] = (byte)((num & 0x0000FF00) >> 8);
            array[2] = (byte)((num & 0x00FF0000) >> 16);
            array[3] = (byte)((num & 0xFF000000) >> 24);
            return array;
        }

        /// <summary>
        /// 限制边界
        /// </summary>
        /// <param name="value">被检测的值</param>
        /// <param name="left">左边界</param>
        /// <param name="right">右边界</param>
        /// <returns>限制后的值</returns>
        static public  double  CheckBound(double value, double left, double right)
        {
            if (value < left)
            {
                return left;
            }
            if (value > right)
            {
                return right;
            }
            return value;

        }
        /// <summary>
        /// 由真实电流值得出下位机需要的电流表达形式
        /// </summary>
        /// <param name="real">电流值(A)</param>
        /// <returns>转换后的电流形式</returns>
        static public uint CalMcuCurrentByReal(double real)
        {
            double rat = 10f / (1f/5f * 1024f); //%0.0488 A/div 


            double mcu_value =  Math.Pow(real/rat, 2) * 20; //%对应的计算值 4bytes

            return (uint)mcu_value;
        }
        /// <summary>
        /// 由时间得出下位机需要的时间形式
        /// </summary>
        /// <param name="time">时间 (s)</param>
        /// <returns>转换后的时间形式</returns>
        static public uint CalMcuTimeByReal(double time)
        {
            double t = 1/ time  * 1e5 * 3; // %需要4 bytes
            return (uint)t;
        }

       
    }
}
