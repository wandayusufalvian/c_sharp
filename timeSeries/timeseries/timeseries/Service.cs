﻿using System;
using System.Collections.Generic;
using System.Text;
using MyCouch;
using System.Net.Http;
using Raven.Client.Documents.Session;
using Raven.Client.Documents;

namespace timeseries
{
    public class Service
    {
        public static void CreateDocuments(String ip, List<int> period, int sensorQuantity, string database)
        {   /*
                create docs with id is autogenerated by couchDB
            */
            using (var db = new MyCouchStore(ip, database))
            {
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s++)
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
        }

        public static void CreateDocuments2(String ip, List<int> period, int sensorQuantity, string database)
        {   /*
                create docs with id is timestamp
            */
            using (var db = new MyCouchStore(ip, database))
            {
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s++)
                            {
                                SensorData2 doc = new SensorData2
                                {
                                    _id = (new DateTime(2021, 1, d + 1, h, m, s)).ToString()
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
        }

        public static void CreateDocuments3(List<int> period, int sensorQuantity)
        {
            using (IDocumentSession session = DocumentStoreHolderRaven.Store.OpenSession())
            {
                for (int d = 0; d <= period[0]; d++)
                {
                    for (int h = 0; h <= period[1]; h++)
                    {
                        for (int m = 0; m <= period[2]; m++)
                        {
                            for (int s = 0; s <= period[3]; s++)
                            {
                                var sensordata = new SensorData
                                {
                                    datetime = new DateTime(2021, 1, d + 1, h, m, s)
                                };
                                Console.WriteLine(sensordata.datetime);
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


                //var sensordata = new SensorData
                //{
                //    datetime = new DateTime(2021, 1, 1, 1, 1, 2)

                //};
                //for (int sen = 0; sen < 5; sen++)
                //{
                //    Random random = new Random();
                //    double newRandom = random.NextDouble();
                //    sensordata.sensorData.Add(newRandom);
                //}
                //session.Store(sensordata);
                //session.SaveChanges();
            }
        }
        public static HttpClient ConnectToCouchDB()
        {
            String usernamepass = "yusuf:yusuf";//"username:password"
            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(usernamepass);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

        


    }
}