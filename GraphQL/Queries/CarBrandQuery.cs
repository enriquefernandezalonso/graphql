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
                "AllBrandsOld",
                //arguments : new QueryArguments(new QueryArgument<IdGraphType> { Name = "id", Description = "The Id of the Brand." }),
                resolve : context =>
                {
                   
                    var brands = db.CarBrands.Include(a => a.Models).Include(d => d.Dealers);
                    return brands;
                   
                });

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