using System;
using System.Collections.Generic;

namespace Store.Products
{
    public interface IStorage : ICollection<IProduct>
    {
        event EventHandler<IProduct> OnProductAddition;

        event EventHandler<IProduct> OnProductRemoval;

        int UniqueCount { get; }
    }
}
