using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Jobs
    {
        // (key,value)=(jenis job,jumlah job)
        private Dictionary<Job, int> dictOfJobs = new Dictionary<Job,int>();
        private int amountOfJob = 0;
        private Job[] arrayOfRandomJobs;

        public Jobs()
        {

        }

        public void addJob(Dictionary<string, List<string>> dictOfLiquidAndMachine, List<int> listOfAmount)
        {
            var keys = dictOfLiquidAndMachine.Keys;
            int i = 0; 
            foreach(string k in keys)
            {
                for(int j = 0; j < dictOfLiquidAndMachine[k].Count; j++)
                {
                    dictOfJobs.Add(new Job(k, dictOfLiquidAndMachine[k][j]), listOfAmount[i]);
                    i += 1;
                }
            }
        }

        private void amountOfJobType()
        {
            var keys = dictOfJobs.Keys;
            foreach (Job j in keys)
            {
                amountOfJob += dictOfJobs[j];
            }
        }

        public void printJobType()
        {
            var keys = dictOfJobs.Keys;
            foreach(Job j in keys)
            {
                Console.WriteLine($"Job Type : {j.jobName} , {j.liquidType} => Job Capacity : {j.volumeCapacity}, Amount of Jobs : {dictOfJobs[j]}");
            }
        }

        //create array of random jobs from dictofjobs 
        public Job[] createArrayOfRandomJobs()
        {
            amountOfJobType();
            List<int> listOfRandomIndex = new List<int>();
            Random random = new Random();
            HashSet<int> myhash = new HashSet<int>();
            arrayOfRandomJobs = new Job[amountOfJob];

            var keys = dictOfJobs.Keys;

            //create list of random index 
            while (listOfRandomIndex.Count < amountOfJob)
            {

                int a = random.Next(0, amountOfJob);
                if (myhash.Add(a))
                {
                    listOfRandomIndex.Add(a);
                }
            }

            //create array of random jobs with index based on listOfRandomIndex
            int k = 0;
            foreach (Job j in keys)
            {
                for(int i = 0; i < dictOfJobs[j]; i++)
                {
                    arrayOfRandomJobs[listOfRandomIndex[k]] = j;
                    k += 1;
                }
            }
            return arrayOfRandomJobs; 
        }

        public void printArrayOfRandomJobs()
        {
            foreach(Job j in arrayOfRandomJobs)
            {
                Console.WriteLine(j.jobName + j.liquidType);
            }
        }

        
    }
}
