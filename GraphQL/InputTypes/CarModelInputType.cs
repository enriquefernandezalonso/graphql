using GraphQL.Types;

namespace api.GraphQL.InputTypes
{
    public class CarModelInputType : InputObjectGraphType<CarModelInput>
    {
        public CarModelInputType()
        {
            Name = nameof(CarModelInput);
            Field(x => x.CarBrandId, true).Description("Brand Id");
            Field(x => x.Name).Description("Model name");
            Field(x => x.Description).Description("Model description");
            Field(x => x.Stock).Description("Stock");
        }
    }
}