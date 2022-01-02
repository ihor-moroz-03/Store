using StoreLogic.Ordering;
using System.Collections.Generic;

namespace StoreLogic.Users
{
    public interface IModerator : IUser
    {
        ICollection<IOrder> Orders { get; }
        ICollection<IDiscount> Discounts { get; }
    }
}
