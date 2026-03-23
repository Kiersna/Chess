using ChessLogic.Moves;
using System.Collections.Generic;
using System.Linq;

namespace ChessLogic.Pieces
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }
        private static readonly Direction[] directions = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West
        };

        public Rook(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDirs(from, board, directions).Select(to => new NormalMove(from, to));
        }
    }
}
