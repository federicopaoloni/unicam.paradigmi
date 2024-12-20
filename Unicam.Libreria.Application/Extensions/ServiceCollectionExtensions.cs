using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Application.Abstractions.Services;
using Unicam.Libreria.Application.Services;
using Unicam.Libreria.Application.Validators;

namespace Unicam.Libreria.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));
            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<IUtentiService, UtentiService>();
            return services;
        }
    }
}
