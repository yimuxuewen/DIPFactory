using DIP.Framework.Container;
using DIP.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Service
{
    public class ServiceB : IServiceB
    {
        public void SayHello()
        {
            Console.WriteLine($"{typeof(ServiceB).FullName}");
        }
        public void SayWhy()
        {
            Console.WriteLine($"Why?");
        }
    }
}
