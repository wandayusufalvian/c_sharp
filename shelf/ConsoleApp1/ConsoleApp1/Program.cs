using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Rack 
    {
        int cols; // banyak kolom dalam satu rack 
        int rows; // banyak baris dalam satu rack 
        int batass; // sampai baris keberapa ukuran kotak besar terakhir. 
        List<String> [,] kotak; //rack sebagai 2D matrix dengan elemen berupa list 

        int kotak_besar = 0;//banyak kotak besar yang masuk 
        int kotak_kecil = 0; // banyak kotak kecil yang masuk 

        int total; //total kapasitas rack dinamis
        int total_besar=0; //total kapasitas rack besar dinamis
        int total_kecil=0;  //total kapasitas rack kecil dinamis

        int total_abs; //total absolute kapasitas rack
        int total_besar_abs; //total absolute kapasitas rack besar 
        int total_kecil_abs;  //total absolute kapasitas rack kecil 
        
        //Constructor 
        public Rack(int col,int row,int batas)
        {
            cols = col;
            rows = row;
            batass = batas;
            kotak = new List<String>[row, col];
            total_besar_abs = col * (batas + 1);
            total_kecil_abs = col * (rows - (batas + 1)); 
            total_abs = total_besar_abs + total_kecil_abs;
        }

        //Inisialisasi nilai awal rack 
        public void Inisialisasi()//nilai awal matrix kotak
        {
            
            for (int i = 0; i < rows; i++)
            {
                
                for (int j = 0; j < cols; j++)
                {
                    
                    kotak[i, j] = new List<String> { "-----", "0" }; 
                }
            }
        }

        //isi barcode dan theta ke dalam rack 
        public void IsiRack(String barcode,String theta)
        {
            Char size = barcode[0]; 
            if (size.Equals('B') || size.Equals('b'))
            {
                if (total_besar == total_besar_abs)
                {
                    Console.WriteLine("[WARNING] Kapasitas penuh!");
                }
                else
                {
                    int ketemu = 0;
                    //enumerate to all location. First Empty -> langsung isi 
                    for (int i = 0; i <= batass; i++)
                    {

                        for (int j = 0; j < cols; j++)
                        {
                            if (kotak[i, j][0].Equals("-----"))
                            {
                                kotak[i, j][0] = barcode;
                                kotak[i, j][1] = theta;
                                ketemu = 1;
                                total_besar = total_besar + 1;
                                kotak_besar = kotak_besar + 1;
                                break;
                            }

                        }
                        if (ketemu == 1) { break; }

                    }
                }
                
            }
            else if (size.Equals('K') || size.Equals('k'))
            {
                if (total == total_abs)
                {
                    Console.WriteLine("[WARNING] Kapasitas penuh!");
                }
                else
                {
                    //prioritas yg ukuran kecil dulu 
                    int ketemu = 0;
                    //enumerate to all location. First Empty -> langsung isi 
                    for (int i = batass + 1; i < rows; i++)
                    {

                        for (int j = 0; j < cols; j++)
                        {
                            if (kotak[i, j][0].Equals("-----"))
                            {
                                kotak[i, j][0] = barcode;
                                kotak[i, j][1] = theta;
                                ketemu = 1;
                                total_kecil = total_kecil + 1;
                                kotak_kecil = kotak_kecil + 1;
                                break;
                            }
                        }
                        if (ketemu == 1) { break; }

                    }
                    //kalau yg kecil gk ada, cek yg besar :
                    if (ketemu == 0)
                    {
                        for (int i = 0; i <= batass; i++)
                        {

                            for (int j = 0; j < cols; j++)
                            {
                                if (kotak[i, j][0].Equals("-----"))
                                {
                                    kotak[i, j][0] = barcode;
                                    kotak[i, j][1] = theta;
                                    ketemu = 1;
                                    total_besar = total_besar + 1;
                                    kotak_kecil = kotak_kecil + 1;
                                    break;
                                }
                            }
                            if (ketemu == 1) { break; }

                        }
                    }
                } 
            }
            else
            {
                Console.WriteLine("size tidak valid");
            }
        }

        //hapus barcode pada rack dengan input koordinat-nya 
        public void HapusRack(int baris,int kolom)
        {
            kotak[baris, kolom] = new List<string> { "-----", "0" };
        }

        //print visualisasi rack 
        public void Prints()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    List<String> hasil =kotak[i, j];
                    Console.Write("["+String.Join(", ", hasil)+"]");
                    //Console.Write(hasil);
                    Console.Write(" ");
                }
                if (i <= batass)
                {
                    Console.Write("B");
                }
                else
                {
                    Console.Write("K");
                }
                
                Console.WriteLine();
            }
        }

        //print nilai stats dari rack 
        public void PrintKapasitas()
        {
            total = total_besar + total_kecil;
            Console.WriteLine("Banyak kotak besar yg masuk = " + kotak_besar);
            Console.WriteLine("Banyak kotak kecil yg masuk = " + kotak_kecil);
            Console.WriteLine("Banyak kotak kecil yg masuk ke Besar= " + (kotak_kecil-total_kecil));
            Console.WriteLine("Kapasitas Besar= (" + total_besar + "/" + total_besar_abs + ")");
            Console.WriteLine("Kapasitas Kecil= (" + total_kecil + "/" + total_kecil_abs + ")");
            Console.WriteLine("Kapasitas Total= (" + total + "/" + total_abs + ")"); 
        }


    }

    class Program
    {

       

        static void Main(string[] args)
        {
            //Rack 1 -> tanpa theta 
            Rack rack1 = new Rack(2, 4, 1);
            rack1.Inisialisasi();
            rack1.IsiRack("B1234","4");
            rack1.IsiRack("B1235", "4");
            //rack1.IsiRack("B1236", "4");
            //rack1.IsiRack("B1237", "4");
            //rack1.IsiRack("B1238", "4");
            rack1.IsiRack("K1234","4");
            rack1.IsiRack("K1235", "4");
            rack1.IsiRack("K1236", "4");
            rack1.IsiRack("K1237", "4");
            rack1.IsiRack("K1238", "4");
            
            rack1.Prints();
            //rack1.HapusRack(3, 1);
            Console.WriteLine();
            //rack1.Prints();
            Console.WriteLine(); 
            //rack1.PrintKapasitas();
            //Rack 2 -> deengan theta 
            Rack rack2 = new Rack(2, 4, 1);
            //rack2.Inisialisasi();

            //------------CONSOLE--------------
            /*
            while (true)
            {
                 
                Console.WriteLine("---------");
                Console.WriteLine("Pilih perintah = ");
                Console.WriteLine("1. Input ke rack 1");
                Console.WriteLine("2. Input ke rack 2");
                Console.WriteLine("3. Hapus kotak di rack 1");
                Console.WriteLine("4. Hapus kotak di rack 2");
                Console.WriteLine("5. Print semua rack");
                Console.WriteLine("0. Keluar");
                Console.WriteLine("---------");
                Console.Write("Input perintah: ");
                int perintah = Convert.ToInt32(Console.ReadLine());
                if (perintah == 0)
                {
                    break; 
                }
                switch (perintah)
                {
                   
                    case 1:
                        Console.Write("Input barcode: ");
                        String barcode1 = Console.ReadLine();
                        String theta1 = "0";
                        rack1.IsiRack(barcode1, theta1);
                        rack1.Prints();
                        break;
                    case 2:
                        Console.Write("Input barcode: ");
                        String barcode2 = Console.ReadLine();
                        Console.Write("Input theta: ");
                        String theta2 = Console.ReadLine();
                        rack2.IsiRack(barcode2, theta2);
                        rack2.Prints();
                        break;
                    case 3:
                        Console.Write("Input kolom: ");
                        int x1 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Input baris: ");
                        int y1 = Convert.ToInt32(Console.ReadLine());
                        rack1.HapusRack(x1, y1);
                        rack1.Prints();
                        break;
                    case 4:
                        Console.Write("Input kolom: ");
                        int x2 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Input baris: ");
                        int y2 = Convert.ToInt32(Console.ReadLine());
                        rack2.HapusRack(x2, y2);
                        rack2.Prints();
                        break;
                    case 5:
                        rack1.Prints();
                        rack2.Prints();
                        break;
                }
            }
            //---------------------------------
            */

        }
    }  
}
