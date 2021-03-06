﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public static class CacheExtensions
    {
        public static void AddMicrosoftCache(this ContainerBuilder builder)
        {
            builder.RegisterType<MicrosoftCacheManager>().As<ICacheManager>();

            builder.RegisterType<CacheInterceptor>().SingleInstance();

        }

        //public static void AddRedisCache(this IServiceCollection services)
        //{
        //    services.AddDistributedRedisCache(o =>
        //    {
        //        o.Configuration = "";
        //        o.InstanceName = "";
        //    });
        //}

        public static ContainerBuilder RegisterWithCacheInterceptors<TImplementer,TService>(this ContainerBuilder builder)
        {
            builder.RegisterType<TImplementer>()
            .As<TService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(CacheInterceptor))
            .SingleInstance();

            return builder;
        }

  

    }
}
