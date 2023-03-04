using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{
    public enum RoomStatus { Running, Waiting }
    
    internal class Room
    {
        public event Action<Room> _roomIsEmptyEvent;
        public GameConfiguration _gameConfig 
        { 
            get; 
            set; 
        }
        public int _ID
        {
            get;
            set;
        }

        public string _name
        {
            get;
            set;
        }

        public RoomStatus _roomStatus
        {
            get;
        }


        public List<Player> _spectators
        {
            get;
            set;
        }

        public List<Player> _players
        {
            get;
            set;
        }

        public Room(Player p,int id,string RoomName, GameConfiguration gameConfig) {
            _players= new List<Player>();
            _players.Add(p);
            _spectators= new List<Player>();
            _roomStatus= RoomStatus.Waiting;
            _gameConfig= gameConfig;
            _name= RoomName;
            _ID= id;
        }

        public void AddPlayer(Player p)
        {
           if (_players.Count()<2)
            {
                _players.Add(p);
            }
            //else
            //{
            //    //handle
            //    MessageBox.Show("only 2 players can join the room")
            //}
        }
        public void RemovePlayer(Player p)
        {
            if(_players.Contains(p))
            {
                _players.Remove(p);
            }
            else if (_spectators.Contains(p))
            {
                _spectators.Remove(p);
            }

            if (_players.Count() == 0)
            {
                if (_roomIsEmptyEvent != null)
                {
                    _roomIsEmptyEvent(this);
                }
            }
        }
        public void AddSpectator(Player p)
        {
            _spectators.Add(p);
        }
    }
}
