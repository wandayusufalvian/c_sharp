using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Rack
    {
        public int RackId;
        public List<Column> Columns { get; set; }
        
        public Rack(int rackid)
        {
            RackId = rackid;
            Columns = new List<Column>();
        }

        public void CreateRack(List<int> IdColumn, Dictionary<int, List<List<int>>> map)
        {
            /*
             * Inisiliasi rack dengan data yg hard-coded.
             * Input = list dari id column dan Dictionary(key=id column, value=list dengan elemen 
             * berupa shelf. Shelf berupa list dengan 2 elemen yaitu barcode dan theta 
             */ 
            foreach (int i in IdColumn)
            {

                Columns.Add(new Column(i));
            }
            foreach (Column col in Columns)
            {
                int idkol = col.IdColumn;
                List<List<int>> IdShelfSize = map[idkol];
                int panjang = IdShelfSize.Count;
                for (int i = 0; i < panjang; i++)
                {
                    col.Shelfs.Add(new Shelf(IdShelfSize[i][0], IdShelfSize[i][1]));
                }
            }
        }
      
        private (int,int) SearchEmptyShelf(int size)
        {
            /*Goal= mencari empty shelf. Fungsi PutLabware harus menjalankan fungsi ini terlebih dahulu 
             *return (index column,index shelf)= jika ketemu shelf kosong 
             *return (-1,-1) = jika tidak ada shelf kosong 
             *khusus size kecil, jika tidak ada small shelf maka search dari column terakhir untuk empty big shelf.  
            */
            //small shelf
            if (size == 1)
            {
                //search yg small size dulu
                for (int i = 0; i < Columns.Count; i++)
                {
                    for (int j = 0; j < Columns[i].Shelfs.Count; j++)
                    {
                        
                        if (Columns[i].Shelfs[j].IdSize == 1 && Columns[i].Shelfs[j].barcode.Equals("-----"))
                        {
                            return (i, j);
                        }
                    }
                }

                //kalau tidak ada yg small size, search yg big size di mulai dari last column 
                for (int i = Columns.Count-1; i >= 0; i--)
                {
                    for (int j = Columns[i].Shelfs.Count-1; j >= 0; j--)
                    {
                        if (Columns[i].Shelfs[j].IdSize == 2 && Columns[i].Shelfs[j].barcode.Equals("-----"))
                        {
                            return (i, j);
                        }
                    }
                }
            }
            //big shelf
            else if (size == 2)
            {
                for(int i = 0; i < Columns.Count; i++)
                {
                    for (int j = 0; j < Columns[i].Shelfs.Count; j++)
                    {
                        if(Columns[i].Shelfs[j].IdSize==2 && Columns[i].Shelfs[j].barcode.Equals("-----"))
                        {
                            return (i, j);
                        }
                    }
                }
            }
           
            return (-1,-1); // TODO
        }
 
        public (int,int) SearchLabware(String barcodess)
        {
            /* Goal = mencari lokasi labware berdasarkan nilai barcode-nya  
             * return (id column, id shelf) = jika ketemu 
             * return (-1,-1) = jika tidak ketemu 
             */
            for (int i = 0; i < Columns.Count; i++)
            {
                for (int j = 0; j < Columns[i].Shelfs.Count; j++)
                {
                    if (Columns[i].Shelfs[j].barcode.Equals(barcodess))
                    {
                        return (Columns[i].IdColumn, Columns[i].Shelfs[j].IdShelf);
                    }
                }
            }
            return (-1, -1); 
        }

        public int PutLabware(int size, string barcode)
        {
            /* Goal = meletakkan labware pada shelf kosong sesuai ukuran labware
             * Cek terlebih dahulu empty shelf menggunakan fungsi SearchEmptyShelf
             * return 0 = jika tidak ada empty shelf 
             * return 1 = jika ada empty shelf dan labware berhasil diisi ke shelf kosong tsb 
             */
            var cek = SearchEmptyShelf(size); //cari empty shelf sesuai ukuran labware 
            if(cek.Item1==-1 && cek.Item2 == -1)
            {
                return 0;  
            }
            else
            {
                Columns[cek.Item1].Shelfs[cek.Item2].barcode = barcode;
                return 1;
            }

        }
  
        public String TakeLabware(int shelf_id)
        {
            /* Goal = ambil labware dari shelf
             * input= id shelf. 
             * return null= shelf valid tp kosong
             * return barcode da set barcode='-----'= shelf valid dan ada labware 
             * throw argumentexception= shelfid tdk valid 
             * 
             */
            foreach (Column col in Columns)
            {
                foreach (Shelf shl in col.Shelfs)
                {
                    if (shl.IdShelf == shelf_id)
                    {
                        if (shl.barcode.Equals("-----"))
                        {
                            return null;
                        }
                        else
                        {
                            String barcode = shl.barcode;
                            return barcode;
                        }
                    }
                }
            }
            //jika shelf id tidak valid
            throw new ArgumentException();
        }

        public int ClearLabware(int shelf_id)
        {
            /* Goal = mengosongkan shelf, tidak perduli kondisi awal shelf kosong atau tidak 
             * input = shelf id dari shelf yg ingin dikosongkan
             * set barcode = '-----' dan return 1 = shelf id valid  
             * throw argumentexception= shelf id tidak valid 
             */
            foreach (Column col in Columns)
            {
                foreach (Shelf shl in col.Shelfs)
                {
                    if (shl.IdShelf == shelf_id)
                    {
                        shl.barcode = "-----";
                        return 1; 
                    }
                }
            }
            //jika shelf id tidak valid
            throw new ArgumentException();
        }
        
        public void ShowRakcs()
        {
            /*
             * print racks
             */
            foreach (Column col in Columns)
            {
                int a = col.IdColumn;
                Console.Write(a + " : ");
                List<Shelf> ls = col.Shelfs;
                for (int j = 0; j < ls.Count; j++)
                {
                    int b = ls[j].IdShelf;
                    int s = ls[j].IdSize;
                    String br = ls[j].barcode;
                    int t = ls[j].theta;
                    Console.Write("[(" + b + "," + s + "):(" + br + "," + t + ")], ");
                }
                Console.WriteLine();

            }
        }

        public void IsiPenuhShelfBesar()
        {
            /*
             * isi seluruh shelf ukuran besar 
             */
            foreach(Column col in Columns)
            {
                foreach(Shelf shl in col.Shelfs)
                {
                    if (shl.IdSize == 2)
                    {
                        shl.AddBarcode("XXXXX"); 
                    }
                }
            }
        }

        public void IsiPenuhShelfKecil()
        {
            /*
             * Isi seluruh shelf ukuran kecil
             */
            foreach (Column col in Columns)
            {
                foreach (Shelf shl in col.Shelfs)
                {
                    if (shl.IdSize == 1)
                    {
                        shl.AddBarcode("XXXXX");
                    }
                }
            }
        }
    }
     
}
