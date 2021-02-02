using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class MachineType
    {
        public class Enum
        {
            public const int jobRX = 15;
            public const int jobFlow = 20; 
        }

        public static readonly MachineType RX = new MachineType() { jobCap = Enum.jobRX, name = "RX" };
        public static readonly MachineType Flow = new MachineType() { jobCap = Enum.jobFlow, name = "Flow" };

        public int jobCap { get; set; }
        public string name { get; set; }

    }
}
