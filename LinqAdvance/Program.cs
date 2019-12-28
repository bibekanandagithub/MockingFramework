using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

namespace LinqAdvance
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //ObseravableRange();
            //DeferredExecution();
            //Console.Read();
            GetExample();
        }

        private static void ObseravableRange()
        {
            var input = Observable.Range(1, 34);
            input.Subscribe(x => Console.WriteLine("The Number is {0}", x));
        }
        private static void QuerySyntax()
        {
           var arr = new List<int>() { 1, 2, 3, 56, 78, 44, 32, 21 };
            var querySyntax = from m in arr
                              where m > 7
                              select m;

            foreach(var s in querySyntax)
            {
                Console.WriteLine(s);
            }
        }
        private static  void MethodSyntax()
        {
            int[] arr = { 12,45,89,82,69};
            var methodSyntax = arr
                .Where(a => a > 23)
                .Select(a => a);
        }

        private static void DeferredExecution()
        {
            var q = Enumerable.Range(0, 10 )
                .Select(x =>
                {
                    Thread.Sleep(500);
                    return x * 10;
                }
                );
            foreach(var number in q)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("----------------------------"); 
            foreach (var number in q)
            {
                Console.WriteLine(number);
            }
        }

        private static void UseofAny()
        {
            var firstList = Enumerable.Empty<int>();
            var secondList = Enumerable.Range(1, 10);
            Console.WriteLine("The first list has member?{0} , the second list has members?{1}",firstList.Any(),secondList.Any());
            Console.WriteLine("Second list contain 6 {0}",secondList.Any(x=>x==6));
        }
        private static void UseofDistinct()
        {
            int[] arr = {1,2,3,1,2,3,4,2,1 };
            var output = arr.Distinct().Select(x => x + 10);
            foreach(var r in output)
                Console.WriteLine(r);
        }

        private  static void ZipOperator()
        {
            string[] shortcut = { "AI", "BI", "CA" };
            string[] fullform = {"All India","Business Intelligence","Charted Account" };
            var result = shortcut.Zip(fullform, (shortcut, fullform) => shortcut + ":  " + fullform);
            foreach(var items in result)
                Console.WriteLine(items);
        }

        private static void GetExample()
        {
            var result = Getbooks();
            var q = from m in result
                    select m.Authors;

            foreach(var a in q)
            {
                foreach(var aa in a)
                {
                    Console.WriteLine(aa.Fname);
                }
            }
           
        }

        private static List<books>  Getbooks()
        {
            new books
            {
                Title = "C# 7",
                Authors = new List<Author>()
                {
                    new Author{ Fname="Christian Nagel!" },
                    new Author{ Fname="Variable Linq"}
                }
            };
            return null;
        }
    }

    class books
    {
        public string Title { set; get; }
        public List<Author> Authors { set; get; }
    }
    class Author
    {
        public string Fname { get; set; }
    }
}
