using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class Client
    {
        // Delegates 
        public delegate void ClientRead(string Message);

        private string _ipAddress { get; set; }
        private int _port { get; set; }
        private TcpClient _tcpClient { get; set; }
        private NetworkStream _networkStream { get; set; }
        private bool _dead { get; set; }
        private bool _idle { get; set; }

        public Client(string address, int port, ClientRead readFunc)
        {
            _dead = false;
            _ipAddress = address;
            _port = port;

            _tcpClient = new TcpClient(address, port);
            _networkStream = _tcpClient.GetStream();

            ThreadStart threadRef = new ThreadStart(() =>
            {
                while(!_dead)
                {
                    if (!_tcpClient.Connected)
                        _dead = true;
                    try
                    {
                        byte[] buffer = new byte[_tcpClient.ReceiveBufferSize];
                        int read = _networkStream.Read(buffer, 0, buffer.Length);
                        if (read > 0)
                        {
                            _idle = true;
                            ASCIIEncoding encoder = new ASCIIEncoding();
                            string message = encoder.GetString(buffer, 0, read);
                            readFunc(message);
                        }
                        else
                            _idle = false;
                    }
                    catch
                    {
                        _dead = true;
                        break;
                    }
                }
                _networkStream.Close();
                _tcpClient.Dispose();
                Thread.CurrentThread.Abort();
            });
            new Thread(threadRef).Start();
        }

        public void SendMessage(string message)
        {
            if(!_dead)
            {
                byte[] buffer = new ASCIIEncoding().GetBytes(message);
                _networkStream.Write(buffer, 0, buffer.Length);
            }
        }

        public bool isDead()
        {
            return _dead;
        }

        public void Kill()
        {
            _dead = true;
        }
    }
}
