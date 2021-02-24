
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DIP.Framework
{
    public class ObjectFactory
    {
        //public static IUserDAL CreateDAL()
        //{
        //    IUserDAL userDAL = null;
        //    string config = ConfigurationManager.GetNode("IUserDAL");
        //    Assembly assembly = Assembly.Load(config.Split(",")[1]);
        //    Type type = assembly.GetType(config.Split(",")[0]);
        //    userDAL = (IUserDAL)Activator.CreateInstance(type);
        //    return userDAL;
        //}

        //public static IUserBLL CreateBLL(IUserDAL userDAL)
        //{
        //    IUserBLL userBLL = null;
        //    string config = ConfigurationManager.GetNode("IUserBLL");
        //    Assembly assembly = Assembly.Load(config.Split(",")[1]);
        //    Type type = assembly.GetType(config.Split(",")[0]);
        //    userBLL = (IUserBLL)Activator.CreateInstance(type,new object[] { userDAL });
        //    return userBLL;
        //}

    }
}
