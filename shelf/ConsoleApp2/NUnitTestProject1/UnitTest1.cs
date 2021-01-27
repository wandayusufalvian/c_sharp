using ConsoleApp1;
using NUnit.Framework;


namespace NUnitTestProject1
{
    public class Tests
    {
        Service srv = null;

        [SetUp]

        public void Setup() { srv = new Service(); }
        
        /* SKENARIO :
         * PutLabware1 = put labware ketika rack masih kosong 
         * PutLabware2 = put labware ketika rack terisi sesuai test case putlabware 1 
         * PutLabware3 = put labware ketika rack terisi sesuai test case putlabware 1 dan 2 
         * PutLabware4 = put labware size besar ketika shelf besar terisi penuh
         * PutLabware5 = put labware size kecil ketika shelf kecil terisi penuh shg isi ke shelf besar terakhir
         * PutLabware6 = put labware kecil ketika seluruh rack penuh
         * TakeLabware1 = shelf id valid, tp shelf kosong 
         * TakeLabware2 = shelf id valid dan labware ada di shelf tsb 
         * TakeLabware3 = shelf id tidak valid 
         * ClearLabware1 = mengosongkan shelf, cek return 1 ketika shelf id valid dan shelf terisi 
         * ClearLabware2 = mengosongkan shelf, cek return 1 ketika shelf id valid dan shelf kosong
         * ClearLabware3 = mengosongkan shelf, cek nilai barcode apakah benar jadi kosong dari sebelumnya terisi 
         * ClearLabware4 = mengosongkan shelf, namun shelf id tidak valid 
         * **fungsi SearchEmptyShelf dan SearchLabware sudah dijalankan pada skenario PutLabware 
         */

        [Test]
        [TestCase(1, "AAAA1", 2)]
        [TestCase(2, "AAAA2", 1)]
        public void PutLabware1(int size, string barcode, int id_shelf)
        {
            srv.ClearAllLabware();
            srv.PutLabware(size, barcode);
            var id = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, id);
            
        }

        [Test]
        [TestCase(1, "AAAA3", 3)]
        [TestCase(2, "AAAA4", 6)]
        public void PutLabware2(int size, string barcode, int id_shelf)
        {
            srv.ClearAllLabware();
            srv.PutLabware(1, "AAAA1");
            srv.PutLabware(2, "AAAA2");
            srv.PutLabware(size, barcode);

            var id = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, id);
        }

        [Test]
        [TestCase(1, "AAAA5",  4)]
        [TestCase(2, "AAAA6", 7)]
        public void PutLabware3(int size, string barcode, int id_shelf)
        {
            srv.ClearAllLabware();
            srv.PutLabware(1, "AAAA1");
            srv.PutLabware(2, "AAAA2");
            srv.PutLabware(1, "AAAA3");
            srv.PutLabware(2, "AAAA4");
            srv.PutLabware(size, barcode);

            var id = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, id);
        }

        [Test]
        public void PutLabware4()
        {
            srv.ClearAllLabware();
            int size = 2;
            string barcode = "AAAA6";
            srv.IsiPenuhShelfBesar();
            int cek = srv.PutLabware(size, barcode);
            Assert.AreEqual(0, cek);
        }

        [Test]
        public void PutLabware5()
        {
            srv.ClearAllLabware();
            int size = 1;
            string barcode = "AAAA6";
            //shelf besar terakhir 
            int id_shelf = 7;

            srv.IsiPenuhShelfKecil();
            srv.PutLabware(size, barcode);
            var id = srv.GetLabwareShelfId(barcode);
            Assert.AreEqual(id_shelf, id);
        }

        [Test]
        public void PutLabware6()
        {
            int size = 1;
            string barcode = "AAAA6";


            srv.IsiPenuhShelfBesar();
            srv.IsiPenuhShelfKecil();

            int cek= srv.PutLabware(size, barcode);
            var id = srv.SearchLabware(barcode);
            Assert.AreEqual(0, cek);
        }

        [Test]
        
        public void TakeLabware1()
        {
            srv.ClearAllLabware();
            int shelfid = 2;
            /* shelf id valid, labware ada 
             */
            srv.PutLabware(1, "AAAA1");

            string returns = srv.TakeLabware(shelfid);
            Assert.AreEqual("AAAA1", returns);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void TakeLabware2(int shelfid)
        {
            srv.ClearAllLabware();
            
            string returns = srv.TakeLabware(shelfid);
            Assert.Null(returns);
        }

        [Test]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void TakeLabware3(int shelfid)
        {
            Assert.Throws<System.ArgumentException>(() => srv.TakeLabware(shelfid));
        }

        [Test]
        public void ClearLabware1()
        {
            string barcode = "AAAA1";
            int size = 1;
            srv.PutLabware(size, barcode);
            var id = srv.GetLabwareShelfId(barcode);
            
            int balik= srv.ClearLabware(id);
            Assert.AreEqual(1, balik);
        }

        [Test]
        public void ClearLabware2()
        {
            int balik = srv.ClearLabware(1);
            Assert.AreEqual(1, balik);
        }

        [Test]
        public void ClearLabware3()
        {
            string barcode = "AAAA1";
            int size = 1;
            srv.PutLabware(size, barcode);
            var id = srv.GetLabwareShelfId(barcode);
            srv.ClearLabware(id);
            string returns= srv.TakeLabware(id); 
            Assert.Null(returns);
            
        }

        [Test]
        [TestCase (20)]
        [TestCase (30)]
        [TestCase (40)]
        public void ClearLabware4(int shelfid)
        {
            Assert.Throws<System.ArgumentException>(() => srv.ClearLabware(shelfid));
        }
        
    }
}