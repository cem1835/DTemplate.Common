using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DTemplate.Common.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseSeriLog(this IHostBuilder builder,string appName ,string elasticURL = "http://localhost:9200")
        {
            var logger = new LoggerConfiguration()
                                    .Enrich.FromLogContext()
                                    .Enrich.WithProperty("Application", appName)
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticURL))
                                    {
                                        AutoRegisterTemplate = true,
                                        IndexFormat = appName+"-log-{0:yyyy.MM.dd}"
                                    })
                                    .CreateLogger();

            Log.Logger = logger;

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));


            builder.ConfigureLogging(loggingBuilder => loggingBuilder.ClearProviders());

            builder.ConfigureServices((configureDelegate, services) => services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger)));

            return builder;
        }
    }
}

