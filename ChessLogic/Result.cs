namespace ChessLogic
{
    public enum EndReason
    {
        Checkmate,
        Stalemate
    }

    public class Result
    {
        public Player? Winner { get; }
        public EndReason Reason { get; }

        private Result(Player? winner, EndReason reason)
        {
            Winner = winner;
            Reason = reason;
        }

        // Wygrana przez mata
        public static Result Win(Player winner)
        {
            return new Result(winner, EndReason.Checkmate);
        }

        // Remis przez pata
        public static Result Draw()
        {
            return new Result(null, EndReason.Stalemate);
        }
    }
}
