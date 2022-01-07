using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB
{
    public class UserModel : IModel
    {
        int IModel.Id { get; init; }

        public string Username { get; init; }

        public int PasswordHash { get; init; }

        public string Role { get; init; }

        public override string ToString() => $"Username: {Username}, Role: {Role}";
    }
}
