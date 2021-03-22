using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace timeseries.Services
{
    class ServicesRavenDB
    {
        public static void WriteDataRavenDB(List<int> period, int sensorQuantity, int interval)
        {   //save data to RavenDB

            var watch = System.Diagnostics.Stopwatch.StartNew();

            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {   //make sure DocumentStoreHolderRaven point to correct database.
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s += interval)
                            {
                                var sensordata = new SensorData
                                {
                                    datetime = new DateTime(2021, 1, d + 1, h, m, s)
                                };
                                for (int sen = 0; sen < sensorQuantity; sen++)
                                {
                                    Random random = new Random();
                                    double newRandom = random.NextDouble();
                                    sensordata.sensorData.Add(newRandom);
                                }
                                session.Store(sensordata);
                            }
                        }
                    }
                }

                session.SaveChanges();
            }

            watch.Stop();
            Console.WriteLine($"Write Time: {watch.ElapsedMilliseconds} ms");
        }

        public static List<SensorData> RetrieveAllDocsRavenDB(int dataQuantity)
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                /* To get data sensor: 
                 * returned list (l) => l.sensorData (list object) => use index to get the value(e.g. : l.sensorData[0])
                 */
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var lisfOfSensorData = session
                                    .Query<SensorData>()
                                    .Select(x => x)
                                    .Take(dataQuantity).ToList();

                watch.Stop();
                Console.WriteLine($"Read Time: {watch.ElapsedMilliseconds} ms");

                return lisfOfSensorData;
            }
        }

        public static List<SensorData> RetrieveDataIntervalTime(DateTime start, DateTime end)
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();

                var lisfOfSensorData = session
                                    .Query<SensorData>()
                                    .Where(x => x.datetime >= start)
                                    .Where(x => x.datetime <= end)
                                    .Select(x => x)
                                    .ToList();

                watch.Stop();
                Console.WriteLine($"Read Time: {watch.ElapsedMilliseconds} ms");
                return lisfOfSensorData;
            }
        }

        public static void RetrieveDataIntervalStep()
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var lisfOfSensorData = session
                                    .Query<SensorData>()
                                    .Select(x => x.sensorData).ToArray();

                watch.Stop();

                //foreach (var y in lisfOfSensorData)
                //{
                //    Console.WriteLine(y);
                //}
                Console.WriteLine($"Read Time: {watch.ElapsedMilliseconds} ms");

            }
        }
    }
}
