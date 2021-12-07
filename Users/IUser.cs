using System;

namespace Store.Users
{
    public interface IUser : IEquatable<IUser>
    {
        string Username { get; }
        int PasswordHash { get; }
    }
}
