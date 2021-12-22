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

        [Test]
        public void Test2()
        {
            var tournamentManager = new TournamentManager.TournamentManager();
            var player1 = new Player(0, "player1", 0, 0);
            var player2 = new Player(1, "player2", 0, 0);
            var player3 = new Player(2, "player3", 0, 0);
            var player4 = new Player(3, "player4", 0, 0);
            var player5 = new Player(4, "player5", 0, 0);
            var player6 = new Player(5, "player6", 0, 0);
            var player7 = new Player(6, "player7", 0, 0);
            var player8 = new Player(7, "player8", 0, 0);
            var player9 = new Player(8, "player9", 0, 0);
            var player10 = new Player(9, "player10", 0, 0);
            var player11 = new Player(10, "player11", 0, 0);
            var player12 = new Player(10, "player12", 0, 0);
            var players = new List<Player>
            {
                player1, player2, player3, player4, player5, player6, player7, player8,
                player9, player10, player11, player12
            };
            var table = tournamentManager.CreateTableByRoundRobin(players, 3, 7);
            Assert.True(table.Groups.Count == 4);
            Assert.True(table.Groups[1].Players[2] == player3);
            Assert.True(table.Groups[2].Players[2] == player6);
            Assert.True(table.Groups[3].Players[2] == player9);
            Assert.True(table.Groups[0].Players[2] == player12);

            Assert.True(table.Groups[0].Players[0] == player1);
            Assert.True(table.Groups[1].Players[0] == player4);
            Assert.True(table.Groups[2].Players[0] == player7);
            Assert.True(table.Groups[3].Players[0] == player10);

            Assert.True(table.Groups[0].Players[1] == player2);
            Assert.True(table.Groups[1].Players[1] == player5);
            Assert.True(table.Groups[2].Players[1] == player8);
            Assert.True(table.Groups[3].Players[1] == player11);
        }

        [Test]
        public void Test3()
        {
            var tournamentManager = new TournamentManager.TournamentManager();
            var player1 = new Player(0, "player1", 0, 0);
            var player2 = new Player(1, "player2", 0, 0);
            var player3 = new Player(2, "player3", 0, 0);
            var player4 = new Player(3, "player4", 0, 0);
            var player5 = new Player(4, "player5", 0, 0);
            var player6 = new Player(5, "player6", 0, 0);
            var player7 = new Player(6, "player7", 0, 0);
            var player8 = new Player(7, "player8", 0, 0);
            var player9 = new Player(8, "player9", 0, 0);
            var player10 = new Player(9, "player10", 0, 0);
            var player11 = new Player(10, "player11", 0, 0);
            var players = new List<Player>
            {
                player1, player2, player3, player4, player5, player6, player7, player8,
                player9, player10, player11
            };
            var table = tournamentManager.CreateTableByRoundRobin(players, 3, 7);
            Assert.True(table.Groups.Count == 4);
            Assert.True(table.Groups[1].Players[2] == player3);
            Assert.True(table.Groups[2].Players[2] == player6);
            Assert.True(table.Groups[3].Players[2] == player9);


            Assert.True(table.Groups[0].Players[0] == player1);
            Assert.True(table.Groups[1].Players[0] == player4);
            Assert.True(table.Groups[2].Players[0] == player7);
            Assert.True(table.Groups[3].Players[0] == player10);

            Assert.True(table.Groups[0].Players[1] == player2);
            Assert.True(table.Groups[1].Players[1] == player5);
            Assert.True(table.Groups[2].Players[1] == player8);
            Assert.True(table.Groups[3].Players[1] == player11);
        }

        [Test]
        public void Test11()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example.json",
                outputStr
            );
            //Assert.True(outputStr == ""); // my output JSON Example for A.A.
        }

        [Test]
        public void Test12()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example2.json",
                outputStr
            );
        }

        [Test]
        public void Test13()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example3.json",
                outputStr
            );
        }

        [Test]
        public void Test14()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example4.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example4.json",
                outputStr
            );
        }

        [Test]
        public void Test15()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example5.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example5.json",
                outputStr
            );
        }

        [Test]
        public void Test16()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/input_example6.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/output_example6.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test1()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_output1.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test2()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_output2.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test3()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_output3.json",
                outputStr
            );
        }

        [Test]
        public void DE_Test4()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_input4.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_DE/DE_output4.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test1()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_output1.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test2()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_output2.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test3()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_output3.json",
                outputStr
            );
        }

        [Test]
        public void Swiss_Test4()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_input4.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_Swiss/Swiss_output4.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test1()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_input1.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_output1.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test2()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_input2.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_output2.json",
                outputStr
            );
        }

        [Test]
        public void SE_Test3()
        {
            var inputStr = File.ReadAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_input3.json"
            ); // my input JSON Example for A.A.
            var inputSettings = JsonConvert.DeserializeObject<InputSettings>(inputStr);
            var tournamentManager = new TournamentManager.TournamentManager();
            var outputSettings = tournamentManager.Index(inputSettings);
            var outputStr = JsonConvert.SerializeObject(outputSettings);
            File.WriteAllText(
                "C:/Users/tanto/RiderProjects/TournamentManager/TournamentManager/Example_SE/SE_output3.json",
                outputStr
            );
        }
    }
}