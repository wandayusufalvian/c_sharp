using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ConsoleApp3
{
    public class Column
    {
        [Key]
        public int ColumnID { get; set; }


        //satu kolom bisa punya banyak shelfs
        public ICollection<Shelf> Shelfs { get; set; }

    }
}
