using System;
using System.Collections.Generic;

namespace Store.Users
{
    public interface IUserData : IEnumerable<IUser>
    {
        event EventHandler<IUser> OnAddition;

        event EventHandler<IUser> OnRemoval;

        void Add(IUser user);

        bool Remove(IUser user);

        bool TryGet<T>(string username, int passwordHash, out T user) where T : class, IUser;
    }
}
