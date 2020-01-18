using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTemplate.Common.Caching
{
    public class CacheRemoveInterceptor : IInterceptor
    {

        private ICacheManager _cacheManager;

        public CacheRemoveInterceptor(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;

        }

        public CacheRemoveAttribute GetCacheRemoveAttribute(IInvocation invocation)
        {
            return Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CacheRemoveAttribute)) as CacheRemoveAttribute;
        }


        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var removeAttr = GetCacheRemoveAttribute(invocation);

            _cacheManager.RemoveByPattern(removeAttr.Key);
        }
    }
}
