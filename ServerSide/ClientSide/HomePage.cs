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

namespace ClientSide
{
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

            //for (int i = 0; i< 5; i++)
            //{
            //    roomPanel = createPanel(200,i);
            //    flowLayoutPanel1.Controls.Add(roomPanel);
            //}

        }

        

        private void RoomCreationHandler(CreateRoomV2MessageContainer updateObj)
        {
            //create UI for room
            CustomRoomPanel newCustomRoomPanel = new CustomRoomPanel(200,
                updateObj.RoomId,
                updateObj.RoomName,
                //updateObj.Player1Id,
                updateObj.Player1Name,
                //updateObj.Player2Id,
                updateObj.Player2Name
                );

            // attach panel to form
            this.Controls.Add(newCustomRoomPanel.RoomPanel);

            // TODO: UPDATE DIC
            Client.RoomPanelDic.Add(updateObj.RoomId, newCustomRoomPanel);
        }

        private void PlayerJoinedRoomHandler(JoinRoomMessageContainer eventObj)
        {
            TextBox targetedPlayerTxtBox =  Client.RoomPanelDic[eventObj.RoomID].Player2Name;
            targetedPlayerTxtBox.Text = eventObj.PlayerName;
            // TODO: DEACTIVATE JOIN BUTTON
        }

        private void PlayerLeftRoomHandler(LeaveRoomMessageContainer eventObj)
        {
            TextBox targetedPlayerTxtBox = Client.RoomPanelDic[eventObj.RoomID].Player2Name;
            targetedPlayerTxtBox.Text = string.Empty;
            // TODO: ACTIVATE JOIN BUTTON, 
        }

        /// <summary>
        ///     TO BE REMOVED: replaced with create room v2 and other handlers
        /// </summary>
        /// <param name="updateObj"></param>
        //private void RoomUpdateHandler(RoomStatusUpdateMessageContainer updateObj)
        //{
        //    // check if room exists
        //    if (Client.RoomPanelDic.ContainsKey(updateObj.RoomId))
        //    {
        //        //TODO:
        //        // if exist update it's data
        //        CustomRoomPanel targetPanel = Client.RoomPanelDic[updateObj.RoomId];
        //        int targetPanelIndex = this.Controls.IndexOf(targetPanel.RoomPanel);
        //        this.Controls[this.Controls.IndexOf(targetPanel.TextBox1)].Text = 
        //    }
        //    else  // if not exist create the room
        //    {
        //        //create UI for room
        //        CustomRoomPanel newCustomRoomPanel = new CustomRoomPanel(200, 
        //            updateObj.RoomId,
        //            updateObj.RoomName,
        //            //updateObj.Player1Id,
        //            updateObj.Player1Name,
        //            //updateObj.Player2Id,
        //            updateObj.Player2Name
        //            );

        //        // attach panel to form
        //        this.Controls.Add(newCustomRoomPanel.RoomPanel);

        //        // TODO: UPDATE DIC
        //        Client.RoomPanelDic.Add(updateObj.RoomId, newCustomRoomPanel);

        //    }
        //}

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
                MessageBox.Show($"{config.Colorr}");
                size = config.Size;
                color = config.Colorr;

                CreateRoomMessageContainer msg = new CreateRoomMessageContainer(Client._UserName, config.RoomName, new ServerSide.GameConfiguration(size, color));
                Client.SendMsg(msg);

                Invalidate();

                Game game = new Game(size, color);
                game.Show();
            }

        }



        /// <summary>
        ///     TO BE REMOVED: replaced with CustomRoomPanelClass
        /// </summary>
        /// <returns></returns>
        //public Panel createPanel
        //    (int height, 
        //    int roomId,
        //    string roomName,
        //    int player1Id, 
        //    string player1Name, 
        //    int? player2Id, 
        //    string player2Name )
        //{
        //    Panel panel1 = new Panel();
        //    Label label1 = new Label();
        //    Label label2 = new Label();
        //    Label label3 = new Label();
        //    TextBox textBox1 = new TextBox();
        //    TextBox textBox2 = new TextBox();
        //    Button button1 = new Button();
        //    Button button2 = new Button();

        //    panel1.Location = new Point(71, 314 + (15 * height));
        //    panel1.Size = new Size(342, 135);
        //    panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

        //    label1.Location = new Point(16, 0);
        //    label1.Text = "Room Number" + (height + 1);
        //    label1.Size = new Size(90, 13);

        //    label2.Location = new Point(31, 25);
        //    label2.Text = "Player 1: ";
        //    label2.Size = new Size(51, 13);

        //    label3.Location = new Point(31, 58);
        //    label3.Text = "Player 2: ";
        //    label3.Size = new Size(51, 13);

        //    textBox1.Location = new Point(100, 22);
        //    textBox1.Text = "";
        //    textBox1.Size = new Size(100, 20);
        //    textBox1.Enabled = false;

        //    textBox2.Location = new Point(100, 55);
        //    textBox2.Text = "";
        //    textBox2.Size = new Size(100, 20);
        //    textBox2.Enabled = false;

        //    button1.Location = new Point(34, 96);
        //    button1.Text = "Play";
        //    button1.Size = new Size(75, 23);
        //    void button1_Click(object sender, EventArgs e)
        //    {
        //        MessageBox.Show(label1.Text);
        //    }
        //    button1.Click += button1_Click;

        //    button2.Location = new Point(125, 96);
        //    button2.Text = "Watch";
        //    button2.Size = new Size(75, 23);
        //    void button2_Click(object sender, EventArgs e)
        //    {
        //        MessageBox.Show(label1.Text);
        //    }
        //    button2.Click += button2_Click;

        //    this.Controls.Add(panel1);
        //    panel1.Controls.Add(label1);
        //    panel1.Controls.Add(label2);
        //    panel1.Controls.Add(label3);
        //    panel1.Controls.Add(textBox1);
        //    panel1.Controls.Add(textBox2);
        //    panel1.Controls.Add(button1);
        //    panel1.Controls.Add(button2);
        //    return panel1;
        //}
    }
}
