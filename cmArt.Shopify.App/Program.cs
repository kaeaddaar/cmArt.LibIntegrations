using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;


namespace cmArt.Shopify.App
{
    public class Program
    {
        public static string[] Args { get; set; }

        public static void MainX(string[] args)
        {
            Args = args;
            CreateHostBuilder(args).Build().Run();
        }

        // https://wakeupandcode.com/tag/worker-service/
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
