using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.MongoRepository
{
    public static class MongoExtensions
    {

        // main method
        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.RegisterMongoOptions();

            builder.RegisterMongoClient();

            builder.RegisterMongoDatabase();

            new MongoInitializer().InitializeAsync();

            builder.RegisterGeneric(typeof(IMongoRepository<>)).As(typeof(MongoRepository<>)).InstancePerLifetimeScope();
        }

        private static void RegisterMongoOptions(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();

                return configuration.GetValue<MongoDbOptions>("mongo");
            }).SingleInstance();
        }
        private static void RegisterMongoClient(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();
        }
        private static void RegisterMongoDatabase(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();
        }
    }
}
