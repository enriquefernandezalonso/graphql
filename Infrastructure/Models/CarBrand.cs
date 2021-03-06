using System.Collections.Generic;

namespace api.Infrastructure.Models
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CarModel> Models { get; set; }
        public List<Dealer> Dealers { get; set; }
    }
}