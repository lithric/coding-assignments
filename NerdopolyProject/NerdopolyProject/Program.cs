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
            List<List<List<object>>> test = (List<List<List<object>>>)Story.GetStoriesBySection("Tens",@"\#");
            Console.WriteLine(test);
            Console.ReadLine();
        }
    }
}
