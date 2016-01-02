using System;
using System.Diagnostics;
using System.ServiceModel;
using GeoLib.WindowsHost.Contracts;

namespace GeoLib.WindowsHost.Services
{
    // Run the service in a worker thread to free the UI thread
    [ServiceBehavior(UseSynchronizationContext = false)]
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            var messageToShow = message + "| Process " + Process.GetCurrentProcess().Id;
            MainWindow.MainUI.ShowMessage(messageToShow);
        }
    }
}