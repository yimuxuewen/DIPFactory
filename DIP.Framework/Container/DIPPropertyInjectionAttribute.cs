using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.Container
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DIPPropertyInjectionAttribute : Attribute
    {
    }
}
