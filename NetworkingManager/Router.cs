using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkingManager
{
    public class Server
    {
        private Router.ServerListener _listenerFunc { get; set; }
        private AddressListener _addrListener { get; set; }

        public Server(Router.ServerListener listener, AddressListener addrListener)
        {
            _listenerFunc = listener;
            _addrListener = addrListener;
        }

        public AddressListener GetHostServer()
        {
            return _addrListener;
        }

        public Router.ServerListener GetListener()
        {
            return _listenerFunc;
        }
    }

    public class Router
    {
        public delegate void ServerListener(string Message);

        private string _latestMessage { get; set; }

        private static List<Server> servers { get; set; }

        private static AddressListener.RouterListener _routerListener;

        public Router()
        {
            _routerListener = MessageDispatcher;
            servers = new List<Server>();
        }
        
        public Server CreateServer(ServerListener listener, string IP, int PORT)
        {
            AddressListener HostListener = new AddressListener(IP, PORT, _routerListener);
            Server server = new Server(listener, HostListener);
            servers.Add(server);
            return server;
        }

        public void CloseDown()
        {
            foreach(Server server in servers)
            {
                server.GetHostServer().Kill();
            }
        }

        private void MessageDispatcher(AddressListener identifier, string Message)
        {
            _latestMessage = Message;
            Server _server = null;
            foreach(Server server in servers)
            {
                AddressListener addr = server.GetHostServer();
                if (addr == identifier)
                {
                    _server = server;
                    break;
                }
            }
            if (_server == null)
                throw new Exception("Unauthorized server sending a message");
            _server.GetListener()(Message);
        }

        public string GetLatestMessage()
        {
            return _latestMessage;
        }
    }
}
