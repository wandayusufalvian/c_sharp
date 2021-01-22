using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Column
    {
        public int ColumnID { get; set; }


        //satu kolom bisa punya banyak shelfs
        public ICollection<Shelf> Shelfs { get; set; }
    }
}
