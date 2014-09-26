using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ZFreeGo.IntelligentControlPlatform.Modbus;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    public partial class MainWindow
    {
        private const  byte downComputeAddress = 0xEA;
        private RTUFrame sendFrame;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "发送数据");
            }

        }
        private void Led1_Click(object sender, RoutedEventArgs e)
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
                    }
                }
            }
        }

        private void Led2_Click(object sender, RoutedEventArgs e)
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
                    }
                }
            }
        }

        private void Led3_Click(object sender, RoutedEventArgs e)
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
                    }
                }
            }
        }
        private void Led4_Click(object sender, RoutedEventArgs e)
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
                    }
                }
            }
        }
        private void LedAll_Click(object sender, RoutedEventArgs e)
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
                    }
                }
            }
        }

    }
}
