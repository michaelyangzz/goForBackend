using IdentityDemo.Data;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Identity
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddMyIdentity(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //       options.UseInMemoryDatabase("abc"));
            var connectionString = Configuration.GetConnectionString("BlogContext");
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));


            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
            }).AddErrorDescriber<IdentityLocalization>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogin = async (context) =>
                {
                    await Task.Run(() =>
                    {
                        if (context.Request.Path.Value.Contains("api"))
                            context.Response.StatusCode = 401;
                        else
                            context.Response.Redirect(options.LoginPath);
                    });
                };

                options.Events.OnRedirectToAccessDenied = async (context) =>
                {
                    await Task.Run(() =>
                    {
                        context.Response.StatusCode = 401;
                    });
                };
            });


            return services;
        }
    }
}
