using System.Linq;
using Api.Database;
using api.Infrastructure.Models;
using api.Infrastructure.Models.temporal;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace api.GraphQL.Queries
{
    public class CarBrandQuery : ObjectGraphType
    {
        public CarBrandQuery(ApplicationDbContext db)
        {
            Field<CarBrandType>(
                nameof(CarBrand),
                arguments : new QueryArguments(new QueryArgument<IdGraphType> { Name = "id", Description = "The Id of the Brand." }),
                resolve : context =>
                {
                    var id = context.GetArgument<int>("id");
                    var author = db
                        .CarBrands
                        .Include(a => a.Models)
                        .FirstOrDefault(i => i.Id == id);
                    return author;
                });

            Field<ListGraphType<CarBrandType>>(
                $"{nameof(CarBrand)}s",
                arguments : new QueryArguments(new QueryArgument<IdGraphType> { Name = "name", Description = "The Id of the Brand." }),
                resolve : context =>
                {
                    var name = context.GetArgument<string>("name");
                    
                    if(name != null)
                    {
                        var authors = db.CarBrands.Include(q => q.Models).Where(a => a.Name == name).ToList();

                        return authors;
                    }
                    else
                    {
                        var authors = db.CarBrands.Include(a => a.Models);
                        return authors;
                    }
                    
                });
        }
    }
}