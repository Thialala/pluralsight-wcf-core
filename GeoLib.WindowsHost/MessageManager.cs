using System;
using GeoLib.WindowsHost.Contracts;

namespace GeoLib.WindowsHost.Services
{
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUI.ShowMessage(message);
        }
    }
}