namespace ChessLogic
{
    public class Direction
    {
        public readonly static Direction North = new Direction(-1, 0);
        public readonly static Direction South = new Direction(1, 0);
        public readonly static Direction East = new Direction(0, 1);
        public readonly static Direction West = new Direction(0, -1);
        public readonly static Direction NorthWest = North + West;
        public readonly static Direction SouthWest = South + West;
        public readonly static Direction NorthEast = North + East;
        public readonly static Direction SouthEast = South + East;
        public int rowDelta { get; }
        public int colDelta { get; }
        public Direction(int rowDelta, int colDelta)
        {
            this.rowDelta = rowDelta;
            this.colDelta = colDelta;
        }
        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.rowDelta + dir2.rowDelta, dir1.colDelta + dir2.colDelta);
        }
        public static Direction operator *(int scalar, Direction dir2)
        {
            return new Direction(scalar * dir2.rowDelta, scalar * dir2.colDelta);
        }
    }
}
