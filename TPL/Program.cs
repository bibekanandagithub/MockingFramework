using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            SpinWait();
            Console.ReadKey();
        }
        private static void SpinWait()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
              {
                  Console.WriteLine("Press any key before 5 second other there will be a boom!!");
                  bool Cancelled = token.WaitHandle.WaitOne(5000);
                  Console.WriteLine(Cancelled?"Bomb Disarmed..":"Boooooommmmmmmmmmmm!!!!");
                
              },token);
            t.Start();
            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();
            Console.WriteLine("Main Programme done..");
            Console.ReadKey();


        }
        private static void MultipleCancellationTokenSource()
        {
            var planned = new CancellationTokenSource();
            var preventive = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token, preventive.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while(true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}");
                    System.Threading.Thread.Sleep(1000);
                }
            },paranoid.Token);
            Console.ReadKey();
            emergency.Cancel();
        }
        private static void Example_2()
        {
            var newTask = new Task<int>(GetLength, "I am Bibek");
            newTask.Start();
            Task<int> task2 = Task.Factory.StartNew(GetLength, "Me");
            Console.WriteLine("Length of First Task is {0}",newTask.Result);
            Console.WriteLine("Length of the seconf  task is {0}",task2.Result);
            Console.WriteLine("Main Programme..");
            Console.Read();

        }
        private static void CancelRequest()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            token.Register(() =>
            {
                Console.WriteLine("Task Cancellation Requested by user...");
            });
            var t = new Task(() =>
              {
                  int i = 0;
                  while(true)
                  {
                      if (token.IsCancellationRequested)
                          //  break;
                          //  throw new OperationCanceledException();
                          token.ThrowIfCancellationRequested();
                      else
                      {
                          Console.WriteLine(i);
                          i++;
                      }
                  }

              }, token);
            t.Start();
            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait Handle Released, Token Cancelled requested by User....");
            });
            Console.ReadKey();
            cts.Cancel();

        }
        private static void Example_1()
        {

            Task.Factory.StartNew(() => Write('*'));
            Task.Factory.StartNew(Write, "bibek");
            var t = new Task(Write, 123);
            t.Start();
            Console.WriteLine("Entering Main Programme");
            Console.Read();
        }
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- >0)
            {
                Console.Write(c);
            }
        }
        public static int GetLength(object o)
        {
            Console.WriteLine($"Current task is is {Task.CurrentId} is processing Object {0}");
            return o.ToString().Length;
        }
        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }
    }
}
