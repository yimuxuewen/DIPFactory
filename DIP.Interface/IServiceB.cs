using DIP.Framework.CustomAOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Interface
{
    public interface IServiceB
    {
        [LogBefore]
        [Login]
        void SayHello();
        //[LogAfter]
        [Monitor]
        void SayWhy();
    }
}
