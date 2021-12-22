using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TournamentManager.Model
{
    public class Group
    {
        public Group(IReadOnlyList<Player> players)
        {
            Players = players;
        }

        [JsonIgnore]
        public IReadOnlyList<Player> Players { get; }

        public IEnumerable<int> PlayersIds => Players.Select(p => p.Id);
    }
}