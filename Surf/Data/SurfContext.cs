using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surf.Models;

namespace Surf.Data
{
    public class SurfContext : DbContext
    {
        public SurfContext (DbContextOptions<SurfContext> options)
            : base(options)
        {
        }

        public DbSet<Surf.Models.Surfboard> Surfboard { get; set; } = default!;
    }
}
