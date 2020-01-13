using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Serilog.Formatting.Elasticsearch;

namespace DTemplate.Common.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseSeriLog(this IHostBuilder builder,string appName=null ,string elasticURL = "http://localhost:9200")
        {
            // TODO : İf you capture the startup errors, add "CaptureStartupErrors(true)" to webBuilder
            // Do not use uppercase on elastic index
            string indexFormat = string.IsNullOrWhiteSpace(appName) ? "logstash-" : $"${appName}-log-"+"{0:yyyy.MM.dd}";
            var logger = new LoggerConfiguration()
                                    .Enrich.FromLogContext()
                                    .Enrich.WithProperty("Application", appName ?? "noname")
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticURL))
                                    {
                                        MinimumLogEventLevel=LogEventLevel.Warning,
                                        AutoRegisterTemplate = true,
                                        IndexFormat = indexFormat,
                                        CustomFormatter= new ExceptionAsObjectJsonFormatter(renderMessage: true)
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

