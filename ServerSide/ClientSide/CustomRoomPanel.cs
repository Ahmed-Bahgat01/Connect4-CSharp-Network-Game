using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{


    /// <summary>
    ///     LEGACY CLASS : TO BE REMOVED
    /// </summary>
    internal class CustomRoomPanel
    {
        public int Height { get; set; }
        public Panel RoomPanel { get; set; }
        public Label RoomName { get; set; }
        public Label Player1 { get; set; }
        public Label Player2 { get; set; }
        public TextBox Player1Name { get; set; }
        public TextBox Player2Name { get; set; }
        public Button JoinButton { get; set; }
        public Button WatchButton { get; set; }

        public CustomRoomPanel(int height,
            int roomId,
            string roomName,
            //int player1Id,
            string player1Name,
            //int? player2Id,
            string player2Name)
        {
            Panel RoomPanel = new Panel();
            Label RoomName = new Label();
            Label Player1 = new Label();
            Label Player2 = new Label();
            TextBox Player1Name = new TextBox();
            TextBox Player2Name = new TextBox();
            Button JoinButton = new Button();
            Button WatchButton = new Button();

            RoomPanel.Location = new Point(71, 314 + (15 * height));
            RoomPanel.Size = new Size(342, 135);
            RoomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            RoomName.Location = new Point(16, 0);
            RoomName.Text = "Room Number" + (height + 1);
            RoomName.Size = new Size(90, 13);

            Player1.Location = new Point(31, 25);
            Player1.Text = "Player 1: ";
            Player1.Size = new Size(51, 13);

            Player2.Location = new Point(31, 58);
            Player2.Text = "Player 2: ";
            Player2.Size = new Size(51, 13);

            Player1Name.Location = new Point(100, 22);
            Player1Name.Text = "";
            Player1Name.Size = new Size(100, 20);
            Player1Name.Enabled = false;

            Player2Name.Location = new Point(100, 55);
            Player2Name.Text = "";
            Player2Name.Size = new Size(100, 20);
            Player2Name.Enabled = false;

            JoinButton.Location = new Point(34, 96);
            JoinButton.Text = "Play";
            JoinButton.Size = new Size(75, 23);
            void button1_Click(object sender, EventArgs e)
            {
                MessageBox.Show(RoomPanel.Text);
            }
            JoinButton.Click += button1_Click;

            WatchButton.Location = new Point(125, 96);
            WatchButton.Text = "Watch";
            WatchButton.Size = new Size(75, 23);
            void button2_Click(object sender, EventArgs e)
            {
                MessageBox.Show(RoomPanel.Text);
            }
            WatchButton.Click += button2_Click;
            RoomPanel.Controls.Add(RoomName);
            RoomPanel.Controls.Add(Player1);
            RoomPanel.Controls.Add(Player2);
            RoomPanel.Controls.Add(Player1Name);
            RoomPanel.Controls.Add(Player2Name);
            RoomPanel.Controls.Add(JoinButton);
            RoomPanel.Controls.Add(WatchButton);
        }
        
    }
}
