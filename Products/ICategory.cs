using System.Collections.Generic;

namespace Store.Products
{
    public interface ICategory : IEnumerable<IDetailFormat>
    {
        string Name { get; }

        IReadOnlySet<ICategory> Children { get; }
    }
}
