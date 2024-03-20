using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration){
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
   
            });

            return services;
        }
    }
}