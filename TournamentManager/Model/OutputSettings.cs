using Newtonsoft.Json;

namespace TournamentManager.Model
{
    public class OutputSettings
    {
        public string TournamentSystem { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TournamentTable Table { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DoubleTournamentTable DoubleTable { get; }

        public OutputSettings(string tournamentSystem, TournamentTable table)
        {
            TournamentSystem = tournamentSystem;
            Table = table;
            DoubleTable = null;
        }

        public OutputSettings(string tournamentSystem, DoubleTournamentTable doubleTable)
        {
            TournamentSystem = tournamentSystem;
            Table = null;
            DoubleTable = doubleTable;
        }
    }
}