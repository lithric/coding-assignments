using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
class StoryObject
{
    public string name;
    public string label;
    public string denote;
    public string text;
    public string prev;
    public int total = 0;
    public StoryObject(Match m, MatchCollection mr = null, string scriptStory = "")
    {
        prev = scriptStory;
        name = m.Groups[1].Value;
        label = m.Groups[2].Value;
        denote = m.Groups[3].Value;
        text = m.Groups[4].Value.Substring(1);
        total = mr.Count;
    }
}
namespace NerdopolyProject
{
    class Story
    {
        public static string[] SectionSymbolList = { @"\:", @"\*", @"\^", @"\#", @"\$", @"\@" };
        public static StreamReader StoryFile = new StreamReader("../../Story.txt");
        public static string StoryScript = StoryFile.ReadToEnd();
        public static object RetrieveNest(string nestLabel, string denote, string scriptStory = "")
        {
            int layerValue = Array.IndexOf(SectionSymbolList, denote);
            string higherText = String.Join("",new ArraySegment<string>(SectionSymbolList, layerValue, SectionSymbolList.Length - layerValue));
            Regex rx1 = new Regex($@"%Story([^ \r\n\t]*)%({nestLabel})({denote})[\r\n]((?:(?!.*%Story.*%.*[{higherText}]).*[^{higherText}]|[\r\n])*[^\r\n])(?![^\r\n])");
            MatchCollection m1 = rx1.Matches(scriptStory != "" ? scriptStory:StoryScript);
            Func<int, StoryObject> returnValue = (x) => { return new StoryObject(m1[x], m1, scriptStory); };
            return returnValue;
        }

        public static StoryObject GetStoryByID(string label, string scriptStory = "")
        {
            StoryObject returnValue;
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label,@"\:",scriptStory);
            returnValue = temp1(0);
            return returnValue;
        }
        public static List<StoryObject> GetStoriesByPage(string label, string scriptStory = "")
        {
            List<StoryObject> returnValue = new List<StoryObject>();
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label, @"\*", scriptStory);
            string tempReturn1 = temp1(0).text;
            Func<int, StoryObject> temp2 = (Func<int, StoryObject>)RetrieveNest(".*", @"\:", tempReturn1);
            for (int i=0; i<temp2(0).total; i++)
            {
                returnValue.Add(temp2(i));
            }
            return returnValue;
        }
        public static List<List<StoryObject>> GetStoriesByChapter(string label, string scriptStory = "")
        {
            List<List<StoryObject>> returnValue = new List<List<StoryObject>>();
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label, @"\^", scriptStory);
            string tempReturn1 = temp1(0).text;
            Func<int, StoryObject> temp2 = (Func<int, StoryObject>)RetrieveNest(".*", @"\*", tempReturn1);
            for (int i=0; i<temp2(0).total; i++)
            {
                List<StoryObject> temp3 = GetStoriesByPage(temp2(i).label);
                returnValue.Add(temp3);
            }
            return returnValue;
        }
        public static List<List<List<StoryObject>>> GetStoriesByBook(string label, string scriptStory = "")
        {
            List<List<List<StoryObject>>> returnValue = new List<List<List<StoryObject>>>();
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label, @"\#", scriptStory);
            string tempReturn1 = temp1(0).text;
            Func<int, StoryObject> temp2 = (Func<int, StoryObject>)RetrieveNest(".*", @"\^", tempReturn1);
            for (int i = 0; i < temp2(0).total; i++)
            {
                List<List<StoryObject>> temp3 = GetStoriesByChapter(temp2(i).label);
                returnValue.Add(temp3);
            }
            return returnValue;
        }

        public static object GetStoriesBySection(string label, string denote, string scriptStory = "")
        {
            object returnValue = new object();
            int SectionLevel = Array.IndexOf(SectionSymbolList, denote);
            if (SectionLevel > 0)
            {
                returnValue = new List<object>();
                Func<int, StoryObject> temp = (Func<int, StoryObject>)RetrieveNest(".*", SectionSymbolList[SectionLevel - 1], ((Func<int, StoryObject>)RetrieveNest(label, SectionSymbolList[SectionLevel], scriptStory))(0).text);
                for (int i = 0; i < temp(0).total; i++)
                {
                    object temp3 = GetStoriesBySection(temp(i).label, SectionSymbolList[SectionLevel - 1]);
                    ((List<object>)returnValue).Add(temp3);
                }
            } else
            {
                returnValue = GetStoryByID(label);
            }
            return returnValue;
        }
    }
}
