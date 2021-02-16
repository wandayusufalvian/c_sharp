using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            /*
             * ASUMSI 
             * - barcode tiap labware unik 
             * - id shelf di tiap rack unik dan tanggungjawab user yg hard-coded id shelf untuk memastikan id shelf tsb unik. 
             */
            //INISIALISASI RACK 
            /*
            Rack r1 = new Rack(1);
            

            //HARD CODED DATA 
            //rack1 
            List<int> IdColumn1 = new List<int> { 1, 2, 3 };
            Dictionary<int, List<List<int>>> map = new Dictionary<int, List<List<int>>>
            {
                {1,new List<List<int>>{ new List<int>{1,2}, new List<int> {2,1}, new List<int> {3,1}}},
                {2,new List<List<int>>{ new List<int>{4,1}, new List<int> {6,2}}},
                {3,new List<List<int>>{ new List<int>{7,2}, new List<int> {8,1}, new List<int> {9,1}}},
            };

            //TESTING 1 : create and show racks 
            Console.WriteLine("Kondisi awal shelf:");
            r1.CreateRack(IdColumn1, map);
            r1.ShowRakcs();

            //TESTING 2 : put small labware and big labware for first time 
            String barcode = "AB123";
            int size = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:"+barcode+",size:"+size+"\n"); 
            r1.PutLabware(size, barcode);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode2 = "AB567";
            int size2 = 2;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode2 + ",size:" + size2 + "\n");            
            r1.PutLabware(size2, barcode2);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode3 = "AB234";
            int size3 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode3 + ",size:" + size3 + "\n");
            r1.PutLabware(size3, barcode3);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode4 = "AC234";
            int size4 = 2;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode4 + ",size:" + size4 + "\n");
            r1.PutLabware(size4, barcode4);
            r1.ShowRakcs();
            Console.WriteLine();

            //TESTING 3 : seach labware
            String barc = "AB567";
            String barc2 = "AB569";
            var y = r1.SearchLabware(barc);
            Console.WriteLine("[SEARCH LABWARE]=> Barcode: " + barc);
            if(y.Item1==-1 && y.Item2 == -1)
            {
                Console.WriteLine("[WARNING] : labware tidak ada"); 
            }
            else
            {
                Console.WriteLine("Labware ada");
                Console.WriteLine("Lokasi=> "+"kolom ke-"+y.Item1+",shelf ke-"+y.Item2);

            }
            Console.WriteLine();
            var y2 = r1.SearchLabware(barc2);
            Console.WriteLine("[SEARCH LABWARE]=> Barcode: " + barc2);
            if (y2.Item1 == -1 && y2.Item2 == -1)
            {
                Console.WriteLine("[WARNING] : labware tidak ada");
            }
            else
            {
                Console.WriteLine("Labware ada");
                Console.WriteLine("Lokasi=> " + "kolom ke-" + y2.Item1 + ",shelf ke-" + y2.Item2);

            }
            Console.WriteLine();

            //TEST 4: take labware 
            //id shelf valid dan gk kosong 
            int id_shelf = 2;
            Console.WriteLine("[TAKE LABWARE]=> id_shelf: " + id_shelf);
            Console.WriteLine(r1.TakeLabware(id_shelf));
            Console.WriteLine();
            //id shelf valid dan kosong 
            int id_shelf2 = 9;
            Console.WriteLine("[TAKE LABWARE]=> id_shelf: " + id_shelf2);
            Console.WriteLine(r1.TakeLabware(id_shelf2));
            Console.WriteLine();
            //id shelf gk valid
            int id_shelf3 = 900;
            Console.WriteLine("[TAKE LABWARE]=> id_shelf: " + id_shelf3);
            Console.WriteLine(r1.TakeLabware(id_shelf3));
            Console.WriteLine();

            //TEST 5: clear shelf
            //VALID ada isi 
            int shelfid = 2;
            Console.WriteLine("[CLEAR SHELF]=> id_shelf: " + shelfid);
            r1.ClearLabware(shelfid);
            r1.ShowRakcs();
            Console.WriteLine();

            //VALID tp kosong
            int shelfid2 = 2;
            Console.WriteLine("[CLEAR SHELF]=> id_shelf: " + shelfid2);
            r1.ClearLabware(shelfid2);
            r1.ShowRakcs();
            Console.WriteLine();

            //GK VALID
            int shelfid3 = 222;
            Console.WriteLine("[CLEAR SHELF]=> id_shelf: " + shelfid3);
            r1.ClearLabware(shelfid3);
            r1.ShowRakcs();
            Console.WriteLine();

            //TEST 6 : put labware besar sampai penuh 
            String barcode6 = "AD123";
            int size6 = 2;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode6 + ",size:" + size6 + "\n");
            r1.PutLabware(size6, barcode6);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode7 = "AE123";
            int size7 = 2;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode7 + ",size:" + size7 + "\n");
            r1.PutLabware(size7, barcode7);
            r1.ShowRakcs();
            Console.WriteLine();


            //TEST 7 : put labware kecil sampai penuh 
            // sisakan shelf besar untuk menunjukkan yg kecil bisa masuk ke shelf besar 
            String barcode8 = "DE123";
            int size8 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode8 + ",size:" + size8 + "\n");
            r1.PutLabware(size8, barcode8);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode9 = "FE123";
            int size9 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode9 + ",size:" + size9 + "\n");
            r1.PutLabware(size9, barcode9);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode10 = "DE456";
            int size10 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode10 + ",size:" + size10 + "\n");
            r1.PutLabware(size10, barcode10);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode11 = "GE456";
            int size11 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode11 + ",size:" + size11 + "\n");
            r1.PutLabware(size11, barcode11);
            r1.ShowRakcs();
            Console.WriteLine();

            
            String barcode12 = "GH123";
            int size12 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode12 + ",size:" + size12 + "\n");
            r1.PutLabware(size12, barcode12);
            r1.ShowRakcs();
            Console.WriteLine();

            //clear shelf besar yg terakhir 
            int shelfid11 = 7;
            Console.WriteLine("[CLEAR SHELF]=> id_shelf: " + shelfid11);
            r1.ClearLabware(shelfid11);
            r1.ShowRakcs();
            Console.WriteLine();

            //clear shelf besar yg pertama 
            int shelfid12 = 1;
            Console.WriteLine("[CLEAR SHELF]=> id_shelf: " + shelfid12);
            r1.ClearLabware(shelfid12);
            r1.ShowRakcs();
            Console.WriteLine();

            //karena sudah ada shelf kosong, lanjut isi yg lagi yg small labware sampai penuh semua

            String barcode13 = "GH123";
            int size13 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode13 + ",size:" + size13 + "\n");
            r1.PutLabware(size13, barcode13);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode14 = "GF123";
            int size14 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode14 + ",size:" + size14 + "\n");
            r1.PutLabware(size14, barcode14);
            r1.ShowRakcs();
            Console.WriteLine();

            String barcode15 = "HF123";
            int size15 = 1;
            Console.Write("[PUT LABWARE]=> " + "Barcode:" + barcode15 + ",size:" + size15 + "\n");
            r1.PutLabware(size15, barcode15);
            r1.ShowRakcs();
            Console.WriteLine();
            
            */
        }
    }
}
