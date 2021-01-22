using ConsoleApp2;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Tests
    {
        private Rack _rack = null;

        [SetUp]
        
        public void Setup()
        {
            _rack = new Rack(1);
            //HARD CODED DATA 
            List<int> IdColumn1 = new List<int> { 1, 2, 3 };
            Dictionary<int, List<List<int>>> map = new Dictionary<int, List<List<int>>>
            {
                {1,new List<List<int>>{ new List<int>{1,2}, new List<int> {2,1}, new List<int> {3,1}}},
                {2,new List<List<int>>{ new List<int>{4,1}, new List<int> {6,2}}},
                {3,new List<List<int>>{ new List<int>{7,2}, new List<int> {8,1}, new List<int> {5,1}}},
            };
            _rack.CreateRack(IdColumn1, map);
        }
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
        [TestCase(1, "AAAA1", 1, 2)]
        [TestCase(2, "AAAA2", 1, 1)]
        public void PutLabware1(int size, string barcode, int id_kol, int id_shelf)
        {
            _rack.PutLabware(size, barcode);
            var id = _rack.SearchLabware(barcode);
            Assert.AreEqual((id_kol, id_shelf), id);
        }

        [Test]
        [TestCase(1, "AAAA3", 1, 3)]
        [TestCase(2, "AAAA4", 2, 6)]
        public void PutLabware2(int size, string barcode, int id_kol, int id_shelf)
        {
            _rack.PutLabware(1, "AAAA1");
            _rack.PutLabware(2, "AAAA2");
            _rack.PutLabware(size, barcode);

            var id = _rack.SearchLabware(barcode);
            Assert.AreEqual((id_kol, id_shelf), id);
        }

        [Test]
        [TestCase(1, "AAAA5", 2, 4)]
        [TestCase(2, "AAAA6", 3, 7)]
        public void PutLabware3(int size, string barcode, int id_kol, int id_shelf)
        {
            _rack.PutLabware(1, "AAAA1");
            _rack.PutLabware(2, "AAAA2");
            _rack.PutLabware(1, "AAAA3");
            _rack.PutLabware(2, "AAAA4");
            _rack.PutLabware(size, barcode);

            var id = _rack.SearchLabware(barcode);
            Assert.AreEqual((id_kol, id_shelf), id);
        }

        [Test]
        public void PutLabware4()
        {
            int size = 2;
            string barcode = "AAAA6"; 
            _rack.IsiPenuhShelfBesar();
            int cek = _rack.PutLabware(size, barcode);
            Assert.AreEqual(0, cek);
        }

        [Test]
        public void PutLabware5()
        {
            int size = 1;
            string barcode = "AAAA6";
            //shelf besar terakhir 
            int id_kol = 3;
            int id_shelf = 7;

            _rack.IsiPenuhShelfKecil();
            _rack.PutLabware(size, barcode);
            var id = _rack.SearchLabware(barcode);
            Assert.AreEqual((id_kol, id_shelf), id);
        }

        [Test]
        public void PutLabware6()
        {
            int size = 1;
            string barcode = "AAAA6";
            int id_kol = 3;
            int id_shelf = 7;

            _rack.IsiPenuhShelfBesar();
            _rack.IsiPenuhShelfKecil();

            int cek=_rack.PutLabware(size, barcode);
            var id = _rack.SearchLabware(barcode);
            Assert.AreEqual(0, cek);
        }

        [Test]
        
        public void TakeLabware1()
        {
            int shelfid = 2; 
            /* shelf id valid, labware ada 
             */
            _rack.PutLabware(1, "AAAA1");

            string returns = _rack.TakeLabware(shelfid);
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
            string returns = _rack.TakeLabware(shelfid);
            Assert.Null(returns);
        }

        [Test]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void TakeLabware3(int shelfid)
        {
            Assert.Throws<System.ArgumentException>(() => _rack.TakeLabware(shelfid));
        }

        [Test]
        public void ClearLabware1()
        {
            string barcode = "AAAA1";
            int size = 1; 
            _rack.PutLabware(size, barcode);
            var id = _rack.SearchLabware(barcode);
            int balik=_rack.ClearLabware(id.Item2);
            Assert.AreEqual(1, balik);
        }

        [Test]
        public void ClearLabware2()
        {
            int balik = _rack.ClearLabware(1);
            Assert.AreEqual(1, balik);
        }

        [Test]
        public void ClearLabware3()
        {
            string barcode = "AAAA1";
            int size = 1;
            _rack.PutLabware(size, barcode);
            var id = _rack.SearchLabware(barcode);
            _rack.ClearLabware(id.Item2);
            string returns=_rack.TakeLabware(id.Item2); 
            Assert.Null(returns);
            
        }

        [Test]
        [TestCase (20)]
        [TestCase (30)]
        [TestCase (40)]
        public void ClearLabware4(int shelfid)
        {
            Assert.Throws<System.ArgumentException>(() => _rack.ClearLabware(shelfid));
        }

    }
}