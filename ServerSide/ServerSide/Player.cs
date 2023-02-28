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
    enum Status {Waiting,Playing,Spectating}
    internal class Player
    {
        public event Action<object, RecievedMessageEventData> _recievedMessageEvent;
        public int _id { get; set; }
        public string _userName { get; set; }
        public Status _status { get; set; }
        public Session _session { get; set; }

        private Thread ListeningThread;

        public Player(TcpClient tcpClient)
        {
            _session = new Session(tcpClient);
            ListenMessage();
            _status = Status.Waiting;
        }

        public void EndClient()
        {
            ListeningThread.Abort();
            _session.Stop();
        }

        private void ListenMessage()
        {
            ListeningThread = new Thread(() => 
            {
                MessageBox.Show("thread started");
                while (true)
                {
                    string msg = _session._streamReader.ReadLine();
                    MessageBox.Show($"from player msg: {msg}");
                    //firing event when message recieved
                    if (_recievedMessageEvent != null)
                    {
                        _recievedMessageEvent(this, new RecievedMessageEventData(msg));
                    }
                }
            });

            ListeningThread.Start();
        }
    }
}
