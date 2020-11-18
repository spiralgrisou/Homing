using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class NetInfo
    {
        public static string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        public static bool IsValidIPAddress(string address)
        {
            IPAddress addr;
            return IPAddress.TryParse(address, out addr);
        }
    }
}
