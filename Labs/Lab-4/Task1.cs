using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab4
{
    public static class Task1
    {
        public static void Main()
        {
            Console.WriteLine("Лабораторна робота №4\n");
            var thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Рiк навчання 2023");
                Console.Write("Студент Олександр Шевчук");
            });
            thread.Start();
            thread.Join();
            Console.WriteLine(" завдання №1 виконав.");

            Console.WriteLine("\nДо наступного завдання (№2)?\n");
            Console.ReadKey();
        }
    }
}
