using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    internal class CustomRoomPanel
    {
        public int Height { get; set; }
        public Panel RoomPanel { get; set; }
        public Label Label1 { get; set; }
        public Label Label2 { get; set; }
        public Label Label3 { get; set; }
        public TextBox TextBox1 { get; set; }
        public TextBox TextBox2 { get; set; }
        public Button Button1 { get; set; }
        public Button Button2 { get; set; }

        public CustomRoomPanel(int height,
            int roomId,
            string roomName,
            //int player1Id,
            string player1Name,
            //int? player2Id,
            string player2Name)
        {
            Panel panel1 = new Panel();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            TextBox textBox1 = new TextBox();
            TextBox textBox2 = new TextBox();
            Button button1 = new Button();
            Button button2 = new Button();

            panel1.Location = new Point(71, 314 + (15 * height));
            panel1.Size = new Size(342, 135);
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            label1.Location = new Point(16, 0);
            label1.Text = "Room Number" + (height + 1);
            label1.Size = new Size(90, 13);

            label2.Location = new Point(31, 25);
            label2.Text = "Player 1: ";
            label2.Size = new Size(51, 13);

            label3.Location = new Point(31, 58);
            label3.Text = "Player 2: ";
            label3.Size = new Size(51, 13);

            textBox1.Location = new Point(100, 22);
            textBox1.Text = "";
            textBox1.Size = new Size(100, 20);
            textBox1.Enabled = false;

            textBox2.Location = new Point(100, 55);
            textBox2.Text = "";
            textBox2.Size = new Size(100, 20);
            textBox2.Enabled = false;

            button1.Location = new Point(34, 96);
            button1.Text = "Play";
            button1.Size = new Size(75, 23);
            void button1_Click(object sender, EventArgs e)
            {
                MessageBox.Show(label1.Text);
            }
            button1.Click += button1_Click;

            button2.Location = new Point(125, 96);
            button2.Text = "Watch";
            button2.Size = new Size(75, 23);
            void button2_Click(object sender, EventArgs e)
            {
                MessageBox.Show(label1.Text);
            }
            button2.Click += button2_Click;

            //this.Controls.Add(panel1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
        }
        
    }
}
