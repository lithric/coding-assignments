using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class ThreadingDemo
    {
        public static void Start()
        {
            Thread thread1 = new Thread(new ParameterizedThreadStart(MyThreading.CountDown));
            Thread thread2 = new Thread(new ParameterizedThreadStart(MyThreading.CountUp));
            thread1.Start(2);
            thread2.Start(4);
        }
    }
}
