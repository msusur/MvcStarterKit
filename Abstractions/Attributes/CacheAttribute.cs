using System.Web;
using System.Web.Caching;
using Castle.DynamicProxy;
using MvcStarterKit.Abstractions.Aspects;

namespace MvcStarterKit.Abstractions.Attributes
{
    /// <summary>
    /// Asp.Net Cache aspect. Gets the cached value if available.
    /// </summary>
    public class CacheAttribute : AspectAttributeBase
    {
        public Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected override bool BeforeExecution(IInvocation invocation)
        {
            string key = CacheHelper.GetCacheParameterName(invocation);

            var value = Cache[key];

            if (value == null)
            {
                invocation.Proceed();
                Cache[key] = invocation.ReturnValue;
            }
            else
            {
                invocation.ReturnValue = Cache[key];
            }

            return false;
        }
    }
}