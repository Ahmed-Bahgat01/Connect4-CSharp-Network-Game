using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class Session
    {
        public TcpClient _tcpClient { get; set; }
        public NetworkStream _networkStream { get; set; }
        public StreamReader _streamReader { get; set; }
        public StreamWriter _streamWriter { get; set; }

        public Session(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _networkStream = tcpClient.GetStream();
            _streamReader = new StreamReader(_networkStream);
            _streamWriter = new StreamWriter(_networkStream);
            _streamWriter.AutoFlush = true;
        }

        public void Stop()
        {
            _streamWriter.Close();
            _streamReader.Close();
            _networkStream.Close();
            _tcpClient.Close();
        }
    }
}
