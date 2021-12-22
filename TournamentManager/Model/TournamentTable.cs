using System.Collections.Generic;

namespace TournamentManager.Model
{
    public class TournamentTable
    {
        public IReadOnlyList<Group> Groups { get; }

        public TournamentTable(IReadOnlyList<Group> groups)
        {
            Groups = groups;
        }
    }
}