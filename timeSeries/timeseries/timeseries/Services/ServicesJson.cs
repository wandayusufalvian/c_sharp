using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace timeseries.Services
{
    public class ServicesJson
    {
        public static void JsonSerialization()
        {
            List<SensorData> SensorDatas = new List<SensorData>();
            SensorDatas.Add(new SensorData()
            {
                datetime=new DateTime(2021,1,1),
                sensorData=new List<double>
                {
                    11,12,13,14,15
                }
            });
            SensorDatas.Add(new SensorData()
            {
                datetime = new DateTime(2021, 1, 2),
                sensorData = new List<double>
                {
                    1,2,3,4,5
                }
            });
            string jsonFile = JsonConvert.SerializeObject(SensorDatas.ToArray());
            System.IO.File.WriteAllText(@"Z:\ini.txt",jsonFile);
        }

        public void JsonDeserialization


    }
}
