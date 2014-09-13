using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrency_Async_Practice
{
    class Program
    {
        static bool _done;
        private static readonly object _locker = new object();

        static void Main(string[] args)
        {
            /*
            //a.執行序,並指定委派
            Thread t = new Thread(WriteY);
            t.Start();

            //a.同時,do something on the main thread.
            for (int i = 0; i < 1000; i++)
            {
                Console.Write('x');
            }

            //b.當前執行序名稱
            Console.Write(Thread.CurrentThread.Name);
            */

            /*
            //c.join 等待此執行序結束
            Thread t = new Thread(Go);
            t.Start();
            t.Join();

            //c.休眠 Thread.Sleep(0) Thread.Yield()
            Thread.Sleep(TimeSpan.FromSeconds(5));      //封鎖此執行緒五秒
            Console.WriteLine("Thread  t has ended");
            */
            
            /*
            //d.Shared State  
            //因為兩個執行緒都在同一個實體呼叫GOGO,所以共享_done field,結果就只會顯示一次
            Program tt = new Program();     //create a common instance
            new Thread(tt.GoGo).Start();
            tt.GoGo();

            //d.Lambda 表示式也會產生 Shared State  
            bool done = false;
            ThreadStart action = () =>
            {
                if (!done)
                {
                    {
                        done = true;
                        Console.Write("Done");
                    }
                }
            };

            new Thread(action).Start();
            action();
            */

            /*
            //e.為了避免shared state影響程式
            //  使用lock保護值性序安全性(thread-safe)
            new Thread(LockGo).Start();
            LockGo();
            */


            //f. 變數i都指向同一個same memory location
            for (int i = 0; i < 10; i++)
            {
                new Thread(() => Console.Write(i)).Start();
            }


            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                new Thread(() => Console.Write(temp)).Start();
            }
        }

        static void LockGo()
        {
            //鎖定
            lock (_locker)
            {
                if (!_done)
                {   //所以不用擔心執行緒會同時進來,產生兩個Done問題
                    Console.Write("Done");
                    _done = true;
                }
            }
        }

        private void GoGo()
        {
            //第二個執行緒 因為_done變成true所以沒反應
            if (!_done)
            {
                _done = true;
                Console.WriteLine("Done");

            }
            else
            {
                Console.WriteLine("//第二個執行緒 因為_done變成true所以沒反應");
            }
        }


        private static void Go(object obj)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write('x');
            }
        }

        private static void WriteY(object obj)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("y");
            }
        }
    }
}
