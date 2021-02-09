using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rovers
{
    public class Containers
    {
        Dictionary<Job, int> dictOfJobs;
        Dictionary<string, List<Job>> dictOfLiquidAndMachine;
        public Dictionary<Container, int> calcContainerNeeded(Job[] arrayOfJobs)
        {   
            Dictionary<Container, int> dictOfContainerNeeded = new Dictionary<Container, int>(); // {jenis container(berdasarkan cairan),jumlah container yg dibutuhkan}
            dictOfLiquidAndMachine = new Dictionary<string, List<Job>>(); // {jenis cairan,list of jenis mesin}
            dictOfJobs = new Dictionary<Job, int>(); //{jenis job,jumlah job}

            //inisialisasi dictOfJobs
            foreach(Job j in arrayOfJobs)
            {
                if (!dictOfJobs.ContainsKey(j))
                {
                    dictOfJobs.Add(j, 1); 
                }
                else
                {
                    dictOfJobs[j] = dictOfJobs[j] + 1;
                }
            }

            var jobs = dictOfJobs.Keys;

            //inisialisasi dictOfLiquidAndMachine
            foreach(Job j in jobs)
            {
                string liq = j.liquidType;
                if (!dictOfLiquidAndMachine.ContainsKey(liq))
                {
                    dictOfLiquidAndMachine.Add(liq, new List<Job> {j});
                }
                else
                {
                    dictOfLiquidAndMachine[liq].Add(j);
                }
            }

            var liquids = dictOfLiquidAndMachine.Keys;

            //inisialisasi nilai container yg dibutuhkan 
            foreach (string l in liquids)
            {
                dictOfContainerNeeded.Add(new Container(l), 0); // nama container=nama liquid
            }
            //hitung container needed
            var containers = dictOfContainerNeeded.Keys; 
            foreach(Container c in containers.ToList()) //jika tidak diubah ke list akan ada exception karena nilai di dictCOntainerNeeded berubah di tiap iterasinya
            {   
                if (dictOfLiquidAndMachine[c.name].Count == 2)
                {
                    int containerNeeded = calcContainerNeededIfTwoMachines(c);
                    dictOfContainerNeeded[c] = containerNeeded;
                }
                else if(dictOfLiquidAndMachine[c.name].Count == 1)
                {
                    int containerNeeded = calcContainerNeededIfOneMachine(c);
                    dictOfContainerNeeded[c] = containerNeeded;
                }
            }
            return dictOfContainerNeeded;
        }

        int calcContainerNeededIfTwoMachines(Container container)
        {
            string l = container.name;
            Job job1 = dictOfLiquidAndMachine[l][0];
            Job job2 = dictOfLiquidAndMachine[l][1];
            int machine1Jobs = dictOfJobs[job1]; //jumlah jobs di mesin 1 untuk liquid l
            int machine2Jobs = dictOfJobs[job2]; //jumlah jobs di mesin 2 untuk liquid l

            int totalVolumeJobNeeded = job1.volumeCapacity * machine1Jobs + job2.volumeCapacity * machine2Jobs;
            int remainder;
            int quotient = Math.DivRem(totalVolumeJobNeeded, container.availableVolume, out remainder);
            int tempContainerNeeded = (remainder == 0 ? quotient : quotient + 1);
            int amountOfContainerNeeded = 0;

            if (tempContainerNeeded <= 1)
            {
                amountOfContainerNeeded = 2;
            }
            else
            {
                int buffer = tempContainerNeeded * Math.Max(job1.volumeCapacity, job2.volumeCapacity);
                int totalVolumeNeededFinal = totalVolumeJobNeeded + buffer;
                quotient = Math.DivRem(totalVolumeNeededFinal, container.availableVolume, out remainder);
                amountOfContainerNeeded = (remainder == 0 ? quotient : quotient + 1);
            }
            return amountOfContainerNeeded;
        }

        int calcContainerNeededIfOneMachine(Container container)
        {
            string l = container.name;
            Job job1 = dictOfLiquidAndMachine[l][0];
            int machine1Jobs = dictOfJobs[job1]; //jumlah jobs di mesin 1 untuk liquid l

            int totalVolumeJobNeeded = job1.volumeCapacity * machine1Jobs; 
            int remainder;
            int quotient = Math.DivRem(totalVolumeJobNeeded, container.availableVolume, out remainder);
            int tempContainerNeeded = (remainder == 0 ? quotient : quotient + 1);
            int amountOfContainerNeeded = 0;

            if (tempContainerNeeded <= 1)
            {
                amountOfContainerNeeded = 1;
            }
            else
            {
                int buffer =job1.volumeCapacity;
                int totalVolumeNeededFinal = totalVolumeJobNeeded + buffer;
                quotient = Math.DivRem(totalVolumeNeededFinal, container.availableVolume, out remainder);
                amountOfContainerNeeded = (remainder == 0 ? quotient : quotient + 1);
            }
            return amountOfContainerNeeded;
        }

        public Container[] createArrayOfContainers(Dictionary<Container, int> dictOfContainerNeeded)
        {
            int amountOfAllContainers=dictOfContainerNeeded.Values.ToList().Sum();
            
            List<int> listOfRandomIndex = new List<int>();
            Container[] arrayOfRandomContainers = new Container[amountOfAllContainers];

            var keys = dictOfContainerNeeded.Keys;
            //create array of random jobs with index based on listOfRandomIndex
            int k = 0;
            foreach (Container c in keys)
            {
                for (int i = 0; i < dictOfContainerNeeded[c]; i++)
                {
                    arrayOfRandomContainers[k] = new Container(c.name); //harus buat object baru
                    k += 1;
                }
            }
            return arrayOfRandomContainers;
        }

    }
}
