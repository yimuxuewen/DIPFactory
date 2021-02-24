using DIP.Framework.Container;
using DIP.IBLL;
using DIP.IDAL;
using DIP.Interface;
using DIP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.BLL
{
    public class UserBLL : IUserBLL
    {


        private IUserDAL _iuserDAL = null;
        private string Name;
        private int Value;
        private readonly IServiceA _serviceA = null;

        [DIPPropertyInjection]
        [DIPParameterShortName("mysql")]
        public IUserDAL UserDALMySql { get; set; }

        [DIPContainer]
        public UserBLL([DIPParameterShortName("mysql")]IUserDAL iuserDAL,IServiceA serviceA, [DIPParameterConstant]string name, [DIPParameterConstant]int value)
        {
            _iuserDAL = iuserDAL;
            _serviceA = serviceA;
            Name = name;
            Value = value;
        }

        public void LastLogin(UserModel user)
        {
            user.LoginTime=DateTime.Now;
            _iuserDAL.Update(user);
        }

        public UserModel Login(string account)
        {
           return _iuserDAL.Find(u=>u.Account==account);
        }
    }
}
