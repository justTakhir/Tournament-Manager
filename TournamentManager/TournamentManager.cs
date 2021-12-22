using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TournamentManager.Model;

namespace TournamentManager
{
    public class TournamentManager
    {
        private static readonly Random Rand = new();

        public static List<Player> ReshufflePlayers(IReadOnlyList<Player> players)
        {
            var newPlayers = new List<Player>(players.Count);
            foreach (var player in players)
            {
                newPlayers.Add(player);
            }

            for (int i = 0; i < newPlayers.Count; i++)
            {
                var index = Rand.Next(i, newPlayers.Count);
                (newPlayers[i], newPlayers[index]) = (newPlayers[index], newPlayers[i]);
            }

            return newPlayers;
        }

        private enum DistributionKind
        {
            Random,
            SwissSorted,
            IdSorted,
            NameSorted
        }

        private static TournamentTable DistributePlayers(IReadOnlyList<Player> players,
            int k, DistributionKind distributionKind, bool emptySlots = false)
        {
            IReadOnlyList<Player> newPlayers = distributionKind switch
            {
                DistributionKind.Random => ReshufflePlayers(players.Where(p => p != null).ToList()),
                DistributionKind.SwissSorted => players.Where(p => p != null).OrderBy(p => p.SwissScore).ToList(),
                DistributionKind.IdSorted => players.Where(p => p != null).OrderBy(p => p.Id).ToList(),
                DistributionKind.NameSorted => players.Where(p => p != null).OrderBy(p => p.Name).ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(distributionKind))
            };

            var n = newPlayers.Count;
            var countOfGroups = (n + k - 1) / k;

            var groups = new List<Group>();

            for (var i = 0; i < countOfGroups; i++)
            {
                var bufGroup = new List<Player>();
                for (var j = 0; j < k; j++)
                {
                    var index = i * k + j;
                    if (index >= n)
                    {
                        if (emptySlots)
                        {
                            bufGroup.Add(null);
                            continue;
                        }

                        break;
                    }

                    bufGroup.Add(newPlayers[index]);
                }

                groups.Add(new Group(bufGroup));
            }

            return new TournamentTable(groups, k);
        }

        private static TournamentTable JoinTables(IEnumerable<TournamentTable> tables)
        {
            var groups = new List<Group>();
            int playersPerGroup = 0;
            foreach (var table in tables)
            {
                groups.AddRange(table.Groups);
                playersPerGroup = table.PlayersPerGroup;
            }

            return new TournamentTable(groups, playersPerGroup);
        }

        private static IEnumerable<TournamentTable> Shift(TournamentTable table)
        {
            yield return table;

            if (table.Groups.Count <= 1)
            {
                yield break;
            }

            for (var circleIndex = 0; circleIndex < table.PlayersPerGroup; circleIndex++)
            {
                var circlePlayers = new List<Player> {table.Groups[0].Players[circleIndex]};
                for (var step = 1; step < table.Groups.Count; step++)
                {
                    circlePlayers.Add(table.Groups[step].Players[circleIndex]);

                    var groups = new List<Group>();
                    for (var i = 0; i < table.Groups.Count; i++)
                    {
                        var players = new List<Player>();
                        for (var j = 0; j < table.Groups[i].Players.Count; j++)
                        {
                            if (j == circleIndex)
                            {
                                players.Add(
                                    table.Groups[(table.Groups.Count + i - step) % table.Groups.Count]
                                        .Players[circleIndex]
                                );
                            }
                            else
                            {
                                players.Add(table.Groups[i].Players[j]);
                            }
                        }

                        groups.Add(new Group(players));
                    }

                    yield return new TournamentTable(groups, table.PlayersPerGroup);
                }

                var circleTable =
                    DistributePlayers(circlePlayers, table.PlayersPerGroup, DistributionKind.IdSorted, true);

                foreach (var tournamentTable in Shift(circleTable))
                {
                    yield return tournamentTable;
                }
            }
        }

        private TournamentTable DeleteNullPlayers(TournamentTable table)
        {
            var groups = new List<Group>();
            for (var i = 0; i < table.Groups.Count; i++)
            {
                var players = new List<Player>();
                for (var j = 0; j < table.Groups[i].Players.Count; j++)
                {
                    if (table.Groups[i].Players[j] == null)
                    {
                        continue;
                    }

                    players.Add(table.Groups[i].Players[j]);
                }

                groups.Add(new Group(players));
            }

            return new TournamentTable(groups, table.PlayersPerGroup);
        }

        public TournamentTable CreateTableByRoundRobin(IReadOnlyList<Player> players, int k)
        {
            var newPlayers = DistributePlayers(players,
                k,
                DistributionKind.IdSorted,
                true);

            return DeleteNullPlayers(JoinTables(Shift(newPlayers)));
        }


        public TournamentTable CreateTableBySwissSystem(IReadOnlyList<Player> players, int k)
        {
            return DistributePlayers(players, k, DistributionKind.SwissSorted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="players">Игроки</param>
        /// <param name="k">Количество игроков, играющих в одной группе</param>
        /// <param name="l">Количество игроков, которые считаются проигравшими</param>
        /// <returns></returns>
        public TournamentTable CreateTableBySingleElimination(
            IReadOnlyList<Player> players,
            int k)
        {
            var newPlayers = players.Where(p => p.Loses == 0).ToList();


            var newTable = DistributePlayers(newPlayers, k, DistributionKind.Random);

            return newTable;
        }

        private TournamentTable CreateBottomTable(
            IReadOnlyList<Player> players,
            int k)
        {
            var newPlayers = players.Where(p => p.Loses == 1).ToList();

            var newTable = DistributePlayers(newPlayers, k, DistributionKind.Random);

            return newTable;
        }

        public DoubleTournamentTable CreateTableByDoubleElimination(IReadOnlyList<Player> players,
            int k)
        {
            var topTable = players.Where(p => p.Loses == 0).ToList();
            var bottomTable = players.Where(p => p.Loses == 1).ToList();
            return new DoubleTournamentTable(
                CreateTableBySingleElimination(topTable, k),
                CreateBottomTable(bottomTable, k)
            );
        }

        public OutputSettings Index(InputSettings settings)
        {
            var tournamentSystem = settings.TournamentSystem;
            switch (settings.TournamentSystem)
            {
                case "RoundRobin":
                    var roundRobinTable = CreateTableByRoundRobin(settings.Players, settings.PlayersPerGroup);
                    return new OutputSettings(tournamentSystem, roundRobinTable);
                case "Swiss":
                    var swissTable = CreateTableBySwissSystem(settings.Players, settings.PlayersPerGroup);
                    return new OutputSettings(tournamentSystem, swissTable);
                case "SE":
                    var seTable = CreateTableBySingleElimination(settings.Players, settings.PlayersPerGroup);
                    return new OutputSettings(tournamentSystem, seTable);
                case "DE":
                    var deTable = CreateTableByDoubleElimination(settings.Players, settings.PlayersPerGroup);
                    return new OutputSettings(tournamentSystem, deTable);
                default:
                    throw new ArgumentOutOfRangeException(nameof(settings.TournamentSystem));
            }
        }
    }
}