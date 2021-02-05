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
        }
    }
}
