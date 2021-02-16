using System;
using System.Collections.Generic;
using System.Linq;

namespace Rovers
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>()
            {   //initialize data for list of random jobs. 
                //{jenis liquid,{{jenis mesin 1,banyak job 1},{jenis mesin 2,banyak job 2},....{jenis mesin n,banyak job n}}}
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Rx,2},{Machine.Flo, 15}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 1}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.Rx, 2},{ Machine.Flo, 2}}},
                {Liquid.D, new Dictionary<Machine,int>{{Machine.Flo, 1},{Machine.MachineX, 2},{Machine.MachineY, 5}}}
            };

            //run simulation...
            List<Job> listOfRandomJobs = Service.createListOfRandomJobs(dictOfLiquidAndMachine);

            //Dictionary<Job, int> liquidAndTotalJobs = Service.jobAndTotalJobs(listOfRandomJobs);
            //var k = liquidAndTotalJobs.Keys;
            //foreach (Job l in k)
            //{
            //    Console.WriteLine($"{l._liquidType.name},{l._machineType.name} : {liquidAndTotalJobs[l]}");
            //}

            //Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachines = Service.reverseListJobToDict(listOfRandomJobs);
            //var k2 = dictOfLiquidAndMachines.Keys;
            //foreach (Liquid l in k2)
            //{
            //    Console.Write($"{l.name} : ");
            //    var kk = dictOfLiquidAndMachines[l].Keys;
            //    foreach (Machine m in kk)
            //    {
            //        Console.Write($"{m.name} : {dictOfLiquidAndMachines[l][m]} ");
            //    }
            //    Console.WriteLine();
            //}

            //Dictionary<Liquid, int> dictLiquidAndMachineQuantity = Service.calcMachineQuantityEachLiquidType(listOfRandomJobs);
            //var k = dictLiquidAndMachineQuantity.Keys; 
            //foreach(Liquid l in k)
            //{
            //    Console.WriteLine($"{l.name} : {dictLiquidAndMachineQuantity[l]}");
            //}




        }
    }
}
