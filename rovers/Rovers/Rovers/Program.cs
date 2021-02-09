using System;
using System.Collections.Generic;

namespace Rovers
{
    class Program
    {
        static void Main(string[] args)
        {
            //inisialisasi 
            Dictionary<string, List<string>> dictOfLiquidAndMachine = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>{"Rx"}},
                {"B", new List<string>{"Rx","Flow"}},
                {"C", new List<string>{"Rx","Flow"}},
                {"D", new List<string>{"Rx","Flow"}}
            };
            List<int> listOfAmount = new List<int> { 2, 
                                                     2,  2, 
                                                     1, 25,
                                                      1, 1 
                                                    };

            Jobs jobs = new Jobs();
            jobs.addJob(dictOfLiquidAndMachine, listOfAmount);
            //jobs.printJobType();
            Job[] arrayOfRandomJobs = jobs.createArrayOfRandomJobs();
            jobs.printArrayOfRandomJobs();

            Containers containers = new Containers();
            Dictionary<Container,int> d = containers.calcContainerNeeded(arrayOfRandomJobs);
            var e = d.Keys;
            
            foreach (Container c in e)
            {
                Console.WriteLine($"Key: {c.name}, Values: {d[c]}");
            }

            Console.WriteLine();

            Container[] ArrayOfContainers = containers.createArrayOfContainers(d);
            foreach (Container c in ArrayOfContainers)
            {
                Console.WriteLine($"{c.name} : {c.availableVolume}");
            }

            Console.WriteLine();
            int sisa = Service.runSimulation(arrayOfRandomJobs, ArrayOfContainers);
            Console.WriteLine("sisa job :"+sisa);

            foreach (Container c in ArrayOfContainers)
            {
                Console.WriteLine($"{c.name} : {c.availableVolume}");
            }


        }
    }
}
