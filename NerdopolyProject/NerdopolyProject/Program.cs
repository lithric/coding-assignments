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
            List<List<List<StoryObject>>> test = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Tens",@"\#");
            Console.WriteLine(test[0][0][0].text);
            Console.ReadLine();
        }
    }
}
