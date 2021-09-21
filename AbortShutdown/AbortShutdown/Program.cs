using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AbortShutdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("shutdown","/f");
        }
    }
}
