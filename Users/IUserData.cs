using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Users
{
    public interface IUserData : IEnumerable<IUser>
    {
        event EventHandler<IUser> OnAddition;

        event EventHandler<IUser> OnRemoval;

        bool Add(IUser user);

        bool Remove(IUser user);

        public bool TryGet<T>(string username, int passwordHash, out T user) where T : class, IUser;
    }
}
