using System;
using Microsoft.EntityFrameworkCore;

namespace CellCulture.Common.Database.LabwareStorage
{
    public class LabwareStorageContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=labware-storage;Username=postgres;Password=postgres",
                    options => options.SetPostgresVersion(new Version(9, 6)));
        }

        public DbSet<Column> Columns { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
    }
}
