using NUnit.Framework;
using Rovers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestRover
{
    public class Tests
    {
        Service service = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            service = new Service();
        }

        [Test]
        // Container needed 
        // Container A : 0
        // Container B : 1
        [TestCase(0, 1, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(0, 4, 0)]
        [TestCase(0, 5, 0)]
        [TestCase(0, 10, 0)]
        [TestCase(0, 15, 0)]

        public void Test1(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            

            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 0); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 0
        // Container B : 2
        [TestCase(0, 16, 0)]
        [TestCase(0, 18, 0)]
        [TestCase(0, 21, 0)]
        [TestCase(0, 25, 0)]
        [TestCase(0, 29, 0)]

        public void Test2(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            

            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 0); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2,2); // Container B
            Assert.That(leftJob == 0, Is.True);
        }


        [Test]
        // Container needed 
        // Container A : 0
        // Container B : 3
        [TestCase(0, 30, 0)]
        [TestCase(0, 38, 0)]
        [TestCase(0, 43, 0)]
        [TestCase(0, 44, 0)]
        public void Test3(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 0); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 3); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 1 (all RxA or all FlowA) 
        // Container B : 0
        [TestCase(2, 0, 0)]
        [TestCase(5, 0, 0)]
        [TestCase(10, 0, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(15, 0, 0)]
        [TestCase(0, 0, 2)]
        [TestCase(0, 0, 4)]
        [TestCase(0, 0, 6)]
        [TestCase(0, 0, 7)]
        [TestCase(0, 0, 11)]

        public void Test4(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            

            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 1); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 0); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 2 (all RxA or all FlowA) 
        // Container B : 0
        [TestCase(16, 0, 0)]
        [TestCase(17, 0, 0)]
        [TestCase(18, 0, 0)]
        [TestCase(25, 0, 0)]
        [TestCase(28, 0, 0)]
        [TestCase(0, 0, 12)]
        [TestCase(0, 0, 14)]
        [TestCase(0, 0, 16)]
        [TestCase(0, 0, 17)]
        [TestCase(0, 0, 21)]

        public void Test5(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {


            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 2); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 0); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 3 (all RxA or all FlowA) 
        // Container B : 0
        [TestCase(29, 0, 0)]
        [TestCase(31, 0, 0)]
        [TestCase(35, 0, 0)]
        [TestCase(42, 0, 0)]
        [TestCase(43, 0, 0)]
        [TestCase(0, 0, 22)]
        [TestCase(0, 0, 24)]
        [TestCase(0, 0, 26)]
        [TestCase(0, 0, 27)]
        [TestCase(0, 0, 32)]

        public void Test6(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {


            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 3); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 0); // Container B
            Assert.That(leftJob == 0, Is.True);
        }


        [Test]
        // Container needed 
        // Container A : 2 (all RxA and all FlowA) 
        // Container B : 0
        [TestCase(2, 0, 2)]
        [TestCase(3, 0, 3)]
        [TestCase(4, 0, 4)]
        [TestCase(5, 0, 5)]
        [TestCase(6, 0, 6)]
        [TestCase(12, 0,12)]
        public void Test7(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            

            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 2); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 0); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 3 (all RxA and all FlowA) 
        // Container B : 0
        [TestCase(13, 0, 12)]
        [TestCase(15, 0, 13)]
        [TestCase(17, 0, 14)]
        [TestCase(19, 0, 15)]
        [TestCase(21, 0, 16)]
        [TestCase(23, 0, 15)]
        public void Test8(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {


            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 3); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 0); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 1  
        // Container B : 1
        [TestCase(2, 2, 0)]
        [TestCase(0, 2, 2)]
        [TestCase(7, 7, 0)]
        [TestCase(0, 7, 7)]
        [TestCase(15, 15, 0)]
        [TestCase(0, 15, 11)]
        public void Test9(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 1); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 1  
        // Container B : 2
        [TestCase(2, 16, 0)]
        [TestCase(0, 18, 2)]
        [TestCase(7, 19, 0)]
        [TestCase(0, 20, 7)]
        [TestCase(15, 22, 0)]
        [TestCase(0, 29, 11)]
        public void Test10(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 1); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 2); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 1  
        // Container B : 3
        [TestCase(2, 30, 0)]
        [TestCase(0, 31, 2)]
        [TestCase(7, 32, 0)]
        [TestCase(0, 33, 7)]
        [TestCase(15, 41, 0)]
        [TestCase(0, 44, 11)]
        public void Test11(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 1); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 3); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 2  
        // Container B : 1
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 3)]
        [TestCase(7, 7, 7)]
        [TestCase(8, 7, 8)]
        [TestCase(10, 15, 10)]
        [TestCase(12, 15, 12)]
        public void Test12(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 2); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 3  
        // Container B : 1
        [TestCase(13, 2, 12)]
        [TestCase(15, 2, 13)]
        [TestCase(16, 7, 13)]
        [TestCase(18, 7, 14)]
        [TestCase(20, 15, 14)]
        [TestCase(23, 15, 15)]
        public void Test13(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 3); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 3  
        // Container B : 2
        [TestCase(13, 16, 12)]
        [TestCase(15, 17, 13)]
        [TestCase(16, 19, 13)]
        [TestCase(18, 20, 14)]
        [TestCase(20, 26, 14)]
        [TestCase(23, 29, 15)]
        public void Test14(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 3); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 2); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 2  
        // Container B : 3
        [TestCase(2, 30, 2)]
        [TestCase(3, 32, 3)]
        [TestCase(7, 37, 7)]
        [TestCase(8, 38, 8)]
        [TestCase(10, 40, 10)]
        [TestCase(12, 44, 12)]
        public void Test15(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 2); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 3); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        // Container needed 
        // Container A : 3  
        // Container B : 3
        [TestCase(13, 30, 12)]
        [TestCase(14, 32, 13)]
        [TestCase(17, 37, 13)]
        [TestCase(18, 38, 13)]
        [TestCase(20, 40, 14)]
        [TestCase(23, 44, 15)]
        public void Test16(int amountOfRxAJob, int amountOfRxBJob, int amountOfFlowAJob)
        {
            Job[] arrayOfJobs = service.arrayOfJobs(amountOfRxAJob, amountOfRxBJob, amountOfFlowAJob);

            var amountOfContainerNeeded = service.calcContainerNeeded(arrayOfJobs);

            Container[] arrayOfContainers = service.arrayOfContainers(amountOfContainerNeeded.Item1, amountOfContainerNeeded.Item2);

            int leftJob = service.runSimulation(arrayOfJobs, arrayOfContainers);
            Assert.AreEqual(amountOfContainerNeeded.Item1, 3); // Container A
            Assert.AreEqual(amountOfContainerNeeded.Item2, 3); // Container B
            Assert.That(leftJob == 0, Is.True);
        }


    }
}