using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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
        public static List<List<string>> GetStoriesByBook(string StoryBook) {
            Regex rx = new Regex($@"(?<=%StoryPage%(.*)\*(?:[^\*]|\r\n|\r|\n)*)%StoryID%(?:.*)\:(?:\r\n|\r|\n)*((?:[^%]|\r\n|\r|\n)*)(?:\r\n|\r|\n)%End%");
            return new List<List<string>>();
        }
        static void Main(string[] args)
        {
            StoryFile.Close();
            List<string> test = GetStoriesByPage("Intro");
            Console.WriteLine(test[1]);
            Console.ReadLine();
        }
    }
}
