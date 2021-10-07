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
            List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                    App.DrawRow(0,Console.WindowWidth/2, i, ConsoleColor.Blue);
            }
            App.SetPixel(20 * 2, 30, ConsoleColor.Red);
            App.DrawRect(50, 20, 5, 20,ConsoleColor.DarkGray);
            Console.ReadLine();
        }
    }
}
