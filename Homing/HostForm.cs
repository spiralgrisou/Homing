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
    }
}
