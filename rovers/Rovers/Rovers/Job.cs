using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Job
    {
        public class Enum
        {
            public const int jobRxA = 15;
            public const int jobRxB = 15;
            public const int jobFlowA = 20;
        }

        public static readonly Job RxA = new Job() { jobCap = Enum.jobRxA, name = "RxA" };
        public static readonly Job RxB = new Job() { jobCap = Enum.jobRxB, name = "RxB" };
        public static readonly Job FlowA = new Job() { jobCap = Enum.jobFlowA, name = "FlowA" };
        
        public int jobCap { get; set; }
        public string name { get; set; }
    }
}
