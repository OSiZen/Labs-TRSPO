using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab3
{
    public static class Task2
    {
        private static Thread[] threads = new Thread[5];
        private static object obj = new object();
        private static Random rand = new Random();
        private static List<int> tempList = new List<int>();
        private static int[,] matrix = new int[5, 6];

        public static void Main()
        {
            Console.WriteLine("Лабораторна робота №3\n");
            var thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("\nРiк навчання 2023");
                Console.Write("Студент Олександр Шевчук");
            });
            Console.WriteLine("Введiть матрицю розмiром 5x6 через кому: ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write($"Рядок №{i + 1}: ");
                int[] value = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = value[j];
                }
            }
            Console.Write("\n");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    lock (obj)
                    {
                        int count = 0;
                        int currentRow = int.Parse(Thread.CurrentThread.Name.Substring(1)) - 1;
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[currentRow, j] % 2 == 0)
                            {
                                count++;
                            }
                        }
                        Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.Priority} пiдрахував, що {currentRow + 1} ряд матрицi мiстить парних елементiв – {count}");
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
            for (int i = 0; i < threads.Length - 1; i++)
            {
                for (int j = i + 1; j < threads.Length; j++)
                {
                    if (threads[i].Priority > threads[j].Priority)
                    {
                        var temp = threads[i];
                        threads[i] = threads[j];
                        threads[j] = temp;
                    }
                }
            }
            thread.Start();
            foreach (var t in threads)
            {
                t.Start();
                Thread.Sleep(150);
            }
            thread.Join();
            Console.WriteLine(" друге завдання виконав.");
        }
    }
}
