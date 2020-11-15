using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public interface IListener
    {
        bool IsIdle();
        bool IsListening();
    }
}
