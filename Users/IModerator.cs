using Store.Ordering;
using System.Collections.Generic;

namespace Store.Users
{
    public interface IModerator : IUser
    {
        ICollection<IOrder> Orders { get; }
        ICollection<IDiscount> Discounts { get; }
    }
}
