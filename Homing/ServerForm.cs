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
        private NetServer _netServer { get; set; }

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            UpdateActiveUsers();
            NetData.MessageDispatcher dispatcher = Listener;
            NetData.ConnectionSuccess connectionSuccess = Connector;
            NetData.ConnectionDisconnection disconnected = Disconnector;
            _netServer = new NetServer(IP_ADDRESS, PORT, 5, 50, dispatcher, disconnected, connectionSuccess, true, false);
            MessageBox.Show("Server created in " + IP_ADDRESS + " on Port: " + PORT + ", Active Connections: " + _netServer.GetConnectionsCount(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void Connector(NetConnection connection)
        {
            ActiveUsers++;
            UpdateActiveUsers();
        }

        public void Disconnector(NetConnection connection)
        {
            ActiveUsers--;
            UpdateActiveUsers();
        }

        public void Listener(NetConnection connection, string msg)
        {
            MessageBox.Show(msg);
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _netServer.Kill();
        }
    }
}
