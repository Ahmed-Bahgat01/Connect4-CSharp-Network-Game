using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    internal class GameConfiguration
    {
        public int _boardSize { get; set; }

        public Color _boardColor { get; set; }
        
        public GameConfiguration(int boardSize,Color color) { 
            _boardColor= color;
            _boardSize= boardSize;
        }

        public override string ToString()
        {
            return (_boardSize.ToString()+"::"+ _boardColor.ToString());
        }

    }
}
