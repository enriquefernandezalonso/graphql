using api.Infrastructure.Models.temporal;
using GraphQL.Types;

namespace api.GraphQL
{
    public class DealerType : ObjectGraphType<Dealer>
    {
        public DealerType()
        {
            Name = nameof(Dealer);
            Field(x => x.Id, type : typeof(IdGraphType)).Description("The Id of the dealer");
            Field(x => x.DealerName).Description("DEaler name");
        }
    }
}