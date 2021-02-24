using DIP.Framework.Container;
using DIP.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Service
{
    public class ServiceA : IServiceA
    {
        [DIPPropertyInjection]
        public IServiceD ServiceD { get; set; }

        private readonly IServiceB _serviceB;

        public int Index { get; set; }
        public int Step { get; set; }

        [DIPContainer]
        public ServiceA(IServiceB serviceB, [DIPParameterConstant]int index)
        {
            _serviceB = serviceB;
            Index = index;
        }

        private IServiceC _serviceC = null;

        [DIPMethodInjection]
        public void InitServiceC(IServiceC serviceC ,[DIPParameterConstant]int index)
        {
            _serviceC = serviceC;
            Step = index;
        }
        public void SayHello()
        {
            Console.WriteLine($"{typeof(ServiceA).FullName}") ;
        }
    }
}
