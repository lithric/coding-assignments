using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMakerproject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Money Maker!");
            Console.Write("Enter amount to convert to coins: ");
            double amount = double.Parse(Console.ReadLine());
            Console.WriteLine($"{amount} cents is equal to...");
            int Gcoin = (int)Math.Floor(amount / 10);
            int Scoin = (int)Math.Floor((amount % 10) / 5);
            int Bcoin = (int)Math.Floor(amount % 5);
            Console.WriteLine($@"Gold coins: {Gcoin}
Silver coins: {Scoin}
Bronze coins: {Bcoin}");
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
