using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace NerdopolyProject
{
    class StoryObject
    {
        public string name;
        public string label;
        public string denote;
        public string text;
        public StoryObject(Match m)
        {
            name = m.Groups[1].Value == "%" ? "" : m.Groups[1].Value;
            label = m.Groups[2].Value;
            denote = m.Groups[3].Value;
            text = m.Groups[4].Value.Substring(1);
        }
    }
    class Story
    {
        /*
        public static Regex RetrieveNest(string pre, string denote)
        {
            return new Regex($@"{pre}{denote}([^{denote}]|\n)*(?!.*{denote})");
        }
        */
        //Ch1[0](1)
        public static string[] SectionSymbolList = { @"\:", @"\*", @"\^", @"\#", @"\$", @"\@" };
        public static StreamReader StoryFile = new StreamReader("../../Story.txt");
        public static string StoryScript = StoryFile.ReadToEnd();
        public static string RetrieveNest(string nestLabel, string denote,string nestName = null)
        {
            int layerValue = Array.IndexOf(SectionSymbolList, denote);
            string higherText = String.Join("",new ArraySegment<string>(SectionSymbolList, layerValue, SectionSymbolList.Length - layerValue));
            if (nestName != null)
            {
                nestName = $"({nestName})%";
            }
            else
            {
                nestName = "";
            }
            Regex rx1 = new Regex($@"%Story(?:{nestName})+([^ \r\n\t])*%*({nestLabel})({denote})[\r\n]((?:(?!.*%Story.*%.*[{higherText}]).*[^{higherText}]|[\r\n])*[^\r\n])(?![^\r\n])");
            MatchCollection m1 = rx1.Matches(StoryScript);
            //returnValue(0) = new StoryObject(m1[0]);
            //returnValue(0).label = (new StoryObject(m1[0])).label;
            Func<int, StoryObject> returnValue = (x) => { return new StoryObject(m1[x]); };
            Console.WriteLine(returnValue(1).text);
            return "";
        }
    }
}
