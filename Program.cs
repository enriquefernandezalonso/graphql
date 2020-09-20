using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Infrastructure.Models;
using Api.Database;
using api.Infrastructure.Models.temporal;
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
            IWebHost host = CreateWebHostBuilder(args).Build();
            using(IServiceScope scope = host.Services.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var audiDb = context.CarBrands.Add(new CarBrand {  Name = "Audi", Description = "Audi brand desciption" ,Models = new List<CarModel>(), Dealers = new List<Dealer>()});
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
                        Description = "DEscription A4"
                    }
                });

                seatDb.Entity.Models.Add(new CarModel
                {
                    Name = "Ateca",
                    Description = "Description seat Ateca"
                    
                });
                
                audiDb.Entity.Dealers.AddRange(new List<Dealer>()
                {
                    new Dealer(){DealerName = "Dealer 1"},
                    new Dealer(){DealerName = "Dealer 2"}
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