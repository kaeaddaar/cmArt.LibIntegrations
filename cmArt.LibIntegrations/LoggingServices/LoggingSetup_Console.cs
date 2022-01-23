using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;


namespace cmArt.LibIntegrations.LoggingServices
{
    // This is untested in its generic form separated into it's own class, but I didn't want to lose this method that I had once used for setting up console logging with Dependency Injection
    internal class LoggingSetup_Console<T> where T : class
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddTransient<T>();
        }
        public static ILogger SetupLogging()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider serviceProvider;
            ILogger<T> logger;
    
            serviceProvider = serviceCollection.BuildServiceProvider();
            logger = serviceProvider.GetService<ILogger<T>>();

            return logger;
        }
    }
}
