using System.Collections.Generic;

namespace MasterLinq
{
    public class Tournament
    {
        public int PlayerId { get; set; }
        public string Title { get; set; }
        public int TakenPlace { get; set; }
        public string Country { get; set; }

        public static IEnumerable<Tournament> GetDemoStats()
        {
            return new List<Tournament>()
            {
                new Tournament() { Country = "Germany", PlayerId = 1, TakenPlace = 1, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 1, TakenPlace = 3, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 1, TakenPlace = 2, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 2, TakenPlace = 2, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 2, TakenPlace = 1, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 2, TakenPlace = 1, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 3, TakenPlace = 5, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 3, TakenPlace = 9, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 3, TakenPlace = 5, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 4, TakenPlace = 4, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 4, TakenPlace = 6, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 4, TakenPlace = 3, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 5, TakenPlace = 7, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 5, TakenPlace = 2, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 5, TakenPlace = 4, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 6, TakenPlace = 3, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 6, TakenPlace = 8, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 6, TakenPlace = 8, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 7, TakenPlace = 9, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 7, TakenPlace = 10, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 7, TakenPlace = 7, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 8, TakenPlace = 6, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 8, TakenPlace = 4, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 8, TakenPlace = 9, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 9, TakenPlace = 8, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 9, TakenPlace = 5, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 9, TakenPlace = 10, Title = "Tournament 3"},
                new Tournament() { Country = "Germany", PlayerId = 10, TakenPlace = 10, Title = "Tournament 1"},
                new Tournament() { Country = "USA", PlayerId = 10, TakenPlace = 7, Title = "Tournament 2"},
                new Tournament() { Country = "Russia", PlayerId = 10, TakenPlace = 6, Title = "Tournament 3"},
            };
        }
    }
}