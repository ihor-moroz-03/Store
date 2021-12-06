using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.Products;
using Store.Ordering;

namespace Store.Users
{
    interface IStoreHandles
    {
        IProductConstructionService ProductConstuctionService { get; }
        IStorage Storage { get; }
        IUserData UserData { get; }
        ICollection<IOrder> Orders { get; }
        ICollection<IDiscount> Discounts { get; }
    }
}
