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

        }

        

        private void RoomCreationHandler(CreateRoomV2MessageContainer updateObj)
        {
            MessageBox.Show("asdasd");

            // LIST VIEW
            string[] row = { updateObj.RoomName, updateObj.Player1Name, updateObj.Player2Name };
            ListViewItem item = new ListViewItem(row);
            RoomsListView.Invoke(new Action( () => { RoomsListView.Items.Add(item); }));

            // add roomid and listview item to dictionary
            Client.RoomListViewItemDic.Add(updateObj.RoomId, item);
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
                
                size = config.Size;
                color = config.Colorr;

                CreateRoomMessageContainer msg = new CreateRoomMessageContainer(Client._UserName, config.RoomName, new ServerSide.GameConfiguration(size, color));
                Client.SendMsg(msg);

                Invalidate();

                Game game = new Game(size, color);
                game.Show();
            }
        }
    }
}
