using api.Infrastructure.Models;
using GraphQL.Types;

namespace api.GraphQL
{
    public class CarModelType : ObjectGraphType<CarModel>
    {
        public CarModelType()
        {
            Name = nameof(CarModel);
            Field(x => x.Id, type : typeof(IdGraphType)).Description("The Id of the model");
            Field(x => x.Name).Description("Model name");
            Field(x => x.Description).Description("Model description");
            Field(x => x.Stock).Description("Stock");
        }
    }
}