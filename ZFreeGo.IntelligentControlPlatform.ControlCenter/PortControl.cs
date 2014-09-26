using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using ZFreeGo.IntelligentControlPlatform.Modbus;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    public partial class MainWindow : Window
    {

        private SerialPort serialPort;
        private bool portState = false;
        private Thread readThread;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // string name;
           // string message;
            //StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
           // Thread readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            InitSerialPort();
        }
        void InitSerialPort()
        {
            serialPort = new SerialPort();
            UpdatePortName(serialPort.PortName);
            UpdatePortBaudRate(serialPort.BaudRate);
            UpdatePortDataBits(serialPort.DataBits);
            UpdatePortParity(serialPort.Parity);
            UpdatePortStopBits(serialPort.StopBits);

            //serialPort.BytesToRead;
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            //serialPort.DataReceived += DataReceivedHandler;

            sendTest.IsEnabled = false;
            HexShow();


        }
        void HexShow()
        {
            deviceAddrTxt.Text = String.Format("{0:x}", 32, 23);
        }
        public  void  UpdatePortName(string defaultPortName)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                portName.Items.Add(s);
            }
            portName.SelectedIndex = 0;
        }

        public  void UpdatePortBaudRate(int defaultPortBaudRate)
        {
            baudRate.Items.Add(1200);
            baudRate.Items.Add(2400);
            baudRate.Items.Add(4800);
            baudRate.Items.Add(9600);
            baudRate.Items.Add(14400);
            baudRate.Items.Add(28800);
            baudRate.Items.Add(38400);
            baudRate.Items.Add(57600);
            baudRate.Items.Add(115200);

            baudRate.SelectedItem = defaultPortBaudRate;
        }
        public void UpdatePortDataBits(int defaultPortDataBits)
        {
            dataBits.Items.Add(5);
            dataBits.Items.Add(6);
            dataBits.Items.Add(7);
            dataBits.Items.Add(8);

            dataBits.SelectedItem = defaultPortDataBits;
           // return int.Parse(dataBits);
        }
        public void UpdatePortParity(Parity defaultPortParity)
        {
            portParity.Items.Add(Parity.Even);
            portParity.Items.Add(Parity.Odd);
            portParity.Items.Add(Parity.Mark);
            portParity.Items.Add(Parity.Space);
            portParity.Items.Add(Parity.None);

            portParity.SelectedItem = defaultPortParity;
           // return (Parity)Enum.Parse(typeof(Parity), parity);
        }

        public void UpdatePortStopBits(StopBits defaultPortStopBits)
        {
          //  stopBits.Items.Add(StopBits.None);
            stopBits.Items.Add(StopBits.One);
            stopBits.Items.Add(StopBits.OnePointFive);
            stopBits.Items.Add(StopBits.Two);

            stopBits.SelectedItem = StopBits.One;
            //return (StopBits)Enum.Parse(typeof(StopBits), stopBits);
        }

        private void OpenSerialPort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!portState)
                {
                    

                    serialPort.PortName = portName.SelectedItem as string;
                    serialPort.BaudRate = (int)baudRate.SelectedItem;
                    serialPort.DataBits = (int)dataBits.SelectedItem;
                    serialPort.Parity = (Parity)portParity.SelectedItem;

                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits.SelectedItem.ToString());

                    

                    serialPort.Open();

                    portState = true;
                    readThread = new Thread(Read);
                  
                    readThread.Start();

                    
                    openSerialPort.Content = "关闭串口";
                    portName.IsEnabled = false;
                    baudRate.IsEnabled = false;
                    dataBits.IsEnabled = false;
                    portParity.IsEnabled = false;
                    stopBits.IsEnabled = false;

                    sendTest.IsEnabled = true;

                }
                else
                {

                    portState = false;
                    readThread.Join(500);
                    readThread.Abort();
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }

                   
                    openSerialPort.Content = "打开串口";
                    portName.IsEnabled = true;
                    baudRate.IsEnabled = true;
                    dataBits.IsEnabled = true;
                    portParity.IsEnabled = true;
                    stopBits.IsEnabled = true;

                    sendTest.IsEnabled = false;
                    
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("开启或关闭端口::" + ex.Message);
                Trace.WriteLine("开启或关闭端口::" + ex.Message);
            }
            
        }
        

        private  void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {

            Action<object> call = ar => { reciveListBox.Items.Add(ar); reciveListBox.ScrollIntoView(ar); };

            //Dispatcher.BeginInvoke(call, "start");

            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //Console.WriteLine("Data Received:");
           // Console.Write(indata);
            Dispatcher.BeginInvoke(call, indata);
        }
        private void Read()
        {
            Action<object> call = ar => { reciveListBox.Items.Add(ar); reciveListBox.ScrollIntoView(ar); };
            Func<byte> reciveByte = () =>
            {
                
                return (byte)serialPort.ReadByte();
            };

            Action<RTUFrame> callShowRecivFrame = ar =>
                {
                    deviceAddrReciveTxt.Text = String.Format("{0:X}, ", ar.Address);
                    funCodeReciveTxt.Text = String.Format("{0:X}", ar.Function);
                    dataLenReciveTxt.Text = String.Format("{0:X}",ar.DataLen);
                   
                    dataReciveTxt.Text = "";
                    foreach (var data in ar.FrameData)
                    {
                        dataReciveTxt.Text += String.Format("{0:X} ", data);
                    }

      
                };

            var reciveTool = new ReciveRtuFrame();
            reciveTool.ReciveAbyte = reciveByte;
           /// Dispatcher.BeginInvoke(call, "start");
            while (portState)
            {
                try
                {
                    try
                    {

                        if (reciveTool.JudgeGetByte(sendFrame))
                        {
                            Dispatcher.BeginInvoke(call, "接收帧");
                            foreach (var cha in reciveTool.ReciveFrame.Frame)
                            {
                                Dispatcher.BeginInvoke(call, cha);
                            }
                            Dispatcher.BeginInvoke(callShowRecivFrame, reciveTool.ReciveFrame);
                            
                        }
                        Thread.Sleep(100);
                        //string message = serialPort.ReadLine();
                        //var message = serialPort.ReadByte();

                        //Dispatcher.BeginInvoke(call, message);

                    }
                    catch (TimeoutException) { }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("串口接收进程::" + ex.Message);
                }
            }
        }
    }
}
