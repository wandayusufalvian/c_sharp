using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface LabwareInterface
    {
        void TakeLabware(string barcode); 

        void ClearLabware(int shelfId); 

        void StoreLabware(string barcode, int shelfId); 

        int GetLabwareShelfId(string barcode); // Returns -1 if not found 

        int GetEmptyShelfId(int shelfType, int[] excludeShelfIds); // Returns -1 if not found 
    }
}
