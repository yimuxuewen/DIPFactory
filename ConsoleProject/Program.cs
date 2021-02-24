using DIP.BLL;
using DIP.DAL;
using DIP.Framework;
using DIP.Framework.Container;
using DIP.Framework.CustomAOP;
using DIP.IBLL;
using DIP.IDAL;
using DIP.Interface;
using DIP.Model;
using DIP.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Hello World!");
            //IDIPContainer dIPContainer = new DIPContainer();
            #region IOC容器
            //dIPContainer.Register<IUserBLL, UserBLL>(paramlist: new object[] { "Lily", 5 });
            //dIPContainer.Register<IUserDAL, UserDAL>();
            //dIPContainer.Register<IUserDAL, UserDALMySql>("mysql");
            //dIPContainer.Register<IServiceA, ServiceA>(paramlist: new object[] { 43 });
            //dIPContainer.Register<IServiceB, ServiceB>();
            //dIPContainer.Register<IServiceC, ServiceC>();
            //dIPContainer.Register<IServiceD, ServiceD>();
            ////dIPContainer.Register<IUserBLL, UserBLL>();

            ////IUserDAL userDALMySql = dIPContainer.Resolve<IUserDAL>("mysql"); 
            //IUserBLL userBLL = dIPContainer.Resolve<IUserBLL>();

            #endregion            //IUserDAL userDAL = ObjectFactory.CreateDAL();

            // dIPContainer.Register<IServiceB, ServiceB>(lifeTimeType: LifeTimeType.PerThread);

            // IServiceB serviceB1 = dIPContainer.Resolve<IServiceB>();
            // IServiceB serviceB2 = dIPContainer.Resolve<IServiceB>();

            // //IDIPContainer container1 = dIPContainer.CreateSubDIPContainer();
            // //IDIPContainer container2 = dIPContainer.CreateSubDIPContainer();

            // //IServiceB serviceB11 = container1.Resolve<IServiceB>();
            // //IServiceB serviceB12 = container1.Resolve<IServiceB>();
            // //IServiceB serviceB21 = container2.Resolve<IServiceB>();
            // //IServiceB serviceB22 = container2.Resolve<IServiceB>();

            // //Console.WriteLine(object.ReferenceEquals(serviceB1, serviceB2));
            // //Console.WriteLine(object.ReferenceEquals(serviceB11, serviceB21));
            // //Console.WriteLine(object.ReferenceEquals(serviceB12, serviceB22));
            // //Console.WriteLine(object.ReferenceEquals(serviceB11, serviceB12));
            // //Console.WriteLine(object.ReferenceEquals(serviceB21, serviceB22));

            // IServiceB serviceB3 = null;
            // IServiceB serviceB4 = null;
            // IServiceB serviceB5 = null;

            // //Task.Run(() =>
            // //{
            // //    Console.WriteLine($" B3 This is Thread {Thread.CurrentThread.ManagedThreadId}");
            // //    serviceB3 = dIPContainer.Resolve<IServiceB>();
            // //});
            // Task.Run(() =>
            // {
            //     Console.WriteLine($" B4 This is Thread {Thread.CurrentThread.ManagedThreadId}");
            //     serviceB4 = dIPContainer.Resolve<IServiceB>();
            // }).ContinueWith(t =>
            // {
            //     //Task.Run(() =>
            //     //{
            //     Console.WriteLine($"B5 This is Thread {Thread.CurrentThread.ManagedThreadId}");
            //     serviceB5 = dIPContainer.Resolve<IServiceB>();
            //     //});
            // }
            //).GetAwaiter().GetResult();
            // Thread.Sleep(1000);
            // CustomCallContext<object>.ShowValue();
            // Console.WriteLine(object.ReferenceEquals(serviceB1, serviceB2));
            // Console.WriteLine(object.ReferenceEquals(serviceB1, serviceB3));
            // Console.WriteLine(object.ReferenceEquals(serviceB1, serviceB4));
            // Console.WriteLine(object.ReferenceEquals(serviceB1, serviceB5));
            // Console.WriteLine(object.ReferenceEquals(serviceB2, serviceB3));
            // Console.WriteLine(object.ReferenceEquals(serviceB2, serviceB4));
            // Console.WriteLine(object.ReferenceEquals(serviceB2, serviceB5));
            // Console.WriteLine(object.ReferenceEquals(serviceB3, serviceB4));
            // Console.WriteLine(object.ReferenceEquals(serviceB3, serviceB5));
            // Console.WriteLine(object.ReferenceEquals(serviceB4, serviceB5));

            //IUserBLL userBLL = ObjectFactory.CreateBLL(userDAL);
            //UserModel userModel= userBLL.Login("Administrator");
            //Console.WriteLine(userModel.Name);

            //CustomAOPTest.Show();
            IDIPContainer dIPContainer = new DIPContainer();
            dIPContainer.Register<IServiceB, ServiceB>(lifeTimeType: LifeTimeType.Singleton);
            IServiceB serviceB = dIPContainer.Resolve<IServiceB>();
            serviceB.SayHello();
            serviceB.SayWhy();
            serviceB = (IServiceB)ContainerAOPExtend.AOP(serviceB, typeof(IServiceB));
            serviceB.SayHello();
            serviceB.SayWhy();
        }
    }
}
