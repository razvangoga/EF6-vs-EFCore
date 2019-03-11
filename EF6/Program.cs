using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = "Data Source=127.0.0.1;Initial Catalog=Tests;User Id=sa;Password=RootSQL1234";

            string id = "00000000-0000-0000-0000-000000000002";

            //works, EF/LinqToEntities can translate Guid.Parse to TSql
            using (GuidsDbContext ctx = new GuidsDbContext(connString))
            {
                GuidQuery item = ctx.GuidQueries.FirstOrDefault(g => g.Id == new Guid(id));

                Console.WriteLine(item.ExternalId);
            }

            try
            {
                //throws exception because EF/LinqToEntities cannot translate Guid.Parse to TSql
                using (GuidsDbContext ctx = new GuidsDbContext(connString))
                {
                    GuidQuery item = ctx.GuidQueries.FirstOrDefault(g => g.Id == Guid.Parse(id));

                    Console.WriteLine(item.ExternalId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("done...");
            Console.ReadKey();
        }
    }
}
