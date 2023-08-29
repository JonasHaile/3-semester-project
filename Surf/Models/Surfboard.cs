using Microsoft.AspNetCore.Routing.Constraints;

namespace Surf.Models
{
    public class Surfboard
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double  Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Equipment { get; set; }
    }
}
