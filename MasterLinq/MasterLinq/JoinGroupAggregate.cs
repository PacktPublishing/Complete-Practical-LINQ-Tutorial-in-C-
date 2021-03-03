using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MasterLinq
{
    public class JoinGroupAggregate
    {
        public static void Demo()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "ChessStats", "Top100ChessPlayers.csv");
            //GroupJoinDemo();
            //GroupByDemo(file);
            ZipDemo();
        }

        public static void ZipDemo()
        {
            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Bavaria", Country ="Germany" },
                new Team { Name = "Barcelona", Country ="Spain" },
                new Team() {Name="Juventus", Country = "Italy"}
            };
            List<Player> players = new List<Player>()
            {
                new Player {Name="Messy", Team="Barcelona"},
                new Player {Name="Neimar", Team="Barcelona"},
                new Player {Name="Robben", Team="Bavaria"},
                new Player {Name="Buffon", Team="Juventus"}
            };

            var result = players.Zip(teams,
                (player, team) => new
                {
                    Name = player.Name,
                    Team = team.Name,
                    Country = team.Country
                });
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} - {item.Team} from {item.Country}");
            }
        }

        public static void GroupJoinDemo()
        {
            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Bavaria", Country ="Germany" },
                new Team { Name = "Barcelona", Country ="Spain" },
                new Team() {Name="Juventus", Country = "Italy"}
            };
            List<Player> players = new List<Player>()
            {
                new Player {Name="Messy", Team="Barcelona"},
                new Player {Name="Neimar", Team="Barcelona"},
                new Player {Name="Robben", Team="Bavaria"},
                new Player {Name="Buffon", Team="Juventus"}
            };

            var result = teams.GroupJoin(
                players,
                t => t.Name,
                pl => pl.Team,
                (team, pls) => new
                {
                    Name = team.Name,
                    Country = team.Country,
                    Players = pls.Select(p => p.Name)
                });
            foreach (var team in result)
            {
                Console.WriteLine($"Players in {team.Name}");
                foreach (string player in team.Players)
                {
                    Console.WriteLine(player);
                }
                Console.WriteLine();
            }
        }

        public static void GroupByDemo(string file)
        {
            var players = File.ReadAllLines(file)
                .Skip(1)
                .Select(ChessPlayer.ParseFideCsv)
                .Where(player => player.BirthYear > 1988)
                .Take(10)
                .GroupBy(p => p.Country)
                .OrderByDescending(g => g.Key)
                .ToList();

            foreach (var player in players)
            {
                Console.WriteLine($"\nThe following players live in {player.Key}");
                foreach (var p in player)
                {
                    Console.WriteLine($"Name:{p.LastName}, Rating={p.Rating}");
                }
            }
        }

        public static void JoinDemo(string file)
        {
            var players = File.ReadAllLines(file)
                .Skip(1)
                .Select(ChessPlayer.ParseFideCsv)
                .Where(player => player.BirthYear > 1988)
                .Take(10).ToList();

            var tournaments = Tournament.GetDemoStats();

            var join = players.Join(tournaments,
                p => p.Id, t => t.PlayerId,
                (p, t) => new
                {
                    p.LastName,
                    p.Rating,
                    t.Title,
                    t.TakenPlace,
                    t.Country
                });

            foreach (var cur in join)
            {
                Console.WriteLine($"{cur.LastName} took {cur.TakenPlace} place at {cur.Title}. Has " +
                                  $"rating {cur.Rating}");
            }

            Console.WriteLine("\nAfter join\n");

            var selectMany = join.GroupBy(x => x.Country)
                                 .SelectMany(g => g.OrderBy(grp => grp.TakenPlace));
            foreach (var cur in selectMany)
            {
                Console.WriteLine($"{cur.LastName} took {cur.TakenPlace} place at {cur.Title}. Has " +
                                  $"rating {cur.Rating}");
            }
        }
    }

    public class Team
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
    }
}
