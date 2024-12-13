using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using System.Security.Policy;
using Unicam.Libreria.Application.Options;

namespace Unicam.Libreria.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services
            ,IConfiguration config)
        {

            services.AddControllersWithViews();
            services.Configure<AzureADOption>(config.GetSection(AzureADOption.SECTION_NAME));

            var azureAdOption = new AzureADOption();
            config.GetSection(AzureADOption.SECTION_NAME).Bind(azureAdOption);
            
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(opt =>
                {
                    opt.ClientId = azureAdOption.ClientId;
                    opt.TenantId = azureAdOption.TenantID;
                    opt.Instance = azureAdOption.Instance;
                    opt.CallbackPath = azureAdOption.CallbackPath;
                    opt.Domain = azureAdOption.Domain;
                    opt.SaveTokens = true;
                    opt.UsePkce = true;
                    opt.Scope.Add("offline_access");
                    opt.ClientSecret = azureAdOption.CLientSecret;
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

            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1";
                    opt.Audience = "api://4ff6e4cd-5ed8-48eb-ad42-bde5bae6d210";
                    //opt.Audience = "AudienceSballato";
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true
                    };
                });
            /*
            services.AddAuthentication("MyJwtToken")
               .AddJwtBearer(opt =>
               {
                   //opt.Audience = "AudienceSballato";
                   opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       
                   };
               });*/

            services.AddFluentValidationAutoValidation();
            
            return services;
        }
    }
}
