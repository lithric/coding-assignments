using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace NerdopolyProject
{
    class Story
    {
        /*
        public static Regex RetrieveNest(string pre, string denote)
        {
            return new Regex($@"{pre}{denote}([^{denote}]|\n)*(?!.*{denote})");
        }
        */
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
            Regex rx1 = new Regex($@"%Story(?:{nestName})+([^ \r\n\t])*%*({nestLabel})({denote})[\r\n]((?!.*%Story.*%.*[{higherText}]).*[^{higherText}]|[\r\n])*[^\r\n](?![^\r\n])");
            MatchCollection m1 = rx1.Matches(StoryScript);
            Console.WriteLine(m1[0]);
            return "";
        }
    }
}
