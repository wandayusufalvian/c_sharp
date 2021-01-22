using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Shelf
    {
        public int ShelfID { get; set; }
        public int ShelfSize { get; set; }
        public String Barcode { get; set; }
        public int Theta { get; set; }

        //satu shelf ke satu kolom saja
        public Column column { get; set; }
    }
}
