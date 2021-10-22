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
    public static Func<string, int> Win = (string dir) => {
        return dir == "x" ? Console.WindowWidth : dir == "px" ? Console.WindowWidth/2: Console.WindowHeight; 
    }; 

    public static void Write(string text,int width,int height,int x, int y, int map = 0,ConsoleColor? color = null)
    {
        Console.BackgroundColor = color == null ? Console.BackgroundColor:(ConsoleColor)color;
        if (text.Contains("\n"))
        {
            string[] bits = text.Split('\n');

            int i = y;
            foreach (string line in bits)
            {
                Console.SetCursorPosition(x,i);
                Console.Write(line);
                i++;
            }
        }
        else
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        Console.BackgroundColor = DefaultColor;
        Console.SetCursorPosition(0, 0);
    }
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

    public static void DrawPixel((int x,int y) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0, bool write = true)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        if (write)
        {
            PixelMap[map][pos.x][pos.y] = color;
        }
        Console.BackgroundColor = color;
        Write("  ",2,1,pos.x*2,pos.y, map: map);
    }
    public static void DrawRow(int pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Win("px");
        foreach (List<ConsoleColor> pixel in PixelMap[map])
        {
            pixel[pos] = color;
        }
        Console.BackgroundColor = color;
        Write(new string(' ', x * 2), x * 2, 1, 0, pos, map: map);
    }
    public static void DrawRow((int row, int length) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int endX = Math.Min(pos.length, x);
        for (int i = 0; i < endX; i++)
        {
            PixelMap[map][i][pos.row] = color;
        }
        Console.BackgroundColor = color;
        Write(new string(' ', endX * 2),endX*2,1,0,pos.row, map: map);
    }
    public static void DrawRow((int row, int x1, int x2) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int endX = Math.Min(Math.Max(pos.x2-pos.x1,0),x);
        if (DrawMode == "METRIC")
        {
            endX = Math.Min(pos.x2, x-pos.x1);
        }
        for (int i = pos.x1; i < endX; i++)
        {
            PixelMap[map][i][pos.row] = color;
        }
        Console.BackgroundColor = color;
        Write(new string(' ', endX * 2), endX * 2, 1, pos.x1 * 2, pos.row, map: map);
    }
    public static void DrawColumn(int pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int y = Console.WindowHeight;
        PixelMap[map][pos] = Enumerable.Repeat(color,y).ToList();
        Console.BackgroundColor = color;
        Write(string.Concat(Enumerable.Repeat("  \n", y)),2,y+Win("y"),pos*2,0, map: map);
    }
    public static void DrawColumn((int column, int length) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int y = Console.WindowHeight;
        int endY = Math.Min(pos.length, y);
        PixelMap[map][pos.column] = Enumerable.Repeat(color, endY).ToList();
        Console.BackgroundColor = color;
        Write(string.Concat(Enumerable.Repeat("  \n", endY)),2,endY,pos.column*2,0, map: map);
    }
    public static void DrawColumn((int column, int y1, int y2) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int y = Console.WindowHeight;
        int endY = Math.Min(pos.y2, y);
        if (DrawMode == "METRIC")
        {
            endY = Math.Min(pos.y2+pos.y1, y-pos.y1);
        }
        PixelMap[map][pos.column].InsertRange(pos.y1,Enumerable.Repeat(color,endY-pos.y1));
        Console.BackgroundColor = color;
        Write(string.Concat(Enumerable.Repeat("  \n", pos.y2)),2,endY,pos.column*2,pos.y1, map: map);
    }
    public static void DrawRect((int x1, int y1, int x2, int y2) pos, ConsoleColor color = ConsoleColor.Gray, int map = 0)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        int x = Console.WindowWidth / 2;
        int y = Console.WindowHeight;
        int endX = Math.Min(Math.Max(pos.x2 - pos.x1, 0), x);
        int endY = Math.Min(pos.y2, y);
        if (DrawMode == "METRIC")
        {
            endX = Math.Min(pos.x2, x - pos.x1);
            endY = Math.Min(pos.y2 + pos.y1, y - pos.y1);
        }
        for (int i = pos.x1; i < endX; i++)
        {
            PixelMap[map][i].InsertRange(pos.y1,Enumerable.Repeat(color,endY-pos.y1));
        }
        Console.BackgroundColor = color;
        if (endX >= Win("px"))
        {
            Write(string.Concat(Enumerable.Repeat(new string(' ', endX * 2), endY)), endX * 2, endY, pos.x1, pos.y1, map: map);
        }
        else
        {
            Write(string.Concat(Enumerable.Repeat(new string(' ', endX * 2) + "\n", endY)), endX * 2, endY, pos.x1, pos.y1, map: map);
        }
    }
}

