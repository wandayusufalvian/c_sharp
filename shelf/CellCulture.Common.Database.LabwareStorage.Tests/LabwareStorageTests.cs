using CellCulture.Common.Database.LabwareStorage;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnitTestProject3
{
    public class Tests
    {
        LabwareStorageService srv = null;
        Column col1 = null;
        Column col2 = null;
        Column col3 = null;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ShelfType trough = ShelfType.Trough;
            ShelfType plate = ShelfType.Plate;
            Shelf shelf1 = new Shelf() { ShelfID = 1, ShelfName = "S1C1", Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf2 = new Shelf() { ShelfID = 2, ShelfName = "S2C1", Type = plate,  Barcode = "", Theta = 0 };
            Shelf shelf3 = new Shelf() { ShelfID = 3, ShelfName = "S3C1", Type = plate,  Barcode = "", Theta = 0 };
            Shelf shelf4 = new Shelf() { ShelfID = 4, ShelfName = "S4C2", Type = plate,  Barcode = "", Theta = 0 };
            Shelf shelf5 = new Shelf() { ShelfID = 5, ShelfName = "S5C3", Type = plate,  Barcode = "", Theta = 0 };
            Shelf shelf6 = new Shelf() { ShelfID = 6, ShelfName = "S6C2", Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf7 = new Shelf() { ShelfID = 7, ShelfName = "S7C3", Type = trough, Barcode = "", Theta = 0 };
            Shelf shelf8 = new Shelf() { ShelfID = 8, ShelfName = "S8C3", Type = plate,  Barcode = "", Theta = 0 };
            List<Shelf> ListShelf1 = new List<Shelf> { shelf1, shelf2, shelf3 };
            List<Shelf> ListShelf2 = new List<Shelf> { shelf4, shelf6 };
            List<Shelf> ListShelf3 = new List<Shelf> { shelf7, shelf8, shelf5 };
            col1 = new Column() { ColumnID = 1, Shelfs = ListShelf1 };
            col2 = new Column() { ColumnID = 2, Shelfs = ListShelf2 };
            col3 = new Column() { ColumnID = 3, Shelfs = ListShelf3 };
            srv = new LabwareStorageService();
        }

        [SetUp]
        public void SetUp()
        {
            using (var db = new LabwareStorageContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Columns.Add(col1);
                db.Columns.Add(col2);
                db.Columns.Add(col3);

                db.SaveChanges();
            }
        }

        /* SCENARIO :

         * StoreLabwareTest_1 = store labware when all shelfs is empty
         * StoreLabwareTest_2 = store labware when when partial a few shelfs is occupied  
         * StoreLabwareTest_3 = store labware when all shelf is empty but there is excluded shelf array
         * StoreLabwareTest_4 = store trough labware when all trough shelfs are not available
         * StoreLabwareTest_5 = store plate labware when all plate shelfs are not available
         * StoreLabwareTest_6 = store plate labware when all shelfs are not available
         * StoreLabwareTest_7 = store plate labware when there is duplicate barcode and available shelf 
         * 
         * TakeLabwareTest_1 = barcode found
         * TakeLabwareTest_2 = barcode not found
         * 
         * ClearLabwareTest_1 = empty the shelf that has been emptied
         * ClearLabwareTest_2 = empty the shelf that has been occupied  
         */

        //TEST 1 : using shelf id 
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
        public void StoreLabwareTest_1(ShelfType ShelfTypeId, string barcode, int id_shelf)
        {

            int[] excludeShelfIds = { };
            int emptyShelfId = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        //TEST 1 : using shelf name
        public static IEnumerable InputParameter_StoreLabwareTest_1_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA1", "S2C1");
                yield return new TestCaseData(ShelfType.Trough, "AAAA2", "S1C1");
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_1_2")]
        public void StoreLabwareTest_1_2(ShelfType ShelfType, string barcode, string name_shelf)
        {

            string[] excludeShelfIds = { };
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfName);
            string notEmptyShelfId = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfId);
        }

        //TEST 2 : using shelf id 
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
            //occupy some shelfs
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            srv.StoreLabware("AAAA1", emptyShelfId);
            int emptyShelfId2 = srv.GetEmptyShelfId(st2, excludeShelfIds);
            srv.StoreLabware("AAAA2", emptyShelfId2);
            //new input
            int emptyShelfId3 = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId3);
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

        //TEST 2 : using shelf name 
        public static IEnumerable InputParameter_StoreLabwareTest_2_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA3", "S3C1");
                yield return new TestCaseData(ShelfType.Trough, "AAAA4", "S6C2");
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_2_2")]
        public void StoreLabwareTest_2_2(ShelfType ShelfType, string barcode, string name_shelf)
        {
            ShelfType st = ShelfType.Plate;
            ShelfType st2 = ShelfType.Trough;
            string[] excludeShelfNames = { };
            //occupy some shelfs
            string emptyShelfName = srv.GetEmptyShelfName(st, excludeShelfNames);
            srv.StoreLabware("AAAA1", emptyShelfName);
            string emptyShelfName2 = srv.GetEmptyShelfName(st2, excludeShelfNames);
            srv.StoreLabware("AAAA2", emptyShelfName2);
            //new input
            string emptyShelfName3 = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);
            srv.StoreLabware(barcode, emptyShelfName3);
            string notEmptyShelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfName);
        }


        //TEST 3 : using shelf id 
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

        //TEST 3 : using shelf name
        public static IEnumerable InputParameter_StoreLabwareTest_3_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", "S4C2");
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", "S7C3");
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_3_2")]
        public void StoreLabwareTest_3_2(ShelfType ShelfType, string barcode, string name_shelf)
        {
            string[] excludeShelfNames = { "S1C1", "S2C1", "S3C1", "S6C2" };
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);
            srv.StoreLabware(barcode, emptyShelfName);
            string notEmptyShelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfName);
        }

        //TEST 4 : using shelf id
        [Test]
        public void StoreLabwareTest_4()
        {
            int[] excludeShelfIds = { 1, 6, 7 };
            ShelfType st = ShelfType.Trough;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }
        //TEST 4 : using shelf name
        [Test]
        public void StoreLabwareTest_4_2()
        {
            string[] excludeShelfNames = { "S1C1", "S6C2", "S7C3" };
            ShelfType st = ShelfType.Trough;
            string emptyShelfName = srv.GetEmptyShelfName(st, excludeShelfNames);
            Assert.AreEqual("", emptyShelfName);
        }

        //TEST 5 : using shelf id 
        [Test]
        public void StoreLabwareTest_5()
        {
            int[] excludeShelfIds = { 2, 3, 4, 5, 8 };
            ShelfType st = ShelfType.Plate;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(7, emptyShelfId);
        }

        //TEST 5 : using shelf name
        [Test]
        public void StoreLabwareTest_5_2()
        {
            string[] excludeShelfNames = { "S2C1", "S3C1", "S4C2", "S5C3", "S8C3" };
            ShelfType st = ShelfType.Plate;
            string emptyShelfName = srv.GetEmptyShelfName(st, excludeShelfNames);
            Assert.AreEqual("S7C3", emptyShelfName);
        }

        //TEST 6 : using shelf id 
        [Test]
        public void StoreLabwareTest_6()
        {
            int[] excludeShelfIds = { 1, 2, 3, 4, 5, 6, 7, 8 };
            ShelfType st = ShelfType.Plate;
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }

        //TEST 6 : using shelf name
        [Test]
        public void StoreLabwareTest_6_2()
        {
            string[] excludeShelfNames = { "S1C1", "S2C1", "S3C1", "S4C2", "S5C3", "S6C2", "S7C3", "S8C3" };
            ShelfType st = ShelfType.Plate;
            string emptyShelfName = srv.GetEmptyShelfName(st, excludeShelfNames);
            Assert.AreEqual("", emptyShelfName);
        }

        //TEST 7 : using shelf id 
        [Test]
        public void TakeLabwareTest_1()
        {
            string barcode = "XXXX1";
            srv.StoreLabware(barcode, 8);
            srv.TakeLabware(barcode);
            int shelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(-1, shelfId);
        }
        //TEST 7 : using shelf name 
        [Test]
        public void TakeLabwareTest_1_2()
        {
            string barcode = "XXXX1";
            srv.StoreLabware(barcode, "S8C3");
            srv.TakeLabware(barcode);
            string shelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual("", shelfName);
        }

        //TEST 8  
        [Test]
        [TestCase("AAAA1")]
        [TestCase("AAAA2")]
        [TestCase("AAAA3")]
        public void TakeLabwareTest_2(string barcode)
        {
            Assert.Throws<System.ArgumentException>(() => srv.TakeLabware(barcode));
        }

        //TEST 9 : using shelf id
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
        public void ClearLabwareTest_1(ShelfType shelfTypeId, int shelfid)
        {
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };
            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }

        //TEST 9 : using shelf name
        public static IEnumerable InputParameter_ClearLabwareTest_2
        {
            get 
            {
                yield return new TestCaseData(ShelfType.Plate, "S4C2");
                yield return new TestCaseData(ShelfType.Trough, "S1C1");
            }
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_2")]
        public void ClearLabwareTest_1_2(ShelfType shelfType, string shelfName)
        {
            srv.ClearLabware(shelfName);
            string[] excludeShelfNames = { "S2C1", "S3C1", "S5C3", "S6C2", "S7C3", "S8C3" };
            string emptyShelfName = srv.GetEmptyShelfName(shelfType, excludeShelfNames);
            Assert.AreEqual(shelfName, emptyShelfName);
        }

        //TEST 10 : using shelf id
        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest")]
        public void ClearLabwareTest_2(ShelfType shelfTypeId, int shelfid)
        {
            //occupy some shelfs
            srv.StoreLabware("NNNN1", 1);
            srv.StoreLabware("NNNN2", 4);
            srv.ClearLabware(shelfid);
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };
            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);
            Assert.AreEqual(shelfid, emptyShelfId);
        }

        //TEST 10 : using shelf name
        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_2")]
        public void ClearLabwareTest_2_2(ShelfType shelfTypeId, string shelfName)
        {
            srv.StoreLabware("NNNN1", 1);
            srv.StoreLabware("NNNN2", 4);
            srv.ClearLabware(shelfName);
            string[] excludeShelfNames = { "S2C1", "S3C1", "S5C3", "S6C2", "S7C3", "S8C3" };
            string emptyShelfName = srv.GetEmptyShelfName(shelfTypeId, excludeShelfNames);
            Assert.AreEqual(shelfName, emptyShelfName);
        }

        //TEST 11 : using shelf id
        public static IEnumerable InputParameter_StoreLabwareTest_7
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", 3);
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", 6);
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_7")]
        public void StoreLabwareTest_7(ShelfType ShelfType, string barcode, int shelfId)
        {
            int[] excludeShelfIds = { };
            int emptyShelfName1 = srv.GetEmptyShelfId(ShelfType.Plate, excludeShelfIds);
            int emptyShelfName2 = srv.GetEmptyShelfId(ShelfType.Trough, excludeShelfIds);
            //add labware with the same barcode 
            srv.StoreLabware("AAAA5", emptyShelfName1);
            srv.StoreLabware("AAAA6", emptyShelfName2);
            int emptyShelfId = srv.GetEmptyShelfId(ShelfType, excludeShelfIds);
            //make sure there is empty shelf
            Assert.AreEqual(shelfId, emptyShelfId);
            // throw exception because even there is empty shelf, ther barcode is duplicated
            Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, emptyShelfId));

        }

        //TEST 11 : using shelf name
        public static IEnumerable InputParameter_StoreLabwareTest_7_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", "S3C1");
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", "S6C2");
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_7_2")]
        public void StoreLabwareTest_7_2(ShelfType ShelfType, string barcode,string shelfName)
        {
            string[] excludeShelfNames = {};
            string emptyShelfName1 = srv.GetEmptyShelfName(ShelfType.Plate, excludeShelfNames);
            string emptyShelfName2 = srv.GetEmptyShelfName(ShelfType.Trough, excludeShelfNames);
            //add labware with the same barcode 
            srv.StoreLabware("AAAA5", emptyShelfName1);
            srv.StoreLabware("AAAA6", emptyShelfName2);
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);
            //make sure there is empty shelf
            Assert.AreEqual(shelfName, emptyShelfName);
            // throw exception because even there is empty shelf, ther barcode is duplicated
            Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, emptyShelfName));

        }
    }
}