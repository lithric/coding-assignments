using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Recursion
    {
        public static int x = 0;
        public static void CharacterSelect()
        {
            Console.WriteLine("Please choose a number");
            string num = Console.ReadLine();
            Console.WriteLine($"Welcome to the game {num}");

            if (num == "5")
            {
                Console.WriteLine("Please enter");
                string charName = Console.ReadLine();
                Console.WriteLine($"Your name is {charName}");
            }

            while (x < 3)
            {
                x++;
            }
        }
    }
}
