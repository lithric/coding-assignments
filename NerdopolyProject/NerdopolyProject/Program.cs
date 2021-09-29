using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NerdopolyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> test = (List<object>)Story.GetStoriesBySection("Intro",@"\*");
            object y = new object();
            int a = Calc.ApplyNest(test);
            Console.ReadLine();
        }
    }
}
