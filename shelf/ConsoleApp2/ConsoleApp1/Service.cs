using System;
using System.Linq;


namespace ConsoleApp1
{
    public class Service : LabwareInterface
    {
        private int SearchEmptyShelf(int size)
        {
            /*Goal= mencari empty shelf.
             *return ShelfID= jika ketemu shelf kosong 
             *return -1 = jika tidak ada shelf kosong 
             *khusus size kecil, jika tidak ada small shelf maka search dari column terakhir untuk empty big shelf.  
            */
            using (var db = new RackContext())
            {   //penting ubah DBSet ke List, jika tidak nanti Error Null Reference
                var Laci = db.Shelfs.ToList();
                //penting diurutkan terlebih dahulu karena data di DBSet tidak urut berdasarkan ID, namun acak.
                var Kolom = db.Columns.OrderBy(o=>o.ColumnID).ToList();
                //small shelf
                if (size == 1)
                {
                    foreach(Column kol in Kolom)
                    {
                        foreach(Shelf shl in kol.Shelfs.OrderBy(o=>o.ShelfID))
                        {
                            if (shl.ShelfSize == 1 && shl.Barcode.Equals("-----"))
                            {
                                int shlID = shl.ShelfID;
                                return shlID;
                            }
                        }
                    }
                    
                    //kalau tidak ada yg small size, search yg big size di mulai dari last column  
                    Kolom.Reverse();
                    foreach (Column kol_balik in Kolom)
                    {
                        foreach(Shelf shl in kol_balik.Shelfs.OrderBy(o => o.ShelfID))
                        {
                            if(shl.ShelfSize == 2 && shl.Barcode.Equals("-----"))
                            {
                                int shlID = shl.ShelfID;
                                return shlID;
                            }
                        }
                    }


                }
                //big shelf
                else if (size == 2)
                {
                    foreach (Column kol in Kolom)
                    {
                        foreach (Shelf shl in kol.Shelfs.OrderBy(o => o.ShelfID))
                        {
                            if (shl.ShelfSize == 2 && shl.Barcode.Equals("-----"))
                            {
                                int shlID = shl.ShelfID;
                                return shlID;
                            }
                        }
                    }
                }
            }
            return -1; // TODO
        }

        public int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds)
        {   /* Goal : cari first shelf
             * 
             */
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                int sukses = 0;
                foreach (Shelf shl in Laci)
                {
                    if (shl.Barcode.Equals(""))
                    {
                        throw new ArgumentException("Empty-Shelf");
                    }
                    else
                    {
                        shl.Barcode = "";
                        db.SaveChanges();
                        sukses = 1;
                    }
                }
                //jika barcode tidak valid 
                if (sukses == 0)
                {
                    throw new ArgumentException("Invalid-Barcode");
                }
            }

            return 0; 
        }

        //ubah jadi GetLabwareShelfId
        public int GetLabwareShelfId(string barcode)
        {
            /* Goal = mencari lokasi labware berdasarkan nilai barcode-nya  
             * return id shelf = jika ketemu 
             * return -1 = jika tidak ketemu 
             */
            using (var db=new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                var Kolom = db.Columns.ToList();
                foreach (Column kol in Kolom)
                {
                    foreach (Shelf shl in kol.Shelfs)
                    {
                        if (shl.Barcode.Equals(barcode))
                        {
                            int shlID = shl.ShelfID;
                            return shlID;
                        }
                    }
                }
            }
            return -1;
        }

        public int PutLabware(int size, string barcode)
        {
            /* Goal = meletakkan labware pada shelf kosong sesuai ukuran labware
             * Cek terlebih dahulu empty shelf menggunakan fungsi SearchEmptyShelf
             * return 0 = jika tidak ada empty shelf 
             * return 1 = jika ada empty shelf dan labware berhasil diisi ke shelf kosong tsb 
             */
             
            using(var db=new RackContext())
            {
                var cek = SearchEmptyShelf(size); //cari empty shelf sesuai ukuran labware
                Console.WriteLine(cek);
                if (cek== -1)
                {
                    return 0;
                }
                else
                {
                    var CariShelf = db.Shelfs.Find(cek);
                    CariShelf.Barcode = barcode;
                    db.SaveChanges();
                    return 1;
                }
            }
        }

        public void StoreLabware(string barcode, int shelfId)
        {
            return 1;
        }

        //harus diganti karena inputnya string, kalau kosong jadinya throw exception 
        //kalau id gk valid??? 
        public void TakeLabware(string barcode) 
        {
            /* Goal = ambil labware dari shelf sehingga shelf jadi kosong 
             * Asumsi = barcode valid 
             * jika shelf ada isinya-> set jadi empty string
             * jika shelf tdk ada isinya -> throw exception  
             * jika barcode tdk valid -> throw exception
             */
            using(var db=new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                int sukses = 0; 
                foreach(Shelf shl in Laci)
                {
                    if (shl.Barcode.Equals(""))
                    {
                        throw new ArgumentException("Empty-Shelf");
                    }
                    else
                    {
                        shl.Barcode = "";
                        db.SaveChanges();
                        sukses = 1;
                    }
                }
                //jika barcode tidak valid 
                if (sukses == 0)
                {
                    throw new ArgumentException("Invalid-Barcode");
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
                int sukses = 0;
                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    if (shl.ShelfID == shelfId)
                    {
                        shl.Barcode = "";
                        db.SaveChanges();
                        sukses = 1;
                    }
                }
                if (sukses == 0)
                {
                    throw new ArgumentException("Invalid-ShelfId");
                }
            }
            
        }

        public void ClearAllLabware()
        {
            /* Goal = mengosongkan seluruh shelfs dari labware 
             */
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    ClearLabware(shl.ShelfID);
                }
            }
        }

        public void IsiPenuhShelfBesar()
        {
            /*
             * isi seluruh shelf ukuran besar 
             */
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    if (shl.ShelfSize==2)
                    {
                        shl.Barcode = "XXXXX";
                    }
                }
                db.SaveChanges();
            }
        }

        public void IsiPenuhShelfKecil()
        {
            /*
             * Isi seluruh shelf ukuran kecil
             */
            using (var db = new RackContext())
            {
                var Laci = db.Shelfs.ToList();
                foreach (Shelf shl in Laci)
                {
                    if (shl.ShelfSize == 1)
                    {
                        shl.Barcode = "XXXXX";
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
