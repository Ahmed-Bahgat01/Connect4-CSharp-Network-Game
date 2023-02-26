using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{
    public partial class Form1 : Form
    {
        private Server _server;
        public Form1()
        {
            InitializeComponent();
            StopBtn.Enabled = false;
            _server = new Server();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            _server.Start();
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            _server.Stop();
            StartBtn.Enabled = true;
            StopBtn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _server.Broadcast("test from server");
        }
    }
}
