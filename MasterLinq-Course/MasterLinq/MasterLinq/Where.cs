using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLinq
{
    public class Where
    {
        public static void Demo()
        {
            var names = new List<string>()
            {
                "Mary", "John", "Bob", "Harry", "Elias", "Ann", "Ada"
            };

            var filteredNames = names.Where(name => name.Length > 3);
            var everySecond = names.Where((name, i) => i % 2 == 0);

            foreach (var filteredName in filteredNames)
            {
                Console.WriteLine(filteredName);
            }

            Console.WriteLine();

            foreach (var item in everySecond)
            {
                Console.WriteLine(item);
            }

        }
    }
}
