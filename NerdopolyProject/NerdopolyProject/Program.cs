using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NerdopolyProject
{
    class Program
    {
        public static List<int> charPos = new List<int> { 125, 30 };
        public static void UpdatePos(int newX,int newY)
        {
            ConsoleColor underCol = App.PixelMap[charPos[0]/2][charPos[1]];
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
            App.DrawRect(0,0,250,60);
            App.DrawColumn(20,10,40,ConsoleColor.Yellow);
            App.DrawPixel(125, 30, ConsoleColor.Green, false);
            int i = 0;
            while (true)
            {
                int xPos = charPos[0];
                int yPos = charPos[1];
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        yPos -= 1;
                        break;
                    case ConsoleKey.A:
                        xPos -= 2;
                        break;
                    case ConsoleKey.S:
                        yPos += 1;
                        break;
                    case ConsoleKey.D:
                        xPos += 2;
                        break;
                }
                UpdatePos(xPos,yPos);
                i++;
            }
            Console.ReadLine();
        }
    }
}
