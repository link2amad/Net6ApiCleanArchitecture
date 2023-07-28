#region Imports

using Application.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#endregion

namespace API.Configurations
{
    public static class ConfigureAuthentications
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            // configure jwt authentication
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyAuthentication", null);

            //services.AddAuthorization(options =>
            //{
            //    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder("ApiKeyAuthentication");
            //    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            //    options.AddPolicy("ApiKeyAuthentication", defaultAuthorizationPolicyBuilder.Build());
            //});

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "JwtBearer";
                x.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string authorization = context.Request.Headers["Authorization"];

                        // If no authorization header found, nothing to process further
                        if (string.IsNullOrEmpty(authorization))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }

                        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authorization.Substring("Bearer ".Length).Trim();
                        }
                        // If no token found, no further work possible
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");

                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static string ExtractToken(string authorization)
        {
            if (authorization != null && authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return authorization.Substring("Bearer ".Length).Trim();
            return authorization;
        }
    }
}