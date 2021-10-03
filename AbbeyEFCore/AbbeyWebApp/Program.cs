using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AbbeyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InsertData();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void InsertData()
        {
            using (var context = new AbbeyClasses.Models.BuckfastabbeyContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                var monkExists = context.Monk.Any();
                // Adds some monks if count is 0
                if (monkExists == false)
                {
                    context.Monk.Add(new AbbeyClasses.Models.Monk
                    {
                        MonkId = 1,
                        FirstName = "William",
                        LastName = "Shrewsbury",
                        Age = 62,
                        Job = "Abbot"
                    });
                    context.Monk.Add(new AbbeyClasses.Models.Monk
                    {
                        MonkId = 2,
                        FirstName = "Jacques",
                        LastName = "Cabot",
                        Age = 37,
                        Job = "Head Cook"
                    });
                    context.Monk.Add(new AbbeyClasses.Models.Monk
                    {
                        MonkId = 3,
                        FirstName = "Jacob",
                        LastName = "Smith",
                        Age = 54,
                        Job = "Librarian"
                    });
                    context.Monk.Add(new AbbeyClasses.Models.Monk
                    {
                        MonkId = 4,
                        FirstName = "Robert",
                        LastName = "Barnes",
                        Age = 38,
                        Job = "Priest"
                    });
                    context.Monk.Add(new AbbeyClasses.Models.Monk
                    {
                        MonkId = 5,
                        FirstName = "Timothy",
                        LastName = "Feldman",
                        Age = 29,
                        Job = "Gardener"
                    });
                    // Saves changes
                    context.SaveChanges();
                }
                
            }
        }
    }
}
