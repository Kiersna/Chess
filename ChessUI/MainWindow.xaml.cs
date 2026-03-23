using ChessLogic;
using ChessLogic.Core;
using ChessLogic.Pieces;
using ChessUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] piecesImages = new Image[8, 8];
        private readonly Rectangle[,] highLights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private Position selectedPosition = null;

        private GameState gameState;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new GameState(Board.Initial(), Player.White);
            DrawBoard(gameState.Board);
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    piecesImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle rectangle = new Rectangle();
                    highLights[r, c] = rectangle;
                    HighLigthGrid.Children.Add(rectangle);
                }

            }
        }
        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    piecesImages[r, c].Source = Images.GetImage(piece);
                }

            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);

            Position position = ToSquarePosition(point);

            if (selectedPosition == null)
            {
                OnFromPositionSelected(position);
            }
            else
            {
                OnToPositionSelected(position);
            }
        }

        private void OnToPositionSelected(Position position)
        {
            selectedPosition = null;
            HideHighLights();
            if (moveCache.TryGetValue(position, out Move move))
            {
                HadleMove(move);
            }
        }

        private void HadleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
        }

        private void OnFromPositionSelected(Position position)
        {
            var moves = gameState.LegalMovesForPieces(position);
            if (moves.Any())
            {
                selectedPosition = position;
                CacheMoves(moves);
                ShowHighLights();
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squereSize = BoardGrid.ActualHeight / 8;
            int row = (int)(point.Y / squereSize);
            int col = (int)(point.X / squereSize);
            return new Position(row, col);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.ToPosition] = move;
            }
        }
        private void ShowHighLights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);
            foreach (Position to in moveCache.Keys)
            {
                highLights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighLights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highLights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
    }
}