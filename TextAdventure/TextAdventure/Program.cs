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
        public static Regex RetrieveNest(string pre, string denote)
        {
            return new Regex($@"{pre}{denote}([^{denote}]|\n)*(?!.*{denote})");
        }
        public static string[] SectionSymbolList = {@"\:",@"\*",@"\^",@"\#",@"\$",@"\@"};
        public static StreamReader StoryFile = new StreamReader("../../Story.txt");
        public static string StoryScript = StoryFile.ReadToEnd();
        public static string GetStoryById(string StoryID="00.00.00", string scriptStory = "") {
            Regex rx1 = new Regex($@"%StoryID%{StoryID}\:(?:\r\n|\r|\n)*((?:[^%]|\r\n|\r|\n)*)(?:\r\n|\r|\n)%End%");
            Match m1 = rx1.Match(scriptStory != "" ? scriptStory : StoryScript);
            if (!m1.Success)
            {
                return "StoryID Not Found";
            }
            return m1.Groups[1].Value;
        }
        public static List<string> GetStoriesByPage(string StoryPage, string scriptStory = "") {
            Regex rx1 = RetrieveNest($@"%Story[A-Za-z]+%{StoryPage}",@"\*");
            Match m1 = rx1.Match(scriptStory != "" ? scriptStory:StoryScript);
            string tempScript = m1.Groups[0].Value;
            Regex rx2 = RetrieveNest(@"%Story[A-Za-z]+%(.*)", @"\:");
            MatchCollection m2 = rx2.Matches(tempScript);

            List<string> returnValue = new List<string>();
            foreach (Match section in m2)
            {
                string tempReturn = GetStoryById(section.Groups[1].Value, tempScript);
                returnValue.Add(tempReturn);
            }
            return returnValue;
        }
        public static List<List<string>> GetStoriesByChapter(string StoryChapter, string scriptStory = "") {
            Regex rx1 = RetrieveNest($@"%Story[A-Za-z]+%{StoryChapter}", @"\^");
            Match m1 = rx1.Match(scriptStory != "" ? scriptStory : StoryScript);
            string tempScript = m1.Groups[0].Value;
            Regex rx2 = RetrieveNest(@"%Story[A-Za-z]+%(.*)", @"\*");
            MatchCollection m2 = rx2.Matches(tempScript);

            List<List<string>> returnValue = new List<List<string>>();
            foreach(Match section in m2)
            {
                List<string> tempReturn = GetStoriesByPage(section.Groups[1].Value, tempScript);
                returnValue.Add(tempReturn);
            }

            return returnValue;
        }

        public static object GetStoriesBySection(int SectionLevel, string StorySectionID, string scriptStory = "") {
            object returnValue = new object();
            if (SectionLevel != 0)
            {
                Regex rx1 = RetrieveNest($@"%Story[A-Za-z]+%{StorySectionID}", $@"{SectionSymbolList[SectionLevel]}");
                Match m1 = rx1.Match(scriptStory != "" ? scriptStory : StoryScript);
                string tempScript = m1.Groups[0].Value;
                Regex rx2 = RetrieveNest(@"%Story[A-Za-z]+%(.*)", $@"{SectionSymbolList[SectionLevel-1]}");
                MatchCollection m2 = rx2.Matches(tempScript);
                foreach (Match section in m2)
                {
                    object tempReturn = new object();
                    switch (SectionLevel)
                    {
                        case 1:
                            returnValue = new List<object>();
                            tempReturn = GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<object>)returnValue).Add(tempReturn);
                            break;
                        case 2:
                            returnValue = new List<List<object>>();
                            tempReturn = (List<object>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<object>>)returnValue).Add((List<object>)tempReturn);
                            break;
                        case 3:
                            returnValue = new List<List<List<object>>>();
                            tempReturn = (List<List<object>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<object>>>)returnValue).Add((List<List<object>>)tempReturn);
                            break;
                        case 4:
                            returnValue = new List<List<List<List<object>>>>();
                            tempReturn = (List<List<List<object>>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<List<object>>>>)returnValue).Add((List<List<List<object>>>)tempReturn);
                            break;
                        case 5:
                            returnValue = new List<List<List<List<List<object>>>>>();
                            tempReturn = (List<List<List<List<object>>>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<List<List<object>>>>>)returnValue).Add((List<List<List<List<object>>>>)tempReturn);
                            break;
                        default:
                            returnValue = new List<object>();
                            tempReturn = GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<object>)returnValue).Add(tempReturn);
                            break;
                    }
                }
            }
            else
            {
                Regex rx1 = new Regex($@"%Story[A-Za-z]+%{StorySectionID}\:(?:\r\n|\r|\n)*((?:[^%]|\r\n|\r|\n)*)(?:\r\n|\r|\n)%End%");
                Match m1 = rx1.Match(scriptStory != "" ? scriptStory : StoryScript);
                if (!m1.Success)
                {
                    return "StoryID Not Found";
                }
                returnValue = m1.Groups[1].Value;
            }
            return returnValue;
        }
        static void Main(string[] args) {StoryFile.Close();
            List<List<object>> test = (List<List<object>>)GetStoriesBySection(2,"Adventure1");
            Console.WriteLine(test[0][0]);
            //Literal num = "5";
            //int[] a = { 5 };
            //if (num == 5)
            //{
            //    Console.WriteLine("true");
            //}
            //Console.WriteLine(num.integ);
            Console.ReadLine();
        }
    }
}
