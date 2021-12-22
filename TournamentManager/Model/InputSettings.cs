using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class InputSettings
    {
        public string TournamentSystem { get; } // Swiss/RoundRobin/SE/DE
        public int PlayersPerGroup { get; }
        public IReadOnlyList<Player> Players { get; }

        public InputSettings(string tournamentSystem, int playersPerGroup, IReadOnlyList<Player> players)
        {
            TournamentSystem = tournamentSystem;
            PlayersPerGroup = playersPerGroup;
            Players = players;
        }
    }
}