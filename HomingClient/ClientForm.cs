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

namespace HomingClient
{
    public partial class ClientForm : Form
    {
        public string IP_ADDRESS { get; set; }
        public int PORT { get; set; }
        private NetClient _client { get; set; }
        private NetDelegates.Connected _connectionEvent { get; set; }
        private NetDelegates.Disconnected _disconnectionEvent { get; set; }
        private NetDelegates.MessageReceived _messageEvent { get; set; }

        public ClientForm()
        {
            _connectionEvent = client_Connected;
            _disconnectionEvent = client_Disconnected;
            _messageEvent = message_Received;
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            _client = new NetClient(IP_ADDRESS, PORT, _connectionEvent, _disconnectionEvent, _messageEvent, true, true, NetClient.LoggingMethod.Console);
            _client.SendCommand("read", new string[] { "yo i'm a client" });
        }

        private void client_Connected()
        {
            MessageBox.Show("Connected!");
        }

        private void client_Disconnected()
        {
            MessageBox.Show("Disconnected!");
        }

        private void message_Received(string msg)
        {
            MessageBox.Show($"Msg recieved: {msg}");
        }
    }
}
