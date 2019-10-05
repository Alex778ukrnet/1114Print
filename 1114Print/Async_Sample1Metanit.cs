using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1114Print
{
    class Async_Sample1Metanit
    {
        private static int f = 0;
        public void Call()
        {
            Console.Write($"{Thread.CurrentThread.ManagedThreadId}Введите число:");
            f = Convert.ToInt32(Console.ReadLine());
            Sample();

            Console.WriteLine($"Квадрат числа {f*f} {Thread.CurrentThread.ManagedThreadId}");
        }

        private async void Sample()
        {
            Console.WriteLine($"Начало метода {Thread.CurrentThread.ManagedThreadId}");
            await Task.Run<int>(() => Factorial());
        }

        private int Factorial()
        {
            var res = 1;
            for (int i=1; i<=f; i++)
            {
                res = res * i;
            }
            Console.WriteLine($"Факториал числа {res}  {Thread.CurrentThread.ManagedThreadId}");
            return res;
        }
    }
}
