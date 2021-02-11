using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rovers
{
    public class Service
    {
        // create list of random jobs
        public static List<Job> createListOfRandomJobs(Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine)
        {
            var keys = dictOfLiquidAndMachine.Keys;
            List<Job> jobNotShuffle = new List<Job>();

            foreach (Liquid k in keys)
            {
                var jobTypeKey = dictOfLiquidAndMachine[k].Keys;
                foreach (Machine j in jobTypeKey)
                {
                    for (int i = 0; i < dictOfLiquidAndMachine[k][j]; i++)
                    {
                        jobNotShuffle.Add(new Job(k, j));
                    }
                }
            }
            return Shuffle(jobNotShuffle);
        }

        // calculate containers needed 
        public static Dictionary<Trough, int> calcTroughNeeded(List<Job> listOfRandomJobs)
        {
            Dictionary<Trough, int> dictOfTroughNeeded = new Dictionary<Trough, int>(); // {Trough,quantity of trough needed}
            Dictionary<Liquid, int> dictLiquidAndMachineQuantity = calcMachineQuantityThatNeedEachLiquidType(listOfRandomJobs); //{Liquid,Total machine quantity}
            List<Liquid> keys = dictLiquidAndMachineQuantity.Keys.ToList();
            Dictionary<Liquid, int> dictLiquidAndVolumeQuantity = calcTotalVolumeNeededEachLiquidType(listOfRandomJobs, keys); //{Liquid,Total volume needed from all jobs}

            //hitung trough yg dibutuhkan per liquid 
            //sebelum ada buffer dan sebelum mempertimbangkan ada berapa mesin yg butuh 
            foreach (Liquid l in keys)
            {
                int remainder;
                int quotient = Math.DivRem(dictLiquidAndVolumeQuantity[l], Trough.initialVolume, out remainder);
                int troughNeeded = (remainder == 0 ? quotient : quotient + 1);
                //jika cuma 1 mesin dan cuma butuh 1 trough=>gk perlu buffer 
                if (troughNeeded == 1 && dictLiquidAndMachineQuantity[l] == 1) { troughNeeded = 1; }
                // jika cuma 1 mesin dan butuh > 1 trough 
                // jika lebih dari satu mesin dan butuh >= 1 trough
                // => butuh buffer
                else
                {
                    int maxDose = Machine.getMaxDose();
                    int buffer = troughNeeded * maxDose;
                    int volumePlusBuffer = dictLiquidAndVolumeQuantity[l] + buffer;
                    quotient = Math.DivRem(volumePlusBuffer, Trough.initialVolume, out remainder);
                    troughNeeded = (remainder == 0 ? quotient : quotient + 1);
                    //jika trough<jumlah mesin
                    if (troughNeeded < dictLiquidAndMachineQuantity[l])
                    {
                        troughNeeded = dictLiquidAndMachineQuantity[l];
                    }
                }
                dictOfTroughNeeded[new Trough(l)] = troughNeeded;
            }
            return dictOfTroughNeeded;
        }

        // run simulation 

        //public static int runSimulation(Job[] JobsArray, Container[] ContainerArray)
        //{

        //    int lengthOfContainer = ContainerArray.Length;
        //    int amountOfJobLeft = JobsArray.Length;
        //    List<int> randomList; // a list of random number for sequence of container index 
        //    for (int i = 0; i < JobsArray.Length; i++)
        //    {   //mengacak urutan container yg akan di-traverse
        //        randomList = RandomListOfContainerIndex(lengthOfContainer);

        //        //request container randomly 
        //        foreach (int j in randomList)
        //        {
        //            bool condition1 = JobsArray[i].liquidType.Equals(ContainerArray[j].name);
        //            bool condition2 = ContainerArray[j].availableVolume >= JobsArray[i].volumeCapacity; 
        //            if (condition1 && condition2)
        //            {
        //                amountOfJobLeft -= 1;
        //                ContainerArray[j].availableVolume -= JobsArray[i].volumeCapacity;
        //                break; 
        //            }
        //        }
        //    }
        //    return amountOfJobLeft;
        //}

        // support functions (and also for testing purpose):
        static List<Job> Shuffle(List<Job> list)
        {
            Random rng = new Random();
            int n = list.Count;
            Job j = null;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                j = list[k];
                list[k] = list[n];
                list[n] = j;
            }
            return list;
        }
        static Dictionary<Liquid, int> calcMachineQuantityThatNeedEachLiquidType(List<Job> listOfRandomJobs)
        {
            Dictionary<Liquid, List<Job>> dictOfLiquidAndMachines = listOfRandomJobs.GroupBy(o => o._liquidType).ToDictionary(g => g.Key, g => g.ToList());
            Dictionary<Liquid, int> dictLiquidAndMachineQuantity = new Dictionary<Liquid, int>();
            var k = dictOfLiquidAndMachines.Keys;
            foreach (Liquid l in k)
            {
                int total = 0;
                foreach (Job j in dictOfLiquidAndMachines[l].ToList()) { total += j._machineType.machineQuantity; }
                dictLiquidAndMachineQuantity[l] = total;
            }
            return dictLiquidAndMachineQuantity;
        }
        static Dictionary<Liquid, int> calcTotalVolumeNeededEachLiquidType(List<Job> listOfRandomJobs, List<Liquid> keys)
        {
            Dictionary<Liquid, int> dictOfTotalVolumeNeededEachLiquidType = new Dictionary<Liquid, int>();
            foreach (Liquid l in keys) { dictOfTotalVolumeNeededEachLiquidType[l] = 0; }

            foreach (Job j in listOfRandomJobs) { dictOfTotalVolumeNeededEachLiquidType[j._liquidType] += j._machineType.liquidDose; }

            return dictOfTotalVolumeNeededEachLiquidType;
        }
        //static List<int> RandomListOfContainerIndex(int lengthOfContainer)
        //{
        //    List<int> randomList = new List<int>();
        //    Random a = new Random();
        //    int MyNumber;

        //    while (randomList.Count < lengthOfContainer)
        //    {
        //        MyNumber = a.Next(0, lengthOfContainer);
        //        if (!randomList.Contains(MyNumber))
        //        {
        //            randomList.Add(MyNumber);
        //        }
        //    }
        //    return randomList;
        //}

        //public static void printContainerLeftVolume(Container[] ArrayOfContainers)
        //{
        //    Console.WriteLine("Left Volume: ");
        //    foreach (Container c in ArrayOfContainers)
        //    {

        //        Console.WriteLine($"{c.name} : {c.availableVolume}");
        //    }
        //}

        //public static Dictionary<string,int> volumeNeededEachLiquid(Job[] aoj)
        //{
        //    Dictionary<string, int> liquidNeeded = new Dictionary<string, int>();
        //    foreach (Job j in aoj)
        //    {
        //        if (!liquidNeeded.ContainsKey(j.liquidType))
        //        {
        //            liquidNeeded[j.liquidType] = j.volumeCapacity;
        //        }
        //        else
        //        {
        //            liquidNeeded[j.liquidType] += j.volumeCapacity; 
        //        }
        //    }
        //    return liquidNeeded;
        //}

        //public static Dictionary<string, int> liquidConsumedEachContainerType(Container[] ArrayOfContainers)
        //{
        //    Dictionary<string, int> liquidConsumed = new Dictionary<string, int>();// {jenis liquid,yg dikonsumsi diseluruh container dg jenis liquid yg sama}
        //    foreach (Container c in ArrayOfContainers)
        //    {
        //        int volumeConsumed = c.initialVolume - c.availableVolume;
        //        if (!liquidConsumed.ContainsKey(c.name))
        //        {

        //            liquidConsumed[c.name] = volumeConsumed;
        //        }
        //        else
        //        {
        //            liquidConsumed[c.name] += volumeConsumed;
        //        }
        //    }
        //    return liquidConsumed;
        //}

    }
}
