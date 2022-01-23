using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;


namespace cmArt.Shopify.App.LoggingSetupService
{
    internal class LoggingSetup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddTransient<ShopifyConsoleApp>();
        }
        public static ILogger SetupLogging()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider serviceProvider;
            ILogger<ShopifyConsoleApp> logger;
    
            serviceProvider = serviceCollection.BuildServiceProvider();
            logger = serviceProvider.GetService<ILogger<ShopifyConsoleApp>>();

            return logger;
        }
    }
}
