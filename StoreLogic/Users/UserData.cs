using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StoreLogic.Users
{
    class UserData : IUserData
    {
        readonly HashSet<IUser> _users = new();

        public event EventHandler<IUser> OnAddition;

        public event EventHandler<IUser> OnRemoval;

        public void Add(IUser user)
        {
            if (!_users.Add(user)) throw new ArgumentException("Such user already exists");

            OnAddition?.Invoke(this, user);
        }

        public IEnumerator<IUser> GetEnumerator() => _users.GetEnumerator();

        public bool Remove(IUser user)
        {
            if (_users.Remove(user))
            {
                OnRemoval?.Invoke(this, user);
                return true;
            }
            return false;
        }

        public bool TryGet<T>(string username, int passwordHash, out T user) where T : class, IUser
        {
            IUser record = _users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
            if (record != default && record is T)
            {
                user = record as T;
                return true;
            }
            user = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
