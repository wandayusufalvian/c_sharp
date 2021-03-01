using System;
using System.Collections.Generic;
using MyCouch;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Linq;

namespace timeseries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //COUCHDB
            //=====EXPERIMENT 1=======
            //List<int> period = new List<int> { 0, 23, 59, 59 };
            //int sensorQuantity = 5;
            //int interval = 1;
            //Services.WriteDataCouchDB(period, sensorQuantity, interval);

            //=====EXPERIMENT 2=======
            //List<int> period = new List<int> { 0, 23, 59, 59 };
            //int sensorQuantity = 5;
            //int interval = 5;
            //Services.WriteDataCouchDB(period, sensorQuantity, interval);
            await Services.QueryingCouchDB(Services.ConnectToCouchDB(), 720);

            //=====EXPERIMENT 3=======
            //List<int> period = new List<int> { 0, 23, 59, 59 };
            //int sensorQuantity = 5;
            //int interval = 10;
            //Services.WriteDataCouchDB(period, sensorQuantity, interval);
            //await Services.QueryingCouchDB(Services.ConnectToCouchDB(), 1440);

            //RAVENDB

            //=====EXPERIMENT 2=======
            //WRITE 
            //List<int> period = new List<int> { 0, 23, 59, 59 };
            //int sensorQuantity = 5;
            //int interval = 5;
            ////Services.WriteDataRavenDB(period, sensorQuantity, interval);

            ////RETRIEVE ALL DOCS (as a list)
            //Services.RetrieveAllDocsRavenDB(720);

            //=====EXPERIMENT 3=======
            //List<int> period = new List<int> { 0, 23, 59, 59 };
            //int sensorQuantity = 5;
            //int interval = 10;
            //Services.WriteDataRavenDB(period, sensorQuantity, interval);

            //RETRIEVE ALL DOCS (as a list)
            //Services.RetrieveAllDocsRavenDB(360);
        }
    }
}
