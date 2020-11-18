namespace NetworkingManager
{
    public class NetDelegates
    {
        #region NetServer Delegates
        public delegate void MessageDispatcher(NetConnection connection, string msg);
        public delegate void ConnectionSuccess(NetConnection connection);
        public delegate void ConnectionDisconnection(NetConnection connection);
        #endregion
        #region NetClient Delegates
        public delegate void MessageReceived(string msg);
        public delegate void Disconnected();
        public delegate void Connected();
        #endregion
    }
}
