using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework.Container
{
    [AttributeUsage(AttributeTargets.Parameter|AttributeTargets.Property)]
    public class DIPParameterShortNameAttribute : Attribute
    {
        public string ShortName { get; private set; }
        public DIPParameterShortNameAttribute(string shortname)
        {
            ShortName = shortname;
        }
    }
}
