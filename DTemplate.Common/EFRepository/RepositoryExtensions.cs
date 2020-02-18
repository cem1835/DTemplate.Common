using Autofac;
using DTemplate.Common.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.EFRepository
{
    public static class RepositoryExtensions
    {
        public static void UseEfRepository<T>(this ContainerBuilder builder) where T:DbContext
        {
            builder.RegisterType<T>().As<DbContext>();

            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>));

        }
    }
}
