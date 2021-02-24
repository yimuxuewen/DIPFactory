using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.Container
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DIPMethodInjectionAttribute : Attribute
    {
    }
}
