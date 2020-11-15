using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomingClient
{
    public partial class ClientForm : Form
    {
        public string IP_ADDRESS { get; set; }
        public int PORT { get; set; }

        public ClientForm()
        {
            InitializeComponent();
        }
    }
}
