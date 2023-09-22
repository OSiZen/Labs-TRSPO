using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab1
{
    public static class Task4
    {
        public static void Main()
        {
            var thread1 = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} -> {Counter.Increment()}");
                    Thread.Sleep(250);
                }
            });
            thread1.Name = "thread1";
            var thread2 = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} -> {Counter.Decrement()}");
                    Thread.Sleep(500);
                }
            });
            thread2.Name = "thread2";
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine($"\nЗначення лiчильника: {Counter.value}");
            Console.ReadKey();
        }
    }

    public static class Counter
    {
        private static object obj = new object();
        public static int value { get; set; }

        public static int Increment()
        {
            lock (obj)
            {
                return ++value;
            }
        }

        public static int Decrement()
        {
            lock (obj)
            {
                return --value;
            }
        }
    }
}