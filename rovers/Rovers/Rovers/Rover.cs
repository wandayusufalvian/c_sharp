using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Rover
    {
        public int idRover { get; set; }

        public const int maxCapRover = 250;
        public const int deadVolume = 30;
        public readonly int liveCapRover=maxCapRover - deadVolume; 
 
        //public int leftCap { get; set; }

        public Rover(int id)
        {
            this.idRover = id;
            //this.leftCap = liveCapRover; 
        }
    }
}
