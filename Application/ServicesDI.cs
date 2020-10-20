using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class ServicesDI
    {
        public static  IServiceCollection AddApplication(this IServiceCollection services)
        {

                return services;
        }
    }
}
