using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CellCulture.Common.Database.LabwareStorage
{
    public class LabwareStorageService : ILabwareStorage
    {
        private readonly object _sync = new object();

        public int GetEmptyShelfId(ShelfType shelfType, int[] excludeShelfIds)
        {
            // Goal: search id from first empty shelf exclude from array excludeShelfIds
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    var shelfID = GetEmptyShelf(db, shelfType)
                            .Where(x => !excludeShelfIds.Contains(x.ShelfID))
                            .Select(x => x.ShelfID)
                            .FirstOrDefault();

                    return (shelfID > 0) ? shelfID : -1;
                }
            }
        }

        public string GetEmptyShelfName(ShelfType shelfType, string[] excludeShelfNames)
        {
            // Goal: search name from first empty shelf exclude from array excludeShelfNames
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    var shelfName = GetEmptyShelf(db, shelfType)
                            .Where(x => !excludeShelfNames.Contains(x.ShelfName))
                            .Select(x => x.ShelfName)
                            .FirstOrDefault();

                    return (!String.IsNullOrEmpty(shelfName)) ? shelfName : "";
                }
            }
        }

        public int GetLabwareShelfId(string barcode)
        {
            // Goal: search shelf id based on labware barcode 
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    var shelf = GetLabwareShelf(db, barcode);

                    return (shelf != null) ? shelf.ShelfID : -1;
                }
            }

        }

        public string GetLabwareShelfName(string barcode)
        {
            // Goal: search shelf name based on labware barcode 
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    var shelf = GetLabwareShelf(db, barcode);

                    return (shelf != null) ? shelf.ShelfName : "";
                }
            }
        }

        public void StoreLabware(string barcode, int shelfId)
        {
            // Goal: put labware in empty shelf
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    if (IsDuplicateFound(db, barcode))
                    {
                        throw new ArgumentException($"Duplicate barcode (barcode: {barcode})");
                    }


                    if (!IsValidShelfId(db, shelfId))
                    {
                        throw new ArgumentException($"Invalid shelf id (id: {shelfId})");
                    }


                    db.Shelfs.Single(x => x.ShelfID == shelfId).Barcode = barcode;

                    db.SaveChanges();
                }
            }
        }

        public void StoreLabware(string barcode, string shelfName)
        {
            // Goal: put labware in empty shelf
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    if (IsDuplicateFound(db, barcode))
                    {
                        throw new ArgumentException($"Duplicate barcode (barcode: {barcode})");
                    }

                    if (!IsValidShelfName(db, shelfName))
                    {
                        throw new ArgumentException($"Invalid shelf name (name: {shelfName})");
                    }

                    db.Shelfs.Single(x => x.ShelfName.Equals(shelfName)).Barcode = barcode;

                    db.SaveChanges();
                }
            }
        }
        public void TakeLabware(string barcode)
        {
            // Goal: empty the shelf if contain labware with specified barcode
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    var shelf = db.Shelfs.FirstOrDefault(x => x.Barcode == barcode);

                    if (shelf == null)
                    {
                        throw new ArgumentException($"Barcode not found (barcode: {barcode})");
                    }

                    shelf.Barcode = "";

                    db.SaveChanges();
                }
            }

        }

        public void ClearLabware(int shelfId)
        {
            // Goal: empty shelf whether initialize empty or not
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    if (!IsValidShelfId(db, shelfId))
                    {
                        throw new ArgumentException($"Invalid shelf id (id: {shelfId})");
                    }

                    db.Shelfs.Single(x => x.ShelfID == shelfId).Barcode = "";

                    db.SaveChanges();
                }
            }
        }

        public void ClearLabware(string shelfName)
        {
            // Goal: empty shelf whether initialize empty or not
            lock (_sync)
            {
                using (var db = new LabwareStorageContext())
                {
                    if (!IsValidShelfName(db, shelfName))
                    {
                        throw new ArgumentException($"Invalid shelf name (name: {shelfName})");
                    }

                    db.Shelfs.First(x => x.ShelfName.Equals(shelfName)).Barcode = "";

                    db.SaveChanges();
                }
            }
        }

        private bool IsValidShelfId(LabwareStorageContext db, int shelfId)
        {
            return db.Shelfs.Any(x => x.ShelfID == shelfId);
        }

        private bool IsValidShelfName(LabwareStorageContext db, string shelfName)
        {
            return db.Shelfs.Any(x => x.ShelfName == shelfName);
        }

        private IQueryable<Shelf> GetEmptyShelf(LabwareStorageContext db, ShelfType shelfType)
        {
            return db.Shelfs.Include(p => p.Type).Where(x => x.Type.Id == shelfType.Id && x.Barcode == "");
        }

        private Shelf GetLabwareShelf(LabwareStorageContext db, string barcode)
        {
            var shelf = db.Shelfs.FirstOrDefault(x => x.Barcode.Equals(barcode));

            return (shelf != null) ? shelf : null;
        }

        private bool IsDuplicateFound(LabwareStorageContext db, string barcode)
        {
            return (from shelf in db.Shelfs where shelf.Barcode == barcode select shelf.ShelfName).Any();
        }
    }
}
