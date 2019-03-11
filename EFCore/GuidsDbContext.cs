using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class GuidsDbContext : DbContext
    {
        public GuidsDbContext(DbContextOptions<GuidsDbContext> options) : base(options)
        {
        }

        public DbSet<GuidQuery> GuidQueries { get; set; }
    }
}
