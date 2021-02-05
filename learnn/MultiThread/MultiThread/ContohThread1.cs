using System;
using System.Collections.Generic;
using System.Text;

namespace MultiThread
{
    public class ContohThread1
    {
        public static void Thread1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("thread 1");
            }
        }

        public static void Thread2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("thread 2");
            }
        }
    }
}
