using StoreLogic.Ordering;
using System.Collections.Generic;

namespace StoreLogic.Users
{
    class Moderator : User, IModerator
    {
        public Moderator(string username, int passwordHash, ICollection<IOrder> orders, ICollection<IDiscount> discounts)
            : base(username, passwordHash)
        {
            Orders = orders; Discounts = discounts;
        }

        public ICollection<IOrder> Orders { get; }

        public ICollection<IDiscount> Discounts { get; }
    }
}
