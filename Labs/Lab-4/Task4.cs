using System;
using System.Threading;

namespace TRSPO_Labs_Shevchuk.Labs.Lab4
{
    public static class Task4
    {
        public static void Main()
        {
            var rand = new Random();
            double[,] mtrx = new double[2, rand.Next(3, 6)];
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {
                for (int j = 0; j < mtrx.GetLength(1); j++)
                {
                    mtrx[i, j] = rand.Next(0, 25);
                }
            }
            Console.WriteLine("Згенеровано матрицю такого вигляду:");
            PrintMtrx(mtrx);
            double a11 = mtrx[0, 0];
            double a21 = mtrx[1, 0];
            var obj = new object();
            var thread1 = new Thread(() =>
            {
                lock (obj)
                {
                    mtrx[1, 0] = a21 - (a21 / a11) * a11;
                    Console.WriteLine($"\nПерший потiк зробив 0 пiд елементом а11 = {a11}");
                    PrintMtrx(mtrx);
                }
                
            });
            var thread2 = new Thread(() =>
            {
                lock (obj)
                {
                    for (int i = 0; i < mtrx.GetLength(1); i++)
                    {
                        mtrx[0, i] = Math.Round(mtrx[0, i] / a11, 2);
                    }
                    Console.WriteLine($"\nДругий потiк подiлив перший рядок на елемент а11 = {a11}");
                    PrintMtrx(mtrx);
                }
            });
            if (a11 != 0)
            {
                thread1.Priority = ThreadPriority.Highest;
                thread2.Priority = ThreadPriority.Lowest;
                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();
            }
            else
            {
                Console.WriteLine("\nНеможливо звести матрицю до схiдчастого вигляду, оскiльки a11 = 0");
            }

            Console.WriteLine("\n");
            Console.ReadKey();
        }

        private static void PrintMtrx(double[,] mtrx)
        {
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {
                for (int j = 0; j < mtrx.GetLength(1); j++)
                {
                    Console.Write(mtrx[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
