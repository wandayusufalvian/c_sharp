using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rovers
{
    public class Service
    {
        //MAIN FUNCTION : 

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
            return ShuffleJobs(jobNotShuffle);
        }

        // calculate containers needed 
        public static Dictionary<Trough, int> calcTroughNeeded(List<Job> listOfRandomJobs)
        {
            Dictionary<Trough, int> dictOfTroughNeeded = new Dictionary<Trough, int>(); // {Trough,quantity of trough needed}
            Dictionary<Liquid, int> dictLiquidAndMachineQuantity = calcMachineQuantityEachLiquidType(listOfRandomJobs); //{Liquid,Total machine quantity}
            Dictionary<Liquid, int> dictLiquidAndVolumeQuantity = calcVolumeAllJobsEachLiquidType(listOfRandomJobs); //{Liquid,Total volume needed from all jobs}
            Dictionary<Liquid, List<Job>> dictOfLiquidAndMachineType = liquidAndMachineType(listOfRandomJobs); //{Liquid, list of machine type}
            //calculate trough needed each liquid type 
            var keys = dictLiquidAndMachineQuantity.Keys; 
            foreach (Liquid l in keys)
            {   
                int remainder;
                int quotient = Math.DivRem(dictLiquidAndVolumeQuantity[l], Trough.initialVolume, out remainder);
                int troughNeeded = (remainder == 0 ? quotient : quotient + 1);
                // if liquid l is needed only by 1 machine and trough needed = 1 
                if (troughNeeded == 1 && dictLiquidAndMachineQuantity[l] == 1) { troughNeeded = 1; }
                // if liquid l is needed only by 1 machine and trough needed > 1 
                // if liquid l is needed by > 1 machine and trough needed = 1 
                // if liquid l is needed by > 1 machine and trough needed > 1
                // + buffer 
                else
                {
                    int maxDose = getMaxDose(dictOfLiquidAndMachineType, l); //buffer each trough 
                    int buffer = troughNeeded * maxDose; //total buffer 
                    int volumePlusBuffer = dictLiquidAndVolumeQuantity[l] + buffer; //recalculate total volume 
                    quotient = Math.DivRem(volumePlusBuffer, Trough.initialVolume, out remainder);
                    troughNeeded = (remainder == 0 ? quotient : quotient + 1); //recalculate trough needed
                    //trough needed < amount of machine that need liquid l 
                    if (troughNeeded < dictLiquidAndMachineQuantity[l])
                    {
                        troughNeeded = dictLiquidAndMachineQuantity[l];
                    }
                }
                dictOfTroughNeeded[new Trough(l)] = troughNeeded;
            }
            return dictOfTroughNeeded;
        }

        //create list of random trough 
        public static List<Trough> createListOfRandomTrough(Dictionary<Trough, int> dictOfTroughNeeded)
        {
            var keys = dictOfTroughNeeded.Keys;
            List <Trough> troughNotShuffle= new List<Trough>();
            foreach (Trough t in keys)
            {
                for(int i=0;i< dictOfTroughNeeded[t]; i++)
                {
                    troughNotShuffle.Add(new Trough(t._liquidType)); 
                }
            }
            return ShuffleTrough(troughNotShuffle);
        }

        // run simulation 
        public static int runSimulation(List<Job> listOfRandomJobs,List<Trough> listOfRandomTrough)
        {
            int amountOfJobLeft = listOfRandomJobs.Count; 
            List<Trough> shuffledTrough;  
            foreach(Job j in listOfRandomJobs)
            {
                shuffledTrough = ShuffleTrough(listOfRandomTrough);
                foreach (Trough t in listOfRandomTrough)
                {
                    bool condition1 = (j._liquidType).Equals(t._liquidType);
                    bool condition2 = t.availableVolume >= j._machineType.liquidDose; 
                    if(condition1 && condition2)
                    {
                        amountOfJobLeft -= 1;
                        t.availableVolume -= j._machineType.liquidDose;
                        break;
                    }
                }
            }
            return amountOfJobLeft;
        }


        // SUPPORT FUNCTIONS :
        static List<Job> ShuffleJobs(List<Job> list)
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

        static List<Trough> ShuffleTrough(List<Trough> list)
        {
            Random rng = new Random();
            int n = list.Count;
            Trough t = null;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                t = list[k];
                list[k] = list[n];
                list[n] = t;
            }
            return list;
        }

        static Dictionary<Liquid, List<Job>> liquidAndMachineType(List<Job> listOfRandomJobs)
        {
            Dictionary<Liquid, List<Job>> dictOfLiquidAndMachines = listOfRandomJobs.GroupBy(o => o._liquidType).ToDictionary(g => g.Key, g => g.ToList().GroupBy(x => x._machineType).Select(group => group.First()).ToList());
            return dictOfLiquidAndMachines; 
        }

        static int getMaxDose(Dictionary<Liquid, List<Job>> dictOfLiquidAndMachineType,Liquid l)
        {
            return dictOfLiquidAndMachineType[l].Select(x => x._machineType.liquidDose).Max(); 
        }

        static Dictionary<Liquid, int> calcMachineQuantityEachLiquidType(List<Job> listOfRandomJobs)
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = reverseListJobToDict(listOfRandomJobs);
            Dictionary<Liquid, int> dictLiquidAndMachineQuantity = new Dictionary<Liquid, int>();
        
            var k = dictOfLiquidAndMachine.Keys;
            foreach (Liquid l in k)
            {
                var k2 = dictOfLiquidAndMachine[l].Keys;
                int total = 0;
                foreach (Machine m in k2)
                {   
                    int jobsNum = dictOfLiquidAndMachine[l][m];
                    int machineNum = m.machineQuantity;
                    //if jobs < machine Quantity
                    if (jobsNum < machineNum)
                    {
                        total += jobsNum;
                    }
                    else
                    {
                        total += machineNum; 
                    }
                }
                dictLiquidAndMachineQuantity.Add(l, total);
            }

            return dictLiquidAndMachineQuantity;
        }

        static Dictionary<Liquid, Dictionary<Machine, int>> reverseListJobToDict(List<Job> listOfRandomJobs)
        {
            Dictionary<Liquid, Dictionary<Machine, int>> dictOfLiquidAndMachine = new Dictionary<Liquid, Dictionary<Machine, int>>();
            foreach(Job j in listOfRandomJobs)
            {
                // initiate first if there is no key 
                if (!dictOfLiquidAndMachine.ContainsKey(j._liquidType))
                {
                    dictOfLiquidAndMachine[j._liquidType] = new Dictionary<Machine, int>();
                }
                if (!dictOfLiquidAndMachine[j._liquidType].ContainsKey(j._machineType))
                {
                    dictOfLiquidAndMachine[j._liquidType].Add(j._machineType, 1);
                }
                else
                {
                    dictOfLiquidAndMachine[j._liquidType][j._machineType] += 1;
                }
            }

            return dictOfLiquidAndMachine;

        }
        static Dictionary<Liquid, int> calcVolumeAllJobsEachLiquidType(List<Job> listOfRandomJobs)
        {
            Dictionary<Liquid, int> dictOfTotalVolumeNeededEachLiquidType = new Dictionary<Liquid, int>();
           
            foreach (Job j in listOfRandomJobs)
            {
                if (!dictOfTotalVolumeNeededEachLiquidType.ContainsKey(j._liquidType))
                {
                    dictOfTotalVolumeNeededEachLiquidType[j._liquidType] = j._machineType.liquidDose;
                }
                else
                {
                    dictOfTotalVolumeNeededEachLiquidType[j._liquidType] += j._machineType.liquidDose;
                }
            }

            return dictOfTotalVolumeNeededEachLiquidType;
        }

        // extra FUNCTION for testing purpose : 
        public static void printContainerLeftVolume(List<Trough> listOfRandomTrough)
        {
            Console.WriteLine("Left Volume: ");
            foreach (Trough t in listOfRandomTrough)
            {
                Console.WriteLine($"{t._liquidType.name} : {t.availableVolume}");
            }
        }

        public static Dictionary<Liquid, int> volumeNeededEachLiquid(List<Job> listOfRandomJobs)
        {
            return calcVolumeAllJobsEachLiquidType(listOfRandomJobs);
        }

        public static Dictionary<Liquid, int> liquidConsumedEachTrough(List<Trough> listOfRandomTrough)
        {
            Dictionary<Liquid, int> liquidConsumed = new Dictionary<Liquid, int>();// {jenis liquid,yg dikonsumsi diseluruh container dg jenis liquid yg sama}
            foreach (Trough t in listOfRandomTrough)
            {
                int volumeConsumed = Trough.initialVolume - t.availableVolume;
                if (!liquidConsumed.ContainsKey(t._liquidType))
                {

                    liquidConsumed[t._liquidType] = volumeConsumed;
                }
                else
                {
                    liquidConsumed[t._liquidType] += volumeConsumed;
                }
            }
            return liquidConsumed;
        }

    }
}
