using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _1114Print
{
    class Program
    {
        static void Main()
        {
            new Async_Sample1Metanit().Call();
            return;

            //Console.WriteLine("Hello World!");

            Console.WriteLine("thread executed {0}", Thread.CurrentThread.ManagedThreadId);

            var foo = new Foo(3,4,5,6,7,1,2,1,7);
            for (int i=0; i<10; i++) foo.CountNext();

            new Task(() =>
                foo.First(() =>
                { Console.WriteLine("printFirst {0}", Thread.CurrentThread.ManagedThreadId); })).Start();

            new Task(() =>
                foo.Second(() =>
                { Console.WriteLine("printSecond {0}", Thread.CurrentThread.ManagedThreadId); })).Start();

            new Task(() =>
                foo.Third(() =>
                { Console.WriteLine("printThird {0}", Thread.CurrentThread.ManagedThreadId); })).Start();

            //ThreadPool.QueueUserWorkItem(new WaitCallback())
            //new Task(() =>
            //{
            //    Console.WriteLine("thread started {0}", Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(1000);
            //    Console.WriteLine("thread ended {0}", Thread.CurrentThread.ManagedThreadId);
            //}).Start();

            Console.ReadKey();
        }
    }



    public class Foo
    {
        static readonly object _locker = new object();
        private HashSet<int> sort = new HashSet<int>(); // = new []{2,1,3}
        private int next = 0;
        //private printFirst = new delegate {Console.WriteLine("printFirst {0}", Thread.CurrentThread.ManagedThreadId);};
        //void printFirst()
        //{
        //    Console.WriteLine("printFirst {0}", Thread.CurrentThread.ManagedThreadId);
        //}
        //void printSecond()
        //{
        //    Console.WriteLine("printSecond {0}", Thread.CurrentThread.ManagedThreadId);
        //}

        //void printThird()
        //{
        //    Console.WriteLine("printThird {0}", Thread.CurrentThread.ManagedThreadId);
        //}

        public void CountNext()
        {
            //Console.WriteLine("next {0} sort: {1}", next, string.Join(',', sort.ToArray()));
            lock (_locker)
            {
                sort.Remove(next);
                next = sort.Any() ? sort.FirstOrDefault() : 0;
            }
        }

        public Foo(params int[] args)
        {
            for (int i=0; i<args.Length; i++)
            {
                //if (args[i]==1 || args[i] == 2 || args[i] == 3)
                    sort.Add(args[i]);
                Console.WriteLine(args[i]);
            }
        }


        public void First(Action printFirst)
        {
            while(next != 1 && sort.Contains(1))
            {
                Thread.Sleep(10);
            }
            CountNext();

            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();
        }

        public void Second(Action printSecond)
        {
            while (next != 2 && sort.Contains(2))
            {
                Thread.Sleep(10);
            }
            CountNext();
            // printSecond() outputs "second". Do not change or remove this line.
            printSecond();
        }

        public void Third(Action printThird)
        {
            while (next != 3 && sort.Contains(3))
            {
                Thread.Sleep(10);
            }
            CountNext();
            // printThird() outputs "third". Do not change or remove this line.
            printThird();
        }
    }
}
