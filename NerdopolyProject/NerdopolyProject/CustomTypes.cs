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

    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    public static extern uint GetLastError();

    public static void DrawPixel(int x, int y,ConsoleColor color)
    {
        Console.BackgroundColor = color;
        Console.SetCursorPosition(x, y);
        Console.Write("  ");
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRow(int row, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        int x = Console.WindowWidth/2;
        Console.SetCursorPosition(0, row);
        Console.Write(string.Concat(Enumerable.Repeat("  ", x)));
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRow(int row, int x1, int x2, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        Console.SetCursorPosition(x1, row);
        Console.Write(string.Concat(Enumerable.Repeat("  ", x2)));
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawColumn(int column, int y1, int y2, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        Console.ForegroundColor = ConsolColor.Black;
        Console.SetCursorPosition(0, y1);
        string indent = string.Concat(Enumerable.Repeat("██", column));
        string text = string.Concat(Enumerable.Repeat(indent + "  \n", y2));
        Console.Write(text);
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
    public static void DrawRect(int x1, int x2, int row1, int row2, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        for (int i = row1; i < row2+row1; i++)
        {
            Console.SetCursorPosition(x1, i);
            Console.Write(string.Concat(Enumerable.Repeat("  ", x2)));
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();
    }
}

