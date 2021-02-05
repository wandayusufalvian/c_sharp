using System;
using System.Threading;

namespace MultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1= new Thread(new ThreadStart(ContohThread1.Thread1));
            Thread thread2 = new Thread(new ThreadStart(ContohThread1.Thread2));
            thread1.Start();
            thread2.Start();

        }
    }

}
