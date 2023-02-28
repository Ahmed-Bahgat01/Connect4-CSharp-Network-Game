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
//using MessagingLib;
using MessageLib;
//using Newtonsoft.Json;

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
        public SignForm()
        {
            InitializeComponent();
        }
        private void SendMsg(string msg)
        {
            _streamWriter.WriteLine(msg);
        }
        private void Connect()
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(_IP, 5500);
            _networkStream = _tcpClient.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream);
            _streamWriter.AutoFlush = true;
            //_streamWriter.WriteLine("connected from client");
        }

        private void SendDisconnect()
        {
            _streamWriter.WriteLine("!DISCONNECT");                         //change format
            CloseClient();
        }
        private void CloseClient()
        {
            _streamReader.Close();
            _streamWriter.Close();
            _tcpClient.Close();
        }

        private void ListenMessage()
        {
            ListeningThread = new Thread(() => {
                while (true)
                {
                    try { 
                        string msg = _streamReader.ReadLine();
                        if (msg == "!DIS") {
                            CloseClient();
                            ListeningThread.Abort();
                        }
                        else
                            MessageBox.Show(msg);
                    }
                    catch(IOException ex)
                    {
                        break;
                    }
                    catch(ObjectDisposedException ex)
                    {
                        break;
                    }
                }
            });

            ListeningThread.Start();
        }
        private void SignIn()
        {

            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Text;
            if(userName != string.Empty&& password != string.Empty)
            {
                //var MessageContent = new {UserName = userName,Password =  password};
                //MessageCS msg;
                SignInMessageContainer msg = new SignInMessageContainer(userName,password);
                _streamWriter.WriteLine(msg.ToJSON());
            }
            else
            {
                MessageBox.Show("please enter username and password");
            }
            
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
                ListenMessage();
                SignIn();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("server is not available!!");
            }
            
        }
        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
                ListenMessage();
                SignIn();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("server is not available!!");
            }

        }
    }
}
