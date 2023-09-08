using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Surf.Areas.Identity.Data;
using Surf.Models;

namespace Surf.Data
{
    public class SurfContext : IdentityDbContext<ApplicationUser>
    {
        public SurfContext (DbContextOptions<SurfContext> options)
            : base(options)
        {
        }

        public DbSet<Surfboard> Surfboard { get; set; } = default!;

    }
}
