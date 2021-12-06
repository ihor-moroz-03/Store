using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Users
{
    abstract class User : IUser
    {
        protected User(string username, int passwordHash)
        {
            Username = username; PasswordHash = passwordHash;
        }

        public string Username { get; }
        public int PasswordHash { get; }
    }
}
