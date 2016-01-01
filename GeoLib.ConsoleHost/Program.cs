using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.ConsoleHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

            //var address = "net.tcp://localhost:8009/GeoService";
            //var binding = new NetTcpBinding();
            //var contract = typeof (IGeoService);
            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            hostGeoManager.Open();

            Console.WriteLine(("Service started. Press [Enter] to exit."));
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
