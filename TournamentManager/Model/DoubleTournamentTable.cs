namespace TournamentManager.Model
{
    public class DoubleTournamentTable
    {
        public DoubleTournamentTable(TournamentTable top, TournamentTable bottom)
        {
            Top = top;
            Bottom = bottom;
        }

        public TournamentTable Top { get; }
        public TournamentTable Bottom { get; }
    }
}