using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq_demonstration
{
    struct Cards
    {
        public static void Add(List<(string, string, string, string)> deck)
        {
            Func<int, (string, string, string, string)> bank = (int x) => {return deck[x];};
        }
    }
}
