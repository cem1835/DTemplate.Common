using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace DTemplate.Common.Authentication.ValidationAttributes
{
    public static class ValidationAttributeExtensions
    {
        public static IServiceCollection UseValidationAttributeMetadataProvider(this IServiceCollection services, Type resourceType)
        {
            services.AddMvc(o =>
             {
                 o.ModelMetadataDetailsProviders.Add(new CustomMetadata_ValidationMessage_Provider(resourceType));
             });

            return services;
        }

        public static IServiceCollection AddModelStateActionFilter(this IServiceCollection services)
        {
            services.AddMvc(o => o.Filters.Add(new ModelActionFilter()));

            return services;
        }
    }
}
