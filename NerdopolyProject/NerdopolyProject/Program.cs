using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace NerdopolyProject
{
    class Program
    {
        public static List<int> charPos = new List<int> { 125/2, 30 };
        public static void UpdatePos(int newX,int newY, bool check = true)
        {
            newX = Math.Max(Math.Min(newX, Console.WindowWidth/2 - 1), 0);
            newY = Math.Max(Math.Min(newY, Console.WindowHeight - 1), 0);
            ConsoleColor underCol = App.PixelMap[0][charPos[0]][charPos[1]];
            ConsoleColor coverCol = App.PixelMap[0][newX][newY];
            if (coverCol == ConsoleColor.Red && check)
            {
                Console.Write("Game Over");
                UpdatePos(250 / 4, 60 / 2,false);
                return;
            }
            App.DrawPixel(charPos[0], charPos[1], 0, underCol, false);
            App.DrawPixel(newX, newY, 0, ConsoleColor.Green, false);
            charPos[0] = newX;
            charPos[1] = newY;
        }
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            App.CreatePixelMap();
            App.DrawRect(0, 0, 250/2, 60, 0);
            App.DrawColumn(20/2, 10, 40, 0, ConsoleColor.Red);
            App.DrawPixel(125/2, 30, 0, ConsoleColor.Green, false);
            //App.DrawPixel(20, 50, 0, ConsoleColor.Green);
            //App.DrawRow(10,0,ConsoleColor.Red);
            //App.DrawRow(25, 60, 0, ConsoleColor.Yellow);
            //App.DrawRow(30, 30, 60, 0, ConsoleColor.Blue);
            //App.DrawColumn(40,0,ConsoleColor.Magenta);
            //App.DrawColumn(60, 30, 0, ConsoleColor.Cyan);
            //App.DrawColumn(90, 0, 1, 0, ConsoleColor.DarkCyan);
            while (true)
            {
                int charX = charPos[0];
                int charY = charPos[1];
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        charY -= 1;
                    break;
                    case ConsoleKey.A:
                        charX -= 1;
                    break;
                    case ConsoleKey.S:
                        charY += 1;
                    break;
                    case ConsoleKey.D:
                        charX += 1;
                    break;
                }
                UpdatePos(charX,charY);
            }
            Console.ReadLine();
        }
    }
}
