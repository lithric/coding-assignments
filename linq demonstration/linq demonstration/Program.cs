using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq_demonstration
{
    class Program
    {
        public static int persist = 20;
        static IEnumerable<string> Pins() // 3
        {
            yield return "rare";
            yield return "marb";
            yield return "well";
        }
        static IEnumerable<string> Ranks() // 13
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
        static IEnumerable<string> Suits() // 4
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }
        public static List<(string, string, string, string)> Manifest(string name) // creates the list of cards with the deck name and returns it as a list of tuples
        {
            List<(string, string, string, string)> returnValue = new List<(string, string, string, string)>();
            var deck = from p in Pins()
                       from s in Suits()
                       from r in Ranks()
                       select new { Pin = p, Rank = r, Suit = s };
            foreach (var card in deck)
            {
                returnValue.Add((card.Pin, card.Rank, card.Suit,name));
            }
            return returnValue;
        }
        public static void DataUpdate(ref List<Func<int, (string, string, string, string)>> data, string name) {
            Func<int, (string, string, string, string)> newData = (int x) => { return Manifest(name)[x]; };
            data.Add(newData);
        }
        static void Main(string[] args)
        {
            // list that executes function
                List<Func<int>> bob = CreateList();
                Console.WriteLine(bob[0]());
                Console.WriteLine(bob[0]());
            // list that executes function

            //the card company problem
            var database = new Dictionary<string, List<(string, string, string, string)>>();
            for (int i=0; i<100; i++) // 156,000 elements in database
            {
                database.Add(i.ToString(),Manifest(i.ToString()));
            }
            // the goal is to reduce the numbers in database while keeping the same data available
            // data[0](0).item1
            // data[deck number](card number).cardInfo
            delegate var complexData = (from d in database group d.Value by d.Key).ToDictionary(p => p);
            Console.WriteLine(complexData);
            List < Func<int, (string, string, string, string)> > data = (from d in database select (Func<int, (string, string, string, string)>)((int x) => { return Manifest(d.Key)[x]; })).ToList();
            // 100x less data stored in the object "data"
            DataUpdate(ref data, "120");

            //the card company problem
            Console.ReadLine();
        }
        public static int ListFunc()
        {
            persist++;
            return persist;
        }
        public static List<Func<int>> CreateList()
        {
            List<Func<int>> bob = new List<Func<int>> { ListFunc };
            return bob;
        }
    }
}
