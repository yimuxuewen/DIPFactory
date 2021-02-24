using DIP.IDAL;
using DIP.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DIP.DAL
{
    public class UserDAL : IUserDAL
    {
        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel()
            {
                Id = 1,
                Name = "Joly",
                Account = "Administrator",
                Email = "Joly@sina.com",
                Password = "23143",
                Role = "Admin",
                LoginTime = DateTime.Now
            };

        }

        public void Update(UserModel useModel)
        {
            Console.WriteLine("数据更新"); ;
        }
    }
}
