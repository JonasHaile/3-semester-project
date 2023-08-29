using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Surf.Models
{
    public class EquipmentSearch
    {
        public List<Surfboard>? Surfboards { get; set; }
        public SelectList? Equipment { get; set; }
        public string? SurfboardEquipment { get; set; }
        public string? SearchString { get; set; }
    }
}
