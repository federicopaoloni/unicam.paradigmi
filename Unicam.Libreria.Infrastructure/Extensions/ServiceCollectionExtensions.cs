using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Libreria.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Abstractions.Email;
using Unicam.Libreria.Infrastructure.Emails;
namespace Unicam.Libreria.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
            , IConfiguration config)
        {
            services.AddDbContext<MyDbContext>(opt =>
            {
                //opt.UseLazyLoadingProxies();
                opt.UseSqlServer(config.GetConnectionString("MyDbContext"));
            });

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMyDbContext, MyDbContext>();
            return services;
        }
    }
}
