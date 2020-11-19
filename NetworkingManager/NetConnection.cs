using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class NetConnection
    {
        private Socket _clientSocket { get; set; }
        private NetData.MessageDispatcher _messageDispatcher { get; set; }
        private NetData.ConnectionDisconnection _connectionDisconnection { get; set; }
        private int _maxReceivable { get; set; }
        private bool _dead { get; set; }

        public NetConnection(Socket clientSocket,
            NetData.MessageDispatcher messageDispatcher,
            NetData.ConnectionDisconnection connectionDisconnection)
        {
            // Init
            _clientSocket = clientSocket;
            _messageDispatcher = messageDispatcher;
            _connectionDisconnection = connectionDisconnection;
            _maxReceivable = 1024;
            _dead = false;

            // Listener
            MessageTranslator();
        }

        private async void MessageTranslator()
        {
            await Task.Run(() =>
            {
                while (!_dead)
                {
                    byte[] tempBuffer = new byte[_maxReceivable];
                    try
                    {
                        _clientSocket.Receive(tempBuffer);
                    }
                    catch (SocketException)
                    {
                        // Client disconnected
                        Kill();
                        break;
                    }
                    string msg = Encoding.ASCII.GetString(tempBuffer);
                    if(!String.IsNullOrWhiteSpace(msg) && !String.IsNullOrEmpty(msg))
                        _messageDispatcher(this, msg);
                }
            });
        }

        public void Kill()
        {
            _dead = true;
            _connectionDisconnection(this);
        }

        public bool isDead()
        {
            return _dead;
        }
    }
}
