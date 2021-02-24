using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.CustomAOP
{
    public class CustomClass
    {
        public virtual void MethodInterceptor()
        {
            Console.WriteLine("This is MethodInterceptor");
        }

        public void MethodNoInterceptor()
        {
            Console.WriteLine("This is MethodNoInterceptor");
        }
    }
}
