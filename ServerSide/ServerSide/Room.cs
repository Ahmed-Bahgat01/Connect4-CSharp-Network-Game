using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{
    public enum RoomStatus { Running, Waiting }
    
    internal class Room
    {
        public event Action<Room> _roomIsEmptyEvent;
        public event Action<Room> _RoomUpdateEvent;        //an event that is fired when any change is occured in the room to be broadcasted
        public event Action<Room> _RoomCreatedEvent;
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
            p._PlayerDisconnectedEvent += PlayerDisconnectedEvent;
            if (_RoomCreatedEvent != null)
            {
                _RoomCreatedEvent(this);
            }
        }

        public void AddPlayer(Player p)
        {
            if (_players.Count() < 2)
            {
                _players.Add(p);
                p._PlayerDisconnectedEvent += PlayerDisconnectedEvent;
            }
            //else
            //{
            //    //handle
            //    MessageBox.Show("only 2 players can join the room")
            //}
            if (_RoomUpdateEvent != null)
            {
                _RoomUpdateEvent(this);
            }
        }

        public void RemovePlayer(Player p)
        {
            if(_players.Contains(p))
            {
                _players.Remove(p);
                if (_RoomUpdateEvent != null)
                {
                    _RoomUpdateEvent(this);
                }
            }
            else if (_spectators.Contains(p))
            {
                _spectators.Remove(p);
                _players.Remove(p);
                if (_RoomUpdateEvent != null)
                {
                    _RoomUpdateEvent(this);
                }
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
            p._PlayerDisconnectedEvent += PlayerDisconnectedEvent;
            if (_RoomUpdateEvent != null)
            {
                _RoomUpdateEvent(this);
            }
        }

        public override string ToString()
        {
            string roomStr = _ID.ToString() +", "+ _name.ToString() + ", " + _gameConfig.ToString() + ", " +
                _roomStatus.ToString() ;
            roomStr += " || Players:";
            foreach (var p in _players)
            {
                roomStr += p.ToString();
                roomStr += ", ";
            }
            roomStr += " || Spectators:";
            foreach (var s in _spectators)
            {
                roomStr += s.ToString();
                roomStr += ", ";
            }
            return roomStr;
        }
        public void displayRoom()     //for test only         //to be deleted
        { 
            MessageBox.Show(ToString()); 
        }
        private void PlayerDisconnectedEvent(Player obj)
        {
            if (_players.Contains(obj))
            {
                _players.Remove(obj);
                if(_players.Count == 0 )
                {
                    if (_roomIsEmptyEvent != null)
                    {
                        _roomIsEmptyEvent(this);
                    }
                }
            }else if (_spectators.Contains(obj))
            {
                _spectators.Remove(obj);
            }
        }
    }

}
