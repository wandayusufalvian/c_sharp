using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rovers
{
    public class Machine
    {
        public string name { get; set;}
        public int machineQuantity { get; set; }
        public int liquidDose { get; set; }

        // add new machine here :
        public static readonly Machine Rx = new Machine() { name = "Rx", machineQuantity = 2, liquidDose = 15 };
        public static readonly Machine Flo = new Machine() { name = "Flo", machineQuantity = 1, liquidDose = 20 };
        public static readonly Machine MachineX = new Machine() { name = "MachineX", machineQuantity = 1, liquidDose = 25 };
        public static readonly Machine MachineY = new Machine() { name = "MachineY", machineQuantity = 5, liquidDose = 30 };

    }
}
