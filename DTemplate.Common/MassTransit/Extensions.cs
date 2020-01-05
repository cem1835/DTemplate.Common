using Autofac;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DTemplate.Common.MassTransit
{
    public static class MassTransitExtensions
    {
        public static void UseMassTransit(this ContainerBuilder builder)
        {
            builder.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    IRabbitMqHost host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("request_service", ec =>
                    {
                        ec.PrefetchCount = 4;

                        ec.UseMessageRetry(r => r.Incremental(3, TimeSpan.FromSeconds(59), TimeSpan.FromSeconds(45)));

                        ec.ConfigureConsumers(context);
                    });
                    cfg.ConfigureEndpoints(context);
                }));

            });
        }

    }
}
