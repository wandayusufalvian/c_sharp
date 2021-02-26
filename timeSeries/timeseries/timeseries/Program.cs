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

namespace timeseries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //couchDB
            //HttpClient client = Service.ConnectToCouchDB();
            //await Experiment.Experiment3(client, 3600);//(client object,how many data retrieved)

            //ravenDB
            //Experiment.Experiment4();
            Experiment.Experiment5(1);



        }
    }
}
