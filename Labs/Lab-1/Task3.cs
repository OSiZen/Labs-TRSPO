using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab1
{
    public static class Task3
    {
        private static object obj = new object();
        private static bool print;
        public static void Main()
        {
            var thread1 = new Thread(() =>
            {
                Print(Thread.CurrentThread.Name);
            });
            thread1.Name = "thread1";
            var thread2 = new Thread(() =>
            {
                Print(Thread.CurrentThread.Name);
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("\nДо наступного завдання (№4)?\n");
            Console.ReadKey();
        }
        private static void Print(string name)
        {
            Monitor.Enter(obj);
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (name == "thread1")
                    {
                        if (print)
                        {
                            Monitor.Wait(obj);
                        }
                        print = true;
                        Console.WriteLine("Потiк №1 -> -");
                        Monitor.Pulse(obj);
                    }
                    else
                    {
                        if (!print)
                        {
                            Monitor.Wait(obj);
                        }
                        print = false;
                        Console.WriteLine("Потiк №2 -> +");
                        Monitor.Pulse(obj);
                    }
                }
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }
    }
}