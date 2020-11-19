using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NetworkingManager
{
    public class NetServer
    {
        private List<NetConnection> _netConnections = new List<NetConnection>();
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private int _backLog = 10;
        private int _serverLimit = 50;

        private string _ipAddress { get; set; }
        private int _port { get; set; }
        private bool _idle { get; set; }
        private bool _dead { get; set; }
        private NetData.MessageDispatcher _msgDispatcher { get; set; }
        private NetData.ConnectionDisconnection _connectionDisconnection { get; set; }
        private NetData.ConnectionDisconnection _topDisconnection { get; set; }
        private NetData.ConnectionSuccess _connectionSuccess { get; set; }
        private bool _logging { get; set; }
        private NetData.LoggingMethod _loggingMethod { get; set; }
        private bool _exitPerms { get; set; }

        public NetServer(string address, int port, int queueLength, int serverLimit, NetData.MessageDispatcher msgDispatcher, NetData.ConnectionDisconnection disconnectionCall, NetData.ConnectionSuccess connectionCall, bool logging = false, bool exittingPerms = false, NetData.LoggingMethod loggingMethod = NetData.LoggingMethod.Console)
        {
            // Init
            _ipAddress = address;
            _port = port;
            _msgDispatcher = msgDispatcher;
            _backLog = queueLength;
            _serverLimit = serverLimit;
            _connectionDisconnection = Destroyer;
            _topDisconnection = disconnectionCall;
            _connectionSuccess = connectionCall;
            _dead = false;
            _idle = true;
            _logging = logging;
            _loggingMethod = _loggingMethod;
            _exitPerms = exittingPerms;

            // Server Init
            if (NetInfo.IsValidIPAddress(_ipAddress))
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(_ipAddress), _port));
                _serverSocket.Listen(_backLog);
            }

            // Listener
            ListenLoop();
        }

        // Connection listener
        private async void ListenLoop()
        {
            await Task.Run(async () =>
            {
                while(!_dead)
                {
                    _idle = true;
                    // If we are not over the limit then accept connections
                    if (_netConnections.Count < _serverLimit)
                    {
                        try
                        {
                            Socket connectedSocket = await _serverSocket.AcceptAsync();
                            _idle = false;
                            byte[] buffer = Encoding.ASCII.GetBytes("connection/success");
                            connectedSocket.Send(buffer);
                            NetConnection connection = new NetConnection(connectedSocket, _msgDispatcher, _connectionDisconnection);
                            _netConnections.Add(connection);
                            _connectionSuccess(connection);
                        }
                        catch (SocketException)
                        {
                            // Client disconnected
                            Log("Client connection failed", default, true);
                            continue;
                        }
                    }
                }
            });
        }

        // Connection disconnection listener
        private void Destroyer(NetConnection connection)
        {
            if (_netConnections.Contains(connection))
            {
                _netConnections.Remove(connection);
                _topDisconnection(connection);
                Log("Client deleted", default, true);
            }
        }

        private void Log(string message, bool exit = false, bool restrictConsole = false, string formCaption = "unassigned", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            if (_logging)
            {
                bool useConsole = true;
                switch (_loggingMethod)
                {
                    case NetData.LoggingMethod.Forms:
                        if (!restrictConsole)
                            useConsole = false;
                        break;
                    case NetData.LoggingMethod.Console:
                        useConsole = true;
                        break;
                }
                if (useConsole)
                    Console.WriteLine(message);
                else
                    MessageBox.Show(message, formCaption, buttons, icon);
                if (exit && _exitPerms)
                    Environment.Exit(0);
            }
        }

        public bool isIdle()
        {
            return _idle;
        }

        public void Kill()
        {
            _dead = true;
            _netConnections = new List<NetConnection>();
            Log("Killed server", false, true);
        }

        public int GetConnectionsCount()
        {
            return _netConnections.Count;
        }

        public bool isDead()
        {
            return _dead;
        }
    }
}
