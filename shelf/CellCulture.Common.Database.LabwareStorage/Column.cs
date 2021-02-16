using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace CellCulture.Common.Database.LabwareStorage
{
    public class Column
    {
        [Key]
        public int ColumnID { get; set; }


        //satu kolom bisa punya banyak shelfs
        public ICollection<Shelf> Shelfs { get; set; }

    }
}
