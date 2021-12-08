using System;
using System.Collections.Generic;

namespace Store.Products
{
    public interface IProduct : IEquatable<IProduct>, IEnumerable<IDetail>
    {
        string Name { get; }

        double Price { get; }

        string Category { get; }
    }
}
