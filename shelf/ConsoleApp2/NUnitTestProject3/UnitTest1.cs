using ConsoleApp3;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnitTestProject3
{
    public class Tests
    {
        Service srv = null;
        Column col1 = null;
        Column col2 = null;
        Column col3 = null;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
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
            List<Shelf> ListShelf1 = new List<Shelf> { shelf1, shelf2, shelf3 };
            List<Shelf> ListShelf2 = new List<Shelf> { shelf4, shelf6 };
            List<Shelf> ListShelf3 = new List<Shelf> { shelf7, shelf8, shelf5 };
            col1 = new Column() { ColumnID = 1, Shelfs = ListShelf1 };
            col2 = new Column() { ColumnID = 2, Shelfs = ListShelf2 };
            col3 = new Column() { ColumnID = 3, Shelfs = ListShelf3 };
            srv = new Service();
        }

        [SetUp]
        public void SetUp()
        {
            using (var db = new RackContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();
                db.Columns.Add(col1);
                db.Columns.Add(col2);
                db.Columns.Add(col3);
                db.SaveChanges();
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
        public static IEnumerable InputParameter_StoreLabwareTest_1
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA1", 2);
                yield return new TestCaseData(ShelfType.Trough, "AAAA2", 1);
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_1")]
        public void StoreLabwareTest_1(ShelfType ShelfTypeId, string barcode,int id_shelf)
        {

            int[] excludeShelfIds = { };
            int emptyShelfId = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        public static IEnumerable InputParameter_StoreLabwareTest_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA3", 3);
                yield return new TestCaseData(ShelfType.Trough, "AAAA4", 6);
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_2")]
        public void StoreLabwareTest_2(ShelfType ShelfTypeId, string barcode, int id_shelf)
        {
            ShelfType st = ShelfType.Plate;
            ShelfType st2 = ShelfType.Trough;
            int[] excludeShelfIds = { };
            //keisi sebagian
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            srv.StoreLabware("AAAA1", emptyShelfId);
            int emptyShelfId2 = srv.GetEmptyShelfId(st2, excludeShelfIds);
            srv.StoreLabware("AAAA2", emptyShelfId2);
            //isi baru
            int emptyShelfId3 = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId3);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        public static IEnumerable InputParameter_StoreLabwareTest_3
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", 4);
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", 7);
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_3")]
        public void StoreLabwareTest_3(ShelfType ShelfTypeId, string barcode, int id_shelf)
        {
            int[] excludeShelfIds = { 1, 2, 3, 6 };
            int emptyShelfId = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_4()
        {
            int[] excludeShelfIds = { 1, 6, 7 };
            ShelfType st = ShelfType.Trough;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_5()
        {
            int[] excludeShelfIds = { 2, 3, 4, 5, 8 };
            ShelfType st = ShelfType.Plate;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(7, emptyShelfId);
        }

        [Test]
        public void StoreLabwareTest_6()
        {
            int[] excludeShelfIds = { 1, 2, 3, 4, 5, 6, 7, 8 };
            ShelfType st = ShelfType.Plate;
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

        public static IEnumerable InputParameter_ClearLabwareTest
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, 4);
                yield return new TestCaseData(ShelfType.Trough, 1);
            }
        }


        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest")]
        public void ClearLabwareTest_1(ShelfType shelfTypeId,int shelfid)
        {
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };
            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest")]
        public void ClearLabwareTest_2(ShelfType shelfTypeId, int shelfid)
        {
            //isi dulu
            srv.StoreLabware("NNNN1", 1);
            srv.StoreLabware("NNNN2", 4);
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };
            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }
    }
}