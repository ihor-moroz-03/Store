using System;
using System.Collections.Generic;

namespace StoreLogic.Products
{
    public interface IStorage : ICollection<IProduct>
    {
        event EventHandler<IProduct> OnProductAddition;

        event EventHandler<IProduct> OnProductRemoval;

        int UniqueCount { get; }
    }
}
