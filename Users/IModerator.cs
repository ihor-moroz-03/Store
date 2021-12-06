using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Ordering;

namespace Store.Users
{
    public interface IModerator : IUser
    {
        ICollection<IOrder> Orders { get; }
        ICollection<IDiscount> Discounts { get; }
    }
}
