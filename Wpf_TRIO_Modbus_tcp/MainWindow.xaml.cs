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

        ModbusClient modbusClient = new ModbusClient("127.0.0.1", 502);
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
                    ConnectStatus.Content = "连接失败！";
                }
                else
                {
                    ConnectStatus.Content = "连接成功！";
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
            bool[] InputStatus =  modbusClient.ReadDiscreteInputs(0, 5);

            string[] Inputlabel= { "label3_Copy0", "label3_Copy1", "label3_Copy2", "label3_Copy3", "label3_Copy4" };

            //Object obj = Grid.Controls.Find("控件名称", fale).First();

            foreach ( int item in  (InputStatus).ToString())
            {

            }
        }
    }
}
