using System;
using System.Threading;
using Timer = System.Timers.Timer;

namespace TRSPO_Labs_Shevchuk.Labs.Lab2
{
    public static class Task3
    {
        private static Random rand = new Random();
        private static object obj = new object();
        private static bool firstThread = false;
        private static bool secondThread = false;
        private static bool threadWait = false;

        public static void Main()
        {
            var timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += (s, e) =>
            {
                lock (obj)
                {
                    if (rand.Next(0, 2) == 0)
                    {
                        firstThread = true;
                    }
                    else
                    {
                        secondThread = true;
                    }
                    if (threadWait == true)
                    {
                        threadWait = false;
                        Monitor.Pulse(obj);
                    }
                }
            };
            timer.Start();
            var thread1 = new Thread(() =>
            {
                while (true)
                {
                    if (firstThread == true)
                    {
                        lock (obj)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Console.WriteLine($"Працює потiк №1");
                            }
                            Console.WriteLine();
                            firstThread = false;
                            threadWait = true;
                            Monitor.Wait(obj);
                        }
                    }
                }
            });
            var thread2 = new Thread(() =>
            {
                while (true)
                {
                    if (secondThread == true)
                    {
                        lock (obj)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Console.WriteLine($"Працює потiк №2");
                            }
                            Console.WriteLine();
                            secondThread = false;
                            threadWait = true;
                            Monitor.Wait(obj);
                        }
                    }
                }
            });
            thread1.Start();
            thread2.Start();
        }
    }
}