using iseng;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iseng
{
    class Program
    {
        static void Main(string[] args)
        {
            List<kotak> daftar = new List<kotak>();
            daftar.Add(new kotak("A", 10));
            daftar.Add(new kotak("A", 20));
            daftar.Add(new kotak("B", 5));
            daftar.Add(new kotak("C", 5));

            var n = (from x in daftar
                    where x._nama.Equals("A")
                    select x._ukuran).Sum();
            Console.WriteLine(n);
                      

        }
    }
}
