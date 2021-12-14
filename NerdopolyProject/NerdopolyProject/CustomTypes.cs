using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Text.RegularExpressions;
using Pastel;

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

public static class StringExtensions
{
    public static string Right(this string s,int count)
    {
        return s + $"\x1b[{count}C";
    }
    public static string Column(this string s, int count)
    {
        return s + $"\x1b[{count}G";
    }
}

public class App
{
    [Flags]
    private enum ConsoleModes : uint
    {
        ENABLE_INSERT_MODE = 0x0020
    }
    public static IDictionary<string,List<List<(string color, string text)>>> PixelMap = new Dictionary<string, List<List<(string color, string text)>>>() { };
    public static string DrawMode = "METRIC";
    public static string DefaultColor = "555555";
    public static Func<string, int> Win = (string dir) => {
        return dir == "x" ? Console.WindowWidth : dir == "px" ? Console.WindowWidth/2: Console.WindowHeight; 
    }; 
    public static IEnumerable<IEnumerable<T>> Compress<T>(IEnumerable<T> obj,int stretch = 1)
    {
        List<IEnumerable<T>> returnValue = new List<IEnumerable<T>>();
        int rep = 1;
        var lastElm = obj.Aggregate((acc,next) => 
        {
            rep++;
            if (Equals(acc,next))
            {
                return next;
            }
            else //(acc.color != next.color)
            {
                returnValue.Add(Enumerable.Repeat(acc,rep*stretch));
                rep = 0;
                return next;
            }
        });
        returnValue.Add(Enumerable.Repeat(lastElm,rep*stretch));
        return returnValue.AsEnumerable();
    }
    private static List<string> MapWork(List<List<(string color,string text)>> map)
    {
        string invisStr = "";
        List<string> data = Enumerable.Repeat("  ",map.Count).ToList();
        Parallel.For(0,map.Count, (i) =>
        {
            string down = "";
            var (color, text) = map[i].Aggregate((acc, next) =>
            {
                if (acc.color == next.color)
                {
                    return (next.color, acc.text + next.text);
                }
                else //(acc.color != next.color)
                {
                    down += acc.color != invisStr ? acc.text.PastelBg(acc.color):"".Right(acc.text.Length);
                    return (next.color, next.text);
                }
            });
            down += color != invisStr ? text.PastelBg(color):"".Right(text.Length);
            data[i] = down;
        });
        return data;
    }
    public static string Pixel(int x,int y,string map = "SCREEN")
    {
        return PixelMap[map][y][x].color;
    }
    public static void DrawPixelMap(string map = "SCREEN",bool write = true, bool preload = false)
    {
        var mapString = MapWork(PixelMap[map]);
        Console.SetCursorPosition(0, 0);
        if (write && !preload)
        {
            Console.Write(string.Concat(from str in mapString select '\n' + str).Substring(1));
        }
    } // get some text or text command that skips characters instead of replacing them
    public static void Write(string text, int x, int y, string map = "SCREEN",string color = null,bool write = true, bool preload = false)
    {
        string[] bits = text.Split('\n');
        int i = y;
        color = color == null ? DefaultColor : color;
        foreach (string line in bits)
        {
            int lineRange = line.Length / (line.Length < Win("px") ? 2:Win("px"));
            if (i >= Win("py") || x >= Win("px"))
            {
                break;
            }
            PixelMap[map][i].RemoveRange(x, lineRange);
            PixelMap[map][i].InsertRange(x, Enumerable.Repeat((color, "  "), lineRange));
            if (write || preload)
            {
                PixelMap["SCREEN"][i].RemoveRange(x, lineRange);
                PixelMap["SCREEN"][i].InsertRange(x, Enumerable.Repeat((color, "  "), lineRange));
            };
            if (write && !preload)
            {
                Console.SetCursorPosition(x * 2, i);
                Console.Write(line.PastelBg(color));
            }
            i++;
        }
    }
    public static void CreatePixelMap(string name, string color = "555555")
    {
        int mapWidth = Console.WindowWidth / 2;
        int mapHeight = Console.WindowHeight;
        if (!PixelMap.ContainsKey("SCREEN"))
        {
            PixelMap.Add("SCREEN", new List<List<(string color, string text)>>());
            for (int i = 0; i < mapHeight; i++)
            {
                PixelMap["SCREEN"].Add(new List<(string color, string text)>());
                for (int j = 0; j < mapWidth; j++)
                {
                    PixelMap["SCREEN"][i].Add((DefaultColor, "  "));
                }
            }
        }
        if (name != "SCREEN")
        {
            PixelMap.Add(name, new List<List<(string color, string text)>>());
            for (int i = 0; i < mapHeight; i++)
            {
                PixelMap[name].Add(new List<(string color, string text)>());
                for (int j = 0; j < mapWidth; j++)
                {
                    PixelMap[name][i].Add((color, "  "));
                }
            }
        }
    }

    public static void DrawPixel((int x,int y) pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        Write("  ",pos.x,pos.y, map: map,write: write,color: color,preload: preload);
    }
    public static void DrawRow(int pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        int x = Win("px");
        Write(new string(' ', x * 2), 0, pos, map: map,write: write,color: color,preload: preload);
    }
    public static void DrawRow((int row, int length) pos, string color = null, string map = "SCREEN",bool write = true, bool preload = false)
    {
        int x = Win("px");
        int endX = Math.Min(pos.length, x);
        Write(new string(' ', endX * 2), 0, pos.row, map: map,write: write,color: color,preload: preload);
    }
    public static void DrawRow((int row, int x1, int x2) pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        int x = Win("px");
        int endX = Math.Min(pos.x2, x-pos.x1);
        Write(new string(' ', endX * 2), pos.x1, pos.row, map: map,write:write, color: color,preload: preload);
    }
    public static void DrawColumn(int pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        Write(string.Concat(Enumerable.Repeat("  \n", Win("py"))), pos, 0, map: map, write: write, color: color,preload: preload);
    }
    public static void DrawColumn((int column, int length) pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        Write(string.Concat(Enumerable.Repeat("  \n", pos.length)), pos.column,0, map: map, write: write, color: color,preload: preload);
    }
    public static void DrawColumn((int column, int y1, int y2) pos, string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        Write(string.Concat(Enumerable.Repeat("  \n", pos.y2)), pos.column,pos.y1, map: map, write: write, color: color,preload: preload);
    }
    public static void DrawRect((int x1, int y1, int x2, int y2) pos,string color = null, string map = "SCREEN", bool write = true, bool preload = false)
    {
        int x = Win("px");
        int y = Console.WindowHeight;
        int endX = Math.Min(pos.x2, x - pos.x1);
        int endY = Math.Min(pos.y2, y - pos.y1);
        if (endX >= Win("px"))
        {
            Write(string.Concat(Enumerable.Repeat(new string(' ', endX * 2), endY)), pos.x1, pos.y1, map: map, write: write, color: color,preload: preload);
        }
        else
        {
            Write(string.Concat(Enumerable.Repeat(new string(' ', endX * 2) + "\n", endY)), pos.x1, pos.y1, map: map, write: write, color: color,preload: preload);
        }
    }
}

