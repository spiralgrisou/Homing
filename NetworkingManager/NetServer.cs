﻿using System;
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
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private int _backLog = 10;
        private int _serverLimit = 50;

        private string _ipAddress { get; set; }
        private int _port { get; set; }
        private bool _idle { get; set; }
        private bool _dead { get; set; }
        private NetDelegates.MessageDispatcher _msgDispatcher { get; set; }
        private NetDelegates.ConnectionDisconnection _connectionDisconnection { get; set; }
        private NetDelegates.ConnectionDisconnection _topDisconnection { get; set; }
        private NetDelegates.ConnectionSuccess _connectionSuccess { get; set; }

        public NetServer(string address, int port, int queueLength, int serverLimit, NetDelegates.MessageDispatcher msgDispatcher, NetDelegates.ConnectionDisconnection disconnectionCall, NetDelegates.ConnectionSuccess connectionCall)
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

            // Server Init
            if (NetInfo.IsValidIPAddress(_ipAddress))
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Parse(_ipAddress), _port));
                _serverSocket.Listen(_backLog);
            }

            // Listener
            ListenLoop();
        }

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
                        Socket connectedSocket = await _serverSocket.AcceptAsync();
                        try
                        {
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
                            continue;
                        }
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

        public void Kill()
        {
            _dead = true;
            _netConnections = new List<NetConnection>();
        }

        public bool isDead()
        {
            return _dead;
        }
    }
}
