using System.Collections.Generic;

namespace StoreLogic.Products
{
    public interface ICategory : IEnumerable<IDetailFormat>
    {
        string Name { get; }
    }
}
