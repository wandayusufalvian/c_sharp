using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    //EF implementation dari ConsoleApp2 
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new RackContext())
            {
                //inisialisasi column dan shelf instance 
                var col1 = new Column() { ColumnID=1};
                var col2 = new Column() { ColumnID =2};
                var col3 = new Column() { ColumnID =3};
                var shl1 = new Shelf() { ShelfID = 1, ShelfSize = 2, Barcode = "-----", Theta = 0 };
                var shl2 = new Shelf() { ShelfID = 2, ShelfSize = 1, Barcode = "-----", Theta = 0 };
                var shl3 = new Shelf() { ShelfID = 3, ShelfSize = 1, Barcode = "-----", Theta = 0 };
                var shl4 = new Shelf() { ShelfID = 4, ShelfSize = 1, Barcode = "-----", Theta = 0 };
                var shl5 = new Shelf() { ShelfID = 5, ShelfSize = 1, Barcode = "-----", Theta = 0 };
                var shl6 = new Shelf() { ShelfID = 6, ShelfSize = 2, Barcode = "-----", Theta = 0 };
                var shl7 = new Shelf() { ShelfID = 7, ShelfSize = 2, Barcode = "-----", Theta = 0 };
                var shl8 = new Shelf() { ShelfID = 8, ShelfSize = 1, Barcode = "-----", Theta = 0 };

                //add column and shelf instance to context 
                ctx.Columns.Add(col1);
                ctx.Columns.Add(col2);
                ctx.Columns.Add(col3);
                ctx.Shelfs.Add(shl1);
                ctx.Shelfs.Add(shl2);
                ctx.Shelfs.Add(shl3);
                ctx.Shelfs.Add(shl4);
                ctx.Shelfs.Add(shl5);
                ctx.Shelfs.Add(shl6);
                ctx.Shelfs.Add(shl7);
                ctx.Shelfs.Add(shl8);

                //link column dengan shelf 

                col1.Shelfs = new List<Shelf>();
                col2.Shelfs = new List<Shelf>();
                col3.Shelfs = new List<Shelf>();
                col1.Shelfs.Add(shl1);
                col1.Shelfs.Add(shl2);
                col1.Shelfs.Add(shl3);
                col2.Shelfs.Add(shl4);
                col2.Shelfs.Add(shl6);
                col3.Shelfs.Add(shl7);
                col3.Shelfs.Add(shl8);
                col3.Shelfs.Add(shl5);
                ctx.SaveChanges();
               

                Console.WriteLine("Data Saved");


            }
        }
    }
}
