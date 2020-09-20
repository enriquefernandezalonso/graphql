using GraphQL.Types;

namespace api.GraphQL.InputTypes
{
    public class DeleteCarBrandInputType : InputObjectGraphType<DeleteBrandInput>
    {
        public DeleteCarBrandInputType()
        {
            Name = nameof(DeleteBrandInput);
            Field(x => x.carBrandId).Description("Brand id to delete");
        }
    }
}