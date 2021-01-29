using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
              /*
            using (var db = new RackContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
            }
            */
            /*
            
            ShelfType trough = ShelfType.Trough;
            ShelfType plate = ShelfType.Plate;
            Shelf shelf1 = new Shelf() { ShelfID = 1, Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf2 = new Shelf() { ShelfID = 2, Type = plate, Barcode = "", Theta = 0 };
            Shelf shelf3 = new Shelf() { ShelfID = 3, Type = plate, Barcode = "", Theta = 0 };
            Shelf shelf4 = new Shelf() { ShelfID = 4, Type = plate, Barcode = "", Theta = 0 };
            Shelf shelf5 = new Shelf() { ShelfID = 5, Type = plate, Barcode = "", Theta = 0 };
            Shelf shelf6 = new Shelf() { ShelfID = 6, Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf7 = new Shelf() { ShelfID = 7, Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf8 = new Shelf() { ShelfID = 8, Type = plate, Barcode = "", Theta = 0 };
            List<Shelf> ListShelf1 = new List<Shelf> { shelf1 , shelf2 , shelf3 };
            List<Shelf> ListShelf2 = new List<Shelf> { shelf4, shelf6 };
            List<Shelf> ListShelf3 = new List<Shelf> { shelf7, shelf8, shelf5 };
            Column col1 = new Column() { ColumnID= 1, Shelfs=ListShelf1 };
            Column col2 = new Column() { ColumnID = 2, Shelfs = ListShelf2 };
            Column col3 = new Column() { ColumnID = 3, Shelfs = ListShelf3 };

            using (var db=new RackContext())
            {
                db.Columns.Add(col1);
                db.Columns.Add(col2);
                db.Columns.Add(col3);
                db.SaveChanges();
            }
            */
            /*
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.Include(p => p.Type).ToList();

                //var Ty = db.ShelfTypes.ToList();
                foreach(Shelf laci in Laci)
                {
                    Console.WriteLine(laci.ShelfID);
                }
            }
            
            */

            //int a=ShelfType.Enum.Trough;
            //ShelfType b=ShelfType.Trough;
            //Console.WriteLine(a);
            //Console.WriteLine(b.Id);
            /*
            Service srv = new Service();
            int[] arr = { };
            int x=srv.GetEmptyShelfId(ShelfType.Trough,arr);
            srv.StoreLabware("AAAA2", x);
            Console.WriteLine(x);
            */
        }
    }
}
