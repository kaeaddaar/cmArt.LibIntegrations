using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using System.IO;


namespace cmArt.Shopify.App
{
    public class Program
    {
        public static string[] Args { get; set; }

        public static void Main(string[] args)
        {
            Args = args;
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                bool folderExists = System.IO.Directory.Exists(folder);
                if (folderExists)
                {
                    File.AppendAllText(folder + "\\log.txt", ex.ToString());
                }
                throw ex;
            }
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
