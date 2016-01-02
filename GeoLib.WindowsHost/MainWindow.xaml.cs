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
using GeoLib.WindowsHost.Contracts;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }

        private SynchronizationContext _synchronizationContext;


        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                         " | Process " + Process.GetCurrentProcess().Id;

            MainUI = this;

            _synchronizationContext = SynchronizationContext.Current;
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
            int mainThreadId = Thread.CurrentThread.ManagedThreadId;

            SendOrPostCallback sendOrPostCallback =
                arg =>
                    lblText.Content =
                        message + Environment.NewLine + " (marshalled from thread " +
                        Thread.CurrentThread.ManagedThreadId + " to thread " + mainThreadId +
                        " | Process " + Process.GetCurrentProcess().Id + ")";

            _synchronizationContext.Send(sendOrPostCallback, null);

        }

        private void btnProcService_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(
                () =>
                {
                    ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
                    IMessageService proxy = factory.CreateChannel();

                    proxy.ShowMessage(DateTime.Now.ToLocalTime().ToLongTimeString());

                    factory.Close();
                }

                );
        }
    }
}
