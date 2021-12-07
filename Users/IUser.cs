namespace Store.Users
{
    public interface IUser
    {
        string Username { get; }
        int PasswordHash { get; }
    }
}
