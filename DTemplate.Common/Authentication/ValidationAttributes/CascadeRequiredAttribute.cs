using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTemplate.Common.Authentication.ValidationAttributes
{
    public class CascadeRequiredAttribute : ValidationAttribute
    {

        public string PropertyName { get; set; }

        public object Equal { get; set; }

        public CascadeRequiredAttribute(string propertyName, object equal)
        {
            PropertyName = propertyName;
            Equal = equal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(validationContext.ObjectInstance).Equals(Equal))
            {
                if (value == null)
                    return new ValidationResult(validationContext.DisplayName + " Alanı Zorunludur.");
            }
            return ValidationResult.Success;
        }
    }
}
