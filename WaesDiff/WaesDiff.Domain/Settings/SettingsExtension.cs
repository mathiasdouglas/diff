namespace WaesDiff.Domain.Settings
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension responsible for inject the appSettings
    /// </summary>
    public static class SettingsExtension
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("Settings");

            // Inject Settings so that others can use too
            services.Configure<Settings>(settingsSection);
        }
    }
}
