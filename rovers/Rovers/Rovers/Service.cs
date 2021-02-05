using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Service
    { 
        public (int,int) calcContainerNeeded(Job[] loj)
        {
            // calculate amount of job (RxA,RxB,FlowA)
            int RxA = 0;
            int RxB = 0;
            int FlowA=0; 
            foreach(Job j in loj)
            {
                if (j.Equals(Job.RxA))
                {
                    RxA += 1; 
                }
                else if (j.Equals(Job.RxB))
                {
                    RxB += 1;
                }
                else
                {
                    FlowA += 1;
                }
            }

            //CALCULATE CONTAINER A THAT ARE NEEDED....
            int totalVolumeJobANeeded = Job.RxA.jobCap * RxA + Job.FlowA.jobCap * FlowA;
            int remainderA;
            Container containerA = new Container("TroughA"); 
            int quotientA= Math.DivRem(totalVolumeJobANeeded, containerA.availableVolume, out remainderA);
            int tempContainerANeeded = (remainderA == 0 ? quotientA : quotientA + 1);
            int amountOfContainerANeeded = 0;
            // if no (RxA and FlowA)
            if (RxA + FlowA == 0)
            {
                amountOfContainerANeeded = 0;
            }
            else
            {
                //if (only RxA or FlowA) and total volum needed < 1 container volume 
                //no buffer 
                bool condition1 = tempContainerANeeded <= 1; 
                if ((RxA == 0 && condition1) || (FlowA == 0 && condition1))
                {
                    amountOfContainerANeeded = 1; 
                }

                //if (RxA and FlowA) and temporary total volum needed < 1 container volume -> no buffer, but need 2 container 
                //if (only RxA or FlowA) and temporary total volum needed > 1 container volume -> (+) buffer
                //if (RxA and FlowA) and total temporary volum needed > 1 container volume -> (+) buffer
                else
                {
                    if (tempContainerANeeded <= 1)
                    {
                        amountOfContainerANeeded = 2;
                    }
                    else
                    {
                        int bufferA = tempContainerANeeded * Math.Max(Job.RxA.jobCap, Job.FlowA.jobCap);
                        int totalVolumeANeededFinal = totalVolumeJobANeeded + bufferA;
                        quotientA = Math.DivRem(totalVolumeANeededFinal, containerA.availableVolume, out remainderA);
                        amountOfContainerANeeded = (remainderA == 0 ? quotientA : quotientA + 1);
                    }
                }

            }
            
            //CALCULATE CONTAINER B THAT ARE NEEDED....
            // no buffer because only Rx have B liquid
            int totalVolumeJobBNeeded = Job.RxB.jobCap * RxB;
            int remainderB;
            Container containerB = new Container("TroughB");
            int quotientB = Math.DivRem(totalVolumeJobBNeeded, containerB.availableVolume, out remainderB);
            int tempContainerBNeeded = (remainderB == 0 ? quotientB : quotientB + 1);

            int amountOfContainerBNeeded = 0;
            if (RxB == 0)
            {
                amountOfContainerBNeeded = 0;
            }
            else
            {
                if(tempContainerBNeeded<=1)
                {
                    amountOfContainerBNeeded = 1;
                }
                else
                {
                    int bufferB = tempContainerBNeeded * Job.RxB.jobCap;
                    int totalVolumeBNeededFinal = totalVolumeJobBNeeded + bufferB;
                    quotientB = Math.DivRem(totalVolumeBNeededFinal, containerB.availableVolume, out remainderB);
                    amountOfContainerBNeeded = (remainderB == 0 ? quotientB : quotientB + 1);
                }
            }
            
            
            return (amountOfContainerANeeded,amountOfContainerBNeeded);
        }

        public Container[] arrayOfContainers(int amountOfContainerA,int amountOfContainerB)
        {
            int amountOfContainer = amountOfContainerA + amountOfContainerB;
            Container[] arrayOfContainer= new Container[amountOfContainer];
            int i = 0;
            for(i = 0; i < amountOfContainerA; i++)
            {
                arrayOfContainer[i] = new Container("TroughA");
            }
            for (int j = i; j < amountOfContainer; j++)
            {
                arrayOfContainer[j] = new Container("TroughB");
            }
            return arrayOfContainer; 
        }

        public Job[] arrayOfJobs(int amountOfRxAJob, int amountOfRxBJob,int amountOfFlowAJob)
        {

            int lengthOfJobArray = amountOfFlowAJob + amountOfRxAJob+ amountOfRxBJob;
            Job[] arrayOfJobs = new Job[lengthOfJobArray];
            Random rd = new Random();
            //worst case => list of jobs is random  
            //input Rx Job to ArrayOfJobs randomly
            int i = 0;
            while(i < amountOfRxAJob)
            {
                int randomIndex = rd.Next(0, lengthOfJobArray);
                if (arrayOfJobs[randomIndex] == null)
                {
                    arrayOfJobs[randomIndex] = Job.RxA;
                    i += 1;
                }
            }
            i = 0; 
            while (i < amountOfRxBJob)
            {
                int randomIndex = rd.Next(0, lengthOfJobArray);
                if (arrayOfJobs[randomIndex] == null)
                {
                    arrayOfJobs[randomIndex] = Job.RxB;
                    i += 1;
                }

            }
            i = 0;
            //input Flow Job to ArrayOfJobs randomly
            while (i < amountOfFlowAJob)
            {
                int randomIndex = rd.Next(0, lengthOfJobArray);
                if (arrayOfJobs[randomIndex] == null)
                {
                    arrayOfJobs[randomIndex] = Job.FlowA;
                    i += 1;
                }
                
            }
            return arrayOfJobs; 
        }

        public int runSimulation(Job[] JobsArray, Container[] ContainerArray)
        {
            /*
             iterate each job to a list of random unique interger as containerArray index.
             iterate container to find container with available volume > Job volume capacity
             */
            int lengthOfContainer = ContainerArray.Length;
            int amountOfJobLeft = JobsArray.Length;
            List<int> randomList; // a list of random number for sequence of container index 
            for (int i = 0; i < JobsArray.Length; i++)
            {
                randomList = RandomListOfContainerIndex(lengthOfContainer);
                
                //request container randomly 
                foreach (int j in randomList)
                {
                    bool condition1 = (JobsArray[i].name.Equals("FlowA") || JobsArray[i].name.Equals("RxA")) && ContainerArray[j].name.Equals("TroughA");
                    bool condition2 = JobsArray[i].name.Equals("RxB") && ContainerArray[j].name.Equals("TroughB");
                    if ((condition1 || condition2) && ContainerArray[j].availableVolume >= JobsArray[i].jobCap)
                    {
                        amountOfJobLeft -= 1;
                        ContainerArray[j].availableVolume -= JobsArray[i].jobCap;
                        break; 
                    }
                }
            }
            return amountOfJobLeft;
        }

        public List<int> RandomListOfContainerIndex(int lengthOfContainer)
        {   
            List<int> randomList = new List<int>();
            Random a = new Random(); 
            int MyNumber;

            while(randomList.Count<lengthOfContainer)
            {
                MyNumber = a.Next(0, lengthOfContainer );
                if (!randomList.Contains(MyNumber))
                {
                    randomList.Add(MyNumber);
                }
            }

            return randomList;
        }
    }
}
