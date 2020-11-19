using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NetworkingManager
{
    public class NetClient
    {
        public enum LoggingMethod
        {
            Console,
            Forms
        }

        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string _ipAddress { get; set; }
        private int _port { get; set; }
        private bool _dead { get; set; }
        private bool _logging { get; set; }
        private bool _exitPerms { get; set; }
        private LoggingMethod _loggingMethod { get; set; }
        private NetData.Disconnected _clientDisconnected { get; set; }
        private NetData.MessageReceived _messageReciever { get; set; }
        private NetData.Connected _clientConnected { get; set; }

        public NetClient(string address, int port, NetData.Connected clientConnecter,
            NetData.Disconnected clientDisconnecter,
            NetData.MessageReceived messageReciever,
            bool logging = false, bool exittingPerms = false, LoggingMethod loggingMethod = LoggingMethod.Console)
        {
            // Init
            _ipAddress = address;
            _port = port;
            _clientDisconnected = clientDisconnecter;
            _messageReciever = messageReciever;
            _clientConnected = clientConnecter;
            _dead = false;
            _logging = logging;
            _loggingMethod = loggingMethod;
            _exitPerms = exittingPerms;

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
                        Log($"Connection attempt {attempts}...", default, default, "Connecting");
                        continue;
                    }
                }
                if (!connected)
                {
                    // Too many attempts
                    Log("Too many attempts trying to connect to the server, exitting.", true, default, "Error", default, MessageBoxIcon.Error);
                    _dead = true;
                    return;
                }
                _clientConnected();
            }

            // Listening for messages
            Listener();
        }

        public void SetLoggingMethod(LoggingMethod method)
        {
            _loggingMethod = method;
        }

        public void SetExitPerms(bool perms)
        {
            _exitPerms = perms;
        }

        public void SetLoggingState(bool logging)
        {
            _logging = logging;
        }

        private void Log(string message, bool exit = false, bool restrictConsole = false, string formCaption = "unassigned", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            if (_logging)
            {
                bool useConsole = true;
                switch (_loggingMethod)
                {
                    case LoggingMethod.Forms:
                        if (!restrictConsole)
                            useConsole = false;
                        break;
                    case LoggingMethod.Console:
                        useConsole = true;
                        break;
                }
                if (useConsole)
                    Console.WriteLine(message);
                else
                    MessageBox.Show(message, formCaption, buttons, icon);
                if(exit && _exitPerms)
                    Environment.Exit(0);
            }
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
                        break;
                    }
                    string msg = Encoding.ASCII.GetString(buffer);
                    if (!String.IsNullOrWhiteSpace(msg) && !String.IsNullOrEmpty(msg))
                        _messageReciever(msg);
                }
            });
        }

        public void SendCommand(string command, params string[] arguments)
        {
            if (!_dead)
            {
                string finalString = String.Empty;
                finalString += command;
                foreach (string argument in arguments)
                {
                    finalString += "/" + argument;
                }
                byte[] buffer = Encoding.ASCII.GetBytes(finalString);
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
            Log("Client is not connected, Cannot sent command!", default, true);
        }

        public void Kill()
        {
            _dead = true;
            _clientDisconnected();
        }
    }
}
