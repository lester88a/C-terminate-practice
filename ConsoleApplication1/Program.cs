using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //display the IP
            Console.WriteLine(GetLocalIPAddress());

            Console.WriteLine("Starting kill the application...");
            //kill the process
            try
            {
                foreach (Process proc in Process.GetProcessesByName("notepad"))
                {
                    if (proc.ProcessName=="notepad")
                    {
                        proc.Kill();
                        Console.WriteLine("The application terminate successful!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, see this following errors:");
                Console.WriteLine(ex); ;
            }

            //detect IP changes
            NetworkChange.NetworkAddressChanged += new
            NetworkAddressChangedEventHandler(AddressChangedCallback);
            Console.WriteLine("Listening for address changes. Press any key to exit.");
            Console.ReadLine();
        }
        //get local IP
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        //listening IP changes
        static void AddressChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                string currentIP = GetLocalIPAddress();
                Console.WriteLine(currentIP);

                if (currentIP.Contains("10.12.0"))
                {
                    Console.WriteLine("KILLLLLL");
                }
            }
        }
    }
}
