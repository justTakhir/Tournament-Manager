using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class InputSettings
    {
        public IReadOnlyList<Player> Players;
        public string TournamentSystem;// Swiss/RoundRobin/SE/DE
        public int TourIndex;
        public int playersPerGroup;
    }
}