using NUnit.Framework;
using Rovers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestRover
{
    public class Tests
    {

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test1()
        {
            //inisialisasi data job 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx","Flow"}},
                {"B", new List<string>{"Rx"}}

            };
            List<int> listOfAmount = new List<int> { 2, 2,
                                                     2
                                                    };
            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);

            //create array of random jobs 
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();

            //hitung container yg dbutuhkan dari array of random jobs 
            Dictionary<Trough, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);

            //buat array or container
            Trough[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);

            // jalankan simulasi ambil container sesuai jenis liquid secara acak 
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            //print sisa volum di tiap container 
            var loc = containersNeeded.Keys;
            List<Trough> soc = loc.OrderBy(o => o.name).ToList();
            Service.printContainerLeftVolume(ArrayOfContainers);

            //cek apakah volume yg dibutuhkan dg yg dikonsumsi di seluruh kontainer sama
            Dictionary<string, int> liquidNeeded = Service.volumeNeededEachLiquid(arrayOfRandomJobs);
            Dictionary<string, int> liquidConsumed = Service.liquidConsumedEachContainerType(ArrayOfContainers);
            List<string> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);

            //cek apakah container yg dibutuhkan sesuai perhitungan manual 
            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B

            //cek apakah seluruh job telah dieksekusi 
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test2()
        {
            //inisialisasi data job 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx","Flow"}},
                {"B", new List<string>{"Rx"}},
                {"C", new List<string>{"Rx","Flow"}},

            };
            List<int> listOfAmount = new List<int> { 2, 2,
                                                     2,
                                                     3, 4
                                                    };
            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);

            //create array of random jobs 
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();

            //hitung container yg dbutuhkan dari array of random jobs 
            Dictionary<Trough, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);

            //buat array or container
            Trough[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);

            // jalankan simulasi ambil container sesuai jenis liquid secara acak 
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            //print sisa volum di tiap container 
            var loc = containersNeeded.Keys;
            List<Trough> soc = loc.OrderBy(o => o.name).ToList();
            Service.printContainerLeftVolume(ArrayOfContainers);

            //cek apakah volume yg dibutuhkan dg yg dikonsumsi di seluruh kontainer sama
            Dictionary<string, int> liquidNeeded = Service.volumeNeededEachLiquid(arrayOfRandomJobs);
            Dictionary<string, int> liquidConsumed = Service.liquidConsumedEachContainerType(ArrayOfContainers);
            List<string> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);

            //cek apakah container yg dibutuhkan sesuai perhitungan manual 
            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.AreEqual(containersNeeded[soc[2]], 2); // Container C

            //cek apakah seluruh job telah dieksekusi 
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test3()
        {
            //inisialisasi data job 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx","Flow"}},
                {"B", new List<string>{"Rx"}},
                {"C", new List<string>{"Rx","Flow"}},
                {"D", new List<string>{"Flow"}},

            };
            List<int> listOfAmount = new List<int> { 2, 2,
                                                     2,
                                                     3, 4,
                                                        20
                                                    };
            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);

            //create array of random jobs 
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();

            //hitung container yg dbutuhkan dari array of random jobs 
            Dictionary<Trough, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);

            //buat array or container
            Trough[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);

            // jalankan simulasi ambil container sesuai jenis liquid secara acak 
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            //print sisa volum di tiap container 
            var loc = containersNeeded.Keys;
            List<Trough> soc = loc.OrderBy(o => o.name).ToList();
            Service.printContainerLeftVolume(ArrayOfContainers);

            //cek apakah volume yg dibutuhkan dg yg dikonsumsi di seluruh kontainer sama
            Dictionary<string, int> liquidNeeded = Service.volumeNeededEachLiquid(arrayOfRandomJobs);
            Dictionary<string, int> liquidConsumed = Service.liquidConsumedEachContainerType(ArrayOfContainers);
            List<string> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);

            //cek apakah container yg dibutuhkan sesuai perhitungan manual 
            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.AreEqual(containersNeeded[soc[2]], 2); // Container C
            Assert.AreEqual(containersNeeded[soc[3]], 2); // Container D

            //cek apakah seluruh job telah dieksekusi 
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test4()
        {
            //inisialisasi data job 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx","Flow"}},
                {"B", new List<string>{"Rx"}},
                {"C", new List<string>{"Rx","Flow"}},
                {"D", new List<string>{"Flow"}},
                {"E", new List<string>{"Rx","Flow"}}


            };
            List<int> listOfAmount = new List<int> { 2, 2,
                                                     2,
                                                     3, 4,
                                                        20,
                                                     15,11
                                                    };
            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);

            //create array of random jobs 
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();

            //hitung container yg dbutuhkan dari array of random jobs 
            Dictionary<Trough, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);

            //buat array or container
            Trough[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);

            // jalankan simulasi ambil container sesuai jenis liquid secara acak 
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            //print sisa volum di tiap container 
            var loc = containersNeeded.Keys;
            List<Trough> soc = loc.OrderBy(o => o.name).ToList();
            Service.printContainerLeftVolume(ArrayOfContainers);

            //cek apakah volume yg dibutuhkan dg yg dikonsumsi di seluruh kontainer sama
            Dictionary<string, int> liquidNeeded = Service.volumeNeededEachLiquid(arrayOfRandomJobs);
            Dictionary<string, int> liquidConsumed = Service.liquidConsumedEachContainerType(ArrayOfContainers);
            List<string> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);
            Assert.AreEqual(liquidNeeded[keys[4]], liquidConsumed[keys[4]]);

            //cek apakah container yg dibutuhkan sesuai perhitungan manual 
            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.AreEqual(containersNeeded[soc[2]], 2); // Container C
            Assert.AreEqual(containersNeeded[soc[3]], 2); // Container D
            Assert.AreEqual(containersNeeded[soc[4]], 3); // Container E

            //cek apakah seluruh job telah dieksekusi 
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test5()
        {
            //inisialisasi data job 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx","Flow"}},
                {"B", new List<string>{"Rx"}},
                {"C", new List<string>{"Rx","Flow"}},
                {"D", new List<string>{"Flow"}},
                {"E", new List<string>{"Rx","Flow"}},
                {"F", new List<string>{"Rx","Flow"}}


            };
            List<int> listOfAmount = new List<int> { 1, 20,
                                                     15,
                                                     20, 6,
                                                        20,
                                                     29,20,
                                                     8,25
                                                    };
            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);

            //create array of random jobs 
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();

            //hitung container yg dbutuhkan dari array of random jobs 
            Dictionary<Trough, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);

            //buat array or container
            Trough[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);

            // jalankan simulasi ambil container sesuai jenis liquid secara acak 
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            //print sisa volum di tiap container 
            var loc = containersNeeded.Keys;
            List<Trough> soc = loc.OrderBy(o => o.name).ToList();
            Service.printContainerLeftVolume(ArrayOfContainers);

            //cek apakah volume yg dibutuhkan dg yg dikonsumsi di seluruh kontainer sama
            Dictionary<string, int> liquidNeeded = Service.volumeNeededEachLiquid(arrayOfRandomJobs);
            Dictionary<string, int> liquidConsumed = Service.liquidConsumedEachContainerType(ArrayOfContainers);
            List<string> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);
            Assert.AreEqual(liquidNeeded[keys[4]], liquidConsumed[keys[4]]);
            Assert.AreEqual(liquidNeeded[keys[5]], liquidConsumed[keys[5]]);

            //cek apakah container yg dibutuhkan sesuai perhitungan manual 
            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.AreEqual(containersNeeded[soc[2]], 2); // Container C
            Assert.AreEqual(containersNeeded[soc[3]], 2); // Container D
            Assert.AreEqual(containersNeeded[soc[4]], 4); // Container E
            Assert.AreEqual(containersNeeded[soc[5]], 3); // Container F

            //cek apakah seluruh job telah dieksekusi 
            Assert.That(leftJob == 0, Is.True);
        }

    }
}