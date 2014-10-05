using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZFreeGo.IntelligentControlPlatform.Modbus;

namespace ZFreeGo.IntelligentControlPlatform.ControlCenter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (serialPort != null)
            {
                
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                
            }
            if (readThread != null)
            {
                readThread.Join(500);
                readThread.Abort();
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
        }

        private void singleHexCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            

            if ((e.Key >= Key.A && e.Key <= Key.F)
                || (e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || e.Key == Key.Left || e.Key == Key.Right
                || e.Key == Key.Up || e.Key == Key.Down
                || e.Key == Key.Delete
                || e.Key == Key.Back)
            {
                

            }
            else
            {
                e.Handled = true;
            }
            
        }

        private void singleHexUpperLenCheck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                var txtbox = e.OriginalSource as TextBox;
                if (txtbox.Text.Length > 2)
                {
                    txtbox.Text = txtbox.Text.Remove(2, txtbox.Text.Length - 2);
                }
                txtbox.Text = txtbox.Text.ToUpper();
            }
            
          
        }

        private void multiHexCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.F)
               || (e.Key >= Key.D0 && e.Key <= Key.D9)
               || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || e.Key == Key.Left || e.Key == Key.Right
               || e.Key == Key.Up || e.Key == Key.Down
               || e.Key == Key.Delete
               || e.Key == Key.Back
               || e.Key == Key.Space)
            {


            }
            else
            {
                e.Handled = true;
            }
        }
        private List<string> GetDecimalByHexString(string hexValues)
        {
            try
            {
                string str = null;
                List<string> result = new List<string>();
                hexValues = hexValues.ToUpper();
                foreach (var hex in hexValues)
                {

                    if (hex != ' ')
                    {
                        if ((hex >= '0' && hex <= '9') || (hex >= 'A' && hex <= 'F'))
                        {
                            if (str == null)
                            {
                                str = hex.ToString();
                            }
                            else
                            {
                                str += hex;


                                if (str.Length == 2)
                                {
                                    result.Add(str);
                                    str = null;

                                }


                            }
                        }

                    }
                    else
                    {
                        if (str != null)
                        {
                            result.Add(str);
                            str = null;
                        }

                    }
                }
                //最后单位检测
                if (str != null)
                {
                    if (str.Length == 1)
                    {
                        result.Add(str);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "字符串转换");
                return null;
            }

        }
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            string hexValues = "16 3ef  34 a0 fhf";
            string str = null;
            List<string> result = new List<string>();
            hexValues = hexValues.ToUpper();
            foreach(var hex in hexValues)
            {
                
                if (hex != ' ')
                {
                    if ((hex >= '0' && hex <= '9') || (hex >= 'A' && hex <= 'F'))
                    {
                        if (str == null)
                        {
                            str = hex.ToString();
                        }
                        else
                        {
                            str += hex;


                            if (str.Length == 2)
                            {
                                result.Add(str);
                                str = null;

                            }


                        }
                    }
                   
                }
                else
                {
                    if (str != null)
                    {
                        result.Add(str);
                        str = null;
                    }

                }
            }

            foreach (var s in result)
            {
                dataTxt.Text += s.ToUpper() + " ";
            }



        }


        private void dataTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                var txtbox = e.OriginalSource as TextBox;

                 txtbox.Text = txtbox.Text.ToUpper();
            }
        }

      


       

       

 


       

        

        

       
      
        

        

        

       
    }
}
