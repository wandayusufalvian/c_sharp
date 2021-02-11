using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Job
    {
        public Machine _machineType;
        public Liquid _liquidType; 
        
        public Job(Liquid liquidType , Machine machineType)
        {
            _machineType = machineType;
            _liquidType = liquidType;
        }
    }
}
