using Microsoft.EntityFrameworkCore;
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
