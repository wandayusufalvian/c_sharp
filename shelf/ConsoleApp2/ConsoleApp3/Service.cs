using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace ConsoleApp3
{
    public class Service : LabwareInterface
    {   
        public int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds)
        {
            /*Goal= mencari id dari first empty shelf selain dari array excludeShelfIds
             *return ShelfID= jika ketemu shelf kosong 
             *return -1 = jika tidak ada shelf kosong 
             *khusus plate, jika tidak ada shelf type plate maka search dari kolom terakhir untuk shelf type trough  
             *search dimulai dari kolom dengan id terkecil 
            */
            using (var db = new RackContext())
            {   
                var Laci = db.Shelfs.Include(p => p.Type).ToList();
                //jika shelf type= Plate
                if (shelfType.Id==200)
                {   
  
                    foreach(Shelf shl in Laci)
                    {
                        if (excludeShelfIds.Contains(shl.ShelfID)==false && shl.Type.Id==200 && shl.Barcode.Equals(""))
                        {
                            int shlID = shl.ShelfID;
                            return shlID;
                        }
                    }

                    
                    //kalau tidak ada yg plate, search yg trough di mulai dari kolom terakhir. 
                    Laci.Reverse();

                    foreach(Shelf shl in Laci)
                    {
                        if(excludeShelfIds.Contains(shl.ShelfID)==false && shl.Type.Id == 100 && shl.Barcode.Equals(""))
                        {
                            int shlID = shl.ShelfID;
                            return shlID;
                        }
                    }



                }
                //jika shelf type = Trough
                else if (shelfType.Id==100)
                {
                    foreach (Shelf shl in Laci)
                    {
                        if (excludeShelfIds.Contains(shl.ShelfID)==false && shl.Type.Id == 100 && shl.Barcode.Equals(""))
                        {
                            int shlID = shl.ShelfID;
                            return shlID;
                        }
                    }
                }
            }
            return -1; 
        }

        public int GetLabwareShelfId(string barcode)
        {
            /* Goal = mencari shelf id dari labware berdasarkan nilai barcode-nya 
             * Asumsi = barcode pasti valid 
             * return shelf id = jika ketemu 
             * return -1 = jika tidak ketemu 
             */
            using (var db=new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    if (shl.Barcode.Equals(barcode))
                    {
                        int shlID = shl.ShelfID;
                        return shlID;
                    }
                }
                return -1;
            }
        }

        public void StoreLabware(string barcode, int shelfId)
        {
            /* Goal = meletakkan labware pada shelf kosong
             * Asumsi = ShelfId diperoleh dari fungsi GetEmptyShelfId dan user sudah menyesuaikan shelftype dari shelf dan labware-nya
             * shelfId pasti valid 
             * proses cari shelf tetap iterasi karena bisa jadi shelfId nilainya tidak urut dan lompat
             */
            using(var db=new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                foreach(Shelf shl in Laci)
                {
                    if (shl.ShelfID==shelfId)
                    {
                        shl.Barcode = barcode;
                        db.SaveChanges();
                        break; 
                    }
                }
            }
        }

        public void TakeLabware(string barcode) 
        {
            /* Goal = kosongkan shelf jika punya labware dengan barcode sesuai input
             * Asumsi = barcode valid 
             * jika barcode ada-> set shelf-nya jadi empty
             * jika barcode tdk ada-> throw exception
             */
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                int sukses = 0; 
                foreach (Shelf shl in Laci)
                {
                    if (shl.Barcode.Equals(barcode))
                    {
                        shl.Barcode = "";
                        db.SaveChanges();
                        sukses = 1;
                        break;
                    }
                }
                if (sukses == 0)
                {
                    throw new ArgumentException();
                }
            }
        }

        public void ClearLabware(int shelfId)
        {
            /* Goal = mengosongkan shelf, tidak perduli kondisi awal shelf kosong atau tidak.
             * Sama dengan takelabware, bedanya tdk throw exception jika kosong.
             */
            using (var db = new RackContext())
            {

                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    if (shl.ShelfID == shelfId)
                    {
                        shl.Barcode = "";
                        db.SaveChanges();
                        break;
                    }
                }
            }
        }

    }
}
