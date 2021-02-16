using ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Tests
    {
        Service srv = null;

        [SetUp]

        public void Setup()
        {

            using (var db = new RackContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                db.Add(new Column
                {
                    ColumnID = 1,
                    Shelfs = new List<Shelf>
                                    {
                                        new Shelf{ShelfID=1,Type=100,Barcode="",Theta=0},
                                        new Shelf{ShelfID=2,Type=200,Barcode="",Theta=0},
                                        new Shelf{ShelfID=3,Type=200,Barcode="",Theta=0}
                                    }
                });
                db.Add(new Column
                {
                    ColumnID = 2,
                    Shelfs = new List<Shelf>
                                    {
                                        new Shelf{ShelfID=4,Type=200,Barcode="",Theta=0},
                                        new Shelf{ShelfID=6,Type=100,Barcode="",Theta=0}
                                    }
                });
                db.Add(new Column
                {
                    ColumnID = 3,
                    Shelfs = new List<Shelf>
                                    {
                                        new Shelf{ShelfID=7,Type=100,Barcode="",Theta=0},
                                        new Shelf{ShelfID=8,Type=200,Barcode="",Theta=0},
                                        new Shelf{ShelfID=5,Type=200,Barcode="",Theta=0}
                                    }
                });
                db.SaveChanges();
                srv = new Service();
            }
        }



        /* SKENARIO :
         * StoreLabwareTest mengimplementasikan 3 method : GetEmptyShelfId,StoreLabware,GetLabwareShelfId
         * 
         * StoreLabwareTest_1 = store labware ketika rack masih kosong => [untuk plate dan trough] 
         * StoreLabwareTest_2 = store labware ketika rack terisi sebagian (isi manual) => [untuk plate dan trough] 
         * StoreLabwareTest_3 = store labware ketika rack masih kosong + excludeShelfIds => [untuk plate dan trough] 
         * StoreLabwareTest_4 = store labware trough ketika shelf trough terisi penuh (penuh ditentukan dari excludeShelfIds)
         * StoreLabwareTest_5 = store labware plate ketika shelf plate terisi penuh (penuh ditentukan dari excludeShelfIds)
         * StoreLabwareTest_6 = store labware plate ketika seluruh shelf terisi penuh (penuh ditentukan dari excludeShelfIds)
         * 
         * TakeLabwareTest_1 = barcode yg dicari ketemu
         * TakeLabwareTest_2 = barcode yg dicari tdk ketemu
         * 
         * ClearLabwareTest_1 = mengosongkan shelf yg sebelumnya sudah kosong
         * ClearLabwareTest_2 = mengosongkan shelf yg sebelumnya sudah terisi  
         */

        [Test]
        [TestCase(200, "AAAA1", 2)]
        [TestCase(100, "AAAA2", 1)]
        public void StoreLabwareTest_1(int ShelfTypeId, string barcode, int id_shelf)
        {
            int[] excludeShelfIds = { };
            int emptyShelfId = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
            
        }

        [Test]
        [TestCase(200, "AAAA3", 3)]
        [TestCase(100, "AAAA4", 6)]
        public void StoreLabwareTest_2(int ShelfTypeId, string barcode, int id_shelf)
        {
            
            int st = 200;
            int st2 = 100;
            int st3 = ShelfTypeId;
            int[] excludeShelfIds = { };
            //keisi sebagian
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            srv.StoreLabware("AAAA1", emptyShelfId);
            int emptyShelfId2 = srv.GetEmptyShelfId(st2, excludeShelfIds);
            srv.StoreLabware("AAAA2", emptyShelfId2);
            //isi baru
            int emptyShelfId3 = srv.GetEmptyShelfId(st3, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId3);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        [Test]
        [TestCase(200, "AAAA5",  4)]
        [TestCase(100, "AAAA6", 7)]
        public void StoreLabwareTest_3(int ShelfTypeId, string barcode, int id_shelf)
        {
            int[] excludeShelfIds = { 1, 2, 3, 6 };
            int st = ShelfTypeId;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_4()
        {
            int[] excludeShelfIds = { 1,6,7 };
            int st = 100;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_5()
        {
            int[] excludeShelfIds = { 2,3,4,5,8 };
            int st = 200;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(7, emptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_6()
        {
            int[] excludeShelfIds = { 1, 2, 3, 4, 5, 6, 7, 8 };
            int st = 200;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }

        [Test]
        
        public void TakeLabwareTest_1()
        {
            //store dulu
            string barcode = "XXXX1"; 
            srv.StoreLabware(barcode, 8);
            //kemudian take 
            srv.TakeLabware(barcode);
            int shelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(-1, shelfId);
        }

        [Test]
        [TestCase("AAAA1")]
        [TestCase("AAAA2")]
        [TestCase("AAAA3")]
        public void TakeLabwareTest_2(string barcode)
        {
            //srv.TakeLabware(barcode);
            Assert.Throws<System.ArgumentException>(() => srv.TakeLabware(barcode));
        }

        [Test]
        [TestCase(1,100)]
        [TestCase(4,200)]
        public void ClearLabwareTest_1(int shelfid,int shelfTypeId)
        {
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6,7,8 };
            int st = shelfTypeId;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }

        [Test]
        [TestCase(1, 100)]
        [TestCase(4, 200)]
        public void ClearLabwareTest_2(int shelfid, int shelfTypeId)
        {
            //isi dulu
            srv.StoreLabware("NNNN1",1);
            srv.StoreLabware("NNNN2", 4);
            
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };
            int st = shelfTypeId;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }
    }
}