using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public enum Player
    {
        None,
        White,
        Black
    }
    public static class PlayerExtansions
    {
        public static Player Opponent(Player player)
        {
            return player switch
            {
                Player.White => Player.Black,
                Player.Black => Player.White,
                _ => Player.None,
            };
            //to samo dzialanie na dole 
            //switch (player)
            //{
            //    case Player.White:
            //        return Player.Black;
            //    case Player.Black:
            //        return Player.White;
            //    default:
            //        return Player.None;
            //}
        }
    }
}
