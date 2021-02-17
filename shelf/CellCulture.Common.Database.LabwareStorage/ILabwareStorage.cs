using System;
using System.Collections.Generic;
using System.Text;

namespace CellCulture.Common.Database.LabwareStorage
{
    public interface ILabwareStorage
    {
        void TakeLabware(string barcode);

        void ClearLabware(int shelfId);

        void ClearLabware(string shelfName);

        void StoreLabware(string barcode, int shelfId);

        void StoreLabware(string barcode, string shelfName);

        int GetLabwareShelfId(string barcode); // Returns -1 if not found 

        string GetLabwareShelfName(string barcode); // Returns empty string if not found 

        int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds); // Returns -1 if not found 

        string GetEmptyShelfName(ShelfType shelfType, string[] excludeShelfNames); // Returns empty string if not found 
    }
}
