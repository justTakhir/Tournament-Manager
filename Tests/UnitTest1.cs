using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using TournamentManager.Model;

namespace Tests
{
    public class Tests
    {
        [TestCase("DoubleElimination/Test1")]
        [TestCase("DoubleElimination/Test2")]
        [TestCase("DoubleElimination/Test3")]
        [TestCase("DoubleElimination/Test4")]
        public void Test12(string testName)
        {
            var inputSettingsStr = File.ReadAllText($"TestData/{testName}/input_settings.json");
            var inputPlayersStr = File.ReadAllText($"TestData/{testName}/input_players.json");

            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputSettingsStr);
            var inputPlayers = JsonConvert.DeserializeObject<InputPlayers>(inputPlayersStr);

            var tournamentManager = new TournamentManager.TournamentManager();

            var outputSettings = tournamentManager.Index(inputSettings, inputPlayers);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);

            if (!Directory.Exists($"OutputData/{testName}"))
            {
                Directory.CreateDirectory($"OutputData/{testName}");
            }

            File.WriteAllText($"OutputData/{testName}/output.json", outputStr);
        }
    }
}