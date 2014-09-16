using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

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
                    portState = true;
                    openSerialPort.Content = "关闭串口";
                    portName.IsEnabled = false;
                    baudRate.IsEnabled = false;
                    dataBits.IsEnabled = false;
                    portParity.IsEnabled = false;
                    stopBits.IsEnabled = false;

                    serialPort.PortName = portName.SelectedItem as string;
                    serialPort.BaudRate = (int)baudRate.SelectedItem;
                    serialPort.DataBits = (int)dataBits.SelectedItem;
                    serialPort.Parity = (Parity)portParity.SelectedItem;

                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits.SelectedItem.ToString());

                    serialPort.ReadTimeout = 500;
                    serialPort.WriteTimeout = 500;

                    serialPort.Open();


                    readThread = new Thread(Read);
                    readThread.Start();
                }
                else
                {
                    portState = false;
                    openSerialPort.Content = "打开串口";
                    portName.IsEnabled = true;
                    baudRate.IsEnabled = true;
                    dataBits.IsEnabled = true;
                    portParity.IsEnabled = true;
                    stopBits.IsEnabled = true;

                    readThread.Join(500);
                   // readThread.Abort();
                    
                    serialPort.Close();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("开启或关闭端口::" + ex.Message);
                Trace.WriteLine("开启或关闭端口::" + ex.Message);
            }
            
        }
        private void sendTest_Click(object sender, RoutedEventArgs e)
        {
            serialPort.WriteLine("ABCDEF0123456789!");
        }
        private void Read()
        {
            Action<object> call = ar => reciveListBox.Items.Add(ar);

            Dispatcher.BeginInvoke(call, "start");
            while (portState)
            {
                try
                {
                    
                    string message = serialPort.ReadLine();
                    if (message != "")
                    {
                        Dispatcher.BeginInvoke(call, message);
                    }
                }
                catch (TimeoutException) { }
            }
        }
    }
}
