using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingManager
{
    public class NetDelegates
    {
        public delegate void MessageDispatcher(NetConnection connection, string msg);
        public delegate void ConnectionSuccess(NetConnection connection);
        public delegate void ConnectionDisconnection(NetConnection connection);
    }
}
