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
            Field<ListGraphType<CarBrandType>>(
                "AllBrands",
                //arguments : new QueryArguments(new QueryArgument<IdGraphType> { Name = "id", Description = "The Id of the Brand." }),
                resolve : context =>
                {
                   
                    var authors = db.CarBrands.Include(a => a.Models).Include(d => d.Dealers);
                    return authors;
                   
                });

            Field<ListGraphType<CarBrandType>>(
                "BrandByName",
                arguments : new QueryArguments(new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the Brand." }),
                resolve : context =>
                {
                    var name = context.GetArgument<string>("name");
                    
                    if(name != null)
                    {
                        var authors = db.CarBrands.Include(q => q.Models)
                            .Where(a => a.Name == name).Include(d => d.Dealers).ToList();

                        return authors;
                    }
                    else
                    {
                        var authors = db.CarBrands.Include(a => a.Models);
                        return authors;
                    }
                    
                });
                
            Field<ListGraphType<CarModelType>>(
                $"AllModelsByBrand",
                arguments : new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "brandId", Description = "The Id of the Brand." }),
                resolve : context =>
                {
                    var brandId = context.GetArgument<int>("brandId");


                    var carBrands = db.CarBrands.Include(q => q.Models);

                    var brand = carBrands.FirstOrDefault(b => b.Id == brandId);

                    return brand.Models;

                });

        }
    }
}