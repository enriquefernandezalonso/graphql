using System.Linq;
using Api.Database;
using api.GraphQL.InputTypes;
using api.Infrastructure.Models.temporal;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace api.GraphQL.Mutations
{
    public class CarBrandModelMutation : ObjectGraphType
    {
        public CarBrandModelMutation(ApplicationDbContext db)
        {
            Field<CarBrandType>(
                $"addBrand",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CarBrandInputType>>
                {
                    Name = "brand",
                    Description = "Brand"
                }),
                resolve: context =>
                {
                    var brand = context.GetArgument<CarBrand>("brand");
                    var lastId = db.CarBrands.Last().Id;
                    var carBrandToAdd = new CarBrand()
                    {
                        Id = lastId + 1,
                        Description = brand.Description,
                        Name = brand.Name,
                        Models = brand.Models
                    };
                    db.CarBrands.Add(carBrandToAdd);
                    db.SaveChanges();
                    return carBrandToAdd;
                });
            
            Field<CarModelType>(
                $"add{nameof(CarModel)}",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CarModelInputType>>
                    { Name = "model", Description = "Model to add to a scpecific car brand"}),
                resolve: context =>
                {
                    var carModel = context.GetArgument<CarModelInput>("model");
                    var carBrand = db
                        .CarBrands
                        .Include(a => a.Models)
                        .FirstOrDefault(i => i.Id == carModel.CarBrandId);
                    
                    var modelToAdd = new CarModel()
                    {
                        Name = carModel.Name,
                        Description = carModel.Description,
                        Stock = carModel.Stock
                    };
                    carBrand.Models.Add(modelToAdd);
                    db.SaveChanges();
                    return modelToAdd;
                });

            
            
            Field<CarBrandType>(
                $"deleteCarBrand",
                arguments : new QueryArguments(new QueryArgument<IdGraphType> { Name = "id", Description = "The Id of the Brand." }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var carBrand = db.CarBrands.FirstOrDefault(brand => brand.Id == id);
                    if (carBrand != null)
                    {
                        db.CarBrands.Remove(carBrand);
                        db.SaveChanges();
                    }
                    
                    return carBrand;
                });
        }
    }
}