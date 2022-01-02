using System;

namespace StoreLogic.Users
{
    public interface IUser : IEquatable<IUser>
    {
        string Username { get; }
        int PasswordHash { get; }
    }
}
