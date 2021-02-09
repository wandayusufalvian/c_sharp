using System;
using System.Collections.Generic;
using System.Text;

namespace Rovers
{
    public class Service
    { 
        public static int runSimulation(Job[] JobsArray, Container[] ContainerArray)
        {

            int lengthOfContainer = ContainerArray.Length;
            int amountOfJobLeft = JobsArray.Length;
            List<int> randomList; // a list of random number for sequence of container index 
            for (int i = 0; i < JobsArray.Length; i++)
            {   //mengacak urutan container yg akan di-traverse
                randomList = RandomListOfContainerIndex(lengthOfContainer);
                
                //request container randomly 
                foreach (int j in randomList)
                {
                    bool condition1 = JobsArray[i].liquidType.Equals(ContainerArray[j].name);
                    bool condition2 = ContainerArray[j].availableVolume >= JobsArray[i].volumeCapacity; 
                    if (condition1 && condition2)
                    {
                        amountOfJobLeft -= 1;
                        ContainerArray[j].availableVolume -= JobsArray[i].volumeCapacity;
                        break; 
                    }
                }
            }
            return amountOfJobLeft;
        }

        static List<int> RandomListOfContainerIndex(int lengthOfContainer)
        {
            List<int> randomList = new List<int>();
            Random a = new Random();
            int MyNumber;

            while (randomList.Count < lengthOfContainer)
            {
                MyNumber = a.Next(0, lengthOfContainer);
                if (!randomList.Contains(MyNumber))
                {
                    randomList.Add(MyNumber);
                }
            }
            return randomList;
        }

    }
}
