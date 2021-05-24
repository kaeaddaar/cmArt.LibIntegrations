﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;



namespace cmArt.Shopify.App
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<Worker> _logger;



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
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsoleExportPSQLQuery.Program.WriteLogFile($"Worker running at: {DateTimeOffset.Now}");

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //"E:\_Customers\_ColonialPhotoNHobbyInc\CachedFiles" 
                //"E:\_Customers\_ColonialPhotoNHobbyInc\CSVFiles" 
                //"E:\_Customers\_ColonialPhotoNHobbyInc\TestImport" 
                //"E:\_Customers\_ColonialPhotoNHobbyInc\DakisLoads" 
                //"DSN=None" 
                //"UseCaching"
                string[] args = new string[7];
                //args[0] = "E:\\_Customers\\_ColonialPhotoNHobbyInc\\CachedFiles";
                //args[1] = "E:\\_Customers\\_ColonialPhotoNHobbyInc\\CSVFiles";
                //args[2] = "E:\\_Customers\\_ColonialPhotoNHobbyInc\\TestImport";
                //args[3] = "E:\\_Customers\\_ColonialPhotoNHobbyInc\\DakisLoads";

                args[0] = Configuration["Dakisinfo:CachedFiles"];
                args[1] = Configuration["Dakisinfo:CSVFiles"];
                args[2] = Configuration["Dakisinfo:CSVConnector"];
                args[3] = Configuration["Dakisinfo:DakisLoads"];
                args[4] = Configuration["Dakisinfo:DNSinfo"];
                args[5] = Configuration["Dakisinfo:Cachinginfo"];
                args[6] = Configuration["Dakisinfo:SupressUpload"];

                smtpAddress = Configuration["Dakisinfo:smtpaddress"];
                portNumber = Convert.ToInt32(Configuration["Dakisinfo:smtpport"]);
                enableSSL = Convert.ToBoolean(Configuration["Dakisinfo:enableSSL"]);
                emailFromAddress = Configuration["Dakisinfo:fromemailaddress"];
                password = Configuration["Dakisinfo:fromemailpassword"];
                emailToAddress = Configuration["Dakisinfo:errormail"];

                int exechour = Convert.ToInt32(Configuration["Dakisinfo:Hours"]);
                int execminute = Convert.ToInt32(Configuration["Dakisinfo:Minutes"]);
                int execsecond = Convert.ToInt32(Configuration["Dakisinfo:Seconds"]);


                //if (DateTime.Now.Hour == exechour && DateTime.Now.Second == execsecond && DateTime.Now.Second == execsecond)
                //{
                //    ConsoleExportPSQLQuery.Program library = new ConsoleExportPSQLQuery.Program(Configuration);
                //    try
                //    {
                //        if (!ConsoleExportPSQLQuery.Program.ArgumentsAreValid_LoadIfValid(args))
                //        {
                //            return;
                //        }

                //        int result = ConsoleExportPSQLQuery.Program.SyncToDakis(args);
                //    }
                //    catch (Exception ex)
                //    {
                //        SendEmail(ex.Message);
                //        throw;
                //    }
                //}

                ConsoleExportPSQLQuery.Program library = new ConsoleExportPSQLQuery.Program(Configuration);
                try
                {
                    if (!ConsoleExportPSQLQuery.Program.ArgumentsAreValid_LoadIfValid(args))
                    {
                        return;
                    }

                    _logger.LogInformation("SyncToDakis", DateTimeOffset.Now);
                    int result = ConsoleExportPSQLQuery.Program.SyncToDakis(args);
                    _logger.LogInformation("SyncToDakis (Completed)", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    ConsoleExportPSQLQuery.Program.WriteLogFile($"Attempting to send error email for error message: {ex.Message}");
                    SendEmail(ex.Message);
                    ConsoleExportPSQLQuery.Program.WriteLogFile("Attempting to send error email (Complete)");

                    throw;
                }


                int second = 1000;
                int minute = second * 60;
                int _15_Minutes = minute * 15;

                _logger.LogInformation("${ _15_Minutes / 60 / 1000} minutes until next planned execution.", DateTimeOffset.Now);
                ConsoleExportPSQLQuery.Program.WriteLogFile($"{_15_Minutes / 60 / 1000} minutes until next planned execution.");

                await Task.Delay(_15_Minutes, stoppingToken);
            }
        }

        public static void SendEmail(string body)
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
    }
}
