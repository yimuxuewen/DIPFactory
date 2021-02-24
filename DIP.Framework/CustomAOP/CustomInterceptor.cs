using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.CustomAOP
{
    public class CustomInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine( $"调用前拦截器的方法名：{invocation.Method.Name}"); 
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine($"拦截方法返回时调用的拦截器的方法名：{invocation.Method.Name}");
            base.PerformProceed(invocation);
        }

        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用后拦截器的方法名：{invocation.Method.Name}");
        }
    }
}
