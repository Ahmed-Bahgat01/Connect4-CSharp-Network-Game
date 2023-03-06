using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    internal static partial class Client
    {
        public static string _UserName { get; set; }
        private static IPAddress _IP = IPAddress.Parse("127.0.0.1");
        private static int _PORT = 5500;
        private static TcpClient _tcpClient;
        private static NetworkStream _networkStream;
        private static StreamReader _streamReader;
        private static StreamWriter _streamWriter;
        private static Thread ListeningThread;
        //public static event Action<RoomStatusUpdateMessageContainer> RoomUpdateEvent;
        public static event Action<CreateRoomV2MessageContainer> CreateRoomEvent;
        public static event Action<OtherPlayerMoveMessageContainer> OtherPlayerMoveEvent ;
        public static event Action<JoinRoomMessageContainer> PlayerJoinedRoomEvent;
        public static event Action<LeaveRoomMessageContainer> PlayerLeftRoomEvent;
        public static event Action<SignInResponseMessageContainer> SignedInSuccessfullyEvent;

        // this dictionary maps each room to it's list item in UI
        public static Dictionary<int, ListViewItem> RoomListViewItemDic= new Dictionary<int, ListViewItem>();

        private static Dictionary<MessageTag, Action<string>> MessageHandlerDic = new Dictionary<MessageTag, Action<string>>
        {
            { MessageTag.SignUpResponse, SignUpResponseHandler },
            { MessageTag.SignInResponse, SignInResponseHandler },
            { MessageTag.RoomStatusUpdate, CreateRoomHandler },

                // >>>>>>> REGISTER messageTag with messageHandler here <<<<<<<
        };


        /// TO BE REMOVED
        // Dic maping roomid with room panel in UI
        //public static Dictionary<int, CustomRoomPanel> RoomPanelDic = new Dictionary<int,CustomRoomPanel>();










        // METHODS

        /// <summary>
        ///     function to send object of type MessageContainer that you defined to server
        /// </summary>
        /// <param name="msg"></param>
        public static void SendMsg(MessageContainer msg)
        {
            _streamWriter.WriteLine(msg.ToJSON());
        }


        /// <summary>
        ///     handles streams and starts connnection with server
        /// </summary>
        public static void Connect()
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
        public static void SendDisconnect()
        {
            _streamWriter.WriteLine("!DISCONNECT");   //change format
            CloseClient();
        }
        public static void CloseClient()
        {
            _streamReader.Close();
            _streamWriter.Close();
            _tcpClient.Close();
        }


        /// <summary>
        ///     asynchronous function that listens for incomming messages 
        /// </summary>
        public static void ListenMessage()
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
        public static bool StartConnection()
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

        public static bool IsConnected()
        {
            if (_networkStream == null) return false;
            else return true;
        }

    }

}
