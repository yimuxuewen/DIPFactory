using DIP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.IBLL
{
    public interface IUserBLL
    {
        UserModel Login(string account);

        void LastLogin(UserModel user);
    }
}
