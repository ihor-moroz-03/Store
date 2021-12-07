using System;
using System.Collections.Generic;

namespace Store.Products
{
    public interface IProduct : IEquatable<IProduct>, IEnumerable<IDetail>
    {
        string this[string detail] { get; }
        string Name { get; }
        double Price { get; }
        string Category { get; }

        bool HasDetail(string detail);

        bool TryGetDetail(string detail, out string value);
    }
}
