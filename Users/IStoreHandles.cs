using Store.Ordering;
using Store.Products;
using System.Collections.Generic;

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
