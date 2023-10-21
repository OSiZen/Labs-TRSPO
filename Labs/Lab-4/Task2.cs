﻿using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab4
{
    public static class Task2
    {
        private static Thread[] threads = new Thread[4];
        private static object obj = new object();
        private static Random rand = new Random();
        private static int numThread;
        private static int value = 7;

        public static void Main()
        {
            for (int i = 0; i < 4; i++)
            {
                threads[i] = new Thread(() =>
                {
                    if (Thread.CurrentThread.Name != $"#{numThread}")
                    {
                        lock (obj)
                        {
                            Console.WriteLine($"{Thread.CurrentThread.Name} мiстить таке значення: {++value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{Thread.CurrentThread.Name} мiстить таке значення: {++value}");
                    }
                });
                threads[i].Name = $"#{i + 1}";
            }
            numThread = rand.Next(0, threads.Length) + 1;
            Console.WriteLine($"Обрано потiк #{numThread} для деблокування");
            foreach (var t in threads)
            {
                t.Start();
                if (t.Name != $"#{numThread}")
                {
                    Thread.Sleep(1000);
                }
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            Console.WriteLine("\nДо наступного завдання (№3)?\n");
            Console.ReadKey();
        }
    }
}
