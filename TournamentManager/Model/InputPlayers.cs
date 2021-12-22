using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class InputPlayers
    {
        public IReadOnlyList<Player> Players { get; }

        public InputPlayers(IReadOnlyList<Player> players)
        {
            Players = players;
        }
    }
}