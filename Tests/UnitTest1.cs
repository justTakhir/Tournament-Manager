using System.Collections.Generic;
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

        [TestCase("example")]
        [TestCase("example2")]
        [TestCase("example3")]
        [TestCase("example4")]
        [TestCase("example5")]
        [TestCase("example6")]
        public void Test12(string testName)
        {
            var inputStr = File.ReadAllText(
                $"TestData/input_{testName}.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                $"TestData/output_{testName}.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test1()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_DE/DE_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_DE/DE_output1.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test2()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_DE/DE_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_DE/DE_output2.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test3()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_DE/DE_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_DE/DE_output3.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test4()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_DE/DE_input4.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_DE/DE_output4.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test1()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_Swiss/Swiss_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_Swiss/Swiss_output1.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test2()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_Swiss/Swiss_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_Swiss/Swiss_output2.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test3()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_Swiss/Swiss_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_Swiss/Swiss_output3.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test4()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_Swiss/Swiss_input4.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_Swiss/Swiss_output4.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test1()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_SE/SE_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_SE/SE_output1.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test2()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_SE/SE_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_SE/SE_output2.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test3()
        {
            var inputStr = File.ReadAllText(
                "TestData/Example_SE/SE_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings, Formatting.Indented);
            File.WriteAllText(
                "TestData/Example_SE/SE_output3.json",
                outputStr
            );
        }
    }
}