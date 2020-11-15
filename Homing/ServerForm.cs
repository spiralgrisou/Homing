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
            activeLabel.Text = "Server Channels: (" + ActiveUsers + " users are active)";
            Router.ServerListener listener = Listener;
            _router = new Router();
            Server server = _router.CreateServer(listener, IP_ADDRESS, PORT);
            MessageBox.Show("Server created in " + IP_ADDRESS + " on Port: " + PORT + ", Active Connections: " + server.GetHostServer().GetConnectionsCount(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
