using System;
using System.Collections.Generic;
using System.Text;

namespace timeseries
{
    public class SensorData
    {
        public DateTime datetime { get; set; }
        public List<double> sensorData{ get; set; }

        public SensorData()
        {
            sensorData = new List<double>(); 
        }
    }
}
