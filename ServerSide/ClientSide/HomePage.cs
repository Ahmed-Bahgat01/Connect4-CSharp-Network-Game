using MessageLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientSide
{
    public delegate void SafeCallDelegate(ListViewItem newItem);
    public partial class HomePage : Form
    {
        // string data
        string compName;
        Color strColor;
        Brush strBrush;
        Font strFont;
        string fontFamily;
        int fontSize;
        Color title;

        //board configuration
        int size;
        Color color;

        //panel
        Panel roomPanel;


        //private delegate void SafeCallDelegate(Panel newPanel);

        public HomePage()
        {
            InitializeComponent();

            //welcome text
            compName = "Welcome "+Client._UserName;
            strColor = Color.DarkRed;
            fontFamily = "Times New Roman";
            fontSize = 20;
            title = Color.DarkRed;

            // subscribe in room events
            Client.CreateRoomEvent += RoomCreationHandler;
            Client.PlayerJoinedRoomEvent += PlayerJoinedRoomHandler;
            Client.PlayerLeftRoomEvent += PlayerLeftRoomHandler;
            Client.CanJoinRoomEvent += CanJoinRoomEventHandler;
            Client.RefreshRoomListEvent += RefreshRoomListHandler;
            Client.startgameEvent += StartSpectateHandler;


        }

        

        private void RoomCreationHandler(CreateRoomV2MessageContainer updateObj)
        {
            //MessageBox.Show("asdasd");

            // LIST VIEW
            string[] row = { updateObj.RoomId.ToString(),updateObj.RoomName, updateObj.Player1Name, updateObj.Player2Name, updateObj.RoomStatus };
            ListViewItem item = new ListViewItem(row);
            RoomsListView.Invoke(new Action( () => { RoomsListView.Items.Add(item); }));

            // add roomid and listview item to dictionary
            Client.RoomListViewItemDic.Add(updateObj.RoomId, item);

            //open room form
            if (Client._UserName == updateObj.Player1Name)
            {
                this.Invoke(new Action(() =>
                {
                    RoomForm roomForm = new RoomForm(updateObj.RoomId, updateObj.RoomName, updateObj.Player1Name);
                    roomForm.Show();
                }));
                
            }
        }

        private void PlayerJoinedRoomHandler(JoinRoomMessageContainer eventObj)
        {

            ListViewItem item = Client.RoomListViewItemDic[eventObj.RoomID];
            // update player2 name to  = player name
            RoomsListView.Invoke(new Action(() => { item.SubItems[2].Text = eventObj.PlayerName; }));

            // TODO: DEACTIVATE JOIN BUTTON
        }

        private void PlayerLeftRoomHandler(LeaveRoomMessageContainer eventObj)
        {
            ListViewItem item = Client.RoomListViewItemDic[eventObj.RoomID];
            // update player2 name to be empty
            RoomsListView.Invoke(new Action(() => { item.SubItems[2].Text = string.Empty; }));

            // TODO: ACTIVATE JOIN BUTTON, 
        }

        private void HomePage_Paint(object sender, PaintEventArgs e)
        {
            DisplayString();
        }

        private void HomePage_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void DisplayString()
        {
            Graphics g = this.CreateGraphics();
            strFont = new Font(fontFamily, fontSize, FontStyle.Underline);
            strBrush = new SolidBrush(title);
            StringFormat strFrmt = new StringFormat();
            strFrmt.Alignment = StringAlignment.Center;
            Rectangle rect1 = new Rectangle(0, 0, this.Width, 100);
            g.DrawString(compName, strFont, strBrush, rect1, strFrmt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RoomConfiguration config = new RoomConfiguration();
            DialogResult config_Result;
            config.Size = size;
            config.Colorr = color;

            config_Result = config.ShowDialog();
            if(config_Result == DialogResult.OK)
            {
                //MessageBox.Show($"{config.Colorr}");
                size = config.Size;
                color = config.Colorr;

                CreateRoomMessageContainer msg = new CreateRoomMessageContainer(Client._UserName, config.RoomName, new ServerSide.GameConfiguration(size, color));
                Client.SendMsg(msg);

                Invalidate();

                

                //Game game = new Game(size, color);
                //game.Show();
            }
        }

        private void RoomsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (RoomsListView.SelectedItems.Count > 0)
            {
                ListViewItem item = RoomsListView.SelectedItems[0];
                int roomId = int.Parse(item.SubItems[0].Text);

               
                JoinRoomMessageContainer msg = new JoinRoomMessageContainer(Client._UserName, roomId);
                Client.SendMsg(msg);
            }
        }

        private void CanJoinRoomEventHandler(OpenRoomForJoinedPlayerMessageContainer RecievedObj)
        {
            this.Invoke(new Action(() =>
            {
                RoomForm roomForm = new RoomForm(RecievedObj.RoomId, RecievedObj.RoomName, RecievedObj.Player1Name, RecievedObj.Player2Name);
                roomForm.Show();
            }));
            
        }

        public void RefreshBtn_Click(object sender, EventArgs e)
        {
            RefreshRoomListContainer msg = new RefreshRoomListContainer(Client._UserName);
            Client.SendMsg(msg);
        }
        private void RefreshRoomListHandler(SendRoomToRoomListMessageContainer updateObj)
        {
            Client.RoomListViewItemDic.Clear();
            RoomsListView.Items.Clear();
            // LIST VIEW
            string[] row = { updateObj.RoomId.ToString(), updateObj.RoomName, updateObj.Player1Name, updateObj.Player2Name, updateObj.RoomStatus };
            ListViewItem item = new ListViewItem(row);
            RoomsListView.Invoke(new Action(() => { RoomsListView.Items.Add(item); }));

            // add roomid and listview item to dictionary
            Client.RoomListViewItemDic.Add(updateObj.RoomId, item);
        }

        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        public void StartSpectateHandler(StartGameContainer RecievedObj)
        {


            if(RecievedObj.RoomStatus== "Running") {
                //MessageBox.Show("spectate");
                int size = RecievedObj.size;
                Color color = RecievedObj.color;

                this.Invoke(new Action(() =>
                {
                    Game game = new Game(size, color,true);
                    this.Hide();
                    game.Show();
                }));
            }
        }
    }
}
