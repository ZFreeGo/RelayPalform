using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ZFreeGo.IntelligentControlPlatform.Modbus;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    public partial class MainWindow
    {
        private const  byte downComputeAddress = 0xEA;
        private RTUFrame sendFrame;

        /// <summary>
        /// 显示发送字节信息。
        /// </summary>
        /// <param name="send">字节数组</param>
        void ShowSendMessage(byte[] send)
        {
            foreach (var data in send)
            {
                sendTxtBox.Text += string.Format("{0:X2} ", data);
            }
            
        }
        private void sendTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                byte len = Convert.ToByte(dataLenTxt.Text, 16); 
                var data = new byte[len];
                var hexStr = GetDecimalByHexString(dataTxt.Text);

                if (hexStr != null)
                {
                    int min = Math.Min(hexStr.Count, len);
                    for (int i = 0 ; i < min; i++)
                    {
                        data[i] = Convert.ToByte(hexStr[i], 16);
                    }
                }


                sendFrame = new RTUFrame(Convert.ToByte(deviceAddrTxt.Text,16), Convert.ToByte(funCodeTxt.Text, 16),
                                         data, len);
                serialPort.Write(sendFrame.Frame, 0, sendFrame.Frame.Length);
                ShowSendMessage(sendFrame.Frame);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "发送数据");
            }

        }
        private void Led1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (e.OriginalSource is RadioButton)
            {
                var radio = e.OriginalSource as RadioButton;
                FunEnum fun = FunEnum.None;
                switch (radio.Name)
                {
                    case "Led1On":
                        {
                            fun = FunEnum.LED1_ON;
                            break;
                        }
                    case "Led1Off":
                        {
                            fun = FunEnum.LED1_OFF;
                            break;
                        }
                    case "Led1Toggle":
                        {
                            fun = FunEnum.LED1_TOGLE;
                            break;
                        }
                    default:
                        {
                            fun = FunEnum.None;
                            break;
                        }
                }
                if (fun != FunEnum.None)
                {
                    //添加发送命令指令
                    if (portState)
                    {
                        var send = new RTUFrame(downComputeAddress, fun);
                        serialPort.Write(send.Frame, 0, send.Frame.Length);
                        ShowSendMessage(sendFrame.Frame);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LED1:" + ex.Message);
            }
        }

        private void Led2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (e.OriginalSource is RadioButton)
            {
                var radio = e.OriginalSource as RadioButton;
                FunEnum fun = FunEnum.None;
                switch (radio.Name)
                {
                    case "Led2On":
                        {
                            fun = FunEnum.LED2_ON;
                            break;
                        }
                    case "Led2Off":
                        {
                            fun = FunEnum.LED2_OFF;
                            break;
                        }
                    case "Led2Toggle":
                        {
                            fun = FunEnum.LED2_TOGLE;
                            break;
                        }
                    default:
                        {
                            fun = FunEnum.None;
                            break;
                        }
                }
                if (fun != FunEnum.None)
                {
                    //添加发送命令指令
                    if (portState)
                    {
                        var send = new RTUFrame(downComputeAddress, fun);
                        serialPort.Write(send.Frame, 0, send.Frame.Length);
                        ShowSendMessage(sendFrame.Frame);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LED2:" + ex.Message);
            }
        }

        private void Led3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (e.OriginalSource is RadioButton)
            {
                var radio = e.OriginalSource as RadioButton;
                FunEnum fun = FunEnum.None;
                switch (radio.Name)
                {
                    case "Led3On":
                        {
                            fun = FunEnum.LED3_ON;
                            break;
                        }
                    case "Led3Off":
                        {
                            fun = FunEnum.LED3_OFF;
                            break;
                        }
                    case "Led3Toggle":
                        {
                            fun = FunEnum.LED3_TOGLE;
                            break;
                        }
                    default:
                        {
                            fun = FunEnum.None;
                            break;
                        }
                }
                if (fun != FunEnum.None)
                {
                    //添加发送命令指令
                    if (portState)
                    {
                        var send = new RTUFrame(downComputeAddress, fun);
                        serialPort.Write(send.Frame, 0, send.Frame.Length);
                        ShowSendMessage(sendFrame.Frame);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LED3:" + ex.Message);
            }
        }
        private void Led4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (e.OriginalSource is RadioButton)
            {
                var radio = e.OriginalSource as RadioButton;
                FunEnum fun = FunEnum.None;
                switch (radio.Name)
                {
                    case "Led4On":
                        {
                            fun = FunEnum.LED4_ON;
                            break;
                        }
                    case "Led4Off":
                        {
                            fun = FunEnum.LED4_OFF;
                            break;
                        }
                    case "Led4Toggle":
                        {
                            fun = FunEnum.LED4_TOGLE;
                            break;
                        }
                    default:
                        {
                            fun = FunEnum.None;
                            break;
                        }
                }
                if (fun != FunEnum.None)
                {
                    //添加发送命令指令
                    if (portState)
                    {
                        var send = new RTUFrame(downComputeAddress, fun);
                        serialPort.Write(send.Frame, 0, send.Frame.Length);
                        ShowSendMessage(sendFrame.Frame);
                    }
                }
            }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("LED4:" + ex.Message);
            }
        }
        private void LedAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (e.OriginalSource is RadioButton)
            {
                var radio = e.OriginalSource as RadioButton;
                FunEnum fun = FunEnum.None;
                switch (radio.Name)
                {
                    case "LedAllOn":
                        {
                            fun = FunEnum.LED_ALL_ON;
                            break;
                        }
                    case "LedAllOff":
                        {
                            fun = FunEnum.LED_ALL_OFF;
                            break;
                        }
                    default:
                        {
                            fun = FunEnum.None;
                            break;
                        }
                }
                if (fun != FunEnum.None)
                {
                    //添加发送命令指令
                    if (portState)
                    {
                        var send = new RTUFrame(downComputeAddress, fun);
                        serialPort.Write(send.Frame, 0, send.Frame.Length);
                        ShowSendMessage(sendFrame.Frame);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LEDALL:" + ex.Message);
            }
            
        }

        //折算到ADC值后的电流平方和
        ushort[] currentSqureSum =
        new ushort[]{
            4624,5075,5547,6040,6554,7088,7644,8221,8819,9437,10077,10737,11419,12122,12845,
            13590,14355,15141,15949,16777,17627,18497,19388,20300,21234,22188,23163,24159,25176,
            26214,27273,28353,29454,30576,31719,32883,34068,35274,36501,37749
        };
        //时间倒数 并放大3*1e5 倍数
        ushort[] timeDaoshu =
        {
            300,308,316,325,334,344,354,365,377,389,403,417,432,449,467,486,507,530,555,582,
            613,646,684,726,774,828,891,964,1050,1153,1279,1435,1634,1898,2263,2802,3679,5355,9832,60000
        };

        /// <summary>
        /// 写下位机EEPROM，每20个字节一组，中间暂停100ms，留出下位机处理时间。
        /// 注意写入范围限制在0xff 以内
        /// </summary>
        /// <param name="writeData">将要写入下位机EEPROM的数据</param>
        /// <param name="startAddress">指定写入下位机EEPROM的首地址</param>
        void EepromWrite(ushort[] writeData, ushort startAddress)
        {

            if (portState)
            {
                byte[] senddata = new byte[22];


                for (byte k = 0; k < 4; k++)
                {

                    senddata[0] = (byte)((startAddress + k * 20) % 256); //起始地址
                    senddata[1] = (byte)((startAddress + k * 20) / 256); //起始地址
                    int j = 2;
                    for (int i = 0 + k * 10; i < k * 10 + 10; i++)
                    {
                        senddata[j++] = (byte)(writeData[i] % 256);
                        senddata[j++] = (byte)(writeData[i] / 256);
                    }
                    var send = new RTUFrame(downComputeAddress, (byte)FunEnum.WRITE_EEPROM, senddata, (byte)senddata.Length);
                    serialPort.Write(send.Frame, 0, send.Frame.Length);
                    ShowSendMessage(sendFrame.Frame);
                    Thread.Sleep(100); //延时以等待下位机处理完毕
                }
            }
            else
            {
                throw new Exception("串口未设置。");
            }
        }
        void EepromWrite(byte[] writeData, ushort startAddress, ushort len)
        {

            if (portState)
            {
                byte[] senddata = new byte[len + 2];


                senddata[0] = (byte)((startAddress) % 256); //起始地址 低8位
                senddata[1] = (byte)((startAddress) /256);  //起始地址  高8位
                
                int j = 2;
                for (int i = 0; i < len; i++)
                {
                    senddata[j++] = writeData[i];
                }
                
                var send = new RTUFrame(downComputeAddress, (byte)FunEnum.WRITE_EEPROM, senddata, (byte)senddata.Length);
                serialPort.Write(send.Frame, 0, send.Frame.Length);
                ShowSendMessage(send.Frame);
                //Thread.Sleep(100); //延时以等待下位机处理完毕
                
            }
            else
            {
                throw new Exception("串口未设置。");
            }
        }
        private byte[] SetProtectValueByTxt(TextBox currentTxt, TextBox timeBox)
        {
            var value = double.Parse(currentTxt.Text);
            var checkvalue = Tool.CheckBound(value, 1, 10);
            currentTxt.Text = checkvalue.ToString();
            uint mcuCurrent = Tool.CalMcuCurrentByReal(checkvalue);

            value = double.Parse(timeBox.Text);
            checkvalue = Tool.CheckBound(value, 0.001, 1000);
            timeBox.Text = checkvalue.ToString();
            uint mcuTime = Tool.CalMcuTimeByReal(checkvalue);

            byte[] currByte = Tool.Uint32ToByte(mcuCurrent);
            byte[] timeByte = Tool.Uint32ToByte(mcuTime);

            byte[] sendata = new byte[8];
            for (byte i = 0; i < 4; i++)
            {
                sendata[i] = currByte[i];
                sendata[i + 4] = timeByte[i];
            }
            return sendata;
           
        }
        /// <summary>
        /// 设置短路速断保护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplySuduanProtect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               var senddata = SetProtectValueByTxt(suduanPotectCurrentTxt, suduanPotectTimeTxt);
               EepromWrite(senddata, 0x200, 8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("短路速断保护:" + ex.Message);
            }

        }
        /// <summary>
        /// 设置短路延时保护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyYanshiProtect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senddata = SetProtectValueByTxt(yanshiPotectCurrentTxt, yanshiPotectTimeTxt);
                EepromWrite(senddata, 0x208, 8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("短路延时保护:" + ex.Message);
            }
        }
        private void LoadFanCurve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EepromWrite(currentSqureSum, 0); //写入电流数据
                EepromWrite(timeDaoshu, 80); //写入时间倒数数据
            }            
            catch (Exception ex)
            {
                MessageBox.Show("发送命令:" + ex.Message);

            }
        }

        /// <summary>
        /// 投保护压板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void protectYabanTouru_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (portState)
                {
                    var send = new RTUFrame(downComputeAddress, FunEnum.PROTECT_RUN);
                    serialPort.Write(send.Frame, 0, send.Frame.Length);
                    ShowSendMessage(sendFrame.Frame);
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("发送命令:" + ex.Message);
            }
        }
        /// <summary>
        /// 退出保护压板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void protectYabanTuichu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (portState)
                {
                    var send = new RTUFrame(downComputeAddress, FunEnum.PROTECT_STOP);
                    serialPort.Write(send.Frame, 0, send.Frame.Length);
                    ShowSendMessage(sendFrame.Frame);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送命令:" + ex.Message);

            }
        }
    }
}
