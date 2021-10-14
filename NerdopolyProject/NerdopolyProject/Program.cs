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
            ConsoleColor underCol = App.PixelMap[charPos[0]][charPos[1]];
            ConsoleColor coverCol = App.PixelMap[newX][newY];
            if (coverCol == ConsoleColor.Red && check)
            {
                Console.Write("Game Over");
                UpdatePos(250 / 4, 60 / 2,false);
                return;
            }
            App.DrawPixel(charPos[0], charPos[1], underCol, false);
            App.DrawPixel(newX, newY, ConsoleColor.Green, false);
            charPos[0] = newX;
            charPos[1] = newY;
        }
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            Console.SetWindowSize(250, 60);
            App.CreatePixelMap();
            App.DrawRect(0, 0, 250/2, 60);
            App.DrawColumn(20/2, 10, 40, ConsoleColor.Yellow);
            App.DrawPixel(125/2, 30, ConsoleColor.Green, false);
            Thread t1 = new Thread(new ThreadStart(() => {
                int jX = 0;
                int jY = 0;
                while (true)
                {
                    Thread.Sleep(100);
                    int untilX = Math.Max(Math.Min((jX - charPos[0]) / Math.Max(jY - charPos[1],1),1),-1);
                    int untilY = Math.Max(Math.Min((jY - charPos[1]) / Math.Max(jX - charPos[0], 1), 1), -1);
                    jX -= untilX;
                    jY -= untilY;
                    if (jX == charPos[0] && jY == charPos[1])
                    {
                        Console.Write("Game Over");
                        UpdatePos(250/4, 60/2,false);
                    }
                    App.DrawPixel(jX, jY, ConsoleColor.Red);
                }
            }));
            t1.Start();
            int i = 0;
            while (true)
            {
                int xPos = charPos[0];
                int yPos = charPos[1];
                ConsoleKey key = Console.ReadKey(true).Key;
                if (xPos == charPos[0] && yPos == charPos[1])
                {
                    switch (key)
                    {
                        case ConsoleKey.W:
                            yPos -= 1;
                            break;
                        case ConsoleKey.A:
                            xPos -= 1;
                            break;
                        case ConsoleKey.S:
                            yPos += 1;
                            break;
                        case ConsoleKey.D:
                            xPos += 1;
                            break;
                    }
                    UpdatePos(xPos, yPos);
                }
                else
                {
                    xPos = charPos[0];
                    yPos = charPos[1];
                    switch (key)
                    {
                        case ConsoleKey.W:
                            yPos -= 1;
                            break;
                        case ConsoleKey.A:
                            xPos -= 1;
                            break;
                        case ConsoleKey.S:
                            yPos += 1;
                            break;
                        case ConsoleKey.D:
                            xPos += 1;
                            break;
                    }
                    UpdatePos(xPos, yPos);
                }
                i++;
            }
            Console.ReadLine();
        }
    }
}
