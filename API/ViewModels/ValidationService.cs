using API.ViewModels;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsUp.API.ViewModels
{
    public static class ValidationService
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserDTO>,NewUserDTOValidation>();
            services.AddTransient<IValidator<UserForLoginDTO>,NewUserForLoginDTOValidation>();
            services.AddTransient<IValidator<UserForRegisterDTO>, NewUserForRegisterValidation>();

            return services;
        }
    }
}
