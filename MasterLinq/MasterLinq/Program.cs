using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLinq
{
    public static class RandomStream
    {
        public static IEnumerable<double> Generate()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }
    }
    public class Program
    {
        public static void Main()
        {
            EfDemo.Run();
        }

        private static void IntersectExceptDemo()
        {
            var products1 = new List<string>() { "milk", "butter", "soda" };
            var products2 = new List<string>() { "coffee", "Butter", "milk", "pizza" };

            var intersect = products1.Intersect(products2, new ProductsComparer());
            var except = products1.Except(products2, new ProductsComparer());

            foreach (var item in intersect)
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine("\n\nExcept");
            foreach (var item in except)
            {
                Console.Write(item + ", ");
            }
        }

        private static void ConcatUnionDemo()
        {
            var products1 = new List<string>() { "milk", "butter", "soda" };
            var products2 = new List<string>() { "coffee", "Butter", "milk", "pizza" };

            Console.WriteLine("Concat");
            foreach (var item in products1.Concat(products2))
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine("\n\nUnion");
            foreach (var item in products1.Union(products2))
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine("\n\nCustom Union");
            foreach (var item in products1.Union(products2, new ProductsComparer()))
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine("\n");
        }

        private static void ElementAtCount()
        {
            var players = ChessPlayer.GetDemoList().ToList();

            int count = players.Count(player => player.Rating > 2800);

            long longCount = players.LongCount();

            ChessPlayer at = players.ElementAt(1);

            Console.WriteLine($"Count:{count}, LongCount:{longCount}, At(1):{at}");
        }

        //private static void ProcessCollection(IList<ChessPlayer> players)
        private static void ProcessCollection(IReadOnlyCollection<ChessPlayer> players)
        {
            Console.WriteLine("FirstNames\n");
            foreach (var player in players)
            {
                Console.WriteLine(player.FirstName);
            }

            Console.WriteLine("\nLastNames\n");
            foreach (var player in players)
            {
                Console.WriteLine(player.LastName);
            }
        }

        private static void MultipleEnumeration()
        {
            List<ChessPlayer> players = FilterPlayersByMinimumRating(2750).ToList();

            Console.WriteLine("FirstNames\n");
            foreach (var player in players)
            {
                Console.WriteLine(player.FirstName);
            }

            Console.WriteLine("\nLastNames\n");
            foreach (var player in players)
            {
                Console.WriteLine(player.LastName);
            }
        }
        private static IEnumerable<ChessPlayer> FilterPlayersByMinimumRating(int minRating)
        {
            return ChessPlayer.GetDemoList().Where(player => player.Rating >= minRating);
        }

        private static void AnyAllContains()
        {
            var players = ChessPlayer.GetDemoList().ToList();

            bool contains = players.Contains(new ChessPlayer()
            {
                Id = 6,
                BirthYear = 1993,
                FirstName = "Wesley",
                LastName = "So",
                Rating = 2780,
                Country = "USA"
            }, new PlayersComparer());

            bool any = players.Any(player => player.Country == "FRA");

            bool all = players.All(player => player.Rating > 2500);

            Console.WriteLine($"Contains = {contains}. Any = {any}. All = {all}");
        }

        private static void GeneratingDataStreams()
        {
            Console.WriteLine("Generating range");
            foreach (var r in Enumerable.Range(5, 8))
            {
                Console.WriteLine(r + " ");
            }

            Console.WriteLine("\nRepeating:");
            foreach (var r in Enumerable.Repeat(10, 5))
            {
                Console.WriteLine(r + " ");
            }

            Console.WriteLine("\nRandomizing");
            foreach (var r in RandomStream.Generate().Where(x => x > 0.7).Take(5))
            {
                Console.WriteLine(r.ToString("F2") + " ");
            }
        }

        public IEnumerable<int> GetData()
        {
            //if no elements please do this:
            return Enumerable.Empty<int>();
        }

        private static void DemoYield()
        {
            var players = ChessPlayer.GetDemoList().Where(p => p.Country == "USA");

            try
            {
                //foreach (var player in players)
                //{
                //    Console.WriteLine(player);
                //}
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"At foreach {ex}");
            }
        }

        private static void DemoNoYield()
        {
            var players = ChessPlayer.GetDemoList().Filter(p => p.Country == "USA");

            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
        }


        private static void NamedAnonSeparate()
        {
            var ratings = new List<int>()
            {
                2200,
                2400,
                2700,
                2800,
                2820
            };

            var ratings1 = ratings.Where(r => r > 2700);
            var ratings2 = ratings.Where(GetRatingsOver2700);
            var ratings3 = ratings.Where(delegate(int rating) { return rating > 2700; });

            foreach (var r in ratings1)
            {
                Console.WriteLine(r);
            }

            foreach (var r in ratings2)
            {
                Console.WriteLine(r);
            }

            foreach (var r in ratings3)
            {
                Console.WriteLine(r);
            }
        }

        private static bool GetRatingsOver2700(int arg)
        {
            return arg > 2700;
        }
    }

    public class ProductsComparer :IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.ToUpper().GetHashCode();
        }
    }
}
