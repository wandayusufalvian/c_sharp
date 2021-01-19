using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Dua
    {
        public void prints()
        {
            Console.WriteLine("tes"); 
        }
    }
    class Satu
    {
        Dua two=new Dua(); 

        public void dotwo()
        {
            two.prints(); 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Satu one = new Satu();
            one.dotwo(); 
            
        }
    }

    
}
