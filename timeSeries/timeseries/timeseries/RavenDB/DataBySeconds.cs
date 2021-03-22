using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace timeseries
{
    class DataBySeconds : AbstractIndexCreationTask<SensorData>
    {
        public DataBySeconds()
        {

            //TimeSpan elapsedSpan = new TimeSpan(sensor.datetime.Ticks - start.Ticks);
            this.Map = sensors => from sensor in sensors
                                  select new { Seconds= (new TimeSpan(sensor.datetime.Ticks - (new DateTime(2021, 1, 1, 0, 0, 0)).Ticks)).TotalSeconds}; 
        }
    }
}
