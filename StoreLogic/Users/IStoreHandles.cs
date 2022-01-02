using StoreLogic.Ordering;
using StoreLogic.Products;
using System.Collections.Generic;

namespace StoreLogic.Users
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
