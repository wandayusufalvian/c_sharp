using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> lists = Directory.EnumerateDirectories("store"); 
            foreach(var dir in lists)
            {

            }
        }
        
    }
}