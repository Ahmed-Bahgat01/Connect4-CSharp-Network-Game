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
        private Dictionary<MessageTag, Action<string>> MessageHandlerDic;

        public SignForm()
        {
            InitializeComponent();

            MessageHandlerDic = new Dictionary<MessageTag, Action<string>>
            {
                { MessageTag.SignUpResponse, MessageHandlers.SignUpResponseHandler },
                { MessageTag.SignInResponse, MessageHandlers.SignInResponseHandler },

                // >>>>>>> REGISTER messageTag with messageHandler here <<<<<<<
            };
        }


        // METHODS

        /// <summary>
        ///     function to send object of type MessageContainer that you defined to server
        /// </summary>
        /// <param name="msg"></param>
        protected void SendMsg(MessageContainer msg)
        {
            _streamWriter.WriteLine(msg.ToJSON());
        }


        /// <summary>
        ///     handles streams and starts connnection with server
        /// </summary>
        private void Connect()
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(_IP, _PORT);
            _networkStream = _tcpClient.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream);
            _streamWriter.AutoFlush = true;
            //_streamWriter.WriteLine("connected from client");
        }


        /// <summary>
        ///     DEPRECATED function needs to be removed
        /// </summary>
        private void SendDisconnect()
        {
            _streamWriter.WriteLine("!DISCONNECT");   //change format
            CloseClient();
        }
        private void CloseClient()
        {
            _streamReader.Close();
            _streamWriter.Close();
            _tcpClient.Close();
        }


        /// <summary>
        ///     asynchronous function that listens for incomming messages 
        /// </summary>
        private void ListenMessage()
        {
            ListeningThread = new Thread(() => {
                while (true)
                {
                    try
                    {
                        string msg = _streamReader.ReadLine();
                        if (msg == "!DIS")
                        {
                            CloseClient();
                            ListeningThread.Abort();
                        }
                        else
                        {
                            // decerializing message
                            SignUpResponseMessageContainer resObj;
                            resObj = JsonConvert.DeserializeObject<SignUpResponseMessageContainer>(msg);
                            // mapping message to it's handler
                            MessageHandlerDic[resObj.Tag](msg);
                        }
                    }
                    catch (IOException ex)
                    {
                        break;
                    }
                    catch (ObjectDisposedException ex)
                    {
                        break;
                    }
                }
            });

            ListeningThread.Start();
        }


        /// <summary>
        ///     function that abstracts(masks) starting connection and listening 
        ///     for incomming messages from server
        /// </summary>
        /// <returns> 
        ///     bool: indicates if connection success or failed
        /// </returns>
        private bool StartConnection()
        {
            bool success = true;
            try
            {
                Connect();
                ListenMessage();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("server is not available!!");
                success = false;
            }
            return success;
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
            if(IsValidSignFormInput())
            {
                SignInMessageContainer msg = new SignInMessageContainer(UserNameTextBox.Text,PasswordTextBox.Text);
                SendMsg(msg);
            }
            else
                MessageBox.Show("make sure to input your correct credentials", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }
        private void SignUp()
        {
            if (IsValidSignFormInput())
            {
                SignUpMessageContainer msg = new SignUpMessageContainer(UserNameTextBox.Text, PasswordTextBox.Text);
                SendMsg(msg);
            }
            else
                MessageBox.Show("Not Valid Inputs");
        }



        // EVENT HANDLERS
        private void SignInBtn_Click(object sender, EventArgs e)
        {
            if (StartConnection())
                SignIn();
            HomePage home = new HomePage();
            home.Show();
        }
        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            if(StartConnection())
                SignUp();
            HomePage home = new HomePage();
            home.Show();
        }
    }
}
