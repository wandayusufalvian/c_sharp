using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CellCulture.Common.Database.LabwareStorage
{
    public class LabwareStorageService : ILabwareStorage
    {
        private readonly object shelfLock = new object();
        public int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds)
        {
            //Goal= search id from first empty shelf exclude from array excludeShelfIds
            int shelfID = 0;
            lock (shelfLock)
            { 
                using (var db = new LabwareStorageContext())
                {
                    shelfID = (from shelf in db.Shelfs.Include(p => p.Type)
                               where excludeShelfIds.Contains(shelf.ShelfID) == false && shelf.Type.Id == shelfType.Id && shelf.Barcode.Equals("")
                               select shelf.ShelfID).FirstOrDefault();

                    if (shelfID > 0) { return shelfID; }
                    //If no empty shelf and shelfType is plate, search trough shelf from first shelf (plate can fit to trough shelf)
                    else if (shelfID == 0 && shelfType.Equals(ShelfType.Plate))
                    {
                        shelfID = (from shelf in db.Shelfs.Include(p => p.Type)
                                   where excludeShelfIds.Contains(shelf.ShelfID) == false && shelf.Type.Id == ShelfType.Trough.Id && shelf.Barcode.Equals("")
                                   select shelf.ShelfID).FirstOrDefault();
                    }
                }  
            }
            return (shelfID > 0) ? shelfID : -1;
        }

        public string GetEmptyShelfName(ShelfType shelfType, string[] excludeShelfNames)
        {
            //Goal= search name from first empty shelf exclude from array xcludeShelfNames
            string shelfName = "";
            lock (shelfLock)
            {
                
                using (var db = new LabwareStorageContext())
                {

                    shelfName = (from shelf in db.Shelfs.Include(p => p.Type)
                                 where excludeShelfNames.Contains(shelf.ShelfName) == false && shelf.Type.Id == shelfType.Id && shelf.Barcode.Equals("")
                                 select shelf.ShelfName).FirstOrDefault();

                    if (!String.IsNullOrEmpty(shelfName)) { return shelfName; }
                    //If no empty plate shelf, search trough shelf from first shelf (plate can fit to trough shelf)
                    else if (String.IsNullOrEmpty(shelfName) && shelfType.Equals(ShelfType.Plate))
                    {
                        shelfName = (from shelf in db.Shelfs.Include(p => p.Type)
                                     where excludeShelfNames.Contains(shelf.ShelfName) == false && shelf.Type.Id == ShelfType.Trough.Id && shelf.Barcode.Equals("")
                                     select shelf.ShelfName).FirstOrDefault();
                    }
                } 
            }
            return (!String.IsNullOrEmpty(shelfName)) ? shelfName : "";

        }

        public int GetLabwareShelfId(string barcode)
        {
            /* Goal = search shelf id based on labware's barcode 
             * Assumption = valid barcode 
             */
            int shelfID = 0;
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    shelfID = (from shelf in db.Shelfs
                               where shelf.Barcode.Equals(barcode)
                               select shelf.ShelfID).FirstOrDefault();
                }
            }
            return (shelfID != 0) ? shelfID : -1;
        }

        public string GetLabwareShelfName(string barcode)
        {   /* Goal = search shelf name based on labware's barcode 
             * Assumption = valid barcode 
             */
            string shelfName = "";
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    shelfName = (from shelf in db.Shelfs
                                 where shelf.Barcode.Equals(barcode)
                                 select shelf.ShelfName).FirstOrDefault();
                }
            }
            return (!String.IsNullOrEmpty(shelfName)) ? shelfName : "";
        }

        public void StoreLabware(string barcode, int shelfId)
        {
            /* Goal = put labware in empty shelf
             * Assumption = valid empty shelf id (got from GetEmptyShelfId)
             */
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {   //throw exception if there is duplicate barcode
                    if ((from shelf in db.Shelfs
                         where shelf.Barcode == barcode
                         select shelf.ShelfName).Any()) { throw new ArgumentException(); }

                (from shelf in db.Shelfs
                 where shelf.ShelfID == shelfId
                 select shelf).First().Barcode = barcode; db.SaveChanges();
                }
            }
            
        }

        public void StoreLabware(string barcode, string shelfName)
        {
            /* Goal = put labware in empty shelf
             * Assumption = valid empty shelf name (got from GetEmptyShelfName)
             */
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    if ((from shelf in db.Shelfs
                         where shelf.Barcode == barcode
                         select shelf.ShelfName).Any()) { throw new ArgumentException(); }

                (from shelf in db.Shelfs
                 where shelf.ShelfName.Equals(shelfName)
                 select shelf).First().Barcode = barcode; db.SaveChanges();
                }
            }
            
        }
        public void TakeLabware(string barcode)
        {
            /* Goal = empty the shelf if contain labware with specified barcode
             * Assumption = valid barcode
             */
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    Shelf shlf = (from shelf in db.Shelfs
                                  where shelf.Barcode.Equals(barcode)
                                  select shelf).FirstOrDefault();
                    if (shlf != null) { shlf.Barcode = ""; db.SaveChanges(); }
                    else { throw new ArgumentException(); }
                }
            }
            
        }

        public void ClearLabware(int shelfId)
        {
            // Goal = empty shelf whether initialize empty or not.
            // valid shelfId
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    (from shelf in db.Shelfs
                     where shelf.ShelfID == shelfId
                     select shelf).First().Barcode = ""; db.SaveChanges();
                }
            }
        }

        public void ClearLabware(string shelfName)
        {
            // Goal = empty shelf whether initialize empty or not.
            // valid shelfName
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    (from shelf in db.Shelfs
                     where shelf.ShelfName.Equals(shelfName)
                     select shelf).First().Barcode = ""; db.SaveChanges();
                }
            }
            
        }
    }
}
