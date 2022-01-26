using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using cmArt.LibIntegrations.LoggingServices;

namespace cmArt.Shopify.App
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<Worker> _logger;


        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                    .AddTransient<ShopifyConsoleApp>();
        }
        private void LogInfo(string msg)
        {
            Console.WriteLine(msg);
            _logger.LogInformation(msg);
        }
        public Worker(ILogger<Worker> logger, IConfiguration config)
        {

            _logger = logger;
            Configuration = config;
        }

        static string smtpAddress = string.Empty;
        static int portNumber = 0;
        static bool enableSSL = false;
        static string emailFromAddress = string.Empty;
        static string password = string.Empty;
        static string emailToAddress = string.Empty;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ServiceCollection serviceCollection;
            ServiceProvider serviceProvider;
            IConfiguration config;
            StaticSettings settings;
            LogToFile logger_ToFile = null;

            try
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                settings = new StaticSettings(config);

                logger_ToFile = new LogToFile();
                logger_ToFile.Init(settings.LogfilePath + "\\Integration_LogFile.txt");

                serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                serviceProvider = serviceCollection.BuildServiceProvider();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Setting Up Log to File: {e.ToString()}");
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                (logger_ToFile ?? new LogToFile()).LogInformation("Worker started running at: {time}", DateTimeOffset.Now);
                bool KeepRunning = true;
                while (KeepRunning)
                {
                    try
                    {
                        ShopifyConsoleApp.Main_Console(new string[0]);
                        KeepRunning = true;
                    }
                    catch (Exception e)
                    {
                        string err = Environment.NewLine + e.ToString();
                        LogInfo(err);
                        int TimeToWait = 60 * 1000;
                        int sec = TimeToWait / 1000;
                        LogInfo($"Begin {sec} second wait before running the Integration App again");
                        await Task.Delay(TimeToWait, stoppingToken);
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        public static void SendEmail(string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = "Workers Error";
                    mail.Body = body;
                    mail.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error emailing error message. Ignoring.");
                Console.WriteLine("Error Body(See below):");
                Console.WriteLine(body);
            }
        }
    }
}
