using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
using GeoLib.Client.Contracts;
using GeoLib.Proxies;
using Binding = System.ServiceModel.Channels.Binding;

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                         " | Process " + Process.GetCurrentProcess().Id;
        }

        private void BtnGetInfo_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != null)
            {
                var proxy = new GeoClient("tcpEP");
                var data = proxy.GetZipInfo(txtZipCode.Text);
                if (data != null)
                { 
                    lblCity.Content = data.City;
                    lblState.Content = data.State;
                }

                proxy.Close();
            }
        }

        private void BtnGetZipCodes_OnClick(object sender, RoutedEventArgs e)
        {
            if (txtState != null)
            {
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();

                //GeoClient proxy = new GeoClient(binding, address);

                var proxy = new GeoClient("tcpEP");
                var datas = proxy.GetZips(txtState.Text);

                if (datas != null)
                {
                    lstZips.ItemsSource = datas;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMsg(txtMessage.Text);

            factory.Close();
        }
    }
}
