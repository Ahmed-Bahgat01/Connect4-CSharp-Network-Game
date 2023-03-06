using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageLib;

namespace ClientSide
{
    public partial class RoomForm : Form
    {
        private int _roomId;
        private string _roomName;
        public RoomForm()
        {
            InitializeComponent();
        }

        public RoomForm(int id,string roomName,string creatorName)
        {
            InitializeComponent();
            _roomId = id;
            _roomName = roomName;
            this.Text= _roomName;
            PlayerslistBox.Items.Add(creatorName);
        }

        public RoomForm(int id, string roomName, string creatorName, string joinnerName)
        {
            InitializeComponent();
            _roomId = id;
            _roomName = roomName;
            this.Text = _roomName;
            PlayerslistBox.Items.Add(creatorName);
            PlayerslistBox.Items.Add(joinnerName);
        }

        private void ReadyBtn_Click(object sender, EventArgs e)
        {
            SendReadyContainer msg = new SendReadyContainer(_roomId);
            Client.SendMsg(msg);
            ReadyBtn.Enabled = false;
        }
    }
}
