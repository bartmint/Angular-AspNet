using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServicesDI
    {
        public static  IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
