using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTemplate.Common.Caching
{
    public class CacheInterceptor : IInterceptor
    {
        private ICacheManager _cacheManager;

        public CacheInterceptor(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            
        }

        public CacheAttribute GetCacheAttribute(IInvocation invocation)
        {
            return Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CacheAttribute)) as CacheAttribute;
        }

        public string GetInvocationKey(IInvocation invocation)
        {
            var methodName = $"{invocation.Method.ReflectedType?.Namespace}.{invocation.Method.ReflectedType?.Name}.{invocation.Method.Name}";
            var arguments = invocation.Arguments.ToList();

            var key = $"{string.Join(",", arguments.Select(s => s != null ? s.ToString() : "<Null>"))}({methodName})";
            return key;
        }

        public void Intercept(IInvocation invocation)
        {
            var key = GetInvocationKey(invocation);

            if (_cacheManager.Exists(key))
            {
                invocation.ReturnValue = _cacheManager.Get<object>(key);
            }
            else
            { 
                invocation.Proceed();

                var cacheAttribute = GetCacheAttribute(invocation);

                _cacheManager.Add(key, invocation.ReturnValue,cacheAttribute.DurationMinute);
            }
        }

        
    }
}
