using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Threading;
using MessageLib;
using Newtonsoft.Json;

namespace ClientSide
{
    public partial class SignForm : Form
    {
        private IPAddress _IP = IPAddress.Parse("127.0.0.1");
        private int _PORT = 5500;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private Thread ListeningThread;
        //private Dictionary<MessageTag, Action<string>> MessageHandlerDic;

        public SignForm()
        {
            InitializeComponent();
        }

        private bool IsValidSignFormInput()
        {
            // rules flags
            bool IsEmptyUserName = UserNameTextBox.Text == string.Empty ? true: false;
            bool IsEmptyPassword = UserNameTextBox.Text == string.Empty ? true: false;

            // checking flags to validate
            if(IsEmptyUserName || IsEmptyPassword ) 
                return false;
            else return true;
        }

        /// <summary>
        /// sends signin message to server (called in signIn UI event)
        /// </summary>
        private void SignIn()
        {
            if (IsValidSignFormInput())
            {
                SignInMessageContainer msg = new SignInMessageContainer(UserNameTextBox.Text,PasswordTextBox.Text);
                Client._UserName = UserNameTextBox.Text;
                Client.SendMsg(msg);
                HomePage home = new HomePage();
                home.Show();
            }
            else
                MessageBox.Show("make sure to input your correct credentials", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }
        private void SignUp()
        {
            if (IsValidSignFormInput())
            {
                SignUpMessageContainer msg = new SignUpMessageContainer(UserNameTextBox.Text, PasswordTextBox.Text);
                Client._UserName=UserNameTextBox.Text;
                Client.SendMsg(msg);
            }
            else
                MessageBox.Show("Not Valid Inputs");
        }



        // EVENT HANDLERS
        private void SignInBtn_Click(object sender, EventArgs e)
        {
            if (Client.StartConnection())
            {
                SignIn();
            }
        }
        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            if(Client.StartConnection())
                SignUp();
            HomePage home = new HomePage();
            home.Show();
        }

        private void SignForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Client.IsConnected())
            {
                Client.SendDisconnect();
            }
            
        }
    }
}
