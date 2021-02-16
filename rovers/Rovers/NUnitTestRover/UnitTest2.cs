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
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Rx,2},{Machine.Flo, 15}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 1}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.Rx, 2},{ Machine.Flo, 2}}},
                {Liquid.D, new Dictionary<Machine,int>{{Machine.Flo, 1},{Machine.MachineX, 2}}}
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            Dictionary<Trough, int> dictOfTroughNeeded = Service.calcTroughNeeded(listOfRandomJobs);
            List<Trough> listOfRandomTroughs = Service.createListOfRandomTrough(dictOfTroughNeeded);
            int jobsLeft = Service.runSimulation(listOfRandomJobs, listOfRandomTroughs);

            //check volume consumed and volume left in trough...
            Dictionary<Liquid, int> liquidNeeded = Service.volumeNeededEachLiquid(listOfRandomJobs);
            Dictionary<Liquid, int> liquidConsumed = Service.liquidConsumedEachTrough(listOfRandomTroughs);
            List<Liquid> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);

            //check amount of trough needed...
            List<Trough> orderedTroughKeys = dictOfTroughNeeded.Keys.OrderBy(o => o._liquidType.name).ToList();
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[0]], 3); // Trough A
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[1]], 1); // Trough B
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[2]], 3); // Trough C
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[3]], 2); // Trough D

            //check jobsLeft==0
            Assert.That(jobsLeft == 0, Is.True);

            //print trough volume left... 
            Service.printContainerLeftVolume(listOfRandomTroughs.OrderBy(o => o._liquidType.name).ToList());
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test2()
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Rx,15},{Machine.Flo, 21}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 15}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.Rx, 2},{Machine.Flo, 2}}},
                {Liquid.D, new Dictionary<Machine,int>{{Machine.Flo, 1},{Machine.MachineX, 2}}},
                {Liquid.E, new Dictionary<Machine,int>{{Machine.Rx,15},{Machine.Flo, 5},{Machine.MachineX, 2}}},
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            Dictionary<Trough, int> dictOfTroughNeeded = Service.calcTroughNeeded(listOfRandomJobs);
            List<Trough> listOfRandomTroughs = Service.createListOfRandomTrough(dictOfTroughNeeded);
            int jobsLeft = Service.runSimulation(listOfRandomJobs, listOfRandomTroughs);

            //check volume consumed and volume left in trough...
            Dictionary<Liquid, int> liquidNeeded = Service.volumeNeededEachLiquid(listOfRandomJobs);
            Dictionary<Liquid, int> liquidConsumed = Service.liquidConsumedEachTrough(listOfRandomTroughs);
            List<Liquid> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);
            Assert.AreEqual(liquidNeeded[keys[4]], liquidConsumed[keys[4]]);

            //check amount of trough needed...
            List<Trough> orderedTroughKeys = dictOfTroughNeeded.Keys.OrderBy(o => o._liquidType.name).ToList();
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[0]], 4); // Trough A
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[1]], 2); // Trough B
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[2]], 3); // Trough C
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[3]], 2); // Trough D
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[4]], 4); // Trough E

            //check jobsLeft==0
            Assert.That(jobsLeft == 0, Is.True);

            //print trough volume left... 
            Service.printContainerLeftVolume(listOfRandomTroughs.OrderBy(o => o._liquidType.name).ToList());
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test3()
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Flo, 11}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 15}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.MachineX, 9}}},
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            Dictionary<Trough, int> dictOfTroughNeeded = Service.calcTroughNeeded(listOfRandomJobs);
            List<Trough> listOfRandomTroughs = Service.createListOfRandomTrough(dictOfTroughNeeded);
            int jobsLeft = Service.runSimulation(listOfRandomJobs, listOfRandomTroughs);
            
            //check volume consumed and volume left in trough...
            Dictionary<Liquid, int> liquidNeeded = Service.volumeNeededEachLiquid(listOfRandomJobs);
            Dictionary<Liquid, int> liquidConsumed = Service.liquidConsumedEachTrough(listOfRandomTroughs);
            List<Liquid> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);

            //check amount of trough needed...
            List<Trough> orderedTroughKeys = dictOfTroughNeeded.Keys.OrderBy(o => o._liquidType.name).ToList();
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[0]], 1); // Trough A
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[1]], 2); // Trough B
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[2]], 1); // Trough C

            //check jobsLeft==0
            Assert.That(jobsLeft == 0, Is.True);

            //print trough volume left... 
            Service.printContainerLeftVolume(listOfRandomTroughs.OrderBy(o => o._liquidType.name).ToList());
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test4()
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Flo, 11}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 15}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.MachineX, 9}}},
                {Liquid.D, new Dictionary<Machine,int>{{Machine.MachineY, 1}}},
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            Dictionary<Trough, int> dictOfTroughNeeded = Service.calcTroughNeeded(listOfRandomJobs);
            List<Trough> listOfRandomTroughs = Service.createListOfRandomTrough(dictOfTroughNeeded);
            int jobsLeft = Service.runSimulation(listOfRandomJobs, listOfRandomTroughs);

            //check volume consumed and volume left in trough...
            Dictionary<Liquid, int> liquidNeeded = Service.volumeNeededEachLiquid(listOfRandomJobs);
            Dictionary<Liquid, int> liquidConsumed = Service.liquidConsumedEachTrough(listOfRandomTroughs);
            List<Liquid> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);
            Assert.AreEqual(liquidNeeded[keys[3]], liquidConsumed[keys[3]]);

            //check amount of trough needed...
            List<Trough> orderedTroughKeys = dictOfTroughNeeded.Keys.OrderBy(o => o._liquidType.name).ToList();
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[0]], 1); // Trough A
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[1]], 2); // Trough B
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[2]], 1); // Trough C
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[3]], 1); // Trough D

            //check jobsLeft==0
            Assert.That(jobsLeft == 0, Is.True);

            //print trough volume left... 
            Service.printContainerLeftVolume(listOfRandomTroughs.OrderBy(o => o._liquidType.name).ToList());
        }

        [Test]
        //[Ignore("Ignore a test")]
        public void Test5()
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{ {Machine.Rx, 15 },{ Machine.Flo, 11},{Machine.MachineX, 9},{Machine.MachineY, 1}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 40}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.MachineX, 9}}},
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            Dictionary<Trough, int> dictOfTroughNeeded = Service.calcTroughNeeded(listOfRandomJobs);
            List<Trough> listOfRandomTroughs = Service.createListOfRandomTrough(dictOfTroughNeeded);
            int jobsLeft = Service.runSimulation(listOfRandomJobs, listOfRandomTroughs);

            //check volume consumed and volume left in trough...
            Dictionary<Liquid, int> liquidNeeded = Service.volumeNeededEachLiquid(listOfRandomJobs);
            Dictionary<Liquid, int> liquidConsumed = Service.liquidConsumedEachTrough(listOfRandomTroughs);
            List<Liquid> keys = liquidNeeded.Keys.ToList();
            Assert.AreEqual(liquidNeeded[keys[0]], liquidConsumed[keys[0]]);
            Assert.AreEqual(liquidNeeded[keys[1]], liquidConsumed[keys[1]]);
            Assert.AreEqual(liquidNeeded[keys[2]], liquidConsumed[keys[2]]);

            //check amount of trough needed...
            List<Trough> orderedTroughKeys = dictOfTroughNeeded.Keys.OrderBy(o => o._liquidType.name).ToList();
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[0]], 5); // Trough A
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[1]], 3); // Trough B
            Assert.AreEqual(dictOfTroughNeeded[orderedTroughKeys[2]], 1); // Trough C

            //check jobsLeft==0
            Assert.That(jobsLeft == 0, Is.True);

            //print trough volume left... 
            Service.printContainerLeftVolume(listOfRandomTroughs.OrderBy(o => o._liquidType.name).ToList());
        }
    }
}