using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Shelf
    {
        public int IdShelf { get; set; }
        //1=kecil,2=besar 
        public int IdSize { get; set; }
        public String barcode { get; set; }
        public int theta { get; set; }

        public Shelf(int id,int size,String barcods="-----",int thetas=0)
        {
            IdShelf = id;
            IdSize = size;
            barcode = barcods;
            theta = thetas; 
        }

        public void AddBarcode(String barkode,int thetas=0)
        {
            barcode = barkode;
            theta = thetas; 
        }

    }
}
