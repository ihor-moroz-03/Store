using System;
using StoreDB;

namespace StoreLogic.Users
{
    class UserFactory : IUserFactory
    {
        readonly IStoreHandles _store;

        public UserFactory(IStoreHandles store)
        {
            _store = store;
        }

        public IUser CreateUser(UserModel user) => user.Role switch
        {
            "Admin" => CreateUser<IAdmin>(user.Username, user.PasswordHash),
            "Moderator" => CreateUser<IModerator>(user.Username, user.PasswordHash),
            "Customer" => CreateUser<ICustomer>(user.Username, user.PasswordHash),
            _ => throw new ArgumentException("Invalid user role")
        };

        public TUser CreateUser<TUser>(string username, int passwordHash) where TUser : class, IUser
        {
            string typename = typeof(TUser).FullName;

            if (typename == typeof(ICustomer).FullName)
                return new Customer(username, passwordHash, Status.Free, _store.Storage, _store.Orders) as TUser;
            if (typename == typeof(IModerator).FullName)
                return new Moderator(username, passwordHash, _store.Orders, _store.Discounts) as TUser;
            if (typename == typeof(IAdmin).FullName)
                return new Admin(
                    username,
                    passwordHash,
                    _store.ProductConstuctionService,
                    _store.Storage,
                    _store.UserData,
                    this
                    ) as TUser;

            throw new NotSupportedException("Provided user type is not supported");
        }
    }
}
