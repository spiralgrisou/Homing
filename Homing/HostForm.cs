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

namespace Homing
{
    public partial class HostForm : Form
    {
        public HostForm()
        {
            InitializeComponent();
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

        private void HostForm_Load(object sender, EventArgs e)
        {
            string currentAddress = NetworkingInformation.GetLocalIPAddress();
            if (String.IsNullOrEmpty(currentAddress))
            {
                MessageBox.Show("No network connection was found, Please connect to the internet and try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            addressLabel.Text = "Enter an address here: (Default address used: " + NetworkingInformation.GetLocalIPAddress() + ")";
            portLabel.Text = "Enter a port to host on here: (Default port used: 1337)";
        }

        private void hostBtn_Click(object sender, EventArgs e)
        {
            string defaultAddress = NetworkingInformation.GetLocalIPAddress();
            int defaultPort = 1337;
            bool useDefaultAddr = false;
            bool useDefaultPort = false;
            if(String.IsNullOrEmpty(addressBox.Text))
                useDefaultAddr = true;
            if (String.IsNullOrEmpty(portBox.Text))
                useDefaultPort = true;
            ServerForm serverForm = new ServerForm();
            if (useDefaultAddr)
                serverForm.IP_ADDRESS = defaultAddress;
            else
                serverForm.IP_ADDRESS = addressBox.Text;
            if (useDefaultPort)
                serverForm.PORT = defaultPort;
            else
                serverForm.PORT = Int32.Parse(portBox.Text);
            serverForm.Show();
        }
    }
}
