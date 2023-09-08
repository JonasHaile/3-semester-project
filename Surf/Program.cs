using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surf.Data;
using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
=======
using Surf.Areas.Identity.Data;
>>>>>>> NewKrav3

namespace Surf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext< SurfDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SurfDbContext") ?? throw new InvalidOperationException("Connection string 'SurfDbContext' not found.")));

                        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SurfDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SurfContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
<<<<<<< HEAD
            
=======
                        app.UseAuthentication();;

>>>>>>> NewKrav3
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
<<<<<<< HEAD

            app.MapRazorPages();

=======
            app.MapRazorPages();
>>>>>>> NewKrav3
            app.Run();
        }
    }
}