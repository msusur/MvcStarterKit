using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;

namespace MvcStarterKit.Abstractions.Attributes
{
    public static class CacheHelper
    {
        public static string GetCacheParameterName(IInvocation invocation)
        {
            return string.Format("{0}_{1}_{2}", invocation.TargetType.FullName, invocation.Method.Name, GetParameterHash(invocation.Arguments));
        }

        private static string GetParameterHash(IEnumerable<object> arguments)
        {
            return arguments.Aggregate("", (current, argument) => current + (argument == null ? string.Empty : argument.ToString()));
        }
    }
}