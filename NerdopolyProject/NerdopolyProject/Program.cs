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
            Func<int, List<object>> test = (Func<int, List<object>>)Story.GetStoriesByPage("Intro");
            Console.WriteLine(((Func<int,string>)test(0)[0])(0));
            Console.ReadLine();
        }
    }
}
