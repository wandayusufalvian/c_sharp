using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Service
    {
        public int calcRoverNeeded(MachineType machType, int sizeOfJobs,MarkasRover marRover)
        {
            //hitung kebutuhan rover
            int capEachJob = machType.jobCap;
            int capRover = marRover.listRovers[0].liveCapRover;
            int totalNeeded = sizeOfJobs * capEachJob;
            int roverNeeded = 0;

            if (totalNeeded < capRover)
            {
                return 1; 
            }
            while (totalNeeded > capRover)
            {
                int quotient = capRover / capEachJob;
                int totalAmbil = quotient * capEachJob;
                totalNeeded -= totalAmbil;
                roverNeeded += 1; 
            }
            roverNeeded += 1;
            //jika rover yang dibutuhkan lebih dari yang tersedia
            if (roverNeeded > marRover.availableRover)
            {
                throw new Exception();
            }
            //update jumlah sisa rover yang available 
            marRover.availableRover -= roverNeeded; 
            //return jumlah rover yang dibutuhkan
            return roverNeeded; 
        }

        
    }
}
