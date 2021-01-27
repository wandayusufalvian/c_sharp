using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ConsoleApp1
{
    public class Column
    {
        [Key]
        public int ColumnID { get; set; }


        //satu kolom bisa punya banyak shelfs
        public ICollection<Shelf> Shelfs { get; set; }

    }
}
