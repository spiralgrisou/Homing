using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace NetworkingManager
{
    public class Connection
    {
        public delegate void HostListen(Connection connection, string Message);
        public delegate void HostDisconnect(Connection connection);

        private bool _dead { get; set; }
        public string LastMessage { get; set; }
        private NetworkStream _clientStream { get; set; }


        private HostListen _hostListen { get; set; }
        private HostDisconnect _hostDisconnect { get; set; }


        public Connection(TcpClient tcpClient, HostListen listener, HostDisconnect disconnector)
        {
            // Init
            _hostListen = listener;
            _hostDisconnect = disconnector;
            _clientStream = tcpClient.GetStream();

            byte[] message = new byte[tcpClient.ReceiveBufferSize];
            int bytesRead = 0;
            ThreadStart threadRef = new ThreadStart(() =>
            {
                _dead = false;
                while (!_dead)
                {
                    try
                    {
                        bytesRead = _clientStream.Read(message, 0, tcpClient.ReceiveBufferSize);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    // Disconnection check
                    if (bytesRead == 0)
                        _dead = true;

                    ASCIIEncoding encoder = new ASCIIEncoding();
                    LastMessage = encoder.GetString(message, 0, bytesRead);
                    DispatchMessage(LastMessage);
                }
            });
            new Thread(threadRef).Start();
        }

        void DispatchMessage(string Message)
        {
            _hostListen(this, Message);
        }

        public void Disconnect()
        {
            _dead = true;
            _hostDisconnect(this);
        }

        public bool isDead()
        {
            return _dead;
        }
    }
}
