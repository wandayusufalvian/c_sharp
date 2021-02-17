using System;
using System.ComponentModel.DataAnnotations;

namespace CellCulture.Common.Database.LabwareStorage
{
    public class Shelf
    {
        [Key]
        public int ShelfID { get; set; }
        public string ShelfName { get; set; }
        public ShelfType Type { get; set; }
        public String Barcode { get; set; }
        public int Theta { get; set; }

        //satu shelf ke satu kolom saja
        public Column column { get; set; }
    }
}
