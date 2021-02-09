using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Job
    {
        public string jobName;
        public string liquidType; 
        public int volumeCapacity;

        Dictionary<string, int> dict = new Dictionary<string, int>(){{"Rx",15}, {"Flow",20}};
        
        public Job(string liquidType, string machineType)
        {
            this.jobName = machineType;
            this.liquidType = liquidType;
            this.volumeCapacity = dict[machineType]; 
        }
    }
}
