using ChessLogic.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessLogic.Core
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        public GameState(Board board, Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
            Board = board;
        }

        // Zwraca legalne ruchy dla bierki na danej pozycji (filtruje ruchy zostawiające króla w szachu)
        public IEnumerable<Move> LegalMovesForPieces(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => !MovePutsKingInCheck(move, CurrentPlayer));
        }

        // Wykonuje ruch i przełącza gracza, następnie sprawdza wynik gry
        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = PlayerExtansions.Opponent(CurrentPlayer);
            CheckForGameOver();
        }

        // Czy dany gracz jest aktualnie w szachu?
        public bool IsInCheck(Player player)
        {
            return Board.IsInCheck(player);
        }

        // Czy dany gracz ma jakiekolwiek legalne ruchy?
        private bool HasAnyLegalMoves(Player player)
        {
            return Board.PiecePositionsFor(player).Any(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board).Any(move => !MovePutsKingInCheck(move, player));
            });
        }

        // Symuluje ruch na kopii planszy i sprawdza czy własny król wchodzi w szach
        private bool MovePutsKingInCheck(Move move, Player player)
        {
            Board copy = Board.Copy();
            move.Execute(copy);
            return copy.IsInCheck(player);
        }

        // Sprawdza zakończenie gry po każdym ruchu
        private void CheckForGameOver()
        {
            // Aktualny gracz nie ma żadnych legalnych ruchów
            if (!HasAnyLegalMoves(CurrentPlayer))
            {
                if (IsInCheck(CurrentPlayer))
                {
                    // Mat - przeciwnik wygrał
                    Result = Result.Win(PlayerExtansions.Opponent(CurrentPlayer));
                }
                else
                {
                    // Pat - remis
                    Result = Result.Draw();
                }
            }
        }

        // Czy gra się skończyła?
        public bool IsGameOver()
        {
            return Result != null;
        }
    }
}