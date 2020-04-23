using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyModbus;
using System.Threading;
using System.Text.RegularExpressions;

namespace Wpf_TRIO_Modbus_tcp
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


        ModbusClient modbusClient = new ModbusClient("192.168.250",502);


        //点击连接
        private void b_connect_Click(object sender, RoutedEventArgs e)
        {
            modbusClient.Connect();
            Thread.Sleep(1000);
            int[] i = modbusClient.ReadInputRegisters(10, 1);
            foreach (int team in i)
            {
                if (team != 1)
                {
                    ConnectStatus.Content = "连接失败";
                }
                else
                {
                    ConnectStatus.Content = "连接成功";
                    ConnectStatus.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }

        //点击断连接
        private void b_disconnect_Click(object sender, RoutedEventArgs e)
        {
            modbusClient.Disconnect();
            Thread.Sleep(1000);
            ConnectStatus.Content = "未连接...";
            ConnectStatus.Foreground = new SolidColorBrush(Colors.Red);
        }


        private void b_read_input_Click(object sender, RoutedEventArgs e)
        {
            //读取输入 input 信号 :  ReadDiscreteInputs
            bool[] InputStatus =  modbusClient.ReadDiscreteInputs(0, 3);
            if (InputStatus[0] == true)
            {
                label3_Copy0.Background = new SolidColorBrush(Colors.Green);
                label3_Copy0.Content = InputStatus[0].ToString();
            }
            else
            {
                label3_Copy0.Background = new SolidColorBrush(Colors.Red);
                label3_Copy0.Content = InputStatus[0].ToString();
            }

            if (InputStatus[1] == true)
            {
                label3_Copy1.Background = new SolidColorBrush(Colors.Green);
                label3_Copy1.Content = InputStatus[1].ToString();
            }
            else
            {
                label3_Copy1.Background = new SolidColorBrush(Colors.Red);
                label3_Copy1.Content = InputStatus[1].ToString();
            }

            if (InputStatus[2] == true)
            {
                label3_Copy2.Background = new SolidColorBrush(Colors.Green);
                label3_Copy2.Content = InputStatus[2].ToString();
            }
            else
            {
                label3_Copy2.Background = new SolidColorBrush(Colors.Red);
                label3_Copy2.Content = InputStatus[2].ToString();
            }

        }

        private void bReadOp_Click(object sender, RoutedEventArgs e)
        {
            //读取输出线圈信号
            bool[] OutputStatus = modbusClient.ReadCoils(8, 3);
           // MessageBox.Show(OutputStatus[0].ToString());
            ///
            if (OutputStatus[0] == true)
            {
                label3_Copy9.Background = new SolidColorBrush(Colors.Green);
                label3_Copy9.Content = OutputStatus[0].ToString();
            }
            else
            {
                label3_Copy9.Background = new SolidColorBrush(Colors.Red);
                label3_Copy9.Content = OutputStatus[0].ToString();
            }

            ///
            if (OutputStatus[1] == true)
            {
                label3_Copy10.Background = new SolidColorBrush(Colors.Green);
                label3_Copy10.Content = OutputStatus[1].ToString();
            }
            else
            {
                label3_Copy10.Background = new SolidColorBrush(Colors.Red);
                label3_Copy10.Content = OutputStatus[1].ToString();
            }

            ///
            if (OutputStatus[2] == true)
            {
                label3_Copy11.Background = new SolidColorBrush(Colors.Green);
                label3_Copy11.Content = OutputStatus[2].ToString();
            }
            else
            {
                label3_Copy11.Background = new SolidColorBrush(Colors.Red);
                label3_Copy11.Content = OutputStatus[2].ToString();
            }

        }

        private void bWriteOpT_Click(object sender, RoutedEventArgs e)
        {
            //批量写输出信号
            bool[] WriteCoilsT = { true,true,true };
            modbusClient.WriteMultipleCoils(8, WriteCoilsT);
            label3_Copy9.Background = new SolidColorBrush(Colors.Green);
            label3_Copy9.Content = "TRUE";
            label3_Copy10.Background = new SolidColorBrush(Colors.Green);
            label3_Copy10.Content = "TRUE";
            label3_Copy11.Background = new SolidColorBrush(Colors.Green);
            label3_Copy11.Content = "TRUE";
        }

        private void bWriteOp_Click(object sender, RoutedEventArgs e)
        {
            //批量写输出信号
            bool[] WriteCoilsT = { false, false, false };
            modbusClient.WriteMultipleCoils(8, WriteCoilsT);
            label3_Copy9.Background = new SolidColorBrush(Colors.Red);
            label3_Copy9.Content = "FALSE";
            label3_Copy10.Background = new SolidColorBrush(Colors.Red);
            label3_Copy10.Content = "FALSE";
            label3_Copy11.Background = new SolidColorBrush(Colors.Red);
            label3_Copy11.Content = "FALSE";
        }

        private void ReadVr_Click(object sender, RoutedEventArgs e)
        {
            //读取多个寄存器的值
           int[] ReadVrVal = modbusClient.ReadHoldingRegisters(0, 3);
            textBoxVr0.Text = ReadVrVal[0].ToString();
            textBoxVr1.Text = ReadVrVal[1].ToString();
            textBoxVr2.Text = ReadVrVal[2].ToString();
        }


        private void WriteVr_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxVr0.Text != null && Regex.IsMatch(textBoxVr0.Text, @"^[+-]?\d*[.]?\d*$"))
            {
                //写多个寄存器的值
                int[] WriteVrVal = { int.Parse(textBoxVr0.Text.Trim()), int.Parse(textBoxVr1.Text.Trim()), int.Parse(textBoxVr2.Text.Trim()) };
                modbusClient.WriteMultipleRegisters(0, WriteVrVal);

                MessageBox.Show("写入成功");
            }
            else
            {
                MessageBox.Show("非法值");
                textBoxVr0.Text = "0";
                textBoxVr1.Text = "0";
                textBoxVr2.Text = "0";

            }

        }
    }
}
