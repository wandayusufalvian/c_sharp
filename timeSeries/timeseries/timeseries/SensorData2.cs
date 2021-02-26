using System;
using System.Collections.Generic;
using System.Text;

namespace timeseries
{
    class SensorData2
    {
        public string _id { get; set; }
        public List<double> sensorData { get; set; }

        public SensorData2()
        {
            sensorData = new List<double>();
        }
    }
}
