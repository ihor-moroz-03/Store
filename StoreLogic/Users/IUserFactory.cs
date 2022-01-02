namespace StoreLogic.Users
{
    public interface IUserFactory
    {
        TUser CreateUser<TUser>(string username, int passwordHash) where TUser : class, IUser;
    }
}
