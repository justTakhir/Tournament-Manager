using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using TournamentManager;
using TournamentManager.Model;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var tournamentManager = new TournamentManager.TournamentManager();
            var player1 = new Player(0, "player1", 1, 23);
            var player2 = new Player(1, "player2", 0, 32);
            var players = new List<Player> {player1, player2};
            var table = tournamentManager.CreateTableBySingleElimination(players, 8);
            Assert.True(table.Groups.Count == 1);
            Assert.True(table.Groups[0].Players[0] == player2);
        }

        [TestCase(7, 3)]
        public void Test2(int n, int k)
        {
            var tournamentManager = new TournamentManager.TournamentManager();
            var players = new List<Player>();
            for (int i = 0; i < n; i++)
            {
                var player = new Player(i, $"player{i + 1}", 0, 0);
                players.Add(player);
            }

            var table = tournamentManager.CreateTableByRoundRobin(players, k);
            ;
            var outputStr = JsonConvert.SerializeObject(table, Formatting.Indented);
            File.WriteAllText(
                $"TestData/Example_RR/RR_output{n}_{k}.json",
                outputStr
            );
        }

        /*[TestCase("example")]
        [TestCase("example2")]
        [TestCase("example3")]
        [TestCase("example4")]
        [TestCase("example5")]
        [TestCase("example6")]*/
        [TestCase("Example_DE/Test1")]
        [TestCase("Example_DE/Test2")]
        [TestCase("Example_DE/Test3")]
        [TestCase("Example_DE/Test4")]
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
            
            // var forAssertStr = File.ReadAllText($"TestData/{testName}/output.json");
            // var forAssert = JsonConvert.DeserializeObject<OutputSettings>(forAssertStr);
            
            // Assert.True(outputSettings == forAssert);
        }
    }
}