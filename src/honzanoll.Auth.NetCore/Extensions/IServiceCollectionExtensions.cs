using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using honzanoll.Auth.Models.Settings;
using System;
using System.Text;
using static honzanoll.Auth.Models.Settings.AuthenticationSettings;

namespace honzanoll.Auth.NetCore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        #region Public methods

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection authenticationSettingsSection = configuration.GetSection(nameof(AuthenticationSettings));
            services.Configure<AuthenticationSettings>(authenticationSettingsSection);

            AuthenticationJwtSettings authenticationSettings = authenticationSettingsSection.Get<AuthenticationSettings>().Jwt;

            byte[] key = Encoding.ASCII.GetBytes(authenticationSettings.SecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromHours(authenticationSettings.ExpirationTime),

                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authenticationSettings.Issuer,
                        ValidAudience = authenticationSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                    };
                });
        }

        #endregion
    }
}
