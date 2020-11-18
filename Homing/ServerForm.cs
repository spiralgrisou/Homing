using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkingManager;

namespace Homing
{
    public partial class ServerForm : Form
    {
        public string IP_ADDRESS;
        public int PORT;

        public int ActiveUsers = 0;

        private Router _router { get; set; }

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            UpdateActiveUsers();
            Router.ServerListener listener = Listener;
            Router.ServerConnected connector = Connector;
            Router.ServerDisconnected disconnector = Disconnector;
            _router = new Router();
            NetServer server = _router.CreateServer(listener, connector, disconnector, IP_ADDRESS, PORT);
            MessageBox.Show("Server created in " + IP_ADDRESS + " on Port: " + PORT + ", Active Connections: " + server.GetHostServer().GetConnectionsCount(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private delegate void SetTextCallback();

        private void UpdateActiveUsers()
        {
            if(activeLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateActiveUsers);
                Invoke(d);
            }
            else
            {
                activeLabel.Text = "Server Channels: (" + ActiveUsers + " users are active)";
            }
        }

        public void Connector(Connection connection)
        {
            MessageBox.Show("User connected!");
            ActiveUsers++;
            UpdateActiveUsers();
        }

        public void Disconnector(Connection connection)
        {
            MessageBox.Show("User disconnected!");
            ActiveUsers--;
            UpdateActiveUsers();
        }

        public void Listener(string Message)
        {
            MessageBox.Show(Message);
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _router.CloseDown();
        }
    }
}
