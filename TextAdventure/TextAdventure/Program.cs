using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex rx = new Regex(@"");
            StreamReader sr = new StreamReader("./Story.txt");
            string line = sr.ReadToEnd();
            // file.ReadAllText(Regex);
            Console.WriteLine(line);
            sr.Close();
            Console.ReadLine();
        }
    }
}
