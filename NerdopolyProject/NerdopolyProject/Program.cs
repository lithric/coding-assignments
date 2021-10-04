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
            List<List<StoryObject>> test = (List<List<StoryObject>>)Story.GetStoriesBySection("Adv1",@"\^");
            Console.WriteLine(test[0][1].text);
            Console.ReadLine();
        }
    }
}
