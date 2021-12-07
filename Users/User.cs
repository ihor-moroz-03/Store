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

        public override bool Equals(object obj) =>
            obj is IUser && Equals(obj as IUser);

        public bool Equals(IUser other) => Username == other.Username;

        public override int GetHashCode() => Username.GetHashCode();
    }
}
