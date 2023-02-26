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
        private void Connect()
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(_IP, 5500);
            _networkStream = _tcpClient.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream);
            _streamWriter.AutoFlush = true;
            _streamWriter.WriteLine("connected from client");
        }

        private void Disconnect()
        {
            _streamWriter.WriteLine("!DISCONNECT");
            _streamReader.Close();
            _streamWriter.Close();
            _tcpClient.Close();
        }

        private void ListenMessage()
        {
            ListeningThread = new Thread(() => {
                while (true)
                {
                    
                    string msg = _streamReader.ReadLine();
                    if (msg == "!DIS") { 
                        ListeningThread.Abort();
                        Disconnect();
                    }
                    else
                        MessageBox.Show(msg);
                }
            });

            ListeningThread.Start();
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            Connect();
            ListenMessage();
        }
    }
}
