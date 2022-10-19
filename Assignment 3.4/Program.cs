using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Task t1 = Task.Factory.StartNew(() => Method10(ct), ct);
            Task t2 = Task.Factory.StartNew(() => Method5(ct), ct);
            
            Task t3 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("\nPress any key to cancel...");
                Console.ReadKey();
                cts.Cancel();
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle((e) =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

            Console.WriteLine();
            Console.WriteLine("T1 status = " + t1.Status);
            Console.WriteLine("T2 status = " + t2.Status);
        }


        private static void Method10(CancellationToken ct)
        {
            Console.WriteLine("Method10 executing for 10 seconds");
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 10_000)
            {
                if (sw.ElapsedMilliseconds >= 1000 && sw.ElapsedMilliseconds % 1000 < 100)
                {
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("Method10 was canceled");
                        ct.ThrowIfCancellationRequested();
                    }
                }

                if (sw.ElapsedMilliseconds >= 3000 && sw.ElapsedMilliseconds < 3200)
                {
                    int y = 0;
                    int x = 100 / y;
                }
            }
            Console.WriteLine("Method10 Done");
        }
        private static void Method5(CancellationToken ct)
        {
            Console.WriteLine("Method5 executing for 5 seconds");
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 5_000)
            {
                if (sw.ElapsedMilliseconds >= 1000 && sw.ElapsedMilliseconds % 1000 < 100)
                {
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("Method5 was canceled");
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }
            Console.WriteLine("Method5 Done");
        }
    }
}
