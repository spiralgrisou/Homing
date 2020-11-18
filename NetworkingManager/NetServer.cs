using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class NetServer
    {
        private List<NetConnection> _netConnections = new List<NetConnection>();
        private string _ipAddress { get; set; }
        private Socket _serverSocket { get; set; }
        private string _port { get; set; }
        private bool _idle { get; set; }
        private bool _dead { get; set; }
        private NetDelegates.MessageDispatcher _msgDispatcher { get; set; }
        private NetDelegates.ConnectionDisconnection _connectionDisconnection { get; set; }
        private NetDelegates.ConnectionDisconnection _topDisconnection { get; set; }
        private NetDelegates.ConnectionSuccess _connectionSuccess { get; set; }

        public NetServer(string address, string port, NetDelegates.MessageDispatcher msgDispatcher, NetDelegates.ConnectionDisconnection disconnectionCall, NetDelegates.ConnectionSuccess connectionCall)
        {
            // Init
            _ipAddress = address;
            _port = port;
            _msgDispatcher = msgDispatcher;
            _connectionDisconnection = Destroyer;
            _topDisconnection = disconnectionCall;
            _connectionSuccess = connectionCall;

            // Listener
            ListenLoop();
        }

        private async void ListenLoop()
        {
            await Task.Run(async () =>
            {
                while(!_dead)
                {
                    Socket connectedSocket = await _serverSocket.AcceptAsync();
                    try
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes("connection success");
                        connectedSocket.Send(buffer);
                        NetConnection connection = new NetConnection(connectedSocket, _msgDispatcher, _connectionDisconnection);
                        _netConnections.Add(connection);
                        _connectionSuccess(connection);
                    }
                    catch (SocketException)
                    {
                        // Client disconnected
                        continue;
                    }
                }
            });
        }

        private void Destroyer(NetConnection connection)
        {
            if (_netConnections.Contains(connection))
            {
                _netConnections.Remove(connection);
                _topDisconnection(connection);
            }
        }

        public bool isIdle()
        {
            return _idle;
        }
        public bool isDead()
        {
            return _dead;
        }
    }
}
