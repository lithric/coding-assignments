using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NerdopolyProject
{
    class Program
    {
        public static List<List<ConsoleColor>> PixelMap = new List<List<ConsoleColor>>() { };
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            App.DrawRow(10, ConsoleColor.Red);
            App.DrawColumn(4, 0, 10, ConsoleColor.Blue);
            Console.ReadLine();
        }
    }
}
