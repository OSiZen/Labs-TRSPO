using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab1
{
    public static class Task2
    {
        public static void Main()
        {
            var thread1 = new Thread(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("люблю");
            });
            var thread2 = new Thread(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Україну!");
            });
            thread1.Start();
            thread2.Start();
            Thread.Sleep(2000);
            Console.WriteLine("я");
            thread1.Join();
            thread2.Join();
            Console.WriteLine("\nДо наступного завдання (№3)?\n");
            Console.ReadKey();
        }
    }
}