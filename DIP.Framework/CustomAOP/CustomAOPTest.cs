using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.CustomAOP
{
    public class CustomAOPTest
    {
        public  static void Show()
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            CustomInterceptor customInterceptor = new CustomInterceptor();
            CustomClass customClass = proxyGenerator.CreateClassProxy<CustomClass>(customInterceptor);

            Console.WriteLine($"当前类型：{customClass.GetType()},父类型：{customClass.GetType().BaseType}");

            customClass.MethodInterceptor();
            Console.WriteLine();
            customClass.MethodNoInterceptor();
        }
    }
}
