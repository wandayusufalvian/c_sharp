using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface LabwareInterface
    {
        void TakeLabware(string barcode); //done

        void ClearLabware(int shelfId); //done

        void StoreLabware(string barcode, int shelfId); //tdk pakai size?

        int GetLabwareShelfId(string barcode); // Returns -1 if not found //done

        int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds); // Returns -1 if not found
    }
}
