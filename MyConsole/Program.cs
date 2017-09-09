using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new ThreadHandler();
            thread.Start();

            Console.WriteLine("Press Escape to stop");
            ListenForEscapeKey();
        }

        static void ListenForEscapeKey()
        {
            do
            {
                while (!Console.KeyAvailable)
                {
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }

    public class ThreadHandler
    {
        private CancellationTokenSource tokenSource;
        public ThreadHandler()
        {
            tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConsoleReader), tokenSource.Token);
        }

        public static void ConsoleReader(object cancellationToken)
        {
            var token = (CancellationToken)cancellationToken;

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                var input = Console.ReadLine();
                var hash = MD5Calculater.MD5Calculater.Calculate(input);
                Console.WriteLine(hash);
            }

            Console.WriteLine("Canceled! ENDING NOW!");
        }
    }
}
