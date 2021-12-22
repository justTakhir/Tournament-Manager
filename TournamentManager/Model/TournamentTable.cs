using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class TournamentTable
    {
        public int PlayersPerGroup { get; }
        public IReadOnlyList<Group> Groups { get; }

        public TournamentTable(IReadOnlyList<Group> groups, int playersPerGroup)
        {
            Groups = groups;
            PlayersPerGroup = playersPerGroup;
        }
    }
}