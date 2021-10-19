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
        static IEnumerable<string> Pins()
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
        public static List<(string, string, string, string)> Manifest(string name)
        {
            List<(string, string, string, string)> returnValue = new List<(string, string, string, string)>();
            var deck = from p in Pins()
                       from s in Suits()
                       from r in Ranks()
                       select new { Pin = p, Rank = r, Suit = s };
            foreach (var card in deck)
            {
                returnValue.Add((card.Pin, card.Rank, card.Suit, name));
            }
            return returnValue;
        }
        static void Main(string[] args)
        {
            List<(string, string, string, string)> deck = Manifest("delta");
            List<int> bob = CreateList();
            Console.WriteLine(bob[0]);
            Console.WriteLine(bob[0]);
            Console.ReadLine();
        }
        public static List<object> CreateList()
        {
            List<Delegate> bob = new List<Delegate> { ListFunc };
            return bob;
        }
        public delegate int ListFunc();
    }
}
