namespace TournamentManager.Model
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }
        public int Loses { get; } // must be define on CATS
        public int SwissScore { get; }

        public Player(int id, string name, int loses, int swissScore)
        {
            Id = id;
            Name = name;
            Loses = loses;
            SwissScore = swissScore;
        }
    }
}