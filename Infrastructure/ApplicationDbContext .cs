using api.Infrastructure.Models;
using api.Infrastructure.Models.temporal;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{

    public class ApplicationDbContext : DbContext
    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
//
//        public DbSet<Quote> Quotes { get; set; }
//        public DbSet<Author> Authors { get; set; }
        
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
    }
}