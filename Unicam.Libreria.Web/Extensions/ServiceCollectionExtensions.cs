using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using Unicam.Libreria.Application.Options;
using Microsoft.Extensions.DependencyInjection;
using Unicam.Libreria.Web.Factories;
using Unicam.Libreria.Application.Abstractions.Context;
using Unicam.Libreria.Application.Abstractions.Services;
using Microsoft.Extensions.Options;
namespace Unicam.Libreria.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services
            ,IConfiguration config)
        {

            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Unicam Libreria Api",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri("https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "profile", "profile" },
                                { "email", "Email" },
                                { "openid", "openid" },
                                { "offline_access", "offline_access" },
                                { "api://4ff6e4cd-5ed8-48eb-ad42-bde5bae6d210/default", "scope" }
                            }
                        }/*,
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri("https://login.microsoftonline.com/b974097e-5347-4135-8e56-9a78283a13f1/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "profile", "profile" },
                                { "email", "Email" },
                                { "openid", "openid" },
                                { "offline_access", "offline_access" },
                            }
                        }*/
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

            });
            services.AddControllersWithViews()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context) =>
                    {
                        return new BadRequestResultFactory(context);
                    };
                });
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
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "https://sts.windows.net/b974097e-5347-4135-8e56-9a78283a13f1";
                    opt.Audience = "api://4ff6e4cd-5ed8-48eb-ad42-bde5bae6d210";
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        RoleClaimType = "MY_ROLE"
                    };
                    opt.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = async (context) =>
                        {
                            var identity = context.Principal.Identity as ClaimsIdentity;
                            var emailClaim = identity.Claims.Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
                            if (emailClaim != null)
                            {
                                string userEmail = emailClaim.Value;
                                var utenteService = context.HttpContext.RequestServices.GetRequiredService<IUtentiService>();
                                var utente = await utenteService.GetUtenteByEmailAsync(userEmail);
                                identity.AddClaim(new Claim("MY_ROLE", utente.Ruolo.DescrizioneRuolo));
                            }
                        }
                    };
                });


            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IS_ADM", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("MY_ROLE", ["ADMIN"]);
                    
                });
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
