using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            _server.PlayerSuccessfullSignInEvent += PlayerSuccessfullSignInHandler;    //subscribe into player Connection event
            _server._PlayerDisconnectedEvent += PlayerDisconnectedHandler;

            _server._RoomCreatedEvent += RoomCreatedEventHandler;
            _server._RoomUpdateEvent += RoomUpdateEventHandler;
            _server._RoomDeleteEvent += _RoomDeleteEventHandler;
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
            PlayersListBox.Items.Clear();
            RoomsListBox.Items.Clear();
            StartBtn.Enabled = true;
            StopBtn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_server.Broadcast("test from server");
        }


        // CHANGE NAME TO PlayerSuccessSignInHandler
        private void PlayerSuccessfullSignInHandler(object sender, string userName)
        {
            // TODO: ADD ONLY WHEN SIGNED IN SUCCESSFULLY
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

        private void PlayerDisconnectedHandler(Player player)
        {
            PlayersListBox.Items.Remove(player._userName);
        }

        private void RoomCreatedEventHandler(Room room)
        {
            RoomsListBox.Items.Add(room.ToString());
        }
        private void RoomUpdateEventHandler(Room room)
        {
            foreach (string strRoom in RoomsListBox.Items)
            {
                if (strRoom.Split(',')[0] == room._ID.ToString())
                {
                    int index = RoomsListBox.Items.IndexOf(strRoom);
                    RoomsListBox.Items.Remove(strRoom);
                    RoomsListBox.Items.Insert(index, room.ToString());
                    break;
                }
            }
        }

        private void _RoomDeleteEventHandler(Room room)
        {
            foreach (var Room in RoomsListBox.Items) 
            {
                string strRoom = Room.ToString();
                if (strRoom.Split(',')[0] == room._ID.ToString())
                {
                    RoomsListBox.Items.Remove(strRoom);
                    break;
                }
                    
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Stop();
        }
    }
}
