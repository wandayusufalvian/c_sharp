using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyCouch;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Linq;
using Raven.Client.Documents.Linq;
using MyCouch.Requests;
using MyCouch.Responses;

namespace timeseries
{
    public class Services
    {
        //couchDB
        public static HttpClient ConnectToCouchDB()
        {
            String usernamepass = "yusuf:yusuf";//"username:password"
            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(usernamepass);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }
        public static void WriteDataCouchDB(List<int> period, int sensorQuantity, int interval)
        {  
            string ip = "http://yusuf:yusuf@10.100.3.54:5984/";
            string database1 = "test"; // interval 1 s
            string database2 = "test2"; // interval 5 s
            string database3 = "test3"; // interval 10 s
            //select database
            string database = database2; 

            var watch = System.Diagnostics.Stopwatch.StartNew();

            using (var db = new MyCouchStore(ip, database))
            {
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s+=interval)
                            {
                                SensorData doc = new SensorData
                                {
                                    datetime = new DateTime(2021, 1, d + 1, h, m, s)
                                };
                                for (int sen = 0; sen < sensorQuantity; sen++)
                                {
                                    Random random = new Random();
                                    double newRandom = random.NextDouble();
                                    doc.sensorData.Add(newRandom);
                                }
                                var response = db.StoreAsync(doc);
                                Console.WriteLine(response.Result);
                            }
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine($"Write Time: {watch.ElapsedMilliseconds} ms");
        }

        public static async Task QueryingCouchDB(HttpClient client,int dataQuantity)
        {  
            var watch = System.Diagnostics.Stopwatch.StartNew();

            HttpResponseMessage result = await client.GetAsync($"http://10.100.3.54:5984/test/_design/12hours/_view/name-name?limit={dataQuantity}");
           
            String a = result.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(a);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        //RavenDB
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
                            for (int s = 0; s <= period[3]; s+= interval)
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
                                    .Select(x =>x)
                                    .Take(dataQuantity).ToList();

                watch.Stop();
                Console.WriteLine($"Read Time: {watch.ElapsedMilliseconds} ms");

                return lisfOfSensorData;
            }
        }
    }
}
