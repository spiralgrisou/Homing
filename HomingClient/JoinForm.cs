using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using NetworkingManager;

namespace HomingClient
{
    public partial class JoinForm : Form
    {
        public JoinForm()
        {
            InitializeComponent();
        }

        private void JoinForm_Load(object sender, EventArgs e)
        {
            string currentAddress = NetInfo.GetLocalIPAddress();
            if (String.IsNullOrEmpty(currentAddress))
            {
                MessageBox.Show("No network connection was found, Please connect to the internet and try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            addressLabel.Text = "Enter an address here: (Default address used: " + NetInfo.GetLocalIPAddress() + ")";
            portLabel.Text = "Enter a port to host on here: (Default port used: 1337)";
        }

        private void creditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            e.Link.Visited = true;
            Process.Start("https://www.github.com/spiralgrisou");
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            string defaultAddress = NetInfo.GetLocalIPAddress();

            int defaultPort = 1337;
            bool useDefaultAddr = false;
            bool useDefaultPort = false;
            if (String.IsNullOrEmpty(addressBox.Text))
                useDefaultAddr = true;
            if (String.IsNullOrEmpty(portBox.Text))
                useDefaultPort = true;
            ClientForm serverForm = new ClientForm();
            if (useDefaultAddr)
                serverForm.IP_ADDRESS = defaultAddress;
            else
            {
                if (NetInfo.IsValidIPAddress(addressBox.Text))
                    serverForm.IP_ADDRESS = addressBox.Text;
                else
                {
                    MessageBox.Show("Please enter a valid IP Address, or leave it empty to use the default address.", "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (useDefaultPort)
                serverForm.PORT = defaultPort;
            else
            {
                try
                {
                    int PORT = Int32.Parse(portBox.Text);
                    if (PORT < 0)
                    {
                        MessageBox.Show("Ports need to be greater than 0.", "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    serverForm.PORT = PORT;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid port, or leave the port box empty to use the default port.", "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Hide();
            serverForm.ShowDialog();
            Close();
        }
    }
}
