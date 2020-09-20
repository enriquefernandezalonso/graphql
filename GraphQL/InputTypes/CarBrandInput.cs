using System.Collections.Generic;
using api.Infrastructure.Models.temporal;

namespace api.GraphQL.InputTypes
{
    public class CarBrandInput
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CarModel> Models { get; set; }
    }
}