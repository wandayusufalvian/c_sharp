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
using timeseries.Services;

namespace timeseries
{
    class Program
    {
        static void Main()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //start execution 
            //write program here :
            ServicesJson.JsonSerialization();

            watch.Stop();
            Console.WriteLine($"Write Time: {watch.ElapsedMilliseconds} ms"); //end of execution


            
        }
    }
}
