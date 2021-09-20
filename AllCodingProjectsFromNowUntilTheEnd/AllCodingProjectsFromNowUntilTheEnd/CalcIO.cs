using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class CalcIO
    {
        public static Literal Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            Literal x = Console.ReadLine();
            return x;
        }
    }
}
