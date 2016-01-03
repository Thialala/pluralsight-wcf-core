using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using System.Windows;

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
                var proxy = new ServiceReference1.GeoServiceClient();
                ServiceReference1.ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
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

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
