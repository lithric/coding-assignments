using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class StoryBoard
    {
        public static string[] SectionSymbolList = { @"\:", @"\*", @"\^", @"\#", @"\$", @"\@" };
        public static StreamReader StoryFile = new StreamReader("../../Story.txt");
        public static string StoryScript = StoryFile.ReadToEnd();
        public static Regex RetrieveNest(string pre, string denote)
        {
            return new Regex($@"{pre}{denote}([^{denote}]|\n)*(?!.*{denote})");
        }
        public static object GetStoriesBySection(int SectionLevel, string StorySectionID, string scriptStory = "")
        {
            object returnValue = new object();
            if (SectionLevel != 0)
            {
                Regex rx1 = RetrieveNest($@"%Story[A-Za-z]+%{StorySectionID}", $@"{SectionSymbolList[SectionLevel]}");
                Match m1 = rx1.Match(scriptStory != "" ? scriptStory : StoryScript);
                string tempScript = m1.Groups[0].Value;
                Regex rx2 = RetrieveNest(@"%Story[A-Za-z]+%(.*)", $@"{SectionSymbolList[SectionLevel - 1]}");
                MatchCollection m2 = rx2.Matches(tempScript);
                foreach (Match section in m2)
                {
                    object tempReturn = new object();
                    bool active = returnValue.GetType().ToString() == "System.Object";
                    switch (SectionLevel)
                    {
                        case 1:
                            returnValue = active ? new List<object>() : returnValue;
                            tempReturn = GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<object>)returnValue).Add(tempReturn);
                            break;
                        case 2:
                            returnValue = active ? new List<List<object>>() : returnValue;
                            tempReturn = (List<object>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<object>>)returnValue).Add((List<object>)tempReturn);
                            break;
                        case 3:
                            returnValue = active ? new List<List<List<object>>>() : returnValue;
                            tempReturn = (List<List<object>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<object>>>)returnValue).Add((List<List<object>>)tempReturn);
                            break;
                        case 4:
                            returnValue = active ? new List<List<List<List<object>>>>() : returnValue;
                            tempReturn = (List<List<List<object>>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<List<object>>>>)returnValue).Add((List<List<List<object>>>)tempReturn);
                            break;
                        case 5:
                            returnValue = active ? new List<List<List<List<List<object>>>>>() : returnValue;
                            tempReturn = (List<List<List<List<object>>>>)GetStoriesBySection(SectionLevel - 1, section.Groups[1].Value, tempScript);
                            ((List<List<List<List<List<object>>>>>)returnValue).Add((List<List<List<List<object>>>>)tempReturn);
                            break;
                        default:
                            returnValue = active ? new List<object>() : returnValue;
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
    }
}
