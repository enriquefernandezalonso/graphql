using api.Infrastructure.Models.temporal;
using GraphQL.Types;

namespace api.GraphQL
{
    public class CarBrandType : ObjectGraphType<CarBrand>
    {
        public CarBrandType()
        {
            Name = nameof(CarBrand);
            Field(x => x.Id, type : typeof(IdGraphType)).Description("Brand Id.");
            Field(x => x.Name, nullable: true).Description("The name of the car brand.");
            Field(x => x.Description, nullable: true).Description("Description of the car brand");
            Field(x => x.Models,nullable: true, type : typeof(ListGraphType<CarModelType>)).Description("Brand's models");
            Field(x => x.Dealers,nullable: true, type : typeof(ListGraphType<DealerType>)).Description("Brand's dealers");
        }
    }
}