using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using Unicam.Libreria.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Data Source=LOCALHOST;User Id=libreria_user;Password=libreria_user;Trust Server Certificate=true";

builder.Services.AddDbContext<MyDbContext>(opt =>
{
    //opt.UseLazyLoadingProxies();
    opt.UseSqlServer(connectionString);
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(opt =>
    {
        opt.ClientId = "4ff6e4cd-5ed8-48eb-ad42-bde5bae6d210";
        opt.TenantId = "b974097e-5347-4135-8e56-9a78283a13f1";
        opt.Instance = "https://login.microsoftonline.com/";
        opt.CallbackPath = "/signin-oidc";
        opt.Domain = "0q1c6.onmicrosoft.com";
        opt.SaveTokens = true;
        opt.UsePkce = true;
        opt.Scope.Add("offline_access");
        opt.ClientSecret = "C0O8Q~TzTfq5Ker0GpcOGqZnAgjHiL7pXSUibaFY";
        opt.ResponseType = OpenIdConnectResponseType.Code;

        opt.Events.OnTokenValidated = async (ctx) =>
        {
            if (ctx.Principal != null)
            {
                var identity = ctx.Principal.Identity as ClaimsIdentity;
                //TODO : Leggo dal database il ruolo dell'utente
                identity!.AddClaim(new Claim("Ruolo", "Admin"));
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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

app.Run();
