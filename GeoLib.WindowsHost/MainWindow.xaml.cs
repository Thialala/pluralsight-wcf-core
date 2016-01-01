using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using GeoLib.Services;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                         " | Process " + Process.GetCurrentProcess().Id;

            MainUI = this;
        }

        private ServiceHost _hostGeoManager;
        private ServiceHost _hostMessageManager;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostGeoManager.Open();

            _hostMessageManager = new ServiceHost(typeof(MessageManager));
            _hostMessageManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager.Close();
            _hostMessageManager.Close();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        internal void ShowMessage(string message)
        {
            lblText.Content = message + "(shown on thread " + Thread.CurrentThread.ManagedThreadId +
                              " | Process " + Process.GetCurrentProcess().Id + ")";
        }
    }
}
