using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surf.Models;
using Surf.Data;
using System;
using System.Linq;

namespace Surf.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfContext(serviceProvider.GetRequiredService<DbContextOptions<SurfContext>>()))
            {
                // Look for any movies.
                if (context.Surfboard.Any())
                {
                    return; // DB has been seeded
                }

                context.Surfboard.AddRange(

                    new Surfboard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Price = 565,
                        Type = "Shortboard",
                        Equipment = "hej med digwdlwjdlwdjlwdjlwkdjlwkdjlwkdjlwkdjlwkdjlwkjdlwkjdlwkdjlkwjdlkwjdlwkdjldwwwwwwwwwwwwwwwww                                                        wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww                                         wwwwwwwwww ",
                        Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                    },

                new Surfboard
                {
                    Name = "The Wide Glider",
                    Length = 7.1,
                    Width = 21.75,
                    Thickness = 2.75,
                    Volume = 44.16,
                    Price = 685,
                    Type = "Funboard",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "The Golden Ratio",
                    Length = 6.3,
                    Width = 21.85,
                    Thickness = 2.9,
                    Volume = 43.22,
                    Price = 695,
                    Type = "Funboard",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "Mahi Mahi",
                    Length = 5.4,
                    Width = 20.75,
                    Thickness = 2.3,
                    Volume = 29.39,
                    Price = 645,
                    Type = "Fish",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "The Emerald",
                    Length = 9.2,
                    Width = 22.8,
                    Thickness = 2.8,
                    Volume = 65.4,
                    Price = 895,
                    Type = "Longboard",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "The Bomb",
                    Length = 5.5,
                    Width = 21,
                    Thickness = 2.5,
                    Volume = 33.7,
                    Price = 645,
                    Type = "Shortboard",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "Walden Magic",
                    Length = 9.6,
                    Width = 19.4,
                    Thickness = 3,
                    Volume = 80,
                    Price = 1025,
                    Type = "Longboard",
                    Equipment = " ",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "Naish One",
                    Length = 12.6,
                    Width = 30,
                    Thickness = 6,
                    Volume = 301,
                    Price = 854,
                    Type = "SUP",
                    Equipment = "Paddle",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "Six Tourer",
                    Length = 11.6,
                    Width = 32,
                    Thickness = 6,
                    Volume = 270,
                    Price = 611,
                    Type = "SUP",
                    Equipment = "Fin, Paddle, Pump, Leash",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                },

                new Surfboard
                {
                    Name = "Naish Maliko",
                    Length = 14,
                    Width = 25,
                    Thickness = 6,
                    Volume = 330,
                    Price = 1304,
                    Type = "SUP",
                    Equipment = "Fin, Paddle, Pump, Leash",
                    Image = "https://ttf.dk/media/1326/DGF_01.jpg"
                });
                    
                context.SaveChanges();

            }
        }
    }
}

