using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace MasterLinq
{
    public class EfDemo
    {
        public static void Run()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChessPlayerDb>());
            //InsertData();
            //QueryData();

            Pitfalls();
            //DemoExpression();
        }

        public static void Demo()
        {
            var db = new ChessPlayerDb();

            IQueryable<ChessPlayer> orderByRating = OrderByRating(db.ChessPlayers);

            var chessPlayer = ChessPlayer.GetDemoList();
            IQueryable<ChessPlayer> byRating = OrderByRating(chessPlayer.AsQueryable());
        }

        public static IQueryable<ChessPlayer> OrderByRating(IQueryable<ChessPlayer> players)
        {
            return players.OrderBy(player => player.Rating);
        }

        private static void DemoExpression()
        {
            Expression<Func<int, int, int>> add = (x, y) => x + y;

            //var result = add(1, 2);
            //Console.WriteLine(result);
            Console.WriteLine(add);
        }
        
        private static void Pitfalls()
        {
            var db = new ChessPlayerDb();
           
            var query = db.ChessPlayers
                .Where(p => p.Rating > 2700)
                .OrderByDescending(p => p.Rating)
                .AsEnumerable()
                .Contains(new ChessPlayer());

            Console.WriteLine();
        }

        private static void InsertData()
        {
            var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "ChessStats", "Top100ChessPlayers.csv");

            var records = File.ReadAllLines(fileLocation)
                .Skip(1)
                .Select(ChessPlayer.ParseFideCsv)
                .ToList();

            var db = new ChessPlayerDb();

            if (!db.ChessPlayers.Any())
            {
                db.ChessPlayers.AddRange(records);
            }

            db.SaveChanges();
        }

        private static void QueryData()
        {
            var db = new ChessPlayerDb();
            db.Database.Log = Console.WriteLine;

            var query = db.ChessPlayers
                .Where(player => player.Rating > 2700)
                .OrderByDescending(player => player.Rating);

            foreach (var player in query)
            {
                Console.WriteLine($"{player.LastName}. Rating:{player.Rating}");
            }
        }
    }

    public class ChessPlayerDb : DbContext
    {
        public DbSet<ChessPlayer> ChessPlayers { get; set; }
    }
}