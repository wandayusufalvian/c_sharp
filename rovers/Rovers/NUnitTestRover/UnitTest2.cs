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

        public void Test1()
        {
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
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();
            Dictionary<Container, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);
            Container[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            var loc = containersNeeded.Keys;
            List<Container> soc = loc.OrderBy(o => o.name).ToList();

            Console.WriteLine("tes");
            //TestContext.WriteLine

            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }

        [Test]
        [Ignore("Ignore a test")]
        public void Test2()
        {
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
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            Containers containers = new Containers();
            Dictionary<Container, int> containersNeeded = containers.calcContainerNeeded(arrayOfRandomJobs);
            Container[] ArrayOfContainers = containers.createArrayOfContainers(containersNeeded);
            int leftJob = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);

            var loc = containersNeeded.Keys;
            List<Container> soc = loc.OrderBy(o => o.name).ToList();

            Console.WriteLine("fdfdfdf");
            TestContext.Out.WriteLine("Message to write to log");
            System.Diagnostics.Debug.WriteLine("Matrix has you...");


            Assert.AreEqual(containersNeeded[soc[0]], 2); // Container A
            Assert.AreEqual(containersNeeded[soc[1]], 1); // Container B
            Assert.That(leftJob == 0, Is.True);
        }


    }
}