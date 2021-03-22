using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace timeseries
{
    class DataByDate : AbstractIndexCreationTask<SensorData>
    {
        public DataByDate()
        {
            this.Map=sensors=> from sensor in sensors
                               select new { Date=sensor.datetime };
        }
    }
}
