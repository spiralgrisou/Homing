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

        private Client _client { get; set; }

        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            Client.ClientRead readFunc = client_Connected;
            _client = new Client(IP_ADDRESS, PORT, readFunc);
            _client.SendMessage("yooo cuz");
            _client.Kill();
            // Application.Exit();
        }

        private void client_Connected(string message)
        {
            MessageBox.Show(message);
        }
    }
}
