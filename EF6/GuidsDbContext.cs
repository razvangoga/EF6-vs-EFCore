using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6
{
    public class GuidsDbContext : DbContext
    {
        public GuidsDbContext(string connString) : base(connString)
        {
            this.Database.Log = s => { Console.WriteLine(s); };
        }

        public DbSet<GuidQuery> GuidQueries { get; set; }
    }
}
