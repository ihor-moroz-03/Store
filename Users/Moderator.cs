using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Ordering;

namespace Store.Users
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
