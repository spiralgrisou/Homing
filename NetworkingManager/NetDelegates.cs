using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingManager
{
    public class NetDelegates
    {
        #region NetConnection Delegates
        public delegate void MessageDispatcherC(NetConnection connection, string msg);
        #endregion
        #region NetServer Delegates
        public delegate void MessageDispatcherS(string msg);
        public delegate void ConnectionValidator(NetConnection connection);
        public delegate void ConnectionDestroyer(NetConnection connection);
        #endregion
    }
}
