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
