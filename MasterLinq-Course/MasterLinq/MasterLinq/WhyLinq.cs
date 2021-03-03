using System;
using System.IO;
using System.Linq;

namespace MasterLinq
{
    public class WhyLinq
    {
        public static void Demo()
        {
            var location = Path.Combine(Directory.GetCurrentDirectory(), "StudentStats");
            DisplayLargestStatFilesWithoutLinq(location);

            Console.WriteLine();

            DisplayLargestStaFilesWithLinq(location);

        }

        private static void DisplayLargestStaFilesWithLinq(string path)
        {
            new DirectoryInfo(path)
                .GetFiles()
                .Filter(file => file.LastWriteTime < new DateTime(2018, 08, 01))
                //.Where(file => file.LastWriteTime < new DateTime(2018, 08, 01))
                .OrderBy(file => file.Length)
                .Take(5)
                .ForEach(file => Console.WriteLine($"{file.Name} weights {file.Length}"));

            //foreach (var file in files)
            //{
            //    Console.WriteLine($"{file.Name} weights {file.Length}");
            //}
        }

        private static void DisplayLargestStatFilesWithoutLinq(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles();
            Array.Sort(files, (x, y) =>
            {
                if (x.Length == y.Length)
                    return 0;
                if (x.Length > y.Length)
                    return 1;
                return -1;
            });

            for (int i = 0; i < 5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name} weights {file.Length}");
            }
        }
    }
}
