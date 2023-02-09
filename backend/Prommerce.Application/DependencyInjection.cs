using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Prommerce.Application.Resources.Users;
using Prommerce.Application.Resources.Users.Models;
using System.Reflection;

namespace Prommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<UserPutDto>, UserValidator>();

            return services;
        }
    }
}