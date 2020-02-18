using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using App.Metrics.AspNetCore;
using App.Metrics.AspNetCore.Health;
using App.Metrics.Formatters.Prometheus;

namespace DTemplate.Common.Metrics
{
    public static class MetricExtensions
    {
        public static IHostBuilder UseAppMetrics(this IHostBuilder hostBuilder)
        {

            //hostBuilder.UseHealth


            return hostBuilder;
        }
    }
}
