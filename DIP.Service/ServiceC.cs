using DIP.Framework.Container;
using DIP.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Service
{
    public class ServiceC : IServiceC
    {
        public void SayHello()
        {
            Console.WriteLine($"{typeof(ServiceA).FullName}") ;
        }
    }
}
