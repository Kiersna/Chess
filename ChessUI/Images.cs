using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic;
using ChessLogic.Pieces;

namespace ChessUI
{
    public class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSource = new Dictionary<PieceType, ImageSource>()
        {
            [PieceType.Pawn] = LoadImage("Assets/PawnW.png"),
            [PieceType.Bishop] = LoadImage("Assets/BishopW.png"),
            [PieceType.Knight] = LoadImage("Assets/KnightW.png"),
            [PieceType.Rook] = LoadImage("Assets/RookW.png"),
            [PieceType.Queen] = LoadImage("Assets/QueenW.png"),
            [PieceType.King] = LoadImage("Assets/KingW.png"),
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSource = new Dictionary<PieceType, ImageSource>()
        {
            [PieceType.Pawn] = LoadImage("Assets/PawnB.png"),
            [PieceType.Bishop] = LoadImage("Assets/BishopB.png"),
            [PieceType.Knight] = LoadImage("Assets/KnightB.png"),
            [PieceType.Rook] = LoadImage("Assets/RookB.png"),
            [PieceType.Queen] = LoadImage("Assets/QueenB.png"),
            [PieceType.King] = LoadImage("Assets/KingB.png"),
        };
        private static ImageSource LoadImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        public static ImageSource GetImage(Player color, PieceType type)
        {
            switch (color)
            {
                case Player.White:
                    return whiteSource[type];
                case Player.Black:
                    return blackSource[type];
                default:
                    return null;
            }
        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null) return null;
            return GetImage(piece.Color, piece.Type);
        }
    }


}
