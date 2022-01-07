using StoreDB;

namespace StoreLogic.Users
{
    public interface IUserFactory
    {
        IUser CreateUser(UserModel user);

        TUser CreateUser<TUser>(string username, int passwordHash) where TUser : class, IUser;
    }
}
