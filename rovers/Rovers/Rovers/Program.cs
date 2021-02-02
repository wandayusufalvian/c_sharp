using System;
using System.Collections.Generic;

namespace Rovers
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Yang Belum :
             * - jika rover ada sisa yang banyak (pakai nilai threshold tertentu)
             * setleah dipakai oleh satu mesin lalu ada mesin lain request maka prioritas rover ini baru kemudian rover yg dari markas 
             * - handling jika terjadi request bersamaan 
             * - 
             */
            MachineType RX = MachineType.RX;
            MachineType Flow = MachineType.Flow;
            MarkasRover marRover = null;
            Service srv = new Service();

            marRover = new MarkasRover();
            for (int i = 1; i <= 5; i++)
            {
                marRover.listRovers.Add(new Rover(i));
            }
            marRover.availableRover = marRover.listRovers.Count;


            int jumlahJobsRX = 50;
            int jumlahJobsFlow = 11;
            int roverNeededFlow = srv.calcRoverNeeded(Flow, jumlahJobsFlow, marRover);
            int roverNeededRX=srv.calcRoverNeeded(RX, jumlahJobsRX, marRover);
            Console.WriteLine(roverNeededRX);
            Console.WriteLine(roverNeededFlow);

        }
    }
}
