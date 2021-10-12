﻿using System;
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
    public static string DrawMode = "METRIC";
    public static ConsoleColor DefaultColor = ConsoleColor.Gray;

    public static void DrawPixel(int x, int y, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        Console.SetCursorPosition((x/2)*2, y);
        Console.Write("  ");
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRow(int row, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int x = Console.WindowWidth/2;
        Console.SetCursorPosition(0, row);
        Console.Write(string.Concat(Enumerable.Repeat("  ", x)));
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRow(int row, int length, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int x = Console.WindowWidth / 2;
        Console.SetCursorPosition(0, row);
        Console.Write(string.Concat(Enumerable.Repeat("  ",Math.Min(length/2,x))));
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRow(int row, int x1, int x2, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int x = Console.WindowWidth / 2;
        int end = Math.Min(Math.Max(x2/2-x1/2,0),x);
        if (DrawMode == "METRIC")
        {
            end = Math.Min(x2/2, x-x1/2);
        }
        Console.SetCursorPosition((x1/2)*2, row);
        Console.Write(string.Concat(Enumerable.Repeat("  ",end)));
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawColumn(int column, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int y = Console.WindowHeight;
        for (int i = 0; i < y; i++)
        {
            Console.SetCursorPosition((column / 2) * 2, i);
            Console.Write("  ");
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawColumn(int column, int length, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int y = Console.WindowHeight;
        for (int i = 0; i<Math.Min(length,y); i++)
        {
            Console.SetCursorPosition((column / 2) * 2, i);
            Console.Write("  ");
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawColumn(int column, int y1, int y2, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int y = Console.WindowHeight;
        int end = Math.Min(y2, y);
        if (DrawMode == "METRIC")
        {
            end = Math.Min(y2+y1, y-y1);
        }
        for (int i = y1; i < end; i++)
        {
            Console.SetCursorPosition((column/2)*2, i);
            Console.Write("  ");
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRect(int x1, int y1, int x2, int y2, ConsoleColor color = ConsoleColor.Gray)
    {
        if (color == ConsoleColor.Gray)
        {
            color = DefaultColor;
        }
        Console.BackgroundColor = color;
        int x = Console.WindowWidth / 2;
        int y = Console.WindowHeight;
        int endX = Math.Min(Math.Max(x2/2 - x1/2,0), x);
        int endY = Math.Min(y2, y);
        if (DrawMode == "METRIC")
        {
            endX = Math.Min(x2/2, x-x1/2);
            endY = Math.Min(y2+y1, y-y1);
        }
        for (int i = y1; i < endY; i++)
        {
            Console.SetCursorPosition((x1/2)*2, i);
            Console.Write(string.Concat(Enumerable.Repeat("  ", endX)));
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
}

