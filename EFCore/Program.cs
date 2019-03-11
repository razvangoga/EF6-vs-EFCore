using System;
using  System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<GuidsDbContext> optBuilder = new DbContextOptionsBuilder<GuidsDbContext>();
            optBuilder.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=Tests;User Id=sa;Password=RootSQL1234");
            
            optBuilder.UseLoggerFactory(GetLoggerFactory());

            string id = "00000000-0000-0000-0000-000000000002";

            //this works, logs and warning, FETCHES THE ENTIRE DATASET and does the WHERE part localy
            using (GuidsDbContext ctx = new GuidsDbContext(optBuilder.Options))
            {
                GuidQuery item = ctx.GuidQueries.FirstOrDefault(g => g.Id == new Guid(id));
                Console.WriteLine(item.ExternalId);
            }

            //https://docs.microsoft.com/en-us/ef/efcore-and-ef6/
            //https://docs.microsoft.com/en-us/ef/core/querying/client-eval#optional-behavior-throw-an-exception-for-client-evaluation
            // configured like this, EFCore will throw an exception like EF6 when the Linq Query is not fully translatable
            optBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));

            try
            {
                using (GuidsDbContext ctx = new GuidsDbContext(optBuilder.Options))
                {
                    GuidQuery item = ctx.GuidQueries.FirstOrDefault(g => g.Id == new Guid(id));
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

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                    builder.AddConsole()
                //    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Trace))
            );

            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }
    }
}
