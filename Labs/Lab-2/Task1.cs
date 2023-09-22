using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab2
{
    public static class Task1
    {
        private static int n = 40;
        private static double a = 0;
        private static double b = 1;
        private static double h = (b - a) / n;
        private static double sum1;
        private static double sum2;
        private static double result;

        public static void Main()
        {
            Console.WriteLine("Лабораторна робота №2");
            var thread1 = new Thread(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Рiк навчання 2023");
                Console.Write("Студент Олександр Шевчук отримав такий результат при обрахунку: ");
            });
            var thread2 = new Thread(() =>
            {
                for (int i = 1; i < n; i++)
                {
                    if (i % 2 != 0)
                    {
                        sum1 += 4.0 / (1 + Math.Pow(a + i * h, 2));
                    }
                }
            });
            var thread3 = new Thread(() =>
            {
                for (int i = 2; i < n; i++)
                {
                    if (i % 2 == 0)
                    {
                        sum2 += 4.0 / (1 + Math.Pow(a + i * h, 2));
                    }
                }
            });
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            result = h / 3 * (4 * (1.0 / (1 + a * a)) + 4 * sum1 + 2 * sum2 + 1.0 / (1 + b * b));
            Console.WriteLine(result);
            Console.Write(result < Math.PI ? "Отримане значення наближається до числа pi." : "Отримане значення бiльше або дорiвнює числу pi.");
            Console.WriteLine(" Отже, перше завдання було виконано.");
            Console.WriteLine("\nДо наступного завдання (№3)?\n");
            Console.ReadKey();
        }
    }
}