using Application.IRepository;
using Infrastructure.Data;
using Infrastructure.Data.Authentications;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureDependancyInjection
    {
      
            public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
            {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("PolicyManagementConnection")));

                    services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();

                    services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 10;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });
            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromDays(30);
                option.SlidingExpiration = true;
            });
            services.AddHttpContextAccessor();
                    return services;
            }
    }
    
}
