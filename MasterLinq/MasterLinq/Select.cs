using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLinq
{
    public class Select
    {
        public static void Demo()
        {
            var players = ChessPlayer.GetDemoList().ToList();

            var ratings = players.Select(player => player.Rating);
            var lastNames = players.Select(player => player.LastName);
            var fullNames = players.Select(player => player.LastName + " " + player.FirstName);

            var anonymousType = players.Select((player, index) =>
            {
                return new
                {
                    Index = index,
                    player.FirstName,
                    player.LastName
                };
            });

            foreach (var rating in ratings)
            {
                Console.WriteLine(rating);
            }

            foreach (var cur in anonymousType)
            {
                Console.WriteLine(cur.FirstName + cur.LastName);
            }
        }
    }
}
