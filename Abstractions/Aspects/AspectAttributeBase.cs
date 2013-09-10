using System;
using Castle.DynamicProxy;

namespace MvcStarterKit.Abstractions.Aspects
{
    /// <summary>
    /// Base class for aspect attributes.
    /// </summary>
    public abstract class AspectAttributeBase : Attribute
    {
        public void ExecuteAttribute(IInvocation invocation)
        {
            var allowExecution = BeforeExecution(invocation);
            if (!allowExecution)
            {
                return;
            }
            invocation.Proceed();
            AfterMethodExecution(invocation);
        }

        protected virtual void AfterMethodExecution(IInvocation invocation)
        {

        }

        protected virtual bool BeforeExecution(IInvocation invocation)
        {
            return true;
        }
    }
}