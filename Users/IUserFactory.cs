using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Users
{
    public interface IUserFactory
    {
        TUser CreateUser<TUser>(string username, int passwordHash) where TUser : class, IUser;
    }
}
