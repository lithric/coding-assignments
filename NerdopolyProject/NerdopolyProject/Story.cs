using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
class StoryObject
{
    public string name;
    public string label;
    public string denote;
    public string text;
    public int total = 0;
    public StoryObject(Match m, MatchCollection mr = null)
    {
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
            Func<int, StoryObject> returnValue = (x) => { return new StoryObject(m1[x], m1); };
            return returnValue;
        }

        public static object GetStoryByID(string label, string scriptStory = "")
        {
            object returnValue = new object();
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label,@"\:",scriptStory);
            if (temp1(0).total > 1)
            {
                returnValue = (Func<int, string>)((x) => { return temp1(x).text; });
            } else
            {
                returnValue = temp1(0).text;
            }
            return returnValue;
        }
        public static object GetStoriesByPage(string label, string scriptStory = "")
        {
            object returnValue = new object();
            Func<int, StoryObject> temp1 = (Func<int, StoryObject>)RetrieveNest(label, @"\*", scriptStory);
            Func<int, StoryObject> temp2 = (Func<int, StoryObject>)RetrieveNest(".*",@"\:",temp1(0).text);
            int totalR = temp2(0).total;
            returnValue = (Func<int, List<object>>)((page) => {
                List<object> tempReturn = new List<object>();
                Func<int, StoryObject> temp1Val = (Func<int, StoryObject>)RetrieveNest(".*", @"\:", temp1(page).text);
                for (int i=0; i < totalR; i++)
                {
                    object tempVal = GetStoryByID(temp1Val(i).label,temp1(page).text);
                    tempReturn.Add(tempVal);
                }
                return tempReturn;
            });
            // returnValue(0)[0]
            return returnValue;
        }
    }
}
