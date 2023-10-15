using System;
using System.Collections.Generic;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab3
{
    public static class Task1
    {
        private static Thread[] threads = new Thread[5];
        private static object obj = new object();
        private static Random rand = new Random();
        private static List<int> tempList = new List<int>();

        public static void Main()
        {
            Console.WriteLine("Лабораторна робота №3\n");
            var thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("\nРiк навчання 2023");
                Console.Write("Студент Олександр Шевчук");
            });
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    lock (obj)
                    {
                        Thread.Sleep(500);
                        Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.Priority}");
                    }
                });
                threads[i].Name = $"#{i + 1}";
                int indx;
                do
                {
                    indx = rand.Next(0, 5);
                } while (tempList.Contains(indx));
                tempList.Add(indx);
                threads[i].Priority = (ThreadPriority)indx;
            }
            thread.Start();
            foreach (var t in threads)
            {
                t.Start();
            }
            thread.Join();
            Console.WriteLine(" перше завдання виконав.");

            Console.WriteLine("\nДо наступного завдання (№2)?");
            Console.ReadKey();
            Console.Write("\n");
        }
    }
}
