using GraphQL.Types;

namespace api.GraphQL.InputTypes
{
    public class CarBrandInputType : InputObjectGraphType<CarBrandInput>
    {
        public CarBrandInputType()
        {
            Name = $"{nameof(CarBrandInput)}";
            Field(x => x.Name).Description("Brand name");
            Field(x => x.Description).Description("Brand identifier");
            Field(x => x.Models, type : typeof(ListGraphType<CarModelInputType>)).Description("Brand's models");
        }
    }
}