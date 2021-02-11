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
                {Liquid.A, new Dictionary<Machine,int>{{Machine.Rx,2},{Machine.Flo, 3}}},
                {Liquid.B, new Dictionary<Machine,int>{{Machine.Rx, 1}}},
                {Liquid.C, new Dictionary<Machine,int>{{Machine.Rx, 2},{ Machine.Flo, 2}}},
                {Liquid.D, new Dictionary<Machine,int>{{Machine.Flo, 1},{Machine.MachineX, 2}}}
            };
            
            /*test array of random job*/
            List<Job> rj = Service.createListOfRandomJobs(dictOfLiquidAndMachine);
            //foreach (Job j in rj)
            //{
            //    Console.WriteLine($"{j._machineType.name} | {j._liquidType.name} | {j._machineType.liquidDose}");
            //}
            Dictionary<Trough, int> dotn = Service.calcTroughNeeded(rj);
            var x = dotn.Keys; 
            foreach(Trough t in x)
            {
                Console.WriteLine($"{t._liquidType.name} | {t.availableVolume} | {dotn[t]}");
            }

            //Console.WriteLine(Containers.calcTroughNeeded(rj));


        }
    }
}
