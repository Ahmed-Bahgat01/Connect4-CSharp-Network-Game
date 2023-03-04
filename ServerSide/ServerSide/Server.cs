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

namespace ServerSide
{

    internal partial class Server
    {
        public List<Player> _players;
        public List<Room> _rooms;
        private IPAddress _IP = IPAddress.Parse("127.0.0.1");
        private int _PORT = 5500;
        private TcpListener _tcpListener;
        private string _gameHistoryPath;
        private string _userDataPath;
        private Dictionary<MessageTag, Action<object, string>> MessageHandlerDic;


        public event Action<object, string> _playerConnectedEvent;
        public Server()
        {
            _tcpListener = new TcpListener(_IP, _PORT);
            _players = new List<Player>();


            MessageHandlerDic = new Dictionary<MessageTag, Action<object, string>>
            {
                { MessageTag.SignIn, SignInHandler },
                { MessageTag.SignUp, SignUpHandler },
                { MessageTag.CreateRoom, CreateRoomHandler },
                { MessageTag.JoinRoom, JoinRoomHandler },
                { MessageTag.SpectateRoom, SpectateRoomHandler },
                { MessageTag.DisFromRoom, DisFromRoomHandler }
                // >>>>>>> REGISTER messageTag with messageHandler here <<<<<<<
            };
        }
        public Server(string ip, int port)
        {
            _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
            _players = new List<Player>();

        }


        /// <summary>
        ///     Starts listening for incoming players connections
        /// </summary>
        public async void Start()       
        {
            _tcpListener.Start();

            while (true)
            {
                try
                {
                    TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
                    Player newPlayer = new Player(tcpClient);                               //create new player from the connected tcpClient

                    newPlayer._PlayerDisconnectedEvent += PlayerDisconnectedMessageHandler; //subscribe into client disconnect event
                    _players.Add(newPlayer);                                                //add the connected player to the list

                    newPlayer._recievedMessageEvent += RecievedPlayerMessageHandler;
                    //_players.Add(newPlayer);


                    
                }
                catch (ObjectDisposedException e)                                           //on server stop
                {
                    break;
                }
            }
        }

        /// <summary>
        ///     sends stop message to all clients and stops server
        /// </summary>
        public void Stop()
        {
            foreach (Player player in _players)
            {
                try
                {
                    player._session._streamWriter.WriteLine("!DIS");        //send disconnect Formatted msg to all player
                }
                catch
                {

                }
                player.EndClient();                                     //close the players sessions
            }
            _tcpListener.Stop();
            _players.Clear();                                           //clear
        }
        public void RecievedPlayerMessageHandler(object sender, string eventData)
        {
            //try
            //{
                MessageContainer msgObj = JsonConvert.DeserializeObject<MessageContainer>(eventData);
                MessageHandlerDic[msgObj.Tag](sender, eventData);
            //}
            //catch (ArgumentNullException)
            //{

            //}
        }


        /// <summary>
        ///     This method sends a message to all connected clients
        ///     used for TESTING PURPOSES ONLY
        /// </summary>
        /// <param name="msg">
        ///     NOTE: message here is not serialized from object
        /// </param>
        public void Broadcast(string msg)     
        {
            foreach (Player player in _players)
            {
                player._session._streamWriter.WriteLine(msg);
            }
        }


        private void PlayerDisconnectedMessageHandler(Player sender)                //on player disconnect
        {
            _players.Remove(sender);
        }


        /// <summary>
        ///     DEPRECATED: replaced by RecievedPlayerMessageHandler() 
        ///     needs to be REMOVED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void ClientRecievedMessageHandler(object sender, string message)     //on reciving message form player
        {
            MessageBox.Show(message);       //to be removed
        }

        public void RoomIsEmptyEventHandler(Room sender)
        {
            _rooms.Remove(sender);
        }
    }
}
