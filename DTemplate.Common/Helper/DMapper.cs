using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DTemplate.Common.Helper
{
    public static class DMapper
    {
        public static TConvert MapToDto<T, TConvert>(this T entity, TConvert convertedObj, object baseEntity = null)
        {

            foreach (var prop in entity.GetType().GetProperties())
            {

                if (prop.CanWrite == false)
                    continue;

                if (Attribute.IsDefined(prop, typeof(ConditionalNotMappedAttribute)))
                    continue;

                var value = prop.GetValue(entity, null);
                if (value == null)
                    continue;
                if (baseEntity != null && baseEntity.Equals(value))
                    continue;


                var target = convertedObj.GetType().GetProperty(prop.Name);
                if (target != null)
                {
                    if (prop.PropertyType.GenericTypeArguments.Length > 0)
                    {
                        var genericArgumentType = convertedObj.GetType().GetProperty(prop.Name).PropertyType.GetGenericArguments()[0];
                        if (genericArgumentType.Name != prop.PropertyType.GenericTypeArguments[0].Name)
                        {
                            if (value.GetType().GetInterface(nameof(IEnumerable)) != null)
                            {
                                // IEnumerable
                                IList myCollection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(genericArgumentType));
                                foreach (var item in value as IEnumerable)
                                {
                                    var convertedDto = item.MapToDto(Activator.CreateInstance(genericArgumentType), entity);

                                    myCollection.Add(convertedDto);
                                }
                                target.SetValue(convertedObj, myCollection);
                            }
                            else
                            {
                                // single Generic Object
                                var genericConvertedObject = Activator.CreateInstance(genericArgumentType);
                                target.SetValue(convertedObj, value.MapToDto(genericConvertedObject, entity));
                            }
                        }
                        else
                        {
                            // Same Property Generic  Object
                            target.SetValue(convertedObj, Convert.ChangeType(value, prop.PropertyType.GenericTypeArguments[0], CultureInfo.InvariantCulture));
                        }
                    }
                    else if (target.PropertyType.FullName != prop.PropertyType.FullName)
                    {
                        // dto transfer
                        var dtoObj = value.MapToDto(Activator.CreateInstance(target.PropertyType.Assembly.GetType(target.PropertyType.FullName)), entity);
                        target.SetValue(convertedObj, dtoObj);
                    }
                    else
                    {
                        // Same Property
                        target.SetValue(convertedObj, Convert.ChangeType(value, prop.PropertyType, CultureInfo.InvariantCulture));
                    }
                }
            }
            return convertedObj;
        }
    }
    public class ConditionalNotMappedAttribute : Attribute
    {
        public ConditionalNotMappedAttribute()
        {

        }
    }
}
