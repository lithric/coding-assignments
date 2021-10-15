using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;

public struct Literal
{
    public int integ;
    public string str;
    public Type type;
    public Literal(object val)
    {
        string aura = val.GetType().ToString();
        switch (aura)
        {
            case "System.Int32":
                integ = (int)val;
                str = val.ToString();
                break;
            case "System.String":
                integ = Int32.TryParse((string)val, out int temp) ? temp : 0;
                str = (string)val;
                break;
            default:
                integ = 0;
                str = "";
                break;
        }
        type = val.GetType();
    }

    public static implicit operator Literal(int x)
    {
        return new Literal(x);
    }
    public static implicit operator Literal(string x)
    {
        return new Literal(x);
    }
    public static implicit operator Literal(List<object> x)
    {
        Literal returnValue = "";
        try
        {
            object test = x[0];
        } catch (Exception)
        {
            return 0;
        }
        object NestTest(List<object> y)
        {
            List<object> tempReturn = new List<object>();
            try
            {
                tempReturn = (List<object>)y[0];
            } catch(Exception)
            {
                returnValue = (int)y[0];
                return tempReturn;
            }
            NestTest(tempReturn);
            return tempReturn;
        }
        NestTest(x);
        return returnValue;
    }
    public static implicit operator bool(Literal x)
    {
        if (x.integ != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Literal operator ==(Literal x, object y)
    {
        string typeValue = y.GetType().ToString();
        switch (typeValue)
        {
            case "System.Int32":
                if (x.integ == (int)y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.Int32[]":
                if (x.integ == ((int[])y)[0] && ((int[])y).Length == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.String":
                if (x.str == (string)y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.String[]":
                if (x.str == ((string[])y)[0] && ((string[])y).Length == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }
    public static Literal operator !=(Literal x, object y)
    {
        string typeValue = y.GetType().ToString();
        switch (typeValue)
        {
            case "System.Int32":
                if (x.integ != (int)y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.Int32[]":
                if (x.integ != ((int[])y)[0] && ((int[])y).Length == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.String":
                if (x.str != (string)y)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case "System.String[]":
                if (x.str != ((string[])y)[0] && ((string[])y).Length == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            default:
                return 1;
        }
    }
}
public class Calc
{
    public static void Prompt(object x)
    {
        Console.WriteLine(x);
    }
    public static void Prompt(object x, ref object y)
    {
        Console.WriteLine(x);
        y = Console.ReadLine();
    }
    public static int Depth(List<object> x)
    {
        int depth = 0;
        try
        {
            object test = x[0];
            depth++;
        } catch(Exception)
        {
            return 0;
        }
        void NestTest(List<object> xT)
        {
            List<object> test = new List<object>();
            try
            {
                test = (List<object>)xT[0];
                depth++;
            } catch
            {
                return;
            }
            NestTest(test);
        }
        NestTest(x);
        return depth;
    }
}
public class App
{
    private const int STD_OUTPUT_HANDLE = -11;
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
    private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;
    public static List<List<List<ConsoleColor>>> PixelMap = new List<List<List<ConsoleColor>>>() { };
    public static string DrawMode = "METRIC";
    public static ConsoleColor DefaultColor = ConsoleColor.Gray;

    public static void CreatePixelMap(ConsoleColor color = ConsoleColor.Black)
    {
        PixelMap.Add(new List<List<ConsoleColor>>());
        int map = PixelMap.Count-1;
        int mapWidth = Console.WindowWidth / 2;
        int mapHeight = Console.WindowHeight;
        for (int i=0; i<mapWidth; i++)
        {
            PixelMap[map].Add(new List<ConsoleColor>());
            for (int j=0; j<mapHeight; j++)
            {
                PixelMap[map][i].Add(color);
            }
        }
    }

    public static void DrawPixel(int x, int y, int map = 0, ConsoleColor color = ConsoleColor.Gray, bool write = true)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        if (write)
        {
            PixelMap[map][x][y] = color;
        }
        Console.BackgroundColor = color;
        Console.Write("  ");
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0, 0, 2, 1, x*2, y);
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawRow(int row, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth/2;
        foreach (List<ConsoleColor> pixel in PixelMap[map])
        {
            pixel[row] = color;
        }
        Console.BackgroundColor = color;
        Console.Write(new string(' ',x*2));
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0, 0, x*2, 1, 0, row);
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawRow(int row, int length, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int endX = Math.Min(length, x);
        for (int i = 0; i < endX; i++)
        {
            PixelMap[map][i][row] = color;
        }
        Console.BackgroundColor = color;
        Console.Write(new string(' ',endX*2));
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0, 0, x * 2, 1, 0, row);
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawRow(int row, int x1, int x2, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int endX = Math.Min(Math.Max(x2-x1,0),x);
        if (DrawMode == "METRIC")
        {
            endX = Math.Min(x2, x-x1);
        }
        for (int i = x1; i < endX; i++)
        {
            PixelMap[map][i][row] = color;
        }
        Console.BackgroundColor = color;
        Console.Write(string.Concat(Enumerable.Repeat("  ",endX)));
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0, 0, endX*2, 1, x1*2, row);
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawColumn(int column, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int y = Console.WindowHeight;
        PixelMap[map][column] = Enumerable.Repeat(color,y).ToList();
        Console.BackgroundColor = color;
        Console.Write(string.Concat(Enumerable.Repeat("  \n", y)));
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0, 0, 2, y, column, 0);
        Console.SetCursorPosition(0, 0);
    }
    public static void DrawColumn(int column, int length, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int y = Console.WindowHeight;
        int endY = Math.Min(length, y);
        PixelMap[map][column] = Enumerable.Repeat(color, endY).ToList();
        for (int i = 0; i<endY; i++)
        {
            Console.SetCursorPosition(column * 2, i);
            Console.Write("  ");
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawColumn(int column, int y1, int y2, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int y = Console.WindowHeight;
        int endY = Math.Min(y2, y);
        if (DrawMode == "METRIC")
        {
            endY = Math.Min(y2+y1, y-y1);
        }
        PixelMap[map][column].InsertRange(y1,Enumerable.Repeat(color,endY-y1));
        for (int i = y1; i < endY; i++)
        {
            Console.SetCursorPosition(column*2, i);
            Console.Write("  ");
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRect(int x1, int y1, int x2, int y2, int map = 0, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int y = Console.WindowHeight;
        int endX = Math.Min(Math.Max(x2 - x1, 0), x);
        int endY = Math.Min(y2, y);
        if (DrawMode == "METRIC")
        {
            endX = Math.Min(x2, x - x1);
            endY = Math.Min(y2 + y1, y - y1);
        }
        for (int i = x1; i < endX; i++)
        {
            PixelMap[map][i].InsertRange(y1,Enumerable.Repeat(color,endY-y1));
        }
        Console.BackgroundColor = color;
        Console.Write(string.Concat(Enumerable.Repeat(new string(' ',endX*2)+"\n",endY)));
        Console.BackgroundColor = DefaultColor;
        Console.MoveBufferArea(0,0,endX,endY,x1,y1);
        //for (int i = y1; i < endY; i++)
        //{
        //    Console.SetCursorPosition(x1*2, i);
            //Console.Write(string.Concat(Enumerable.Repeat("  ", endX)));
        //    Console.Write(new string(' ', endX * 2));
        //}
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
}

