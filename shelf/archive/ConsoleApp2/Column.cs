using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Column
    {
        public List<Shelf> Shelfs { get; set; }
        public int IdColumn;
        public Column(int id)
        {
            IdColumn = id;
            Shelfs = new List<Shelf>();
        }
    }
}
