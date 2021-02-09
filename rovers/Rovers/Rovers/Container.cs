using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Container
    {

        public const int maxCapContainer= 250;
        public const int deadVolume= 20;

        public int availableVolume { get; set; }
        public string name { get; set; }

        public Container(string name)
        {
            this.availableVolume = maxCapContainer - deadVolume; 
            this.name = name;
        }
    }
}
