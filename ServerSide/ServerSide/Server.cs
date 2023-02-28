using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MessageLib;
using Newtonsoft.Json;
using ServerSide.Handlers;

namespace ServerSide
{

    internal class Server
    {
        private List<Player> _players;
        private IPAddress _IP = IPAddress.Parse("127.0.0.1");                   
        private int _PORT = 5500;                                               
        private TcpListener _tcpListener;
        private string _gameHistoryPath;
        private string _userDataPath;
        private Thread _playerThread;



        public Server()
        {
            _tcpListener = new TcpListener(_IP, _PORT);
            _players = new List<Player>();
        }
        public Server(string ip, int port)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            _players = new List<Player>();
            
        }

        public  void Start()
        {
            _playerThread = new Thread(() =>
            {
                while (true)
                {
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                    Player newPlayer = new Player(tcpClient);
                    // subscribe into client recieved message event
                    newPlayer._recievedMessageEvent += ClientRecievedMessageHandler;
                    _players.Add(newPlayer);
                }
            });
            _tcpListener.Start();
            _playerThread.Start();
        }
        public void Stop()                              //work in progress
        {
            foreach (Player player in _players)
            {
                player._session._streamWriter.WriteLine("!DIS");       //send disconnect Formatted msg to player
                player.EndClient();
            }
            _playerThread.Abort();
            _tcpListener.Stop();
        }
        public void ClientRecievedMessageHandler(object sender, RecievedMessageEventData eventData)
        {

            MessageContainer msg = JsonConvert.DeserializeObject<MessageContainer>(eventData._msg);
            if(msg.Tag == MessageTag.SignIn)
            {
                SignInHandler handler = new SignInHandler(sender, eventData);
                handler.Handle(msg);
            }
            MessageBox.Show($"from server got a message: {eventData._msg}");

        }
        public void Broadcast(string msg)
        {
            foreach (Player player in _players)
            {
                player._session._streamWriter.WriteLine(msg);
            }
        }


    }
}
