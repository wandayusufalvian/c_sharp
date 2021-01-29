using Microsoft.EntityFrameworkCore;
namespace ConsoleApp1
{
    public class RackContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=rack_db;Username=postgres;Password=yusuf");
        
        public RackContext() : base() { }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }


        

    }


}
