using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{
    enum Status {Waiting,Playing,Spectating,Disconnected}
    internal class Player
    {
        public event Action<object, string> _recievedMessageEvent;
        public event Action<Player> _PlayerDisconnectedEvent;
        public int _id { get; set; }
        public string _userName { get; set; } = "player";   //"player" to be removed
        public Status _status { get; set; }                 //Waiting,Playing,Spectating,Disconnected
        public Session _session { get; set; }               //socket data

        public Player(TcpClient tcpClient)
        {
            _session = new Session(tcpClient);
            ListenMessage();
            _status = Status.Waiting;
        }

        public void EndClient()                             //close player streams
        {
            _session.Stop();
        }

        private async void ListenMessage()
        {
            while (true)
            {
                try                                         //listen to the incomming messages from player
                 {   
                    string msg = await _session._streamReader.ReadLineAsync();

                    if (msg == "!DISCONNECT")                       //Disconnect from player format
                    {
                        if (_PlayerDisconnectedEvent != null)      //firing event when player disconnected
                        {
                            _PlayerDisconnectedEvent(this);
                        }
                        EndClient();
                        _status=Status.Disconnected;
                    }
                    else
                    {
                        if (_recievedMessageEvent != null)      //firing event when message recieved
                        {
                            _recievedMessageEvent(this, msg);
                        }
                    }
                }
                catch (ObjectDisposedException e)           //catch the exception when a player is disconnected
                {
                    break;
                }

            }

            
        }
    }
}

