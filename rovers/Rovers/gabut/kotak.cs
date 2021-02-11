using System;
using System.Collections.Generic;
using System.Text;

namespace iseng
{
    public class kotak
    {
        public string _nama { get; set;}
        public int _ukuran { get; set; }

        public kotak(string nama, int ukuran)
        {
            _nama = nama;
            _ukuran = ukuran;
        }
    }
}
