using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Users
{
    public interface IUser
    {
        string Username { get; }
        int PasswordHash { get; }
    }
}
