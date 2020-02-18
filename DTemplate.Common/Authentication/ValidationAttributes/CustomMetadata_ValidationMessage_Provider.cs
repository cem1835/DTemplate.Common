using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using System.Text;

namespace DTemplate.Common.Authentication.ValidationAttributes
{
    public class CustomMetadata_ValidationMessage_Provider : IValidationMetadataProvider
    {
        private ResourceManager resourceManager;
        private Type resourceType;
        public CustomMetadata_ValidationMessage_Provider( Type type)
        {
            resourceType = type;
            resourceManager = new ResourceManager(type.FullName, type.GetTypeInfo().Assembly);
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {

            if (context.ValidationMetadata.ValidatorMetadata.Count > 0)
            {
                foreach (var attribute in context.ValidationMetadata.ValidatorMetadata)
                {
                    ValidationAttribute validationAttribute = attribute as ValidationAttribute;

                    if (validationAttribute != null && string.IsNullOrEmpty(validationAttribute.ErrorMessage) && validationAttribute.ErrorMessageResourceName == null)
                    {
                        var name = validationAttribute.GetType().Name;
                        if (resourceManager.GetString(name) != null)
                        {
                            validationAttribute.ErrorMessageResourceType = resourceType;
                            validationAttribute.ErrorMessageResourceName = name;
                            validationAttribute.ErrorMessage = null;
                        }
                    }
                }
            }
        }
    }
}
