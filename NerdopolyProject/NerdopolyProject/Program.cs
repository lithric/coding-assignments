using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Pastel;
using System.Diagnostics;
namespace NerdopolyProject
{
    class Program
    {
        [Flags]
        private enum ConsoleModes : uint
        {
            ENABLE_INSERT_MODE = 0x0020
        }
        [DllImport("kernel32.dll")]
        private extern static IntPtr GetStdHandle(int nStdHandle);

        [StructLayout(LayoutKind.Sequential)]
        struct CHAR_INFO
        {
            public char AsciiChar;
            public short Attributes;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        

        [StructLayout(LayoutKind.Sequential)]
        struct COORD
        {
            public short X;
            public short Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CONSOLE_SCREEN_BUFFER_INFO
        {

            public COORD dwSize;
            public COORD dwCursorPosition;
            public short wAttributes;
            public SMALL_RECT srWindow;
            public COORD dwMaximumWindowSize;

        }
        [DllImport("kernel32.dll")]
        static extern bool ReadConsoleOutput(
            IntPtr hConsoleOutput,
            out CHAR_INFO lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            in SMALL_RECT lpReadRegion
        );
        

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleScreenBufferInfo(
            IntPtr hConsoleOutput,
            out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
        );
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleTextAttribute(
            IntPtr hConsoleOutput,
            short wAttributes
        );
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(
            IntPtr hConsoleHandle,
            short dwMode
        );
        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_ERROR_HANDLE = -12;

        public static List<int> charPos = new List<int> { 125/2, 30 };
        public static List<int> enemyPos = new List<int> { 120, 10 };
        public static void UpdatePos(int newX,int newY, bool check = true)
        {
            newX = Math.Max(Math.Min(newX, Console.WindowWidth/2 - 1), 0);
            newY = Math.Max(Math.Min(newY, Console.WindowHeight - 1), 0);
            string underCol = App.Pixel(charPos[0],charPos[1],map:"Start");
            string coverCol = App.Pixel(newX, newY, map:"Start");
            string deathCheck = App.Pixel(newX, newY, map:"Death");
            if (deathCheck == "#FF0000" && check)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Task.Run(async delegate
                {
                    await Task.Delay(100);
                    Console.ForegroundColor = ConsoleColor.Red;
                });
                Task.Run(async delegate
                {
                    await Task.Delay(1000);
                    Console.ForegroundColor = ConsoleColor.Red;
                });
                UpdatePos(250 / 4, 60 / 2,false);
                return;
            }
            App.DrawPixel(pos: (charPos[0], charPos[1]),color: underCol,map: "Char",preload: true);
            App.DrawPixel(pos: (newX, newY), color: "#00FF00",map: "Char",preload: true);
            App.DrawPixelMap("SCREEN");
            charPos[0] = newX;
            charPos[1] = newY;
        }
        public static void EnemyAction()
        {
            while (true)
            {
                string under = App.Pixel(enemyPos[0], enemyPos[1], map: "Start");
                App.DrawPixel(pos: (enemyPos[0], enemyPos[1]), color: "#000000", map: "Death", write: false);
                App.DrawPixel(pos: (enemyPos[0], enemyPos[1]), color: under, map: "Start");
                if (enemyPos[0] > charPos[0])
                {
                    enemyPos[0] -= 1;
                }
                else
                {
                    enemyPos[0] += 1;
                }
                if (enemyPos[1] > charPos[1])
                {
                    enemyPos[1] -= 1;
                }
                else
                {
                    enemyPos[1] += 1;
                }
                App.DrawPixel(pos: (enemyPos[0], enemyPos[1]), color: "#FF0000", map: "Death");
                Thread.Sleep(400);
            }
        }
        
        static void Main(string[] args)
        {
            //List<List<List<StoryObject>>> game1Story = (List<List<List<StoryObject>>>)Story.GetStoriesBySection("Game1",@"\#");
            IntPtr stdout = GetStdHandle(STD_OUTPUT_HANDLE);
            SetConsoleMode(stdout, 0x0004); // ENABLE_VIRTUAL_TERMINAL_PROCESSING
            //SetConsoleMode(stdout, 0x0008); // DISABLE_NEWLINE_AUTORETURN
            Console.SetWindowSize(250, 60);
            Console.CursorVisible = false;
            App.CreatePixelMap("Start");
            //App.DrawPixel(pos: (125/2, 15), color: "#00FF00", map: "Start", write: false);
            //App.DrawPixelMap("Start");
            App.CreatePixelMap("Char");
            App.CreatePixelMap("Death", "#000000");
            App.DrawRect(pos: (0, 0, 250/2, 60) ,map: "Start",write:false);
            App.DrawColumn(pos: (30/2, 10, 40) ,color: "FF0000",map: "Start", preload: true);
            App.DrawPixel(pos: (125/2, 30) ,color: "00FF00",map: "Char",preload: true);
            App.DrawPixel(pos: (20, 50) ,color: "#00FF00", map:"Start",preload:true);
            App.DrawRow(pos: (10, 10) ,color: "#FF0000", map:"Death",write: false);
            App.DrawRow(pos: (10, 10), color: "#FFFFFF", map: "Start",preload:true);
            //App.DrawPixelMap("SCREEN");
            Console.WriteLine("\u001b[31mok[0m");
            CONSOLE_SCREEN_BUFFER_INFO ok;
            GetConsoleScreenBufferInfo(stdout, out ok);
            Console.Write(ok.srWindow.Right);
            /*
            CHAR_INFO pass1;
            SMALL_RECT pass2;
            ReadConsoleOutput(stdout, out pass1, new COORD {X=2,Y=1}, new COORD {X=0,Y=0}, new SMALL_RECT {Left=2,Right=3,Top=0,Bottom=1});
            Console.Write(pass1.AsciiChar);
            */
            //App.DrawColumn(pos: (20 / 2, 10, 40), color: ConsoleColor.Red, map: "Start");
            //App.DrawRect(pos: (1, 10, 20, 20), map: 0,color: ConsoleColor.Blue);
            //App.DrawRow(25, 60, 0, ConsoleColor.Yellow);
            //App.DrawRow(30, 30, 60, 0, ConsoleColor.Blue);
            //App.DrawColumn(40,0,ConsoleColor.Magenta);
            //App.DrawColumn(60, 30, 0, ConsoleColor.Cyan);
            //App.DrawColumn(90, 0, 1, 0, ConsoleColor.DarkCyan);
            /*
            Thread enemy = new Thread(EnemyAction);
            enemy.Start();
            */
            while (true)
            {
                int charX = charPos[0];
                int charY = charPos[1];
                switch (Console.ReadKey(true).Key)
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
                UpdatePos(charX, charY);
            }
            Console.ReadLine();
        }
    }
}
