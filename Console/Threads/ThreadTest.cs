using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.Threads
{
    public class ThreadTest
    {
        public static void RunWithoutThread()
        {
            long nthPrime = FindPrimeNumber(100000000); //set higher value for more time
        }
        private static long FindPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            System.Console.WriteLine("find " + a);
            }
            return (--a);
        }
        
        public static void RunWithTask()
        {
            Task<int> task = new Task<int>(LongRunningTask);
            task.Start();
            System.Console.WriteLine("moving on");
            System.Console.WriteLine(task.Result);
            System.Console.ReadKey();
        }
        private static int LongRunningTask()
        {
            System.Console.WriteLine("thread started");
            Thread.Sleep(3000);
            System.Console.WriteLine("thread completed");
            return 1;
        }

        public async Task RunWithTask2()
        {
            System.Console.WriteLine("task started");
            await Task.Delay(3000);
            System.Console.WriteLine("task completed");
        }
    }
}
