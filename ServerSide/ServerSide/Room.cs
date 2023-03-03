using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    public enum RoomStatus { Running, Waiting }
    internal class Room
    {
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

        public void AddPlayer(Player p)
        {
           // if 
        }
        public void RemovePlayer()
        {

        }
        public void AddSpectator()
        {

        }
    }
}
