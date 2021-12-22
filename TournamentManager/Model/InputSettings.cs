using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class InputSettings
    {
        public string TournamentSystem { get; } // Swiss/RoundRobin/SE/DE
        public int PlayersPerGroup { get; }

        public InputSettings(string tournamentSystem, int playersPerGroup)
        {
            TournamentSystem = tournamentSystem;
            PlayersPerGroup = playersPerGroup;
        }
    }
}