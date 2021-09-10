using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ans;
            object resp;
            bool[] anss = { };
            Console.Write("What is 2+2?");
            resp = int.Parse(Console.ReadLine());
            if ((int)resp == 4)
            {
                Console.WriteLine("Good");
                anss.Append(true);
            }
            else
            {
                anss.Append(false);
            }
            Console.ReadKey();
            Console.Clear();
            // nitivease is a made up word, but it should exist. Means (easy or common concept)
            // nit-tiv-veese
            //nitiveasient should mean (most intuitive or common concept)
            //nit-tiv-veese-ian-t
            Console.WriteLine("What is the nitiveasient(most intuitive or common concept) texture of a ball?");
            resp = Console.ReadLine();
            ans = (string)resp == "rubber";
            if (ans)
            {
                Console.WriteLine("Good");
            } else
            {
                Console.WriteLine("Nope");
            }
            anss.Append(ans);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("What is the nitiveasient(most intuitive or common concept) number between 1 and 10?");
            resp = Console.ReadLine();
            ans = (string)resp == "7" ? true : false;
            anss.Append(ans);
            Console.WriteLine(ans ? "Good":"Goof");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"question 1 correct?: {anss[0]}" +
                $"question 2 correct?: {anss[1]}" +
                $"question 3 correct?: {anss[2]}" +
                $"question 1 and 2 correct?: {anss[0] && anss[1]}" +
                $"question 1 or 3 correct?: {anss[0] || anss[2]}" +
                $"question 2 correct and not 3?: {anss[1] && !anss[2]}");
        }
    }
}
