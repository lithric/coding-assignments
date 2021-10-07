using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class MyThreading
    {

        public static void CountDown(object iter)
        {
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine($"Timer Number 1: {i} seconds.");
                Thread.Sleep(100);
            }
            CountUp(iter);
        }

        public static void CountUp(object iter)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Timer Number 2: {i} seconds.");
                Thread.Sleep(100);
            }
            CountDown(iter);
        }
    }
}
