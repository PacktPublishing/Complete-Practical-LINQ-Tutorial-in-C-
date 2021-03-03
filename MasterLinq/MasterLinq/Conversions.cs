using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MasterLinq
{
    public class Conversions
    {
        public static void Demo()
        {
            //ToArrayToList();
            //ToDictionary();
            ToLookup();
        }

        private static void ToLookup()
        {
            Type[] sampleTypes = {typeof(List<>), typeof(string),
                typeof(Enumerable), typeof(XmlReader)};

            IEnumerable<Type> allTypes = sampleTypes
                .Select(t => t.Assembly)
                .SelectMany(a => a.GetTypes());

            ILookup<string, Type> lookup = allTypes.ToLookup(t => t.Namespace);

            foreach (Type type in lookup["System"])
            {
                Console.WriteLine($"{type.FullName}, {type.Assembly.GetName().Name}");
            }
        }

        private static void ToDictionary()
        {
            var players = ChessPlayer.GetDemoList();
            var chessPlayers = players.ToDictionary(x=>x.Id);

            foreach (var player in chessPlayers)
            {
                Console.WriteLine($"Id:{player.Key}. Last Name:{player.Value.LastName}");
            }
        }

        private static void ToArrayToList()
        {
            List<ChessPlayer> players1 = ChessPlayer.GetDemoList().ToList();
            ChessPlayer[] players2 = ChessPlayer.GetDemoList().ToArray();

            Console.WriteLine("Players in list");
            foreach (var player in players1)
            {
                Console.WriteLine($"Player Name:{player.LastName}");
            }
            Console.WriteLine("\nPlayers in array\n");
            foreach (var player in players2)
            {
                Console.WriteLine($"Player Name:{player.LastName}");
            }
            //players2.ToList();
        }

        private static void OfTypeAndCast()
        {
            object[] objs = {"12345", 12};
            try
            {
                objs.Cast<string>().ToArray();
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("\nOfType\n");

            string[] strs = objs.OfType<string>().ToArray();
            foreach (var str in strs)
            {
                Console.WriteLine(str);
            }
        }
    }
}
