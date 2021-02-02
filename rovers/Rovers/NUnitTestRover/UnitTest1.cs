using NUnit.Framework;
using Rovers;

namespace NUnitTestRover
{
    public class Tests
    {

        MachineType RX = MachineType.RX;
        MachineType Flow = MachineType.Flow;
        MarkasRover marRover = null;
        Service srv = new Service();

       

        [SetUp]
        public void SetUp()
        {
            marRover = new MarkasRover();
            for (int i = 1; i <= 5; i++)
            {
                marRover.listRovers.Add(new Rover(i));
            }
            marRover.availableRover = marRover.listRovers.Count;
        }

        /* asumsi = - RX dan Flow tidak request dalam waktu bersamaan
         *          - Hanya Rover yang full capacity yg bisa dipanggil. Rover yg sudah pernah dipanggil meskipun sisa banyak
         *          tidak bisa dipanggil oleh yg lain. 
         *          
         * Test1 = RX make request dan available rovers cukup, lalu FLow make request dan  available rovers cukup
         * Test2 = Flow make request dan available rovers cukup, lalu RX make request dan  available rovers cukup
         * Test3 = RX make request dan available rovers cukup, lalu FLow make request dan  available rovers tidak cukup
         * Test4 = Flow make request dan available rovers cukup, lalu RX make request dan  available rovers tidak cukup
         * Test5 = RX make request dan available rovers tidak cukup, lalu FLow make request dan  available rovers cukup
         * Test6 = Flow make request dan available rovers tidak cukup, lalu RX make request dan  available rovers cukup
         * Test7 = Flow make request dan available rovers tidak cukup, lalu RX make request dan  available rovers tidak cukup
         */

        [Test]
        public void Test1()
        {
            int jumlahJobsRX = 15;
            int jumlahJobsFlow = 12;
            int roverNeededRX=srv.calcRoverNeeded(RX, jumlahJobsRX, marRover);
            int roverNeededFlow = srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover);
            Assert.That(roverNeededRX == 2, Is.True);
            Assert.That(roverNeededFlow == 2, Is.True); 
        }

        [Test]
        public void Test2()
        {
            int jumlahJobsRX = 15;
            int jumlahJobsFlow = 11;
            int roverNeededFlow = srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover);
            int roverNeededRX = srv.calcRoverNeeded(RX, jumlahJobsRX, marRover);
            Assert.That(roverNeededRX == 2, Is.True);
            Assert.That(roverNeededFlow == 1, Is.True);
        }

        [Test]
        public void Test3()
        {
            int jumlahJobsRX = 15;
            int jumlahJobsFlow = 55;
            int roverNeededRX = srv.calcRoverNeeded(RX, jumlahJobsRX, marRover);
            Assert.That(roverNeededRX == 2, Is.True);
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover));
        }

        [Test]
        public void Test4()
        {
            int jumlahJobsRX = 80;
            int jumlahJobsFlow = 11;
            int roverNeededFlow = srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover);
            Assert.That(roverNeededFlow == 1, Is.True);
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(RX, jumlahJobsRX, marRover));
        }

        [Test]
        public void Test5()
        {
            int jumlahJobsRX = 90;
            int jumlahJobsFlow = 11;
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(RX, jumlahJobsRX, marRover));
            int roverNeededFlow = srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover);
            Assert.That(roverNeededFlow == 1, Is.True);
        }

        [Test]
        public void Test6()
        {
            int jumlahJobsRX = 15;
            int jumlahJobsFlow = 100;
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover));
            int roverNeededRX = srv.calcRoverNeeded(RX, jumlahJobsRX, marRover);
            Assert.That(roverNeededRX == 2, Is.True);
            
        }

        [Test]
        public void Test7()
        {
            int jumlahJobsRX = 150;
            int jumlahJobsFlow = 100;
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(RX, jumlahJobsRX, marRover));
            Assert.Throws<System.Exception>(() => srv.calcRoverNeeded(RX, jumlahJobsFlow, marRover));
        }
    }
}