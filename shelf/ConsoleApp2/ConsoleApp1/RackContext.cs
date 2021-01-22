using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ConsoleApp1
{
    public class RackContext: DbContext
    {
        public RackContext(): base()
        {

        }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
    }

    
}
