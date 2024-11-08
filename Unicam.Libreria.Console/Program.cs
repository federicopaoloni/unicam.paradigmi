// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unicam.Libreria.Console;
using Unicam.Libreria.Infrastructure.Database;

string connectionString = "Data Source=LOCALHOST;User Id=libreria_user;Password=libreria_user;Trust Server Certificate=true";


var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddDbContext<MyDbContext>(opt =>
    {
        //opt.UseLazyLoadingProxies();
        opt.UseSqlServer(connectionString);
    });
    services.AddScoped<MainService>();
});

var app = builder.Build();

var mainService = app.Services.GetRequiredService<MainService>();

await mainService.ExecuteAsync();
//var context = new MyDbContext();

