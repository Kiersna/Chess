using ChessLogic.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            for (int c = 0; c < 8; c++)
            {
                this[1, c] = new Pawn(Player.Black);
                this[6, c] = new Pawn(Player.White);
            }
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        // Zwraca wszystkie pozycje na których stoją bierki danego gracza
        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);
                    if (!IsEmpty(pos) && this[pos].Color == player)
                    {
                        yield return pos;
                    }
                }
            }
        }

        // Sprawdza czy król danego gracza jest atakowany przez przeciwnika
        public bool IsInCheck(Player player)
        {
            Player opponent = PlayerExtansions.Opponent(player);

            // Sprawdzamy czy którykolwiek ruch przeciwnika atakuje króla
            return PiecePositionsFor(opponent).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.GetMoves(pos, this).Any(move => IsKingCaptured(move, player));
            });
        }

        // Sprawdza czy dany ruch zbija króla wskazanego gracza
        private bool IsKingCaptured(Move move, Player player)
        {
            Position to = move.ToPosition;
            return !IsEmpty(to) && this[to] is King && this[to].Color == player;
        }

        // Tworzy głęboką kopię planszy (potrzebne do symulacji ruchów)
        public Board Copy()
        {
            Board copy = new Board();
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (pieces[r, c] != null)
                        copy.pieces[r, c] = pieces[r, c].Copy(); // głęboka kopia
                }
            }
            return copy;
        }
    }
}
