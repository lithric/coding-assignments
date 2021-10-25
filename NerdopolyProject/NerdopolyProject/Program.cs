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
            ConsoleColor underCol = App.Pixel(charPos[0],charPos[1],map:"Start");
            ConsoleColor coverCol = App.Pixel(newX, newY, map: "Start");
            if (coverCol == ConsoleColor.Red && check)
            {
                Console.BackgroundColor = App.DefaultColor;
                Task.Run(async delegate
                {
                    await Task.Delay(100);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Game Over");
                });
                Task.Run(async delegate
                {
                    await Task.Delay(1000);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("                              ");
                });
                UpdatePos(250 / 4, 60 / 2,false);
                return;
            }
            App.DrawPixel(pos: (charPos[0], charPos[1]),color: underCol,map: "Char");
            App.DrawPixel(pos: (newX, newY), color: ConsoleColor.Green,map: "Char");
            charPos[0] = newX;
            charPos[1] = newY;
        }
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            Console.CursorVisible = false;
            App.CreatePixelMap("Start");
            App.CreatePixelMap("Char");
            App.DrawRect(pos: (0, 0, 250/2, 60) ,map: "Start");
            App.DrawColumn(pos: (20/2, 10, 40) ,color: ConsoleColor.Red,map: "Start");
            App.DrawPixel(pos: (125/2, 30) ,color: ConsoleColor.Green,map: "Char");
            App.DrawPixel(pos: (20, 50) ,color: ConsoleColor.Green,map:"Start");
            App.DrawRow(pos: (10,20) ,color: ConsoleColor.Red,map:"Start");
            //App.DrawRect(pos: (1, 10, 20, 20), map: 0,color: ConsoleColor.Blue);
            //App.DrawRow(25, 60, 0, ConsoleColor.Yellow);
            //App.DrawRow(30, 30, 60, 0, ConsoleColor.Blue);
            //App.DrawColumn(40,0,ConsoleColor.Magenta);
            //App.DrawColumn(60, 30, 0, ConsoleColor.Cyan);
            //App.DrawColumn(90, 0, 1, 0, ConsoleColor.DarkCyan);
            while (true)
            {
                int charX = charPos[0];
                int charY = charPos[1];
                switch(Console.ReadKey(false).Key)
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
