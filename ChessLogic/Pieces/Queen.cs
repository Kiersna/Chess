using ChessLogic.Moves;
using System.Collections.Generic;
using System.Linq;

namespace ChessLogic.Pieces
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override Player Color { get; }
        private readonly static Direction[] directions = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthEast,
            Direction.SouthEast,
            Direction.NorthWest,
            Direction.SouthWest
        };

        public Queen(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDirs(from, board, directions).Select(to => new NormalMove(from, to));
        }
    }
}
