using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MasterLinq
{
    public class XmlLinq
    {
        public static void CsvToXml()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "ChessStats", "Top100ChessPlayers.csv");

            var records = File.ReadAllLines(file)
                .Skip(1)
                .Select(ChessPlayer.ParseFideCsv)
                .ToList();

            var doc = new XDocument();
            var players = new XElement("Players",
                records.Select(record=> new XElement("Player",
                    new XAttribute("Id", record.Id),
                    new XAttribute("Rating", record.Rating),
                    new XAttribute("BirthYear", record.BirthYear),
                    new XAttribute("Country", record.Country),
                    new XAttribute("FirstName", record.FirstName),
                    new XAttribute("LastName", record.LastName))
                ));
            doc.Add(players);
            doc.Save("ChessPlayers2.xml");
        }

        public static void ReadXml()
        {
            var doc = XDocument.Load("ChessPlayers2.xml");

            var query = doc.Element("Players")
                    ?.Elements("Player")
                    .Where(item => (int) item.Attribute("Rating") > 2700)
                    .Select(item => item.Attribute("LastName")?.Value)
                    ?? Enumerable.Empty<string>();

            foreach (var lastName in query)
            {
                Console.WriteLine(lastName);
            }
        }
    }
}
