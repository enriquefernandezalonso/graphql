using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Infrastructure.Models;
using Api.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var audiDb = context.CarBrands.Add(new CarBrand {  Name = "Audi", Description = "Audi brand description" ,Models = new List<CarModel>(), Dealers = new List<Dealer>()});
                var seatDb = context.CarBrands.Add(new CarBrand { Name = "Seat", Description = "Seat brand description", Models = new List<CarModel>(), Dealers = new List<Dealer>()});

                audiDb.Entity.Models.AddRange(new List<CarModel>
                {
                    new CarModel
                    {
                        Name = "A3",
                        Description = "Description A3"
                    },
                    new CarModel
                    {
                        Name = "A4",
                        Description = "Description A4"
                    }
                });
                
                audiDb.Entity.Dealers.AddRange(new List<Dealer>()
                {
                    new Dealer
                    {
                        DealerName = "Dealer 1", 
                        Address = "Address dealer 1"
                    },
                    new Dealer
                    {
                        DealerName = "Dealer 2",
                        Address = "Address dealer 2"
                        
                    }
                });

                context.SaveChanges();
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}