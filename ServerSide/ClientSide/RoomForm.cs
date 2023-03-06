using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MessageLib;
using Newtonsoft.Json;

namespace ClientSide
{
    public partial class RoomForm : Form
    {
        private int _roomId;
        private string _roomName;
        public RoomForm()
        {
            InitializeComponent();
            Client.startgameEvent += StartGameHandler;
        }

        public RoomForm(int id,string roomName,string creatorName)
        {
            InitializeComponent();
            _roomId = id;
            _roomName = roomName;
            this.Text= _roomName;
            PlayerslistBox.Items.Add(creatorName);
            Client.startgameEvent += StartGameHandler;
        }

        public RoomForm(int id, string roomName, string creatorName, string joinnerName)
        {
            InitializeComponent();
            _roomId = id;
            _roomName = roomName;
            this.Text = _roomName;
            PlayerslistBox.Items.Add(creatorName);
            PlayerslistBox.Items.Add(joinnerName);
            Client.startgameEvent += StartGameHandler;
        }

        private void ReadyBtn_Click(object sender, EventArgs e)
        {
            SendReadyContainer msg = new SendReadyContainer(_roomId);
            Client.SendMsg(msg);
            ReadyBtn.Enabled = false;
        }

        public void StartGameHandler(StartGameContainer RecievedObj)
        {
            
            
            //MessageBox.Show("other side");
            int size = RecievedObj.size;
            Color color = RecievedObj.color;
            
            this.Invoke(new Action(() =>
            {
                Game game = new Game(size, color);
                this.Hide();
                game.Show();
            }));
            
            
            
        }
    }
}
