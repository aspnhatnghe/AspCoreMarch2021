using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    //.Enrich.FromLogContext()
                    //.WriteTo.Console(new RenderedCompactJsonFormatter())
                    //.WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                    //.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Default Log Microsoft ==> output Console log
                //.ConfigureLogging(logging =>
                //{
                //    logging.ClearProviders();
                //    logging.AddConsole();
                //})
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
