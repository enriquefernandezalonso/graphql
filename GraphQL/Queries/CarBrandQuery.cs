using System.Linq;
using Api.Database;
using api.Infrastructure.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace api.GraphQL.Queries
{
    public class CarBrandQuery : ObjectGraphType
    {
        public CarBrandQuery(ApplicationDbContext db)
        {
            Field<ListGraphType<CarBrandType>>(
                "allBrands",
                arguments : new QueryArguments(new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the Brand." }),
                resolve : context =>
                {
                    var name = context.GetArgument<string>("name");
                    
                    if(name != null)
                    {
                        var brands = db.CarBrands.Include(q => q.Models).Include(d => d.Dealers)
                            .Where(a => a.Name == name).ToList();

                        return brands;
                    }
                    else
                    {
                        var brands = db.CarBrands.Include(a => a.Models).Include(d => d.Dealers);
                        return brands;
                    }
                    
                });

        }
    }
}