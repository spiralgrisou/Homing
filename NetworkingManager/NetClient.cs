using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class NetClient
    {
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string _ipAddress { get; set; }
        private int _port { get; set; }

        private bool _dead { get; set; }

        private NetDelegates.Disconnected _clientDisconnected { get; set; }
        private NetDelegates.MessageReceived _messageReciever { get; set; }

        public NetClient(string address, int port, NetDelegates.Disconnected clientDisconnecter, NetDelegates.MessageReceived messageReciever)
        {
            // Init
            _ipAddress = address;
            _port = port;
            _clientDisconnected = clientDisconnecter;
            _messageReciever = messageReciever;
            _dead = false;

            // Client Init
            if (NetInfo.IsValidIPAddress(_ipAddress))
            {
                int attempts = 0;
                bool connected = false;
                while (true)
                {
                    // Attempts check
                    if (attempts >= 10)
                    {
                        connected = false;
                        break;
                    }
                    try
                    {
                        attempts++;
                        _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(_ipAddress), _port));
                        connected = true;
                        break;
                    }
                    catch (SocketException)
                    {
                        // Log connection failure
                        continue;
                    }
                }
                if (!connected)
                {
                    // Too many attempts
                    return;
                }
            }

            // Listening for messages
            Listener();

            // Disconnected
        }

        private async void Listener()
        {
            await Task.Run(() =>
            {
                while (!_dead)
                {
                    byte[] buffer = new byte[_clientSocket.ReceiveBufferSize];
                    try
                    {
                        _clientSocket.Receive(buffer);
                    }
                    catch (SocketException)
                    {
                        // Client disconnected because of receive failure
                        Kill();
                    }
                    string msg = Encoding.ASCII.GetString(buffer);
                    _messageReciever(msg);
                }
            });
        }

        public void SendCommand(string command, params string[] arguments)
        {
            string finalString = String.Empty;
            finalString += command;
            foreach (string argument in arguments)
            {
                finalString += "/" + argument;
            }
            byte[] buffer = Encoding.ASCII.GetBytes(finalString);
            if (!_dead)
            {
                try
                {
                    _clientSocket.Send(buffer);
                }
                catch (SocketException)
                {
                    // Server down
                    Kill();
                }
            }
        }

        public void Kill()
        {
            _dead = true;
            _clientDisconnected();
        }
    }
}
