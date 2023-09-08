using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surf.Areas.Identity.Data;
using Surf.Models;
using System;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace Surf.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            //using (var context = new SurfDbContext(serviceProvider.GetRequiredService<DbContextOptions<SurfDbContext>>()))
            //{
            //    // Look for any movies.
            //    if (context.Surfboard.Any())
            //    {
            //        context.Surfboard.RemoveRange(context.Surfboard); // Remove all records from the table
            //        context.SaveChanges();
            //        //return;
            //    }

            //    context.Surfboard.AddRange(

            //        new Surfboard
            //        {
            //            Name = "The Minilog",
            //            Length = 6,
            //            Width = 21,
            //            Thickness = 2.75,
            //            Volume = 38.8,
            //            Price = 565,
            //            Type = "Shortboard",
            //            Equipment = " ",
            //            Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p347_i15_w5000.jpeg?width=160"
            //        },

            //    new Surfboard
            //    {
            //        Name = "The Wide Glider",
            //        Length = 7.1,
            //        Width = 21.75,
            //        Thickness = 2.75,
            //        Volume = 44.16,
            //        Price = 685,
            //        Type = "Funboard",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p335_i10_w5000.jpeg?width=640"
            //    },

            //    new Surfboard
            //    {
            //        Name = "The Golden Ratio",
            //        Length = 6.3,
            //        Width = 21.85,
            //        Thickness = 2.9,
            //        Volume = 43.22,
            //        Price = 695,
            //        Type = "Funboard",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p329_i37_w1168.png?width=640"
            //    },

            //    new Surfboard
            //    {
            //        Name = "Mahi Mahi",
            //        Length = 5.4,
            //        Width = 20.75,
            //        Thickness = 2.3,
            //        Volume = 29.39,
            //        Price = 645,
            //        Type = "Fish",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p340_i29_w5000.jpeg?width=160"
            //    },

            //    new Surfboard
            //    {
            //        Name = "The Emerald",
            //        Length = 9.2,
            //        Width = 22.8,
            //        Thickness = 2.8,
            //        Volume = 65.4,
            //        Price = 895,
            //        Type = "Longboard",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p336_i55_w5000.jpeg?width=160"
            //    },

            //    new Surfboard
            //    {
            //        Name = "The Bomb",
            //        Length = 5.5,
            //        Width = 21,
            //        Thickness = 2.5,
            //        Volume = 33.7,
            //        Price = 645,
            //        Type = "Shortboard",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p5_i4_w160.jpeg"
            //    },

            //    new Surfboard
            //    {
            //        Name = "Walden Magic",
            //        Length = 9.6,
            //        Width = 19.4,
            //        Thickness = 3,
            //        Volume = 80,
            //        Price = 1025,
            //        Type = "Longboard",
            //        Equipment = " ",
            //        Image = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p285_i27_w333.jpeg?width=160"
            //    },

            //    new Surfboard
            //    {
            //        Name = "Naish One",
            //        Length = 12.6,
            //        Width = 30,
            //        Thickness = 6,
            //        Volume = 301,
            //        Price = 854,
            //        Type = "SUP",
            //        Equipment = "Paddle",
            //        Image = "https://kite-prod.b-cdn.net/13845/naish-one-alana-12-6-s26-inflatable-sup.jpg"
            //    },

            //    new Surfboard
            //    {
            //        Name = "Six Tourer",
            //        Length = 11.6,
            //        Width = 32,
            //        Thickness = 6,
            //        Volume = 270,
            //        Price = 611,
            //        Type = "SUP",
            //        Equipment = "Fin, Paddle, Pump, Leash",
            //        Image = "https://kite-prod.b-cdn.net/16394-thickbox_default/stx-tourer-11-6-2022-2023-inflatable-sup-package.jpg"
            //    },

            //    new Surfboard
            //    {
            //        Name = "Naish Maliko",
            //        Length = 14,
            //        Width = 25,
            //        Thickness = 6,
            //        Volume = 330,
            //        Price = 1304,
            //        Type = "SUP",
            //        Equipment = "Fin, Paddle, Pump, Leash",
            //        Image = "https://www.oneopensky.dk/img/1024/1024/resize/S/2/S26SB-L140C28_103943.jpg"
            //    });

            //    context.SaveChanges();
            //}


            //Seed Roles 
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //Create Admin

            var user = new ApplicationUser
            {
                UserName = "Admin@Admin.dk",
                Email = "Admin@Admin.dk",
                FirstName = "Hans",
                Lastname = "Hansen",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "!Admin123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

        }
    }
}

