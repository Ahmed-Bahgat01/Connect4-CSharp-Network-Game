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
    public partial class RoomConfiguration : Form
    {
        int size;
        Color colr;
        public RoomConfiguration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public int Size
        {

            set
            {
                size = value;
                if (size == 0)
                {
                    radioButton1.Checked = true;
                }

                else if (size == 1)
                {
                    radioButton2.Checked = true;
                }
                else if (size == 2)
                {
                    radioButton3.Checked = true;
                }
                else if (size == 3)
                {
                    radioButton4.Checked = true;
                }
                else if (size == 4)
                {
                    radioButton5.Checked = true;
                }
                else if (size == 5)
                {
                    radioButton6.Checked = true;
                }
                else if (size == 6)
                {
                    radioButton7.Checked = true;
                }

            }

            get
            {
                if (radioButton1.Checked == true)
                {
                    size = 0;
                }

                else if (radioButton2.Checked == true)
                {
                    size = 1;
                }
                else if (radioButton3.Checked == true)
                {
                    size = 2;
                }
                else if (radioButton4.Checked == true)
                {
                    size = 3;
                }
                else if (radioButton5.Checked == true)
                {
                    size = 4;
                }
                else if (radioButton6.Checked == true)
                {
                    size = 5;
                }
                else if (radioButton7.Checked == true)
                {
                    size = 6;
                }
                return size;
            }
        }

        public Color Colorr
        {
            set
            {
                colr = value;
                if (colr == Color.Red)
                {
                    radioButton8.Checked = true;
                }

                else if (colr == Color.Yellow)
                {
                    radioButton9.Checked = true;
                }

            }
            get
            {
                if (radioButton8.Checked == true)
                {
                    colr = Color.Red;
                }

                else if (radioButton9.Checked == true)
                {
                    colr = Color.Yellow;
                }
                return colr;
            }
        }
    }
}
