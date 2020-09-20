﻿using System;
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

//                var ablDbEntry = context.Authors.Add(new Author { Name = "Abraham Lincoln", Quotes = new List<Quote>() });
//                var aristotleDbEntry = context.Authors.Add(new Author { Name = "Aristotle", Quotes = new List<Quote>() });
//
//                ablDbEntry.Entity.Quotes.AddRange(new List<Quote>
//                {
//                    new Quote
//                    {
//                        Category = "inspiration",
//                            Text = "Whatever you are, be a good one."
//                    },
//                    new Quote
//                    {
//                        Category = "books",
//                            Text = "My Best Friend is a person who will give me a book I have not read."
//                    }
//                });
//
//                aristotleDbEntry.Entity.Quotes.Add(new Quote
//                {
//                    Category = "inspiration",
//                        Text = "You will never do anything in this world without courage. It is the greatest quality of the mind next to honor."
//                });
                
                
                
                
                var audiDb = context.CarBrands.Add(new CarBrand {  Name = "Audi", Description = "Audi brand desciption" ,Models = new List<CarModel>() });
                var seatDb = context.CarBrands.Add(new CarBrand { Name = "Seat", Description = "Seat brand description", Models = new List<CarModel>() });

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

                context.SaveChanges();
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}