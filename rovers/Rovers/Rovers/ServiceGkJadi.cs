using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    class ServiceGkJadi
    {
        public int calcContainerNeeded(Job[] loj, int containerDeadVolume)
        {
            // Job and Container initialization 
            Job RX = Job.RX;
            Job Flow = Job.Flow;
            Container container = new Container(containerDeadVolume);
            // Get max volume from each Machine Job Capacity
            int maxEachJobVolume = Math.Max(RX.jobCap, Flow.jobCap);
            int lengthOfJobLists = loj.Length;
            int totalVolumeNeeded = maxEachJobVolume * lengthOfJobLists;
            int remainder;
            int quotient = Math.DivRem(totalVolumeNeeded, container.availableVolume, out remainder);
            //quotient = 0 means that it only needed 1 container
            //to handle Rx and Flow that request container at the same time, we need minimum 2 container when quotient=0 or 1 with remainder =0
            if (quotient == 0 || (quotient == 1 && remainder == 0))
            {
                return 2;
            }
            else
            {
                return remainder == 0 ? quotient : quotient + 1;
            }


            //public (int,int) calcContainerNeeded(Jobs[] loj)
            //{
            //    // calculate amount of job (RxA,RxB,FlowA)
            //    int RxA = 0;
            //    int RxB = 0;
            //    int FlowA=0; 
            //    foreach(Jobs j in loj)
            //    {
            //        if (j.Equals(Jobs.RxA))
            //        {
            //            RxA += 1; 
            //        }
            //        else if (j.Equals(Jobs.RxB))
            //        {
            //            RxB += 1;
            //        }
            //        else
            //        {
            //            FlowA += 1;
            //        }
            //    }

            //    //CALCULATE CONTAINER A THAT ARE NEEDED....
            //    int totalVolumeJobANeeded = Jobs.RxA.jobCap * RxA + Jobs.FlowA.jobCap * FlowA;
            //    int remainderA;
            //    Container containerA = new Container("TroughA"); 
            //    int quotientA= Math.DivRem(totalVolumeJobANeeded, containerA.availableVolume, out remainderA);
            //    int tempContainerANeeded = (remainderA == 0 ? quotientA : quotientA + 1);
            //    int amountOfContainerANeeded = 0;
            //    // if no (RxA and FlowA)
            //    if (RxA + FlowA == 0)
            //    {
            //        amountOfContainerANeeded = 0;
            //    }
            //    else
            //    {
            //        //if (only RxA or FlowA) and total volum needed < 1 container volume 
            //        //no buffer 
            //        bool condition1 = tempContainerANeeded <= 1; 
            //        if ((RxA == 0 && condition1) || (FlowA == 0 && condition1))
            //        {
            //            amountOfContainerANeeded = 1; 
            //        }

            //        //if (RxA and FlowA) and temporary total volum needed < 1 container volume -> no buffer, but need 2 container 
            //        //if (only RxA or FlowA) and temporary total volum needed > 1 container volume -> (+) buffer
            //        //if (RxA and FlowA) and total temporary volum needed > 1 container volume -> (+) buffer
            //        else
            //        {
            //            if (tempContainerANeeded <= 1)
            //            {
            //                amountOfContainerANeeded = 2;
            //            }
            //            else
            //            {
            //                int bufferA = tempContainerANeeded * Math.Max(Jobs.RxA.jobCap, Jobs.FlowA.jobCap);
            //                int totalVolumeANeededFinal = totalVolumeJobANeeded + bufferA;
            //                quotientA = Math.DivRem(totalVolumeANeededFinal, containerA.availableVolume, out remainderA);
            //                amountOfContainerANeeded = (remainderA == 0 ? quotientA : quotientA + 1);
            //            }
            //        }

            //    }

            //    //CALCULATE CONTAINER B THAT ARE NEEDED....
            //    // no buffer because only Rx have B liquid
            //    int totalVolumeJobBNeeded = Jobs.RxB.jobCap * RxB;
            //    int remainderB;
            //    Container containerB = new Container("TroughB");
            //    int quotientB = Math.DivRem(totalVolumeJobBNeeded, containerB.availableVolume, out remainderB);
            //    int tempContainerBNeeded = (remainderB == 0 ? quotientB : quotientB + 1);

            //    int amountOfContainerBNeeded = 0;
            //    if (RxB == 0)
            //    {
            //        amountOfContainerBNeeded = 0;
            //    }
            //    else
            //    {
            //        if(tempContainerBNeeded<=1)
            //        {
            //            amountOfContainerBNeeded = 1;
            //        }
            //        else
            //        {
            //            int bufferB = tempContainerBNeeded * Jobs.RxB.jobCap;
            //            int totalVolumeBNeededFinal = totalVolumeJobBNeeded + bufferB;
            //            quotientB = Math.DivRem(totalVolumeBNeededFinal, containerB.availableVolume, out remainderB);
            //            amountOfContainerBNeeded = (remainderB == 0 ? quotientB : quotientB + 1);
            //        }
            //    }
            //    return (amountOfContainerANeeded,amountOfContainerBNeeded);
            //}

            /*


public Container[] arrayOfContainers(string[] arrayOfJobName, int[] arrayOfContainerNeeded)
{
    // input berupa list of jumlah kontainer tiap jenis cairan 
    int amountOfContainer = 0;
    foreach (int x in arrayOfJobAmount)
    {
        amountOfContainer += x;
    }
    Container[] arrayOfContainer = new Container[amountOfContainer];

    int k = 0;
    for (int i = 0; i < arrayOfJobAmount.Length; i++)
    {
        for (int j = 0; j < arrayOfJobAmount[i]; j++)
        {
            arrayOfContainer[k] = new Container(arrayOfJobName[i]);

        }
    }

    for (i = 0; i < amountOfContainerA; i++)
    {
        arrayOfContainer[i] = new Container("TroughA");
    }
    for (int j = i; j < amountOfContainer; j++)
    {
        arrayOfContainer[j] = new Container("TroughB");
    }
    return arrayOfContainer;
}

*/

            public static int runSimulation(Jobs[] JobsArray, Container[] ContainerArray)
            {

                int lengthOfContainer = ContainerArray.Length;
                int amountOfJobLeft = JobsArray.Length;
                List<int> randomList; // a list of random number for sequence of container index 
                for (int i = 0; i < JobsArray.Length; i++)
                {   //mengacak urutan container yg akan di-traverse
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

        }
    }
}
