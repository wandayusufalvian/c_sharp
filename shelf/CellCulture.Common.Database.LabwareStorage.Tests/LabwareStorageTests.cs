using CellCulture.Common.Database.LabwareStorage;
using NUnit.Framework;
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
         * StoreLabwareTest_2 = store labware when when a few shelfs is occupied  
         * StoreLabwareTest_3 = store labware when all shelf is empty but there is excluded shelf array
         * StoreLabwareTest_4 = store labware when all shelfs with same type or all shelf(both type) are not available
         * StoreLabwareTest_5 = store labware when there is duplicate barcode and available shelf 
         * StoreLabwareTest_6 = store labware with invalid shelf id or name 
         * 
         * TakeLabwareTest_1 = barcode found
         * TakeLabwareTest_2 = barcode not found
         * 
         * ClearLabwareTest_1 = empty the shelf that has been emptied
         * ClearLabwareTest_2 = empty the shelf that has been occupied  
         * ClearLAbwareTest_3 = empty the shelf with invalid shelf id or name 
         */


        // StoreLabwareTest_1
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
        public void StoreLabwareTest_1(ShelfType ShelfType, string barcode, int id_shelf)
        {
            //get empty shelf
            int[] excludeShelfIds = { };
            int emptyShelfId = srv.GetEmptyShelfId(ShelfType, excludeShelfIds);

            //store to empty shelf with valid shelf id 
            srv.StoreLabware(barcode, emptyShelfId);

            //check occupied shelf
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

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
            //get empty shelf
            string[] excludeShelfIds = { };
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfIds);

            //store to empty shelf with valid name 
            srv.StoreLabware(barcode, emptyShelfName);

            //check occupied shelf
            string notEmptyShelfId = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfId);
        }

        //StoreLabwareTest_2 
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
            //initialization
            ShelfType st = ShelfType.Plate;
            ShelfType st2 = ShelfType.Trough;
            int[] excludeShelfIds = { };

            //occupy some shelfs
            int emptyShelfId = srv.GetEmptyShelfId(st, excludeShelfIds);
            srv.StoreLabware("AAAA1", emptyShelfId);
            int emptyShelfId2 = srv.GetEmptyShelfId(st2, excludeShelfIds);
            srv.StoreLabware("AAAA2", emptyShelfId2);

            //store to shelf with valid shelf id
            int emptyShelfId3 = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId3);

            //check occupied shelf
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

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
            //Initialization
            ShelfType st = ShelfType.Plate;
            ShelfType st2 = ShelfType.Trough;
            string[] excludeShelfNames = { };

            //occupy some shelfs
            string emptyShelfName = srv.GetEmptyShelfName(st, excludeShelfNames);
            srv.StoreLabware("AAAA1", emptyShelfName);
            string emptyShelfName2 = srv.GetEmptyShelfName(st2, excludeShelfNames);
            srv.StoreLabware("AAAA2", emptyShelfName2);

            //store to shelf with valid shelf id
            string emptyShelfName3 = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);
            srv.StoreLabware(barcode, emptyShelfName3);

            //check occupied shelf
            string notEmptyShelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfName);
        }

        //StoreLabwareTest_3
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
            //initialization 
            int[] excludeShelfIds = { 1, 2, 3, 6 };

            //store to shelf with valid shelf id
            int emptyShelfId = srv.GetEmptyShelfId(ShelfTypeId, excludeShelfIds);
            srv.StoreLabware(barcode, emptyShelfId);

            //check occupied shelf 
            int notEmptyShelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, notEmptyShelfId);
        }

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
            //initialization
            string[] excludeShelfNames = { "S1C1", "S2C1", "S3C1", "S6C2" };

            //store to shelf with valid shelf name
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);
            srv.StoreLabware(barcode, emptyShelfName);

            //check occupied shelf 
            string notEmptyShelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual(name_shelf, notEmptyShelfName);
        }

        //StoreLabwareTest_4
        public static IEnumerable InputParameter_StoreLabwareTest_4
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, new int[] { 2, 3, 4, 5, 8 });
                yield return new TestCaseData(ShelfType.Trough,new int[] { 1, 6, 7 });
                yield return new TestCaseData(ShelfType.Plate, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                yield return new TestCaseData(ShelfType.Trough, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_4")]
        public void StoreLabwareTest_4(ShelfType shelfType,int[] excludeShelfIds)
        {
            //check no empty shelf
            int emptyShelfId = srv.GetEmptyShelfId(shelfType, excludeShelfIds);
            Assert.AreEqual(-1, emptyShelfId);
        }
        public static IEnumerable InputParameter_StoreLabwareTest_4_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, new string[] { "S2C1", "S3C1", "S4C2", "S5C3", "S8C3" });
                yield return new TestCaseData(ShelfType.Trough, new string[] { "S1C1", "S6C2", "S7C3" });
                yield return new TestCaseData(ShelfType.Plate, new string[] { "S1C1", "S2C1", "S3C1", "S4C2", "S5C3", "S6C2", "S7C3", "S8C3" });
                yield return new TestCaseData(ShelfType.Trough, new string[] { "S1C1", "S2C1", "S3C1", "S4C2", "S5C3", "S6C2", "S7C3", "S8C3" });
            }
        }

        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_4_2")]
        public void StoreLabwareTest_4_2(ShelfType shelfType, string[] excludeShelfNames)
        {
            //check no empty shelf
            string emptyShelfName = srv.GetEmptyShelfName(shelfType, excludeShelfNames);
            Assert.AreEqual("", emptyShelfName);
        }

        //StoreLabwareTest_5
        public static IEnumerable InputParameter_StoreLabwareTest_5
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", 3);
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", 6);
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_5")]
        public void StoreLabwareTest_5(ShelfType ShelfType, string barcode, int shelfId)
        {
            //initialization 
            int[] excludeShelfIds = { };

            //get first empty shelf for both type 
            int emptyShelfName1 = srv.GetEmptyShelfId(ShelfType.Plate, excludeShelfIds);
            int emptyShelfName2 = srv.GetEmptyShelfId(ShelfType.Trough, excludeShelfIds);

            //add labware with the same barcode with InputParameter_StoreLabwareTest_5
            srv.StoreLabware("AAAA5", emptyShelfName1);
            srv.StoreLabware("AAAA6", emptyShelfName2);

            //get second empty shelf 
            int emptyShelfId = srv.GetEmptyShelfId(ShelfType, excludeShelfIds);

            //make sure the shelf id in InputParameter_StoreLabwareTest_5 is empty shelf
            Assert.AreEqual(shelfId, emptyShelfId);

            // throw exception because even there is empty shelf, the barcode is duplicated in other shelf
            var exception = Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, emptyShelfId));
            Assert.AreEqual($"Duplicate barcode (barcode: {barcode})", exception.Message);

        }

        public static IEnumerable InputParameter_StoreLabwareTest_5_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "AAAA5", "S3C1");
                yield return new TestCaseData(ShelfType.Trough, "AAAA6", "S6C2");
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_5_2")]
        public void StoreLabwareTest_5_2(ShelfType ShelfType, string barcode, string shelfName)
        {
            //initialization 
            string[] excludeShelfNames = { };

            //get first empty shelf for both type 
            string emptyShelfName1 = srv.GetEmptyShelfName(ShelfType.Plate, excludeShelfNames);
            string emptyShelfName2 = srv.GetEmptyShelfName(ShelfType.Trough, excludeShelfNames);

            //add labware with the same barcode with InputParameter_StoreLabwareTest_5_2
            srv.StoreLabware("AAAA5", emptyShelfName1);
            srv.StoreLabware("AAAA6", emptyShelfName2);

            //get second empty shelf 
            string emptyShelfName = srv.GetEmptyShelfName(ShelfType, excludeShelfNames);

            //make sure the shelf name in InputParameter_StoreLabwareTest_5_2 is empty shelf
            Assert.AreEqual(shelfName, emptyShelfName);

            // throw exception because even there is empty shelf, the barcode is duplicated in other shelf
            var exception = Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, shelfName));
            Assert.AreEqual($"Duplicate barcode (barcode: {barcode})", exception.Message);

        }

        //StoreLabwareTest_6
        public static IEnumerable InputParameter_StoreLabwareTest_6
        {
            get
            {
                yield return new TestCaseData("AAAA5", 30);
                yield return new TestCaseData("AAAA6", 60);
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_6")]
        public void StoreLabwareTest_6(string barcode, int shelfId)
        {
            var exception = Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, shelfId));
            Assert.AreEqual($"Invalid shelf id (id: {shelfId})", exception.Message);
        }

        public static IEnumerable InputParameter_StoreLabwareTest_6_2
        {
            get
            {
                yield return new TestCaseData("AAAA5", "S3C11");
                yield return new TestCaseData("AAAA6", "S6C21");
            }
        }
        [Test]
        [TestCaseSource("InputParameter_StoreLabwareTest_6_2")]
        public void StoreLabwareTest_6_2(string barcode, string shelfName)
        {

            var exception = Assert.Throws<System.ArgumentException>(() => srv.StoreLabware(barcode, shelfName));
            Assert.AreEqual($"Invalid shelf name (name: {shelfName})", exception.Message);

        }

        //TakeLabwareTest_1 
        [Test]
        public void TakeLabwareTest_1()
        {
            //store and then take at the same shelf
            string barcode = "XXXX1";
            srv.StoreLabware(barcode, 8);
            srv.TakeLabware(barcode);

            //check whether empty or not. If empty then takelabware works
            int shelfId = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(-1, shelfId);
        }
  
        [Test]
        public void TakeLabwareTest_1_2()
        {
            //store and then take at the same shelf
            string barcode = "XXXX1";
            srv.StoreLabware(barcode, "S8C3");
            srv.TakeLabware(barcode);

            //check whether empty or not. If empty then takelabware works
            string shelfName = srv.GetLabwareShelfName(barcode);
            Assert.AreEqual("", shelfName);
        }

        //TakeLabwareTest_2
        [Test]
        [TestCase("AAAA1")]
        [TestCase("AAAA2")]
        [TestCase("AAAA3")]
        public void TakeLabwareTest_2(string barcode)
        {
            Assert.Throws<System.ArgumentException>(() => srv.TakeLabware(barcode));
        }

        //ClearLabwareTest_1
        public static IEnumerable InputParameter_ClearLabwareTest_1
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, 4);
                yield return new TestCaseData(ShelfType.Trough, 1);
            }
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_1")]
        public void ClearLabwareTest_1(ShelfType shelfTypeId, int shelfid)
        {
            srv.ClearLabware(shelfid);

            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };

            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);

            Assert.AreEqual(shelfid, emptyShelfId);
        }

        public static IEnumerable InputParameter_ClearLabwareTest_1_2
        {
            get 
            {
                yield return new TestCaseData(ShelfType.Plate, "S4C2");
                yield return new TestCaseData(ShelfType.Trough, "S1C1");
            }
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_1_2")]
        public void ClearLabwareTest_1_2(ShelfType shelfType, string shelfName)
        {
            srv.ClearLabware(shelfName);

            string[] excludeShelfNames = { "S2C1", "S3C1", "S5C3", "S6C2", "S7C3", "S8C3" };

            string emptyShelfName = srv.GetEmptyShelfName(shelfType, excludeShelfNames);

            Assert.AreEqual(shelfName, emptyShelfName);
        }

        //ClearLabwareTest_2
        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_1")]
        public void ClearLabwareTest_2(ShelfType shelfTypeId, int shelfid)
        {   
            //initialization
            int[] excludeShelfIds = { 2, 3, 5, 6, 7, 8 };

            //occupy some shelfs
            srv.StoreLabware("NNNN1", 1);
            srv.StoreLabware("NNNN2", 4);

            srv.ClearLabware(shelfid);

            int emptyShelfId = srv.GetEmptyShelfId(shelfTypeId, excludeShelfIds);

            Assert.AreEqual(shelfid, emptyShelfId);
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_1_2")]
        public void ClearLabwareTest_2_2(ShelfType shelfTypeId, string shelfName)
        {
            //initialization 
            string[] excludeShelfNames = { "S2C1", "S3C1", "S5C3", "S6C2", "S7C3", "S8C3" };

            //occupy some shelfs
            srv.StoreLabware("NNNN1", 1);
            srv.StoreLabware("NNNN2", 4);

            srv.ClearLabware(shelfName);
            
            string emptyShelfName = srv.GetEmptyShelfName(shelfTypeId, excludeShelfNames);

            Assert.AreEqual(shelfName, emptyShelfName);
        }

        //ClearLabwareTest_3
        public static IEnumerable InputParameter_ClearLabwareTest_3
        {
            get
            {
                yield return new TestCaseData(40);
                yield return new TestCaseData(10);
            }
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_3")]
        public void ClearLabwareTest_3(int shelfid)
        {
            var exception = Assert.Throws<System.ArgumentException>(() => srv.ClearLabware(shelfid));
            Assert.AreEqual($"Invalid shelf id (id: { shelfid})", exception.Message);
        }

        public static IEnumerable InputParameter_ClearLabwareTest_3_2
        {
            get
            {
                yield return new TestCaseData(ShelfType.Plate, "S4L2");
                yield return new TestCaseData(ShelfType.Trough, "S1L1");
            }
        }

        [Test]
        [TestCaseSource("InputParameter_ClearLabwareTest_3_2")]
        public void ClearLabwareTest_3_2(ShelfType shelfType, string shelfName)
        {
            var exception = Assert.Throws<System.ArgumentException>(() => srv.ClearLabware(shelfName));
            Assert.AreEqual($"Invalid shelf name (name: {shelfName})", exception.Message);
        }

    }
}