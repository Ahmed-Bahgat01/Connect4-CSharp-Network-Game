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
            compName = "Welcome UserName";
            strColor = Color.DarkRed;
            fontFamily = "Times New Roman";
            fontSize = 20;
            title = Color.DarkRed;

            for (int i = 0; i< 5; i++)
            {
                roomPanel = createPanel(200,i);
                flowLayoutPanel1.Controls.Add(roomPanel);
            }

            

            
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
            Rectangle rect1 = new Rectangle(50, 100, this.Width, 100);
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
                //CreateRoomMessageContainer msg = new CreateRoomMessageContainer("koko", "room1", new ServerSide.GameConfiguration(1, Color.Red));
                //SendMsg(msg);
                size = config.Size;
                color = config.Colorr;

                Invalidate();

                Game game = new Game(size, color);
                game.Show();
            }

        }

        public Panel createPanel(int height, int id)
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

            this.Controls.Add(panel1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);

            return panel1;

        }
    }
}
