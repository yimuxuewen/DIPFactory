using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DIP.Framework.CustomAOP
{
    public static class ContainerAOPExtend
    {
        public  static object AOP(object oj,Type interfacetype)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            AOPInterceptor aopInterceptor = new AOPInterceptor();
            oj = proxyGenerator.CreateInterfaceProxyWithTarget(interfacetype,oj,aopInterceptor);
            return oj;
        }


    }

    public abstract class BaseAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation,Action action);
    }


    public class LogBeforeAttribute : BaseAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"LogBeforeAttribute is Do .Time:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff")}");
                action.Invoke();
            };
        }
    }
    public class LogAfterAttribute : BaseAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"LogAfterAttribute is Do .Time:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff")}");
                action.Invoke();
            };
        }
    }
    public class LoginAttribute : BaseAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"LoginAttribute is Do .Time:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff")}");
                action.Invoke();
            };
        }
    }

    public class MonitorAttribute : BaseAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"MonitorAttribute is Do .Time:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff")}");
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                action.Invoke();
                stopWatch.Stop();
                Console.WriteLine($"方法耗时时间:{stopWatch.Elapsed.TotalMilliseconds}毫秒");

            };
        }
    }

    public class AOPInterceptor : StandardInterceptor
    {
        protected override void PreProceed(IInvocation invocation)
        {
            //Console.WriteLine($"调用前拦截器的方法名：{invocation.Method.Name}");
        }

        protected override void PerformProceed(IInvocation invocation)
        {
            Action action = () => { base.PerformProceed(invocation); };
            var method = invocation.Method;
            if (method.IsDefined(typeof(BaseAttribute), true))
            {
                foreach (BaseAttribute item in method.GetCustomAttributes(typeof(BaseAttribute), true).ToArray().Reverse())
                {
                    action= item.Do(invocation,action);
                }
            }
            action.Invoke();
            //Console.WriteLine($"拦截方法返回时调用的拦截器的方法名：{invocation.Method.Name}");
            //base.PerformProceed(invocation);
        }

        protected override void PostProceed(IInvocation invocation)
        {
            //Console.WriteLine($"调用后拦截器的方法名：{invocation.Method.Name}");
        }
    }

}
