using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using DIP.Framework.CustomAOP;

namespace DIP.Framework.Container
{
    public class DIPContainer : IDIPContainer
    {
        Dictionary<string, RegisterModel> DIPContainerDic = new Dictionary<string, RegisterModel>();
        Dictionary<string, object[]> DIPContainerValueDic = new Dictionary<string, object[]>();
        Dictionary<string, object> DIPContainerScoped = new Dictionary<string, object>();

        public DIPContainer()
        {

        }
        public DIPContainer(Dictionary<string, RegisterModel> dIPContainerDic, Dictionary<string, object[]> dIPContainerValueDic, Dictionary<string, object> dIPContainerScoped)
        {
            this.DIPContainerDic = dIPContainerDic;
            this.DIPContainerValueDic = dIPContainerValueDic;
            this.DIPContainerScoped = dIPContainerScoped;
        }

        public IDIPContainer CreateSubDIPContainer()
        {
            return new DIPContainer(this.DIPContainerDic, this.DIPContainerValueDic,new Dictionary<string, object>());
        }

        private string GetKey(string fullname, string shortname) => $"{fullname}___{shortname}";

        private string GetShortName(ICustomAttributeProvider customAttributeProvider)
        {
            if (customAttributeProvider.IsDefined(typeof(DIPParameterShortNameAttribute), true))
            {
                var attribute = (DIPParameterShortNameAttribute)(customAttributeProvider.GetCustomAttributes(typeof(DIPParameterShortNameAttribute), true)[0]);
                return attribute.ShortName;
            }
            return null;
        }

        public void Register<TFrom, TTo>(string shortname = null, object[] paramlist = null, LifeTimeType lifeTimeType = LifeTimeType.Transient) where TTo : TFrom
        {
            //string key = typeof(TFrom).FullName;
            string key = GetKey(typeof(TFrom).FullName, shortname);
            if (paramlist != null && paramlist.Length > 0)
            {
                if (!DIPContainerValueDic.ContainsKey(key))
                {
                    DIPContainerValueDic.Add(key, paramlist);
                }
                else
                {
                    DIPContainerValueDic[key] = paramlist;
                }
            }
            if (DIPContainerDic.ContainsKey(key))
            {
                DIPContainerDic[key] = new RegisterModel() { LifeType = lifeTimeType, ModelType = typeof(TTo) };
            }
            else
            {
                DIPContainerDic.Add(key, new RegisterModel() { LifeType = lifeTimeType, ModelType = typeof(TTo) });
            }
        }

        public TFrom Resolve<TFrom>(string shortname = null, object[] paramlist = null)
        {
            return (TFrom)ResolveObject(typeof(TFrom), shortname, paramlist);
        }
        private object ResolveObject(Type abstracttype, string shortname = null, object[] paramlist = null)
        {
            //string key = abstracttype.FullName;
            string key = GetKey(abstracttype.FullName, shortname);
            if (DIPContainerDic.ContainsKey(key))
            {
                RegisterModel registerModel = DIPContainerDic[key];
                Type type = registerModel.ModelType;
                switch (registerModel.LifeType)
                {
                    case LifeTimeType.Transient:
                        Console.WriteLine("瞬时实例--前");
                        break;
                    case LifeTimeType.Singleton:
                        if (registerModel.SingletonInstance == null)
                        {
                            break;
                        }
                        else
                        {
                            return registerModel.SingletonInstance;
                        }
                    case LifeTimeType.Scoped:
                        if (!DIPContainerScoped.ContainsKey(key)|| DIPContainerScoped[key]==null)
                        {
                            break;
                        }
                        else
                        {
                            return DIPContainerScoped[key];
                        }
                    case LifeTimeType.PerThread:
                        object threadobject = CustomCallContext<object>.GetData($"{key}{Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($"{key}{Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($"threadobject:{threadobject}");
                        if (threadobject == null)
                        {
                            break;
                        }
                        else
                        {
                            return threadobject;
                        };
                    default:
                        break;
                }
                #region 准备构造函数的参数
                //获取指定标记特性的构造函数
                ConstructorInfo ctor = type.GetConstructors().FirstOrDefault(m => m.IsDefined(typeof(DIPContainerAttribute), true));
                //找到构造函数中参数最多的
                if (ctor == null)
                {
                    ctor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();
                }
                //获取第一个构造函数
                //var ctor = type.GetConstructors()[0];

                List<object> list = new List<object>();
                object[] paraConstant = DIPContainerValueDic.ContainsKey(key) ? DIPContainerValueDic[key] : null;
                int index = 0;
                foreach (var para in ctor.GetParameters())
                {
                    if (para.IsDefined(typeof(DIPParameterConstantAttribute), true))
                    {
                        list.Add(paraConstant[index++]);
                    }
                    else
                    {
                        Type paraType = para.ParameterType;
                        object objectInstance = ResolveObject(paraType);
                        list.Add(objectInstance);
                    }
                    //string paraKey = paraType.FullName;
                    //if (DIPContainerDic.ContainsKey(paraKey))
                    //{
                    //    Type paraTargetType = DIPContainerDic[paraKey];
                    //    list.Add(Activator.CreateInstance(type));
                    //}
                    //else
                    //{
                    //    //DIPContainerDic.Add(paraKey, ()Activator.CreateInstance(type));
                    //}
                }
                #endregion

                object oInstance = Activator.CreateInstance(type, list.ToArray());

                #region 准备构造函数的属性,通过属性标记
                foreach (var property in type.GetProperties().Where(m => m.IsDefined(typeof(DIPPropertyInjectionAttribute), true)))
                {
                    Type propType = property.PropertyType;
                    string parashortname = GetShortName(property);
                    object propInstance = ResolveObject(propType, parashortname);
                    property.SetValue(oInstance, propInstance);
                }
                #endregion

                #region 准备构造函数的方法,通过属性标记
                int methodindex = 0;
                foreach (var method in type.GetMethods().Where(m => m.IsDefined(typeof(DIPMethodInjectionAttribute), true)))
                {
                    List<object> listmethod = new List<object>();
                    ParameterInfo[] parameterInfos = method.GetParameters();
                    foreach (var parameterInfo in parameterInfos)
                    {
                        if (parameterInfo.IsDefined(typeof(DIPParameterConstantAttribute), true))
                        {
                            listmethod.Add(paraConstant[methodindex++]);
                        }
                        else
                        {
                            Type methodType = parameterInfo.ParameterType;
                            string parashortname = GetShortName(method);

                            object methodInstance = ResolveObject(methodType, parashortname);
                            listmethod.Add(methodInstance);
                        }
                    }

                    method.Invoke(oInstance, listmethod.ToArray());
                }
                #endregion
                switch (registerModel.LifeType)
                {
                    case LifeTimeType.Transient:
                        Console.WriteLine("瞬时实例--后");
                        break;
                    case LifeTimeType.Singleton:
                        //registerModel.SingletonInstance = oInstance;
                        registerModel.SingletonInstance = ContainerAOPExtend.AOP(oInstance, abstracttype);
                        break;
                    case LifeTimeType.Scoped:
                        //DIPContainerScoped[key] = oInstance;
                        DIPContainerScoped[key] = ContainerAOPExtend.AOP(oInstance, abstracttype);
                        break;
                    case LifeTimeType.PerThread:
                        CustomCallContext<object>.SetData($"{key}{Thread.CurrentThread.ManagedThreadId}", oInstance);
                        break;
                    default:
                        break;
                }

                //return oInstance;
                return ContainerAOPExtend.AOP(oInstance,abstracttype);
            }
            else
            {
                return null;
            }
        }
    }
}
