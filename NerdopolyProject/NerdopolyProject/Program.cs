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
        public static List<int> charPos = new List<int> { 125, 30 };
        public static void UpdatePos()
        {
            App.DrawPixel(charPos[0], charPos[1], ConsoleColor.Green);
        }
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            App.DrawRect(0,0,250,60);
            App.DrawPixel(125, 30, ConsoleColor.Green);
            int i = 0;
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        charPos[1] -= 1;
                        break;
                    case ConsoleKey.A:
                        charPos[0] -= 2;
                        break;
                    case ConsoleKey.S:
                        charPos[1] += 1;
                        break;
                    case ConsoleKey.D:
                        charPos[0] += 2;
                        break;
                }
                UpdatePos();
                i++;
            }
            Console.ReadLine();
        }
    }
}
