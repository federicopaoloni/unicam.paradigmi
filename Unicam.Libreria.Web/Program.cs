using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Abstractions.Email;
using Unicam.Libreria.Application.Options;
using Unicam.Libreria.Infrastructure.Database;
using Unicam.Libreria.Infrastructure.Emails;
using Unicam.Libreria.Web.Extensions;
using Unicam.Libreria.Application.Extensions;
using Unicam.Libreria.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Unicam.Libreria.Application.Factories;
using Unicam.Libreria.Application.Models.Responses;
using Serilog;
using Microsoft.Extensions.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

               Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("C:\\Unicam\\Unicam.Paradigmi\\Unicam.Libreria.Web\\bin\\Debug\\net8.0\\log.txt", rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 7
    )
    // set default minimum level
    .MinimumLevel.Information()
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

//Per leggere una configurazione secca
//var clientIdFromConfig = builder.Configuration.GetValue<string>("AzureAd:ClientId");

builder.Services
        .AddUiServices(builder.Configuration)
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.OAuthClientId("4ff6e4cd-5ed8-48eb-ad42-bde5bae6d210");
    opt.OAuthUsePkce();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            Exception errorToRet = contextFeature.Error;
            if (contextFeature.Error is Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                errorToRet = dbEx.InnerException ?? dbEx;
            }
            var res = ResponseFactory
                .WithError(errorToRet);
            await context.Response.WriteAsJsonAsync(
                res
                );
        }
    });
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Console.WriteLine("Sto rispondendo direttamente dal middleware");
    //await context.Response.WriteAsync("Rispondo dal middleware");
    //Con la riga qui sotto blocco l'esecuzione
    //await Task.CompletedTask;
    //Con la riga sottostante passo al middleware successivo
    await next.Invoke(context);
    Console.WriteLine(context.Response.StatusCode.ToString());

});

app.Map("/LibriNoHandling", (config) =>
{
    config.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Bloccato dal Run");
        context.Response.StatusCode = 500;
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
