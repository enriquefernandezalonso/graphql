using api.Infrastructure.Models;
using GraphQL.Types;

namespace api.GraphQL
{
    public class DealerType : ObjectGraphType<Dealer>
    {
        public DealerType()
        {
            Name = nameof(Dealer);
            Field(x => x.Id, type : typeof(IdGraphType)).Description("The Id of the dealer");
            Field(x => x.DealerName).Description("Dealer name");
            Field(x => x.Address).Description("Address");
        }
    }
}