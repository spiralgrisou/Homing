namespace NetworkingManager
{
    public class NetData
    {
        #region Shared data
        public enum LoggingMethod
        {
            Console,
            Forms
        }
        #endregion
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
