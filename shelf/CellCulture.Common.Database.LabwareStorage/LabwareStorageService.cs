using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CellCulture.Common.Database.LabwareStorage
{
    public class LabwareStorageService : ILabwareStorage
    {
        private readonly object shelfLock = new object();

        private IQueryable<Shelf> GetEmptyShelf(ShelfType shelfType, LabwareStorageContext db)
        {
            return db.Shelfs.Include(p => p.Type).Where(x => x.Type.Id == shelfType.Id && x.Barcode.Equals("")); 
        }
        public int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds)
        {
            //Goal= search id from first empty shelf exclude from array excludeShelfIds
            int shelfID = -1;
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {   //should put db as argument here instead using db in GetEmptyShelf method, otherwise : Object Dispose Exception
                    shelfID = GetEmptyShelf(shelfType,db).Where(x => excludeShelfIds.Contains(x.ShelfID) == false).Select(x => x.ShelfID).FirstOrDefault();
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
                {   //should put db as argument here instead using db in GetEmptyShelf method, otherwise : Object Dispose Exception
                    shelfName = GetEmptyShelf(shelfType, db).Where(x => excludeShelfNames.Contains(x.ShelfName) == false).Select(x => x.ShelfName).FirstOrDefault();
                }
            }
            return (!String.IsNullOrEmpty(shelfName)) ? shelfName : "";
        }

        private Shelf GetLabwareShelf(string barcode)
        {
            Shelf shelf = null;
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    shelf = db.Shelfs.Where(x=>x.Barcode.Equals(barcode)).FirstOrDefault();
                }
            }
            return (shelf != null) ? shelf : null;
        }

        public int GetLabwareShelfId(string barcode)
        {
            /* Goal = search shelf id based on labware's barcode 
             * Assumption = valid barcode 
             */
            Shelf shelf = null;
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    shelf = GetLabwareShelf(barcode);
                }
            }
            return (shelf != null) ? shelf.ShelfID : -1;
        }

        public string GetLabwareShelfName(string barcode)
        {   /* Goal = search shelf name based on labware's barcode 
             * Assumption = valid barcode 
             */
            Shelf shelf = null;
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    shelf = GetLabwareShelf(barcode);
                }
            }
            return (shelf != null) ? shelf.ShelfName : "";
        }

        private bool AnyBarcodeDuplicate(string barcode)
        {
            using (var db = new LabwareStorageContext())
            {   
                return (from shelf in db.Shelfs
                        where shelf.Barcode == barcode
                        select shelf.ShelfName).Any();
            }
        }

        public void StoreLabware(string barcode, int shelfId)
        {
            // Goal = put labware in empty shelf
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {   
                    if (!db.Shelfs.Where(x => x.ShelfID == shelfId).Any()) {throw new ArgumentException($"Shelf {shelfId} is invalid"); }
                    if (AnyBarcodeDuplicate(barcode)) { throw new ArgumentException("There is duplicate barcode"); }
                    db.Shelfs.Where(x => x.ShelfID == shelfId).First().Barcode = barcode;
                    db.SaveChanges();
                }
            }
        }

        public void StoreLabware(string barcode, string shelfName)
        {
            // Goal = put labware in empty shelf
            lock (shelfLock)
            {
                using (var db = new LabwareStorageContext())
                {
                    if (!db.Shelfs.Where(x => x.ShelfName.Equals(shelfName)).Any()) { throw new ArgumentException($"Shelf {shelfName} is invalid"); }
                    if (AnyBarcodeDuplicate(barcode)) { throw new ArgumentException("There is duplicate barcode"); }
                    db.Shelfs.Where(x => x.ShelfName.Equals(shelfName)).First().Barcode = barcode;
                    db.SaveChanges();
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
                    Shelf shlf = db.Shelfs.Where(x=>x.Barcode.Equals(barcode)).FirstOrDefault();
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
                    db.Shelfs.Where(x=>x.ShelfID== shelfId).First().Barcode = ""; 
                    db.SaveChanges();
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
                    db.Shelfs.Where(x => x.ShelfName.Equals(shelfName)).First().Barcode = "";
                    db.SaveChanges();
                }
            }
        }
    }
}
