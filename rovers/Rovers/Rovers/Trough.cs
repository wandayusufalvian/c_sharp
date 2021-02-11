using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Trough
    {
        public const int maxVolume= 250;
        public const int deadVolume= 20;

        public int availableVolume { get; set; }
        public static int initialVolume = maxVolume - deadVolume; 
        public Liquid _liquidType { get;  }
        public Trough(Liquid liquidType)
        {
            this.availableVolume = maxVolume - deadVolume;
            this._liquidType = liquidType;
        }
    }
}
