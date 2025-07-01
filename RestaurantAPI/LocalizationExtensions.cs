using Microsoft.Extensions.Localization;
using System.Globalization;

namespace RestaurantAPI
{
    public static class LocalizationExtensions
    {
        public static void AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

        public static void UseCustomLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { "en-US", "es-ES", "pt-BR" };
            var envLang = Environment.GetEnvironmentVariable("API_LANG") ?? "en-US";
            var culture = supportedCultures.Contains(envLang) ? envLang : "en-US";
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture),
                SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
                SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
            };
            app.UseRequestLocalization(localizationOptions);
        }
    }
}
