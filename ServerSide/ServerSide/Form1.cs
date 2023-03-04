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
            _server._playerConnectedEvent += playerConnectedHandler;    //subscribe into player Connection event
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
        private void playerConnectedHandler(object sender, string userName)
        {
            PlayersListBox.Items.Add(userName);
        }

        private void button2_Click(object sender, EventArgs e)      //for test only         //to be deleted
        {
           foreach(var p in _server._players)
            {
                p.displayPlayer();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var r in _server._rooms)
            {
                r.displayRoom();
            }
        }
    }
}
