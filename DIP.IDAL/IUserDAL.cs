using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DIP.Model;

namespace DIP.IDAL
{
    public interface IUserDAL
    {
        UserModel Find(Expression<Func<UserModel, bool>> expression);

        void Update(UserModel useModel);

    }
}
