using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Framework
{
    public class RegisterModel
    {
        public Type ModelType { get; set; }

        public LifeTimeType LifeType { get; set; }

        public object SingletonInstance { get; set; }

    }

    public enum LifeTimeType
    {
        Transient,//瞬时实例
        Singleton,//单例实例
        Scoped,//作用域实例
        PerThread//线程实例
    }
}
