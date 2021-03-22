using System;
using System.Collections.Generic;
using System.Text;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace timeseries
{
    class DataBySecondsModFive : AbstractIndexCreationTask<SensorData>
    {
        public DataBySecondsModFive()
        {

            //TimeSpan elapsedSpan = new TimeSpan(sensor.datetime.Ticks - start.Ticks);
            this.Map = sensors => from sensor in sensors
                                  select new { DateTime=sensor.datetime, 
                                      ModFive = (new TimeSpan(sensor.datetime.Ticks - (new DateTime(2021, 1, 1, 0, 0, 0)).Ticks)).TotalSeconds %5 };
        }
    }
}
