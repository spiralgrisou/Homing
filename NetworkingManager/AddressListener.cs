using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class AddressListener : IListener
    {
        public delegate void RouterListener(AddressListener identifier, string Message);

        private List<Connection> _connections { get; set; }
        private bool _listening { get; set; }
        private TcpListener _tcpListener { get; set; }
        private bool _idle { get; set; }
        private RouterListener _router { get; set; }

        public AddressListener(string ADDRESS, int PORT, RouterListener router)
        {
            _router = router;
            _connections = new List<Connection>();
            _listening = true;
            _tcpListener = new TcpListener(IPAddress.Parse(ADDRESS), PORT);
            if (_tcpListener == null)
                throw new Exception("Address or port is not valid");
            ThreadStart threadRef = new ThreadStart(() =>
            {
                _tcpListener.Start();
                while (_listening)
                {
                    if (_tcpListener.Pending())
                    {
                        _idle = false;
                        Connection.HostListen listener = ConnectionListener;
                        Connection.HostDisconnect disconnector = Disconnect;
                        Connection connection = new Connection(_tcpListener.AcceptTcpClient(), listener, disconnector);
                        _connections.Add(connection);
                    }
                    else
                        _idle = true;
                }
            });
            new Thread(threadRef).Start();
        }

        bool IListener.IsIdle()
        {
            return _idle;
        }

        bool IListener.IsListening()
        {
            return _listening;
        }

        public void Kill()
        {
            _listening = false;
            foreach (Connection connection in _connections.ToArray())
            {
                connection.Disconnect();
            }
            _tcpListener.Stop();
        }

        private void Disconnect(Connection connection)
        {
            if(_connections.Contains(connection))
                _connections.Remove(connection);
        }

        private void ConnectionListener(Connection Messager, string Message)
        {
            if (!_connections.Contains(Messager))
                throw new Exception("Unauthorized connection request");
            _router(this, Message);
        }

        public int GetConnectionsCount()
        {
            return _connections.Count;
        }
    }
}
