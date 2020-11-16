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
        private Router.ServerConnected _connectedFunc { get; set; }
        private Router.ServerDisconnected _disconnectedFunc { get; set; }
        private AddressListener _addrListener { get; set; }

        public Server(Router.ServerListener listener, Router.ServerConnected connector, Router.ServerDisconnected disconnector, AddressListener addrListener)
        {
            _listenerFunc = listener;
            _connectedFunc = connector;
            _disconnectedFunc = disconnector;
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
        
        public Router.ServerDisconnected GetDisconnector()
        {
            return _disconnectedFunc;
        }

        public Router.ServerConnected GetConnector()
        {
            return _connectedFunc;
        }
    }

    public class Router
    {
        public delegate void ServerListener(string Message);

        public delegate void ServerConnected(Connection connection);

        public delegate void ServerDisconnected(Connection connection);

        private string _latestMessage { get; set; }
        private List<Server> servers { get; set; }
        private AddressListener.RouterListener _routerListener { get; set; }
        private AddressListener.RouterConnected _routerConnector { get; set; }
        private AddressListener.RouterDisconnected _routerDisconnector { get; set; }
        public Router()
        {
            _routerListener = MessageDispatcher;
            _routerConnector = ConnectionDispatcher;
            _routerDisconnector = DisconnectionDispatcher;
            servers = new List<Server>();
        }
        
        public Server CreateServer(ServerListener listener, ServerConnected connector, ServerDisconnected disconnector, string IP, int PORT)
        {
            AddressListener HostListener = new AddressListener(IP, PORT, _routerListener, _routerConnector, _routerDisconnector);
            Server server = new Server(listener, connector, disconnector, HostListener);
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

        private void ConnectionDispatcher(AddressListener identifier, Connection connection)
        {
            Server _server = null;
            foreach (Server server in servers)
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
            _server.GetConnector()(connection);
        }

        private void DisconnectionDispatcher(AddressListener identifier, Connection connection)
        {
            Server _server = null;
            foreach (Server server in servers)
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
            _server.GetDisconnector()(connection);
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
