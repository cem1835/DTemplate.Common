using Autofac.Extras.DynamicProxy;
using DTemplate.Common.Caching;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Consumers
{
    [Intercept(typeof(CacheManager))]
    public class ProductConsumer : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var data = context.Message;

            await context.RespondAsync(new ProductResult {Message="Cem" });
        }
    }
}
