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

        // initiate new machine here :
        public static readonly Machine Rx = new Machine() { name = "Rx", machineQuantity = 2, liquidDose = 15 };
        public static readonly Machine Flo = new Machine() { name = "Flo", machineQuantity = 1, liquidDose = 20 };
        public static readonly Machine MachineX = new Machine() { name = "MachineX", machineQuantity = 1, liquidDose = 25 };
        // after that : don't forget to update new machine object in this list 
        public static List<Machine> listOfMachine = new List<Machine>(){Rx,Flo,MachineX};

        public static int getMaxDose()
        {
            return listOfMachine.Max(x => x.liquidDose);
        }

    }
}
