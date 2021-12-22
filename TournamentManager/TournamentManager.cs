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
            int countOfGroups, int n, int k, DistributionKind distributionKind, bool emptySlots = false)
        {
            IReadOnlyList<Player> newPlayers = distributionKind switch
            {
                DistributionKind.Random => ReshufflePlayers(players),
                DistributionKind.SwissSorted => players.OrderBy(p => p.SwissScore).ToList(),
                DistributionKind.IdSorted => players.OrderBy(p => p.Id).ToList(),
                DistributionKind.NameSorted => players.OrderBy(p => p.Name).ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(distributionKind))
            };

            var groups = new List<Group>();

            for (var i = 0; i < countOfGroups; i++)
            {
                var bufGroup = new List<Player>();
                for (var j = 0; j < k; j++)
                {
                    var index = i * k + j;
                    if (index == n)
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

            return new TournamentTable(groups);
        }

        private static int GetCountOfGroups(int n, int k)
        {
            return (n + k - 1) / k;
        }

        private static TournamentTable Shift(TournamentTable table, int circleIndex, int step)
        {
            var groups = new List<Group>();
            for (var i = 0; i < table.Groups.Count; i++)
            {
                var players = new List<Player>();
                for (var j = 0; j < table.Groups[i].Players.Count; j++)
                {
                    if (j == circleIndex)
                    {
                        players.Add(
                            table.Groups[(table.Groups.Count + i - step) % table.Groups.Count].Players[circleIndex]
                        );
                    }
                    else
                    {
                        players.Add(table.Groups[i].Players[j]);
                    }
                }

                groups.Add(new Group(players));
            }

            return new TournamentTable(groups);
        }

        private TournamentTable OrganizeLastRounds(TournamentTable table, int countOfGroups, int k, int index)
        {
            // надо узнать максимальное число челов в одном кольце - это собственно, число групп разделить на к
            var countOfFullOneRingGroup = countOfGroups / k;
            var newIndex = index % k;
            
            var rings = new List<Group>();
            for (int i = 0; i < k; i++) // обходим каждое кольцо
            {
                var ring = new List<Player>();
                for (int j = 0; j < countOfGroups; j++)
                { 
                    ring.Add(table.Groups[j].Players[i]);
                }

                
                rings.Add(new Group(ring));
            }

            var nigga = new List<TournamentTable>();
            foreach (var ring in rings)
            {//
                nigga.Add(CreateTableByRoundRobin(ring.Players, k, newIndex));
            }

            return new TournamentTable(rings);//parasha
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

            return new TournamentTable(groups);
        }

        public TournamentTable CreateTableByRoundRobin(IReadOnlyList<Player> players, int k, int tourIndex)
        {
            var n = players.Count;
            var countOfGroups = GetCountOfGroups(n, k);

            var newPlayers = DistributePlayers(players,
                countOfGroups,
                n,
                k,
                DistributionKind.IdSorted,
                true);
            if (tourIndex == 0)
            {
                return newPlayers;
            }

            // todo добавить дополнительные матчи для челов из одного кольца
            var countOfRound = 1 + (countOfGroups - 1) * k;
            if (tourIndex >= countOfRound)
            {
                // если у нас предпоследний раунд, то пора разыгрывать мачти между игроками из одного кольца
                // будем приводить таблицу в состояние, когда игроки из одного кольца вместе
                // наши группы всегда заполнены(настоящими или нет игроками)
                // поэтому мы получим таблицу из группированных по группам колец
                // длину кольца мы знаем
                // поэтому поделим её 
                newPlayers = OrganizeLastRounds(newPlayers, countOfGroups, k, tourIndex - countOfRound);
            }
            else
            {
                newPlayers = Shift(
                    newPlayers,
                    (tourIndex - 1) / (countOfGroups - 1),
                    1 + (tourIndex - 1) % (countOfGroups - 1)
                );
            }

            return DeleteNullPlayers(newPlayers);
        }

        public TournamentTable CreateTableBySwissSystem(IReadOnlyList<Player> players, int k)
        {
            var n = players.Count;
            var countOfGroups = GetCountOfGroups(n, k);
            return DistributePlayers(players, countOfGroups, n, k, DistributionKind.SwissSorted);
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

            var n = newPlayers.Count;
            var countOfGroups = GetCountOfGroups(n, k);

            var newTable = DistributePlayers(newPlayers, countOfGroups, n, k, DistributionKind.Random);

            return newTable;
        }

        private TournamentTable CreateBottomTable(
            IReadOnlyList<Player> players,
            int k)
        {
            var newPlayers = players.Where(p => p.Loses == 1).ToList();

            var n = newPlayers.Count;
            var countOfGroups = GetCountOfGroups(n, k);

            var newTable = DistributePlayers(newPlayers, countOfGroups, n, k, DistributionKind.Random);

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
            switch (settings.TournamentSystem)
            {
                case "RoundRobin":
                    return new OutputSettings()
                    {
                        Table = CreateTableByRoundRobin(
                            settings.Players,
                            settings.playersPerGroup,
                            settings.TourIndex),
                        TournamentSystem = settings.TournamentSystem
                    };
                case "Swiss":
                    return new OutputSettings()
                    {
                        Table = CreateTableBySwissSystem(settings.Players, settings.playersPerGroup),
                        TournamentSystem = settings.TournamentSystem
                    };
                case "SE":
                    return new OutputSettings()
                    {
                        Table = CreateTableBySingleElimination(settings.Players, settings.playersPerGroup),
                        TournamentSystem = settings.TournamentSystem
                    };
                case "DE":
                    return new OutputSettings()
                    {
                        DoubleTable = CreateTableByDoubleElimination(
                            settings.Players,
                            settings.playersPerGroup),
                        TournamentSystem = settings.TournamentSystem
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(settings.TournamentSystem));
            }
        }
    }
}