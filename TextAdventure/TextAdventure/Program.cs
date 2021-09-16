using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
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
                integ = Int32.TryParse((string)val, out int temp) ? temp:0;
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
    public static implicit operator bool(Literal x)
    {
        if (x.integ != 0)
        {
            return true;
        } else
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
namespace TextAdventure
{
    class Program
    {
        public static StreamReader StoryFile = new StreamReader("../../Story.txt");
        public static string StoryScript = StoryFile.ReadToEnd();
        public static string GetStoryById(string StoryID="00.00.00") {
            Regex rx = new Regex($@"%StoryID%{StoryID}\:(?:\r\n|\r|\n)*((?:[^%]|\r\n|\r|\n)*)(?:\r\n|\r|\n)%End%");
            Match m = rx.Match(StoryScript);
            if (!m.Success)
            {
                return "StoryID Not Found";
            }
            return m.Groups[1].Value;
        }
        public static List<string> GetStoriesByPage(string StoryPage) {
            Regex rx = new Regex($@"(?<=%StoryPage%{StoryPage}\*(?:[^\*]|\r\n|\r|\n)*)%StoryID%(?:.*)\:(?:\r\n|\r|\n)*((?:[^%]|\r\n|\r|\n)*)(?:\r\n|\r|\n)%End%");
            MatchCollection m = rx.Matches(StoryScript);
            List<string> returnValue = new List<string>();
            for (int i=0; i<m.Count; i++)
            {
                returnValue.Add(m[i].Groups[1].Value);
            }
            return returnValue;
        }
        public static List<List<string>> GetStoriesByChapter(string StoryChapter) {
            Regex rx1 = new Regex($@"Adventure1\^([^\^]|\n)*(?=\n.*\^)");
            return new List<List<string>>();
        }
        static void Main(string[] args) {StoryFile.Close();
            //List<string> test = GetStoriesByPage("Intro");
            //Console.WriteLine(test[1]);
            Literal num = "5";
            int[] a = { 5 };
            if (num == 5)
            {
                Console.WriteLine("true");
            }
            Console.WriteLine(num.integ);
            Console.ReadLine();
        }
    }
}
