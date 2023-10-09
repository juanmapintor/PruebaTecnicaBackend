namespace PruebaTecnica.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfigurationRoot? BuiltConfiguration;

        public static IConfigurationRoot GetConfiguration(string? contentRootPath = null)
        {
            if (BuiltConfiguration != null)
                return BuiltConfiguration;

            var configurationbuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            if (contentRootPath != null)
            {
                configurationbuilder.SetBasePath(contentRootPath);
            }

            BuiltConfiguration = configurationbuilder.Build();

            return BuiltConfiguration;
        }
    }
}
