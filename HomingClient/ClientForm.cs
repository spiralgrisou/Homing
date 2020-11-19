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
        private NetData.Connected _connectionEvent { get; set; }
        private NetData.Disconnected _disconnectionEvent { get; set; }
        private NetData.MessageReceived _messageEvent { get; set; }
        private int _activeUsers { get; set; }

        public ClientForm()
        {
            _connectionEvent = client_Connected;
            _disconnectionEvent = client_Disconnected;
            _messageEvent = message_Received;
            _activeUsers = 0;
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            _client = new NetClient(IP_ADDRESS, PORT, _connectionEvent, _disconnectionEvent, _messageEvent, true, true, NetClient.LoggingMethod.Console);
            _client.SendCommand("read", new string[] { "yo i'm a client" });
        }

        private void client_Connected()
        {
            _activeUsers++;
            UpdateActiveUsers();
            MessageBox.Show("Connected!");
        }

        private void client_Disconnected()
        {
            _activeUsers--;
            UpdateActiveUsers();
            MessageBox.Show("Disconnected!");
        }

        private delegate void SetTextCallback();

        private void UpdateActiveUsers()
        {
            if (activeLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateActiveUsers);
                Invoke(d);
            }
            else
            {
                activeLabel.Text = "Server Channels: (" + _activeUsers + " users are active)";
            }
        }

        private void message_Received(string msg)
        {
            try
            {
                NetMessage message = NetParser.ParseMessage(msg);
                List<string> args = message.GetArguments();
                if (message.HasCommand())
                {
                    string command = message.GetCommand();
                    switch (command)
                    {
                        case "connection":
                            string status = args[0];
                            if (status != "success")
                            {
                                MessageBox.Show(status, "Connection status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Something went wrong
                throw new Exception(ex.Message);
            }
        }
    }
}
