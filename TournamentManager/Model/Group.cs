using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class Group
    {
        public Group(IReadOnlyList<Player> players)
        {
            Players = players;
        }

        public IReadOnlyList<Player> Players { get; }
    }
}